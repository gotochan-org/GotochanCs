using ResultZero;
using System.Runtime.InteropServices;
using System.Text;

namespace GotochanCs;

public static class Compiler {
    private static string IdentifyLine(int Identifier) => $"Line{Identifier}";
    private static string IdentifyLabel(int Identifier) => $"Label{Identifier}";
    private static string IdentifyVariable(int Identifier) => $"Variable{Identifier}";
    private static string IdentifyGotoLabelLines() => "GotoLabelLines";
    private static string IdentifyGotoGotoLabelIdentifier() => "GotoGotoLabelIdentifier";
    private static string IdentifyGotoGotoLabel() => "GotoGotoLabel";
    private static string IdentifyTemporary(int Identifier) => $"Temporary{Identifier}";

    public static Result<CompileResult> Compile(ParseResult ParseResult) {
        string Output = "";

        // Create compiler state
        CompilerState CompilerState = new();

        // Add labels
        foreach ((string LabelName, int LabelIndex) in ParseResult.LabelIndexes) {
            int LabelIdentifier = CompilerState.Labels.Count + 1;
            CompilerState.Labels[LabelName] = LabelIdentifier;
        }

        // Get instructions as span
        ReadOnlySpan<Instruction> InstructionsSpan = CollectionsMarshal.AsSpan(ParseResult.Instructions);

        // Compile instructions
        StringBuilder InstructionsBuilder = new();
        for (int Index = 0; Index < InstructionsSpan.Length; Index++) {
            Instruction Instruction = InstructionsSpan[Index];

            // Line
            {
                // Get line identifier
                string LineIdentifier = IdentifyLine(Instruction.Location.Line);
                // Output label
                InstructionsBuilder.Append($"{LineIdentifier}: ");
            }
            // Label
            foreach ((string LabelName, int LabelIndex) in ParseResult.LabelIndexes) {
                if (LabelIndex == Index) {
                    // Get label identifier
                    string LabelIdentifier = IdentifyLabel(CompilerState.Labels[LabelName]);
                    // Output label
                    InstructionsBuilder.Append($"{LabelIdentifier}: ");
                    break;
                }
            }

            // Compile instruction
            if (CompileInstruction(Instruction, ref CompilerState).TryGetError(out Error InstructionError, out string? InstructionOutput)) {
                return InstructionError;
            }
            // Output instruction
            InstructionsBuilder.Append(InstructionOutput + "\n");
        }
        // Append instructions to output
        Output += InstructionsBuilder.ToString() + "\n";

        // Compile goto goto label switch
        StringBuilder GotoGotoLabelSwitchBuilder = new();
        GotoGotoLabelSwitchBuilder.Append($"{IdentifyGotoGotoLabel()}: ");
        GotoGotoLabelSwitchBuilder.Append($"if (!{IdentifyGotoLabelLines()}.TryGetValue({IdentifyGotoGotoLabelIdentifier()}, out int LabelIdentifier)) {{" + "\n");
        GotoGotoLabelSwitchBuilder.Append($"return new {nameof(Error)}(\"no entry for goto label\");" + "\n");
        GotoGotoLabelSwitchBuilder.Append("}" + "\n");
        GotoGotoLabelSwitchBuilder.Append("switch (LabelIdentifier) {" + "\n");
        foreach ((string LabelName, int LabelIdentifier) in CompilerState.Labels) {
            GotoGotoLabelSwitchBuilder.Append($"case \"{IdentifyLabel(LabelIdentifier)}\":" + "\n");
            GotoGotoLabelSwitchBuilder.Append($"goto {LabelIdentifier};" + "\n");
        }
        GotoGotoLabelSwitchBuilder.Append("}" + "\n");

        // Compile variables
        StringBuilder VariablesBuilder = new();
        foreach ((string VariableName, int VariableIdentifier) in CompilerState.Variables) {
            // Add variable declaration
            VariablesBuilder.Append($"{nameof(Thingie)} {IdentifyVariable(VariableIdentifier)} = {CompileNothing()};" + "\n");
        }
        // Prepend variables to output
        Output = VariablesBuilder.ToString() + "\n" + Output;

        // Output goto label lines
        string GotoLabelLinesOutput = "";
        GotoLabelLinesOutput += $"Dictionary<int, int> {IdentifyGotoLabelLines()} = [];" + "\n";
        GotoLabelLinesOutput += $"int {IdentifyGotoGotoLabelIdentifier()} = -1;" + "\n";
        // Prepend goto label lines to output
        Output = GotoLabelLinesOutput + "\n" + Output;

        // Compile set variables in actor
        // TODO

        // Finish
        return new CompileResult() {
            Source = ParseResult.Source,
            Output = Output,
        };
    }
    private static Result<string> CompileInstruction(Instruction Instruction, ref CompilerState CompilerState) {
        string Output = "";

        // Output scope
        Output += "{" + "\n";

        // Condition
        if (Instruction.Condition is not null) {
            // Create temporary for condition
            Output += CompileTemporary(ref CompilerState, out int ConditionTemporaryIdentifier);
            // Compile condition
            if (CompileExpression(Instruction.Condition, ref CompilerState, ConditionTemporaryIdentifier).TryGetError(out Error ConditionError, out string? ConditionOutput)) {
                return ConditionError;
            }
            // Output condition
            Output += ConditionOutput;
            // Output check condition is flag
            Output += $"if ({IdentifyTemporary(ConditionTemporaryIdentifier)}.{nameof(Thingie.Type)} is not {nameof(ThingieType)}.{nameof(ThingieType.Flag)}) {{" + "\n";
            Output += $"return new {nameof(Error)}($\"{Instruction.Condition.Location.Line}: condition must be flag, not '{{{IdentifyTemporary(ConditionTemporaryIdentifier)}.{nameof(Thingie.Type)}}}'\");" + "\n";
            Output += "}" + "\n";
            // Output check condition
            Output += $"if ({IdentifyTemporary(ConditionTemporaryIdentifier)}) {{" + "\n";
        }

        // Set variable
        if (Instruction is SetVariableInstruction SetVariableInstruction) {
            // Get or add variable
            if (!CompilerState.Variables.TryGetValue(SetVariableInstruction.VariableName, out int VariableIdentifier)) {
                VariableIdentifier = CompilerState.Variables.Count + 1;
                CompilerState.Variables[SetVariableInstruction.VariableName] = VariableIdentifier;
            }
            // Create temporary for value
            Output += CompileTemporary(ref CompilerState, out int ValueTemporaryIdentifier);
            // Compile value
            if (CompileExpression(SetVariableInstruction.Value, ref CompilerState, ValueTemporaryIdentifier).TryGetError(out Error ValueError, out string? ValueOutput)) {
                return ValueError;
            }
            // Output value
            Output += ValueOutput;
            // Output assign
            Output += $"{IdentifyVariable(VariableIdentifier)} = {IdentifyTemporary(ValueTemporaryIdentifier)};" + "\n";
        }
        // Label
        else if (Instruction is LabelInstruction) {
            // Pass
            Output += ";" + "\n";
        }
        // Goto line
        else if (Instruction is GotoLineInstruction GotoLineInstruction) {
            // Get line identifier
            string LineIdentifier = IdentifyLine(GotoLineInstruction.LineNumber);
            // Output goto
            Output += $"goto {LineIdentifier};" + "\n";
        }
        // Goto label
        else if (Instruction is GotoLabelInstruction GotoLabelInstruction) {
            // Get label identifier
            string LabelIdentifier = IdentifyLabel(CompilerState.Labels[GotoLabelInstruction.LabelName]);
            // Output goto
            Output += $"goto {LabelIdentifier};" + "\n";
        }
        // Goto goto label
        else if (Instruction is GotoGotoLabelInstruction GotoGotoLabelInstruction) {
            // Get label identifier
            string LabelIdentifier = IdentifyLabel(CompilerState.Labels[GotoGotoLabelInstruction.LabelName]);
            // Output goto goto parameter
            Output += $"{IdentifyGotoGotoLabelIdentifier()} = {LabelIdentifier};" + "\n";
            // Output goto goto
            Output += $"goto {IdentifyGotoGotoLabel()};" + "\n";
        }
        // Not implemented
        else {
            return new Error($"{Instruction.Location.Line}: unhandled instruction: '{Instruction.GetType()}'");
        }

        // Condition
        if (Instruction.Condition is not null) {
            // Output end check condition
            Output += "}" + "\n";
        }

        // Output end scope
        Output += "}" + "\n";
        // Reset temporary counter
        CompilerState.TemporaryCounter = 0;

        // Finish
        return Output;
    }
    private static Result<string> CompileExpression(Expression Expression, ref CompilerState CompilerState, int TemporaryIdentifier) {
        string Output = "";

        // Constant
        if (Expression is ConstantExpression ConstantExpression) {
            // Output constant
            Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {CompileThingie(ConstantExpression.Value)};" + "\n";
        }
        // Get variable
        else if (Expression is GetVariableExpression GetVariableExpression) {
            // Get or add variable
            if (!CompilerState.Variables.TryGetValue(GetVariableExpression.VariableName, out int VariableIdentifier)) {
                VariableIdentifier = CompilerState.Variables.Count;
                CompilerState.Variables[GetVariableExpression.VariableName] = VariableIdentifier;
            }
            // Output variable
            Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {IdentifyVariable(VariableIdentifier)};" + "\n";
        }
        // Unary
        else if (Expression is UnaryExpression UnaryExpression) {
            // Create temporary for expression
            Output += CompileTemporary(ref CompilerState, out int ExpressionTemporaryIdentifier);
            // Compile expression
            if (CompileExpression(UnaryExpression.Expression, ref CompilerState, ExpressionTemporaryIdentifier).TryGetError(out Error ExpressionError, out string? ExpressionOutput)) {
                return ExpressionError;
            }
            // Output expression
            Output += ExpressionOutput;

            // Plus
            if (UnaryExpression.Operator is UnaryOperator.Plus) {
                // Output unary plus
                Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {nameof(Thingie)}.{nameof(Thingie.Plus)}({CompileLocationLiteral(UnaryExpression.Location)}, {IdentifyTemporary(0)});" + "\n";
            }
            // Minus
            else if (UnaryExpression.Operator is UnaryOperator.Minus) {
                // Output unary minus
                Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {nameof(Thingie)}.{nameof(Thingie.Minus)}({CompileLocationLiteral(UnaryExpression.Location)}, {IdentifyTemporary(0)});" + "\n";
            }
            // Invalid
            else {
                return new Error($"{Expression.Location.Line}: invalid unary operator: '{UnaryExpression.Operator}'");
            }

            // Output check no error
            Output += $"if ({IdentifyTemporary(TemporaryIdentifier)}.{nameof(Result.IsError)}) {{" + "\n";
            Output += $"return {IdentifyTemporary(TemporaryIdentifier)};" + "\n";
            Output += "}" + "\n";
        }
        // Binary
        else if (Expression is BinaryExpression BinaryExpression) {
            // Create temporaries for expressions
            Output += CompileTemporary(ref CompilerState, out int Expression1TemporaryIdentifier);
            Output += CompileTemporary(ref CompilerState, out int Expression2TemporaryIdentifier);
            // Compile expressions
            if (CompileExpression(BinaryExpression.Expression1, ref CompilerState, Expression1TemporaryIdentifier).TryGetError(out Error Expression1Error, out string? Expression1Output)) {
                return Expression1Error;
            }
            if (CompileExpression(BinaryExpression.Expression2, ref CompilerState, Expression2TemporaryIdentifier).TryGetError(out Error Expression2Error, out string? Expression2Output)) {
                return Expression2Error;
            }
            // Output expressions
            Output += Expression1Output;
            Output += Expression2Output;

            // Add
            if (BinaryExpression.Operator is BinaryOperator.Add) {

            }
            // Subtract
            else if (BinaryExpression.Operator is BinaryOperator.Subtract) {

            }
            // Multiply
            else if (BinaryExpression.Operator is BinaryOperator.Multiply) {

            }
            // Divide
            else if (BinaryExpression.Operator is BinaryOperator.Divide) {

            }
            // Modulo
            else if (BinaryExpression.Operator is BinaryOperator.Modulo) {

            }
            // Exponentiate
            else if (BinaryExpression.Operator is BinaryOperator.Exponentiate) {

            }
            // Equals
            else if (BinaryExpression.Operator is BinaryOperator.Equals) {

            }
            // Not equals
            else if (BinaryExpression.Operator is BinaryOperator.NotEquals) {

            }
            // Greater than
            else if (BinaryExpression.Operator is BinaryOperator.GreaterThan) {

            }
            // Less than
            else if (BinaryExpression.Operator is BinaryOperator.LessThan) {

            }
            // Greater than or equal to
            else if (BinaryExpression.Operator is BinaryOperator.GreaterThanOrEqualTo) {

            }
            // Less than or equal to
            else if (BinaryExpression.Operator is BinaryOperator.LessThanOrEqualTo) {

            }
            // Invalid
            else {
                return new Error($"{Expression.Location.Line}: invalid binary operator: '{BinaryExpression.Operator}'");
            }
        }
        // Invalid
        else {
            return new Error($"{Expression.Location.Line}: invalid expression: '{Expression}'");
        }

        // Finish
        return Output;
    }
    private static string CompileTemporary(ref CompilerState CompilerState, out int TemporaryIdentifier, Thingie? DefaultValue = null) {
        CompilerState.TemporaryCounter++;
        TemporaryIdentifier = CompilerState.TemporaryCounter;
        return $"{nameof(Thingie)} {IdentifyTemporary(TemporaryIdentifier)}{(DefaultValue is not null ? $" = {CompileNothing()}" : "")};" + "\n";
    }
    private static string CompileStringLiteral(string String) {
        return $"@\"{String.Replace("\"", "\"\"")}\"";
    }
    private static string CompileLocationLiteral(SourceLocation Location) {
        return $"new {nameof(SourceLocation)}({CompileStringLiteral(Location.Source)}, {Location.Index}, {Location.Line})";
    }
    private static string CompileThingie(Thingie Thingie) {
        return Thingie.Type switch {
            ThingieType.Nothing => CompileNothing(),
            ThingieType.Flag => CompileFlag(Thingie.CastFlag()),
            ThingieType.Number => CompileNumber(Thingie.CastNumber()),
            ThingieType.String => CompileString(Thingie.CastString()),
            _ => throw new NotImplementedException($"unhandled {nameof(ThingieType)}: '{Thingie.Type}'")
        };
    }
    private static string CompileNothing() {
        return $"{nameof(Thingie)}.{nameof(Thingie.Nothing)}()";
    }
    private static string CompileFlag(bool Flag) {
        return $"{nameof(Thingie)}.{nameof(Thingie.Flag)}({(Flag ? "true" : "false")})";
    }
    private static string CompileNumber(double Number) {
        return $"{nameof(Thingie)}.{nameof(Thingie.Number)}({Number})";
    }
    private static string CompileString(string String) {
        return $"{nameof(Thingie)}.{nameof(Thingie.String)}({CompileStringLiteral(String)})";
    }

    private struct CompilerState() {
        public Dictionary<string, int> Variables { get; set; } = [];
        public Dictionary<string, int> Labels { get; set; } = [];
        public int TemporaryCounter { get; set; } = 0;
    }
}

public class CompileResult {
    public required string Source { get; init; }
    public required string Output { get; init; }
}