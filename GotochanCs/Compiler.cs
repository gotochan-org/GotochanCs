using System.Runtime.InteropServices;
using LinkDotNet.StringBuilder;
using ResultZero;

namespace GotochanCs;

/// <summary>
/// Converts Gotochan instructions to C# code.
/// </summary>
public static class Compiler {
    private static string IdentifyActor() => "Actor";
    private static string IdentifyIndex(int Identifier) => $"Index{Identifier}";
    private static string IdentifyLabel(int Identifier) => $"Label{Identifier}";
    private static string IdentifyGotoLabelSwitchIdentifier() => "GotoLabelSwitchIdentifier";
    private static string IdentifyGotoLabelSwitch() => "GotoLabelSwitch";
    private static string IdentifyTemporary(int Identifier) => $"Temporary{Identifier}";
    private static string IdentifyEndLabel() => "EndLabel";

    /// <summary>
    /// Converts the given code to C# code.
    /// </summary>
    public static Result<CompileResult> Compile(string Source) {
        if (Parser.Parse(Source).TryGetError(out Error ParseError, out ParseResult ParseResult)) {
            return ParseError;
        }
        return Compile(ParseResult);
    }
    /// <summary>
    /// Converts the given instructions to C# code.
    /// </summary>
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

        // Enter class and method
        CompilerState.Depth += 2;

        // Output lock actor
        Output += Indent(CompilerState.Depth);
        Output += $"lock ({IdentifyActor()}.{nameof(Actor.Lock)}) {{" + "\n";
        CompilerState.Depth++;

