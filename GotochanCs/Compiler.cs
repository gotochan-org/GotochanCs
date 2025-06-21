using System.Text;
using System.Runtime.InteropServices;
using ResultZero;

namespace GotochanCs;

public static class Compiler {
    private static string IdentifyActor() => "Actor";
    private static string IdentifyIndex(int Identifier) => $"Index{Identifier}";
    private static string IdentifyLabel(int Identifier) => $"Label{Identifier}";
    private static string IdentifyVariable(int Identifier) => $"Variable{Identifier}";
    private static string IdentifyGotoLabelIndex(int Identifier) => $"GotoLabelIndex{Identifier}";
    private static string IdentifyGotoLabelSwitchIdentifier() => "GotoLabelSwitchIdentifier";
    private static string IdentifyGotoLabelSwitch() => "GotoLabelSwitch";
    private static string IdentifyTemporary(int Identifier) => $"Temporary{Identifier}";
    private static string IdentifyGotoExternalLabelError() => "GotoExternalLabelError";
    private static string IdentifyEndLabel() => "EndLabel";

    public static Result<CompileResult> Compile(ParseResult ParseResult, CompileOptions CompileOptions) {
        string Output = "";

        // Create compiler state
        CompilerState CompilerState = new() {
            ParseResult = ParseResult,
        };

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
                // Get index identifier
                int IndexIdentifier = ParseResult.LineIndexes[Instruction.Location.Line] + 1;
                // Output label
                InstructionsBuilder.Append($"{IdentifyIndex(IndexIdentifier)}:" + "\n");
            }
            // Label
            foreach ((string LabelName, int LabelIndex) in ParseResult.LabelIndexes) {
                if (LabelIndex == Index) {
                    // Get label identifier
                    int LabelIdentifier = CompilerState.Labels[LabelName];
                    // Output label
                    InstructionsBuilder.Append($"{IdentifyLabel(LabelIdentifier)}:" + "\n");
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

        // Compile goto label switch
        StringBuilder GotoLabelSwitchBuilder = new();
        {
            GotoLabelSwitchBuilder.Append($"goto {IdentifyEndLabel()};" + "\n");
            GotoLabelSwitchBuilder.Append($"{IdentifyGotoLabelSwitch()}:" + "\n");
            GotoLabelSwitchBuilder.Append($"switch ({IdentifyGotoLabelSwitchIdentifier()}) {{" + "\n");
            foreach (int LineIndex in ParseResult.LineIndexes.Values.Distinct()) {
                // Get index identifier
                int IndexIdentifier = LineIndex + 1;
                // Add case for label
                GotoLabelSwitchBuilder.Append($"case {IndexIdentifier}:" + "\n");
                GotoLabelSwitchBuilder.Append($"goto {IdentifyIndex(IndexIdentifier)};" + "\n");
            }
            GotoLabelSwitchBuilder.Append("default:" + "\n");
            GotoLabelSwitchBuilder.Append($"throw new {nameof(InvalidProgramException)}();");
            GotoLabelSwitchBuilder.Append("}" + "\n");
        }
        // Append goto label switch to output
        Output += GotoLabelSwitchBuilder.ToString() + "\n";

        // Output end variable
        Output += $"{IdentifyEndLabel()}:" + "\n";
        Output += ";" + "\n";

        // Compile variables
        StringBuilder VariablesBuilder = new();
        foreach ((string VariableName, int VariableIdentifier) in CompilerState.Variables) {
            // Add variable declaration
            VariablesBuilder.Append($"{nameof(Thingie)} {IdentifyVariable(VariableIdentifier)} = {CompileNothing()};" + "\n");
        }
        // Prepend variables to output
        Output = VariablesBuilder.ToString() + "\n" + Output;

        // Compile goto label indexes
        StringBuilder GotoLabelIndexesBuilder = new();
        foreach ((string LabelName, int LabelIdentifier) in CompilerState.Labels) {
            // Add goto label index declaration
            GotoLabelIndexesBuilder.Append($"int {IdentifyGotoLabelIndex(LabelIdentifier)} = -1;" + "\n");
        }
        // Prepend goto label indexes to output
        Output = GotoLabelIndexesBuilder.ToString() + "\n" + Output;

        // Output goto label switch parameters
        string GotoLabelSwitchParametersOutput = "";
        GotoLabelSwitchParametersOutput += $"int {IdentifyGotoLabelSwitchIdentifier()} = -1;" + "\n";
        // Prepend goto label switch parameters to output
        Output = GotoLabelSwitchParametersOutput + "\n" + Output;

        // Compile set variables in actor
        // TODO

        // Create source file
        string SourceFile = CreateSourceFile(Output, CompileOptions.NamespaceName, CompileOptions.ClassName, CompileOptions.MethodName);

        // Finish
        return new CompileResult() {
            Source = ParseResult.Source,
            Output = SourceFile,
        };
    }
    public static Result<CompileResult> Compile(ParseResult ParseResult) {
        return Compile(ParseResult, new CompileOptions());
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
            Output += $"if ({IdentifyTemporary(ConditionTemporaryIdentifier)}.{nameof(Result<Thingie>.Value)}.{nameof(Thingie.Type)} is not {nameof(ThingieType)}.{nameof(ThingieType.Flag)}) {{" + "\n";
            Output += $"return new {nameof(Error)}($\"{Instruction.Condition.Location.Line}: condition must be flag, not '{{{IdentifyTemporary(ConditionTemporaryIdentifier)}.{nameof(Result<Thingie>.Value)}.{nameof(Thingie.Type)}}}'\");" + "\n";
            Output += "}" + "\n";
            // Output check condition
            Output += $"if ({IdentifyTemporary(ConditionTemporaryIdentifier)}.{nameof(Result<Thingie>.Value)}.{nameof(Thingie.CastFlag)}()) {{" + "\n";
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
            Output += $"{IdentifyVariable(VariableIdentifier)} = {IdentifyTemporary(ValueTemporaryIdentifier)}.{nameof(Result<Thingie>.Value)};" + "\n";
        }
        // Label
        else if (Instruction is LabelInstruction) {
            // Pass
            Output += ";" + "\n";
        }
        // Goto line
        else if (Instruction is GotoLineInstruction GotoLineInstruction) {
            // Check if line exists
            if (!CompilerState.ParseResult.LineIndexes.TryGetValue(GotoLineInstruction.LineNumber, out int IndexIdentifier)) {
                // Output goto end
                Output += $"goto {IdentifyEndLabel()};" + "\n";
            }
            else {
                // Output goto
                Output += $"goto {IdentifyIndex(IndexIdentifier + 1)};" + "\n";
            }
        }
        // Goto label
        else if (Instruction is GotoLabelInstruction GotoLabelInstruction) {
            // Get label identifier
            if (!CompilerState.Labels.TryGetValue(GotoLabelInstruction.LabelName, out int LabelIdentifier)) {
                // Output goto external label
                Output += $"if ({IdentifyActor()}.{nameof(Actor.GotoExternalLabel)}({CompileLocationLiteral(GotoLabelInstruction.Location)}, {CompileStringLiteral(GotoLabelInstruction.LabelName)}).{nameof(Result.TryGetError)}(out {nameof(Result.Error)} {IdentifyGotoExternalLabelError()})) {{" + "\n";
                Output += $"return {IdentifyGotoExternalLabelError()};" + "\n";
                Output += "}" + "\n";
            }
            else {
                // Get index identifier
                int IndexIdentifier = CompilerState.ParseResult.LineIndexes[GotoLabelInstruction.Location.Line] + 1;
                // Output set goto label index
                Output += $"{IdentifyGotoLabelIndex(LabelIdentifier)} = {IndexIdentifier + 1};" + "\n";
                // Output goto
                Output += $"goto {IdentifyLabel(LabelIdentifier)};" + "\n";
            }
        }
        // Goto goto label
        else if (Instruction is GotoGotoLabelInstruction GotoGotoLabelInstruction) {
            // Get label identifier
            int LabelIdentifier = CompilerState.Labels[GotoGotoLabelInstruction.LabelName];
            // Output check goto label index exists
            Output += $"if ({IdentifyGotoLabelIndex(LabelIdentifier)} < 0) {{" + "\n";
            Output += $"return new {nameof(Error)}($\"{GotoGotoLabelInstruction.Location.Line}: no entry for goto label: '{GotoGotoLabelInstruction.LabelName}'\");" + "\n";
            Output += "}" + "\n";
            // Output set goto goto parameters
            Output += $"{IdentifyGotoLabelSwitchIdentifier()} = {IdentifyGotoLabelIndex(LabelIdentifier)};" + "\n";
            // Output goto goto
            Output += $"goto {IdentifyGotoLabelSwitch()};" + "\n";
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
        bool OmitErrorCheck = false;

        // Constant
        if (Expression is ConstantExpression ConstantExpression) {
            // Expression cannot error
            OmitErrorCheck = true;
            // Output constant
            Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {CompileThingie(ConstantExpression.Value)};" + "\n";
        }
        // Get variable
        else if (Expression is GetVariableExpression GetVariableExpression) {
            // Expression cannot error
            OmitErrorCheck = true;
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
                Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {nameof(Thingie)}.{nameof(Thingie.Plus)}({CompileLocationLiteral(UnaryExpression.Location)}, {IdentifyTemporary(ExpressionTemporaryIdentifier)}.{nameof(Result<Thingie>.Value)});" + "\n";
            }
            // Minus
            else if (UnaryExpression.Operator is UnaryOperator.Minus) {
                // Output unary minus
                Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {nameof(Thingie)}.{nameof(Thingie.Minus)}({CompileLocationLiteral(UnaryExpression.Location)}, {IdentifyTemporary(ExpressionTemporaryIdentifier)}.{nameof(Result<Thingie>.Value)});" + "\n";
            }
            // Invalid
            else {
                return new Error($"{Expression.Location.Line}: invalid unary operator: '{UnaryExpression.Operator}'");
            }
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
                // Output binary add
                Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {nameof(Thingie)}.{nameof(Thingie.Add)}({CompileLocationLiteral(BinaryExpression.Location)}, {IdentifyTemporary(Expression1TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)}, {IdentifyTemporary(Expression2TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)});" + "\n";
            }
            // Subtract
            else if (BinaryExpression.Operator is BinaryOperator.Subtract) {
                // Output binary subtract
                Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {nameof(Thingie)}.{nameof(Thingie.Subtract)}({CompileLocationLiteral(BinaryExpression.Location)}, {IdentifyTemporary(Expression1TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)}, {IdentifyTemporary(Expression2TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)});" + "\n";
            }
            // Multiply
            else if (BinaryExpression.Operator is BinaryOperator.Multiply) {
                // Output binary multiply
                Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {nameof(Thingie)}.{nameof(Thingie.Multiply)}({CompileLocationLiteral(BinaryExpression.Location)}, {IdentifyTemporary(Expression1TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)}, {IdentifyTemporary(Expression2TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)});" + "\n";
            }
            // Divide
            else if (BinaryExpression.Operator is BinaryOperator.Divide) {
                // Output binary divide
                Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {nameof(Thingie)}.{nameof(Thingie.Divide)}({CompileLocationLiteral(BinaryExpression.Location)}, {IdentifyTemporary(Expression1TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)}, {IdentifyTemporary(Expression2TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)});" + "\n";
            }
            // Modulo
            else if (BinaryExpression.Operator is BinaryOperator.Modulo) {
                // Output binary modulo
                Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {nameof(Thingie)}.{nameof(Thingie.Modulo)}({CompileLocationLiteral(BinaryExpression.Location)}, {IdentifyTemporary(Expression1TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)}, {IdentifyTemporary(Expression2TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)});" + "\n";
            }
            // Exponentiate
            else if (BinaryExpression.Operator is BinaryOperator.Exponentiate) {
                // Output binary exponentiate
                Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {nameof(Thingie)}.{nameof(Thingie.Exponentiate)}({CompileLocationLiteral(BinaryExpression.Location)}, {IdentifyTemporary(Expression1TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)}, {IdentifyTemporary(Expression2TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)});" + "\n";
            }
            // Equals
            else if (BinaryExpression.Operator is BinaryOperator.Equals) {
                // Output binary equals
                Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {nameof(Thingie)}.{nameof(Thingie.Equals)}({CompileLocationLiteral(BinaryExpression.Location)}, {IdentifyTemporary(Expression1TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)}, {IdentifyTemporary(Expression2TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)});" + "\n";
            }
            // Not equals
            else if (BinaryExpression.Operator is BinaryOperator.NotEquals) {
                // Output binary not equals
                Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {nameof(Thingie)}.{nameof(Thingie.NotEquals)}({CompileLocationLiteral(BinaryExpression.Location)}, {IdentifyTemporary(Expression1TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)}, {IdentifyTemporary(Expression2TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)});" + "\n";
            }
            // Greater than
            else if (BinaryExpression.Operator is BinaryOperator.GreaterThan) {
                // Output binary greater than
                Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {nameof(Thingie)}.{nameof(Thingie.GreaterThan)}({CompileLocationLiteral(BinaryExpression.Location)}, {IdentifyTemporary(Expression1TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)}, {IdentifyTemporary(Expression2TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)});" + "\n";
            }
            // Less than
            else if (BinaryExpression.Operator is BinaryOperator.LessThan) {
                // Output binary less than
                Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {nameof(Thingie)}.{nameof(Thingie.LessThan)}({CompileLocationLiteral(BinaryExpression.Location)}, {IdentifyTemporary(Expression1TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)}, {IdentifyTemporary(Expression2TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)});" + "\n";
            }
            // Greater than or equal to
            else if (BinaryExpression.Operator is BinaryOperator.GreaterThanOrEqualTo) {
                // Output binary greater than or equal to
                Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {nameof(Thingie)}.{nameof(Thingie.GreaterThanOrEqualTo)}({CompileLocationLiteral(BinaryExpression.Location)}, {IdentifyTemporary(Expression1TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)}, {IdentifyTemporary(Expression2TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)});" + "\n";
            }
            // Less than or equal to
            else if (BinaryExpression.Operator is BinaryOperator.LessThanOrEqualTo) {
                // Output binary less than or equal to
                Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {nameof(Thingie)}.{nameof(Thingie.LessThanOrEqualTo)}({CompileLocationLiteral(BinaryExpression.Location)}, {IdentifyTemporary(Expression1TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)}, {IdentifyTemporary(Expression2TemporaryIdentifier)}.{nameof(Result<Thingie>.Value)});" + "\n";
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

        // Output check no error
        if (!OmitErrorCheck) {
            Output += $"if ({IdentifyTemporary(TemporaryIdentifier)}.{nameof(Result.IsError)}) {{" + "\n";
            Output += $"return {IdentifyTemporary(TemporaryIdentifier)}.{nameof(Result.Error)};" + "\n";
            Output += "}" + "\n";
        }

        // Finish
        return Output;
    }
    private static string CompileTemporary(ref CompilerState CompilerState, out int TemporaryIdentifier, Thingie? DefaultValue = null) {
        CompilerState.TemporaryCounter++;
        TemporaryIdentifier = CompilerState.TemporaryCounter;
        return $"{nameof(Result<Thingie>)}<{nameof(Thingie)}> {IdentifyTemporary(TemporaryIdentifier)}{(DefaultValue is not null ? $" = {CompileNothing()}" : "")};" + "\n";
    }
    private static string CompileStringLiteral(string String) {
        return $"@\"{String.Replace("\"", "\"\"")}\"";
    }
    private static string CompileLocationLiteral(SourceLocation Location) {
        return $"new {nameof(SourceLocation)}({Location.Line}, {Location.Column})";
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
    private static string CreateSourceFile(string Output, string? NamespaceName, string ClassName, string MethodName) {
        List<string> Components = [];

        // Add usings
        Components.Add("""
            using System;
            using System.Collections.Generic;
            using GotochanCs;
            using ResultZero;
            """);

        // Add namespace
        if (NamespaceName is not null) {
            Components.Add($"""
                namespace {NamespaceName};
                """);
        }

        // Add class and method
        Components.Add($$"""
            public static partial class {{ClassName}} {
                public static {{nameof(Result)}} {{MethodName}}({{nameof(Actor)}} {{IdentifyActor()}}) {
                    {{Output}}

                    return {{nameof(Result)}}.{{nameof(Result.Success)}};
                }
            }
            """);

        // Finish
        return string.Join("\n\n", Components);
    }

    private record struct CompilerState() {
        public required ParseResult ParseResult { get; set; }
        public Dictionary<string, int> Variables { get; set; } = [];
        public Dictionary<string, int> Labels { get; set; } = [];
        public int TemporaryCounter { get; set; } = 0;
    }
}

public readonly record struct CompileResult {
    public required string Source { get; init; }
    public required string Output { get; init; }
}

public readonly record struct CompileOptions() {
    public string? NamespaceName { get; init; } = null;
    public string ClassName { get; init; } = "CompileOutput";
    public string MethodName { get; init; } = "Execute";
}