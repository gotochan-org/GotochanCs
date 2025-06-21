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
            int LabelIdentifier = CompilerState.Labels.Count;
            CompilerState.Labels[LabelName] = LabelIdentifier;
        }

        // Output goto label lines
        Output += $"Dictionary<int, int> {IdentifyGotoLabelLines()} = [];" + "\n";
        Output += $"int {IdentifyGotoGotoLabelIdentifier()} = \"\";" + "\n";

        // Get instructions as span
        ReadOnlySpan<Instruction> InstructionsSpan = CollectionsMarshal.AsSpan(ParseResult.Instructions);

        // Compile instructions
        StringBuilder InstructionsBuilder = new();
        for (int Index = 0; Index < InstructionsSpan.Length; Index++) {
            Instruction Instruction = InstructionsSpan[Index];

            // Line
            foreach ((int LineNumber, int LineIndex) in ParseResult.LineIndexes) {
                if (LineIndex == Index) {
                    // Get line identifier
                    string LineIdentifier = IdentifyLine(LineNumber);
                    // Output label
                    InstructionsBuilder.Append($"{LineIdentifier}: ");
                    break;
                }
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
            VariablesBuilder.Append($"{nameof(Thingie)} {IdentifyVariable(VariableIdentifier)} = {OutputNothing()};" + "\n");
        }
        // Prepend variables to output
        Output = VariablesBuilder.ToString() + "\n" + Output;

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
            // Output temporary variable
            Output += $"{nameof(Thingie)} {IdentifyTemporary(0)} = {OutputNothing()};" + "\n";

            // Compile condition
            if (CompileExpression(Instruction.Condition, ref CompilerState, 0).TryGetError(out Error ConditionError, out string? ConditionOutput)) {
                return ConditionError;
            }
            // Output condition
            Output += ConditionOutput + "\n";
            Output += $"if ({IdentifyTemporary(0)}) {{" + "\n";
        }

        // Set variable
        if (Instruction is SetVariableInstruction SetVariableInstruction) {
            // Get or add variable
            if (!CompilerState.Variables.TryGetValue(SetVariableInstruction.VariableName, out int VariableIdentifier)) {
                VariableIdentifier = CompilerState.Variables.Count;
                CompilerState.Variables[SetVariableInstruction.VariableName] = VariableIdentifier;
            }
            // Compile value
            if (CompileExpression(SetVariableInstruction.Value, ref CompilerState, 0).TryGetError(out Error ValueError, out string? ValueOutput)) {
                return ValueError;
            }
            // Output assign
            Output += $"{VariableIdentifier} = {ValueOutput};" + "\n";
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
            // Output goto
            Output += $"{IdentifyGotoGotoLabelIdentifier()} = {LabelIdentifier};" + "\n" +
                $"goto {IdentifyGotoGotoLabel()};" + "\n";
        }
        // Not implemented
        else {
            return new Error($"{Instruction.Location.Line}: unhandled instruction: '{Instruction.GetType()}'");
        }

        // Condition
        if (Instruction.Condition is not null) {
            // Output end condition
            Output += "}" + "\n";
        }

        // Output end scope
        Output += "}" + "\n";

        // Finish
        return Output;
    }
    private static Result<string> CompileExpression(Expression Expression, ref CompilerState CompilerState, int TemporaryIdentifier) {
        string Output = "";

        // Constant
        if (Expression is ConstantExpression ConstantExpression) {
            // Output constant
            return ConstantExpression.Value.Type switch {
                ThingieType.Nothing => OutputNothing(),
                ThingieType.Flag => OutputFlag(ConstantExpression.Value.CastFlag()),
                ThingieType.Number => OutputNumber(ConstantExpression.Value.CastNumber()),
                ThingieType.String => OutputString(ConstantExpression.Value.CastString()),
                _ => throw new NotImplementedException($"{ConstantExpression.Location.Line}: unhandled {nameof(ThingieType)}: '{ConstantExpression.Value.Type}'")
            };
        }
        // Get variable
        else if (Expression is GetVariableExpression GetVariableExpression) {
            // Get or add variable
            if (!CompilerState.Variables.TryGetValue(GetVariableExpression.VariableName, out int VariableIdentifier)) {
                VariableIdentifier = CompilerState.Variables.Count;
                CompilerState.Variables[GetVariableExpression.VariableName] = VariableIdentifier;
            }
            // Output variable
            return IdentifyVariable(VariableIdentifier);
        }
        // Unary
        else if (Expression is UnaryExpression UnaryExpression) {
            // Create temporary for expression
            int ExpressionTemporaryIdentifier = CompilerState.TemporaryCounter++;
            Output += $"{nameof(Thingie)} {IdentifyTemporary(ExpressionTemporaryIdentifier)} = {OutputNothing()};" + "\n";
            // Compile expression
            if (CompileExpression(UnaryExpression.Expression, ref CompilerState, ExpressionTemporaryIdentifier).TryGetError(out Error ExpressionError, out string? ExpressionOutput)) {
                return ExpressionError;
            }
            // Output expression
            Output += ExpressionOutput + "\n";

            // Plus
            if (UnaryExpression.Operator is UnaryOperator.Plus) {
                // Output unary plus
                Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {nameof(Thingie)}.{nameof(Thingie.Plus)}({OutputLocationLiteral(UnaryExpression.Location)}, {IdentifyTemporary(0)})";
            }
            // Minus
            else if (UnaryExpression.Operator is UnaryOperator.Minus) {
                // Output unary minus
                Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {nameof(Thingie)}.{nameof(Thingie.Minus)}({OutputLocationLiteral(UnaryExpression.Location)}, {IdentifyTemporary(0)})" + "\n";
            }
            // Invalid
            else {
                return new Error($"{Expression.Location.Line}: invalid unary operator: '{UnaryExpression.Operator}'");
            }

            // Output error check
            Output += $"if ({IdentifyTemporary(TemporaryIdentifier)}.{nameof(Result.IsError)} return {IdentifyTemporary(TemporaryIdentifier)};" + "\n";
        }
        // Binary
        else if (Expression is BinaryExpression BinaryExpression) {
            // Compile expressions
            if (CompileExpression(BinaryExpression.Expression1, ref CompilerState, 0).TryGetError(out Error Expression1Error, out string? Expression1Output)) {
                return Expression1Error;
            }
            if (CompileExpression(BinaryExpression.Expression2, ref CompilerState, 1).TryGetError(out Error Expression2Error, out string? Expression2Output)) {
                return Expression2Error;
            }
            // Output expressions
            Output += Expression1Output + "\n";
            Output += Expression2Output + "\n";

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
    private static string OutputStringLiteral(string String) {
        return $"@\"{String.Replace("\"", "\"\"")}\"";
    }
    private static string OutputLocationLiteral(SourceLocation Location) {
        return $"new {nameof(SourceLocation)}({OutputStringLiteral(Location.Source)}, {Location.Index}, {Location.Line})";
    }
    private static string OutputNothing() {
        return $"{nameof(Thingie)}.{nameof(Thingie.Nothing)}()";
    }
    private static string OutputFlag(bool Flag) {
        return $"{nameof(Thingie)}.{nameof(Thingie.Flag)}({(Flag ? "true" : "false")})";
    }
    private static string OutputNumber(double Number) {
        return $"{nameof(Thingie)}.{nameof(Thingie.Number)}({Number})";
    }
    private static string OutputString(string String) {
        return $"{nameof(Thingie)}.{nameof(Thingie.String)}({OutputStringLiteral(String)})";
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