        // Compile instructions
        using ValueStringBuilder InstructionsBuilder = new(stackalloc char[64]);
        for (int Index = 0; Index < InstructionsSpan.Length; Index++) {
            Instruction Instruction = InstructionsSpan[Index];

            // Index
            {
                // Get index identifier
                int IndexIdentifier = Index + 1;
                // Output label
                InstructionsBuilder.Append(Indent(CompilerState.Depth - 1));
                InstructionsBuilder.Append($"{IdentifyIndex(IndexIdentifier)}:" + "\n");
            }
            // Label
            foreach ((string LabelName, int LabelIndex) in ParseResult.LabelIndexes) {
                if (LabelIndex == Index) {
                    // Get label identifier
                    int LabelIdentifier = CompilerState.Labels[LabelName];
                    // Output label
                    InstructionsBuilder.Append(Indent(CompilerState.Depth - 1));
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
        string GotoLabelSwitch = CompileGotoLabelSwitch(ref CompilerState);
        // Append goto label switch to output
        Output += GotoLabelSwitch + "\n";

        // Output end label
        Output += Indent(CompilerState.Depth - 1);
        Output += $"{IdentifyEndLabel()}:" + "\n";
        Output += Indent(CompilerState.Depth);
        Output += ";" + "\n";

        // Output end lock actor
        CompilerState.Depth--;
        Output += Indent(CompilerState.Depth);
        Output += "}" + "\n";

        // Output goto label switch parameters
        string GotoLabelSwitchParametersOutput = "";
        GotoLabelSwitchParametersOutput += $"int {IdentifyGotoLabelSwitchIdentifier()};" + "\n";
        // Prepend goto label switch parameters to output
        Output = GotoLabelSwitchParametersOutput + "\n" + Output;

        // Create source file
        string SourceFile = CompileSourceFile(Output, CompileOptions.NamespaceName, CompileOptions.ClassName, CompileOptions.MethodName);

        // Finish
        return new CompileResult() {
            Source = ParseResult.Source,
            Output = SourceFile,
        };
    }
    /// <summary>
    /// Converts the given instructions to C# code.
    /// </summary>
    public static Result<CompileResult> Compile(ParseResult ParseResult) {
        return Compile(ParseResult, new CompileOptions());
    }

    private static Result<string> CompileInstruction(Instruction Instruction, ref CompilerState CompilerState) {
        string Output = "";

        // Output scope
        Output += Indent(CompilerState.Depth);
        Output += "{" + "\n";
        CompilerState.Depth++;

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
            Output += Indent(CompilerState.Depth);
            Output += $"if ({IdentifyTemporary(ConditionTemporaryIdentifier)}.{nameof(Result<Thingie>.Value)}.{nameof(Thingie.Type)} is not {nameof(ThingieType)}.{nameof(ThingieType.Flag)}) {{" + "\n";
            Output += Indent(CompilerState.Depth + 1);
            Output += $"return new {nameof(Error)}($\"{Instruction.Condition.Location.Line}: condition must be flag, not '{{{IdentifyTemporary(ConditionTemporaryIdentifier)}.{nameof(Result<Thingie>.Value)}.{nameof(Thingie.Type)}}}'\");" + "\n";
            Output += Indent(CompilerState.Depth);
            Output += "}" + "\n";

            // Output check condition
            Output += Indent(CompilerState.Depth);
            Output += $"if ({IdentifyTemporary(ConditionTemporaryIdentifier)}.{nameof(Result<Thingie>.Value)}.{nameof(Thingie.CastFlag)}()) {{" + "\n";
            CompilerState.Depth++;
        }

        // Set variable
        if (Instruction is SetVariableInstruction SetVariableInstruction) {
            // Create temporary for value
            Output += CompileTemporary(ref CompilerState, out int ValueTemporaryIdentifier);
            // Compile value
            if (CompileExpression(SetVariableInstruction.Value, ref CompilerState, ValueTemporaryIdentifier).TryGetError(out Error ValueError, out string? ValueOutput)) {
                return ValueError;
            }
            // Output value
            Output += ValueOutput;
            // Output set variable
            Output += Indent(CompilerState.Depth);
            Output += $"{IdentifyActor()}.{nameof(Actor.SetVariable)}({CompileStringLiteral(SetVariableInstruction.VariableName)}, {IdentifyTemporary(ValueTemporaryIdentifier)}.{nameof(Result<Thingie>.Value)});" + "\n";
        }
        // Label
        else if (Instruction is LabelInstruction) {
            // Output empty statement
            Output += Indent(CompilerState.Depth);
            Output += ";" + "\n";
        }
        // Goto line
        else if (Instruction is GotoLineInstruction GotoLineInstruction) {
            // Check if line exists
            if (!CompilerState.ParseResult.LineIndexes.TryGetValue(GotoLineInstruction.LineNumber, out int LineIndex)) {
                // Output goto end
                Output += Indent(CompilerState.Depth);
                Output += $"goto {IdentifyEndLabel()};" + "\n";
            }
            else {
                // Output goto
                Output += Indent(CompilerState.Depth);
                Output += $"goto {IdentifyIndex(LineIndex + 1)};" + "\n";
            }
        }
        // Goto label
        else if (Instruction is GotoLabelInstruction GotoLabelInstruction) {
            // Get label identifier
            if (!CompilerState.Labels.TryGetValue(GotoLabelInstruction.LabelName, out int LabelIdentifier)) {
                // Create temporary for goto external label
                Output += CompileTemporary(ref CompilerState, out int GotoExternalLabelTemporaryIdentifier, Type: $"{nameof(Result)}");
                // Output goto external label
                Output += Indent(CompilerState.Depth);
                Output += $"{IdentifyTemporary(GotoExternalLabelTemporaryIdentifier)} = {IdentifyActor()}.{nameof(Actor.GotoExternalLabel)}({CompileLocationLiteral(GotoLabelInstruction.Location)}, {CompileStringLiteral(GotoLabelInstruction.LabelName)});" + "\n";
                // Output check no error
                Output += Indent(CompilerState.Depth);
                Output += $"if ({IdentifyTemporary(GotoExternalLabelTemporaryIdentifier)}.{nameof(Result.IsError)}) {{" + "\n";
                Output += Indent(CompilerState.Depth + 1);
                Output += $"return {IdentifyTemporary(GotoExternalLabelTemporaryIdentifier)}.{nameof(Result.Error)};" + "\n";
                Output += Indent(CompilerState.Depth);
                Output += "}" + "\n";
            }
            else {
                // Get line index
                int LineIndex = CompilerState.ParseResult.LineIndexes[GotoLabelInstruction.Location.Line] + 1;
                // Output set goto label index
                Output += Indent(CompilerState.Depth);
                Output += $"{IdentifyActor()}.{nameof(Actor.SetGotoLabelIndex)}({CompileStringLiteral(GotoLabelInstruction.LabelName)}, {LineIndex - 1});" + "\n";
                // Output goto
                Output += Indent(CompilerState.Depth);
                Output += $"goto {IdentifyLabel(LabelIdentifier)};" + "\n";
            }
        }
        // Goto goto label
        else if (Instruction is GotoGotoLabelInstruction GotoGotoLabelInstruction) {
            // Get label identifier
            int LabelIdentifier = CompilerState.Labels[GotoGotoLabelInstruction.LabelName];
            // Create temporary for get goto label index
            Output += CompileTemporary(ref CompilerState, out int GetGotoLabelIndexTemporaryIdentifier, Type: "int");
            // Output check goto label index exists
            Output += Indent(CompilerState.Depth);
            Output += $"{IdentifyTemporary(GetGotoLabelIndexTemporaryIdentifier)} = {IdentifyActor()}.{nameof(Actor.GetGotoLabelIndex)}({CompileStringLiteral(GotoGotoLabelInstruction.LabelName)});" + "\n";
            Output += Indent(CompilerState.Depth);
            Output += $"if ({IdentifyTemporary(GetGotoLabelIndexTemporaryIdentifier)} < 0) {{" + "\n";
            Output += Indent(CompilerState.Depth + 1);
            Output += $"return new {nameof(Error)}(@\"{GotoGotoLabelInstruction.Location.Line}: no entry for goto label: '{GotoGotoLabelInstruction.LabelName}'\");" + "\n";
            Output += Indent(CompilerState.Depth);
            Output += "}" + "\n";
            // Output set goto goto parameters
            Output += Indent(CompilerState.Depth);
            Output += $"{IdentifyGotoLabelSwitchIdentifier()} = {IdentifyTemporary(GetGotoLabelIndexTemporaryIdentifier)} + 1 + 1;" + "\n";
            // Output goto goto
            Output += Indent(CompilerState.Depth);
            Output += $"goto {IdentifyGotoLabelSwitch()};" + "\n";
        }
        // Not implemented
        else {
            return new Error($"{Instruction.Location.Line}: unhandled instruction: '{Instruction.GetType()}'");
        }

        // Condition
        if (Instruction.Condition is not null) {
            // Output end check condition
            CompilerState.Depth--;
            Output += Indent(CompilerState.Depth);
            Output += "}" + "\n";
        }

        // Output end scope
        CompilerState.Depth--;
        Output += Indent(CompilerState.Depth);
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
            Output += Indent(CompilerState.Depth);
            Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {CompileThingie(ConstantExpression.Value)};" + "\n";
        }
        // Get variable
        else if (Expression is GetVariableExpression GetVariableExpression) {
            // Expression cannot error
            OmitErrorCheck = true;
            // Get or add variable
            if (!CompilerState.Variables.TryGetValue(GetVariableExpression.VariableName, out int VariableIdentifier)) {
                VariableIdentifier = CompilerState.Variables.Count + 1;
                CompilerState.Variables[GetVariableExpression.VariableName] = VariableIdentifier;
            }
            // Output variable
            Output += Indent(CompilerState.Depth);
            Output += $"{IdentifyTemporary(TemporaryIdentifier)} = {IdentifyActor()}.{nameof(Actor.GetVariable)}({CompileStringLiteral(GetVariableExpression.VariableName)});" + "\n";
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

            // Indent
            Output += Indent(CompilerState.Depth);

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

            // Indent
            Output += Indent(CompilerState.Depth);

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
            Output += Indent(CompilerState.Depth);
            Output += $"if ({IdentifyTemporary(TemporaryIdentifier)}.{nameof(Result.IsError)}) {{" + "\n";
            Output += Indent(CompilerState.Depth + 1);
            Output += $"return {IdentifyTemporary(TemporaryIdentifier)}.{nameof(Result.Error)};" + "\n";
            Output += Indent(CompilerState.Depth);
            Output += "}" + "\n";
        }

        // Finish
        return Output;
    }
    private static string CompileTemporary(ref CompilerState CompilerState, out int TemporaryIdentifier, string Type = $"{nameof(Result<Thingie>)}<{nameof(Thingie)}>", Thingie? DefaultValue = null) {
        // Increment temporary variable identifier
        CompilerState.TemporaryCounter++;
        TemporaryIdentifier = CompilerState.TemporaryCounter;

        // Output declare temporary variable
        return Indent(CompilerState.Depth)
            + $"{Type} {IdentifyTemporary(TemporaryIdentifier)}{(DefaultValue is not null ? $" = {CompileNothing()}" : "")};" + "\n";
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
    private static string CompileGotoLabelSwitch(ref CompilerState CompilerState) {
        string Output = "";

        // Output goto end label
        Output += Indent(CompilerState.Depth);
        Output += $"goto {IdentifyEndLabel()};" + "\n";

        // Output goto label switch label
        Output += Indent(CompilerState.Depth - 1);
        Output += $"{IdentifyGotoLabelSwitch()}:" + "\n";

        // Output goto label switch
        Output += Indent(CompilerState.Depth);
        Output += $"switch ({IdentifyGotoLabelSwitchIdentifier()}) {{" + "\n";
        CompilerState.Depth++;

        // Output goto label cases
        foreach (int IndexIdentifier in CompilerState.ParseResult.LineIndexes.Values.Distinct()) {
            // Add case for label
            Output += Indent(CompilerState.Depth);
            Output += $"case {IndexIdentifier + 1}:" + "\n";
            Output += Indent(CompilerState.Depth + 1);
            Output += $"goto {IdentifyIndex(IndexIdentifier + 1)};" + "\n";
        }

        // Output default case
        Output += Indent(CompilerState.Depth);
        Output += "default:" + "\n";
        Output += Indent(CompilerState.Depth + 1);
        Output += $"throw new {nameof(InvalidProgramException)}();" + "\n";

        // Output end goto label switch
        CompilerState.Depth--;
        Output += Indent(CompilerState.Depth);
        Output += "}" + "\n";

        // Finish
        return Output;
    }
    private static string CompileSourceFile(string Output, string? NamespaceName, string ClassName, string MethodName) {
        List<string> Lines = [];

        // Add pragma disable warnings
        Lines.Add("#pragma warning disable IDE0079 // Remove unnecessary suppression");
        Lines.Add("#pragma warning disable IDE0005 // Using directive is unnecessary");
        Lines.Add("#pragma warning disable CS0162 // Unreachable code detected");
        Lines.Add("#pragma warning disable CS0168 // Variable is declared but never used");
        Lines.Add("#pragma warning disable IDE0059 // Unnecessary assignment of a value");
        Lines.Add("#pragma warning restore IDE0079 // Remove unnecessary suppression");
        Lines.Add("");

        // Add usings
        Lines.Add("using System;");
        Lines.Add("using System.Collections.Generic;");
        Lines.Add("using GotochanCs;");
        Lines.Add("using ResultZero;");
        Lines.Add("");

        // Add namespace
        if (NamespaceName is not null) {
            Lines.Add($"namespace {NamespaceName};");
            Lines.Add("");
        }

        // Add class and method
        Lines.Add($"public static partial class {ClassName} {{");
        Lines.Add(Indent(1) + $"public static {nameof(Result)} {MethodName}({nameof(Actor)} {IdentifyActor()}) {{");
        Lines.Add(Indent(2) + Output);
        Lines.Add(Indent(2) + $"return {nameof(Result)}.{nameof(Result.Success)};");
        Lines.Add(Indent(1) + "}");
        Lines.Add("}");

        // Finish
        return string.Join("\n", Lines);
    }
    private static string Indent(int Depth) {
        if (Depth <= 0) {
            return "";
        }
        return new string(' ', Depth * 4);
    }

    private record struct CompilerState() {
        public required ParseResult ParseResult { get; set; }
        public Dictionary<string, int> Variables { get; set; } = [];
        public Dictionary<string, int> Labels { get; set; } = [];
        public int TemporaryCounter { get; set; } = 0;
        public int Depth { get; set; } = 0;
    }
}

/// <summary>
/// A result from <see cref="Compiler"/>.
/// </summary>
public readonly record struct CompileResult {
    /// <summary>
    /// The original Gotochan code.
    /// </summary>
    public required string Source { get; init; }
    /// <summary>
    /// The resulting C# code.
    /// </summary>
    public required string Output { get; init; }
}

/// <summary>
/// Options for <see cref="Compiler"/>.
/// </summary>
public readonly record struct CompileOptions() {
    /// <summary>
    /// The namespace (or none) to generate.
    /// </summary>
    public string? NamespaceName { get; init; } = null;
    /// <summary>
    /// The static partial class to generate.
    /// </summary>
    public string ClassName { get; init; } = "CompileOutput";
    /// <summary>
    /// The static method to generate.
    /// </summary>
    public string MethodName { get; init; } = "Execute";
}