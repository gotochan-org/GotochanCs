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

        // Output temporary variables
        Output += $"{nameof(Thingie)} {IdentifyTemporary(0)}, {IdentifyTemporary(1)} = {nameof(Thingie)}.{nameof(Thingie.Nothing)}();";

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
            VariablesBuilder.Append($"{nameof(Thingie)} {IdentifyVariable(VariableIdentifier)} = {nameof(Thingie)}.{nameof(Thingie.Nothing)}();" + "\n");
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

        // Condition
        if (Instruction.Condition is not null) {
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
            // Output reset temporary
            Output += $"{IdentifyTemporary(0)} = {nameof(Thingie)}.{nameof(Thingie.Nothing)}();" + "\n";
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
            // Output reset temporary
            Output += $"{IdentifyTemporary(0)} = {nameof(Thingie)}.{nameof(Thingie.Nothing)}();" + "\n";
        }

        // Finish
        return Output;
    }
    private static Result<string> CompileExpression(Expression Expression, ref CompilerState CompilerState, int TemporaryIdentifier) {
        string Output = "";

        // Constant
        if (Expression is ConstantExpression ConstantExpression) {
            // Output constant
            return ConstantExpression.Value.Type switch {
                ThingieType.Nothing => $"{nameof(Thingie)}.{nameof(Thingie.Nothing)}()",
                ThingieType.Flag => $"{nameof(Thingie)}.{nameof(Thingie.Flag)}({(ConstantExpression.Value.CastFlag() ? "true" : "false")})",
                ThingieType.Number => $"{nameof(Thingie)}.{nameof(Thingie.Number)}({ConstantExpression.Value.CastNumber()})",
                ThingieType.String => $"{nameof(Thingie)}.{nameof(Thingie.String)}({CreateStringLiteral(ConstantExpression.Value.CastString())})",
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
            // Compile expression
            if (CompileExpression(UnaryExpression.Expression, ref CompilerState, 0).TryGetError(out Error ExpressionError, out string? ExpressionOutput)) {
                return ExpressionError;
            }
            // Output expression
            Output += ExpressionOutput + "\n";

            // Plus
            if (UnaryExpression.Operator is UnaryOperator.Plus) {
                /*// Output unary plus
                Output += $"switch ({IdentifyTemporary(0)}.{nameof(Thingie.Type)}) {{" + "\n";
                Output += $"case {nameof(ThingieType)}.{nameof(ThingieType.Number)}:" + "\n";
                Output += $"{IdentifyTemporary(0)} = {nameof(Thingie)}.{nameof(Thingie.Number)}(+{IdentifyTemporary(0)}.{nameof(Thingie.CastNumber)}())" + "\n";
                Output += "break;" + "\n";
                Output += "default:" + "\n";
                Output += "return new Error($\"invalid type for '{UnaryExpression.Operator}': '{Value.Type}'\");" + "\n";
                Output += $"return new {nameof(Error)}(\"invalid type for '{UnaryExpression.Operator}': '{IdentifyTemporary(0)}.{nameof(Thingie.Type)}'\");" + "\n";
                Output += $"}}" + "\n";*/

                /*//if (!CompilerState.Methods.ContainsKey("Plus")) {
                CompilerState.Methods.TryAdd("Plus", $$"""
                    void Plus({{nameof(Thingie)}} Value) {
                        switch (Value.{{nameof(Thingie.Type)}}) {
                            case {{nameof(ThingieType)}}.{{nameof(ThingieType.Number)}}:
                                break;
                            default:
                                return new Error($"{Expression.Location.Line}: invalid type for '{UnaryExpression.Operator}': '{ValueSle.Type}'");
                        }
                    }
                    """);
                //}*/

                // Output unary plus
                Output += $"{nameof(Thingie)}.{nameof(Thingie.Plus)}({CreateLocationLiteral(UnaryExpression.Location)}, {IdentifyTemporary(0)})" + "\n";
            }
            // Minus
            else if (UnaryExpression.Operator is UnaryOperator.Minus) {
                /*// Number
                if (ValueSle.Type is ThingieType.Number) {
                    return Thingie.Number(-ValueSle.CastNumber());
                }
                // Invalid
                else {
                    return new Error($"{Expression.Location.Line}: invalid type for '{UnaryExpression.Operator}': '{ValueSle.Type}'");
                }*/

                // Output unary minus
                Output += $"{nameof(Thingie)}.{nameof(Thingie.Minus)}({CreateLocationLiteral(UnaryExpression.Location)}, {IdentifyTemporary(0)})" + "\n";
            }
            // Invalid
            else {
                return new Error($"{Expression.Location.Line}: invalid unary operator: '{UnaryExpression.Operator}'");
            }

            // Output reset temporary
            Output += $"{IdentifyTemporary(0)} = {nameof(Thingie)}.{nameof(Thingie.Nothing)}();" + "\n";
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

            // TODO
            // ...

            // Output reset temporaries
            Output += $"{IdentifyTemporary(0)} = {IdentifyTemporary(1)} = {nameof(Thingie)}.{nameof(Thingie.Nothing)}();" + "\n";
        }
        // Invalid
        else {
            return new Error($"{Expression.Location.Line}: invalid expression: '{Expression}'");
        }

        // Finish
        return Output;
    }

    /*public static Result<CompileResult> Compile(ParseResult ParseResult) {
        List<SLE.Expression> Sles = [];
        Dictionary<string, SLE.ParameterExpression> VariableSles = [];

        // Create line targets
        Dictionary<int, SLE.LabelTarget> LineTargets = [];
        foreach (KeyValuePair<int, int> LineIndex in ParseResult.LineIndexes) {
            LineTargets[LineIndex.Key] = SLE.Expression.Label();
        }
        // Create label targets
        Dictionary<string, SLE.LabelTarget> LabelTargets = [];
        foreach (KeyValuePair<string, int> LabelIndex in ParseResult.LabelIndexes) {
            LabelTargets[LabelIndex.Key] = SLE.Expression.Label(name: LabelIndex.Key);
        }

        // Create return target
        SLE.LabelTarget ReturnTarget = SLE.Expression.Label(typeof(Error));

        // Create parameter SLEs
        SLE.ParameterExpression ActorSle = SLE.Expression.Parameter(typeof(Actor), "Actor");
        // Create accessor SLEs
        SLE.Expression<Func<Actor, Dictionary<string, int>>> GotoLabelIndexes = (Actor) => Actor.GotoLabelIndexes;

        // Get instructions as span
        ReadOnlySpan<Instruction> InstructionsSpan = CollectionsMarshal.AsSpan(ParseResult.Instructions);

        // Compile instructions as SLEs
        for (int Index = 0; Index < InstructionsSpan.Length; Index++) {
            Instruction Instruction = InstructionsSpan[Index];

            // Line
            foreach (KeyValuePair<int, int> LineIndex in ParseResult.LineIndexes) {
                if (LineIndex.Value == Index) {
                    // Get line target
                    SLE.LabelTarget LineTarget = LineTargets[LineIndex.Key];
                    // Create line SLE
                    SLE.LabelExpression LabelSle = SLE.Expression.Label(LineTarget);
                    Sles.Add(LabelSle);
                    break;
                }
            }
            // Label
            foreach (KeyValuePair<string, int> LabelIndex in ParseResult.LabelIndexes) {
                if (LabelIndex.Value == Index) {
                    // Get label target
                    SLE.LabelTarget LabelTarget = LabelTargets[LabelIndex.Key];
                    // Create label SLE
                    SLE.LabelExpression LabelSle = SLE.Expression.Label(LabelTarget);
                    Sles.Add(LabelSle);
                    break;
                }
            }

            // Compile instruction as SLE
            if (CompileInstruction(Instruction, VariableSles, LineTargets, LabelTargets, ReturnTarget).TryGetError(out Error InstructionError, out SLE.Expression? InstructionSle)) {
                return InstructionError;
            }
            Sles.Add(InstructionSle);
        }

        // Add successful return SLE
        Sles.Add(SLE.Expression.Return(ReturnTarget, SLE.Expression.Constant(Result.Success)));
        // Add return target SLE
        Sles.Add(SLE.Expression.Label(ReturnTarget));

        // Set variables in actor
        // TODO

        // Create block SLE
        SLE.BlockExpression BlockSle = SLE.Expression.Block(Sles);
        // Create lambda SLE
        SLE.Expression<Func<Actor, Result>> LambdaSle = SLE.Expression.Lambda<Func<Actor, Result>>(BlockSle, ActorSle);
        // Compile lambda SLE
        Func<Actor, Result> Delegate = LambdaSle.Compile();

        // Finish
        return new CompileResult() {
            Source = ParseResult.Source,
            Delegate = Delegate,
        };
    }

    [RequiresDynamicCode(DelegateCreationRequiresDynamicCode)]
    private static Result<SLE.Expression?> CompileInstruction(Instruction Instruction, Dictionary<string, SLE.ParameterExpression> VariableSles,
        Dictionary<int, SLE.LabelTarget> LineTargets, Dictionary<string, SLE.LabelTarget> LabelTargets, SLE.LabelTarget ReturnTarget
    ) {
        // Condition
        SLE.Expression? ConditionSle = null;
        if (Instruction.Condition is not null) {
            // Compile condition
            if (CompileExpression(Instruction.Condition, VariableSles).TryGetError(out Error ConditionError, out ConditionSle)) {
                return ConditionError;
            }
        }

        // Instruction
        SLE.Expression? InstructionSle = null;
        // Set variable
        if (Instruction is SetVariableInstruction SetVariableInstruction) {
            // Get or create variable SLE
            if (!VariableSles.TryGetValue(SetVariableInstruction.TargetVariable, out SLE.ParameterExpression? VariableSle)) {
                VariableSle = SLE.Expression.Variable(typeof(Thingie), SetVariableInstruction.TargetVariable);
                VariableSles[SetVariableInstruction.TargetVariable] = VariableSle;
            }
            // Create value SLE
            if (CompileExpression(SetVariableInstruction.Expression, VariableSles).TryGetError(out Error ValueError, out SLE.Expression? ValueSle)) {
                return ValueError;
            }
            // Create assign SLE
            SLE.BinaryExpression AssignSle = SLE.Expression.Assign(VariableSle, ValueSle);
            InstructionSle = AssignSle;
        }
        // Label
        else if (Instruction is LabelInstruction LabelInstruction) {
            // Pass
        }
        // Goto line
        else if (Instruction is GotoLineInstruction GotoLineInstruction) {
            // Get line target
            SLE.LabelTarget LineTarget = LineTargets[GotoLineInstruction.TargetLine];
            // Create goto SLE
            SLE.GotoExpression GotoSle = SLE.Expression.Goto(LineTarget);
            InstructionSle = GotoSle;
        }
        // Goto label
        else if (Instruction is GotoLabelInstruction GotoLabelInstruction) {
            // Get label target
            SLE.LabelTarget LabelTarget = LabelTargets[GotoLabelInstruction.TargetLabel];
            // Create goto SLE
            SLE.GotoExpression GotoSle = SLE.Expression.Goto(LabelTarget);
            InstructionSle = GotoSle;
        }
        // Goto goto label
        else if (Instruction is GotoGotoLabelInstruction GotoGotoLabelInstruction) {
            // 
            SLE.Expression<Func<Dictionary<string, int>, bool>> HasGotoLabelIndexSle = (GotoLabelIndexes) => GotoLabelIndexes.ContainsKey(GotoGotoLabelInstruction.TargetLabel);
            //
            SLE.Expression<Func<Dictionary<string, int>, int>> GetGotoLabelIndexSle = (GotoLabelIndexes) => GotoLabelIndexes[GotoGotoLabelInstruction.TargetLabel];
            //
            SLE.ConstantExpression ErrorSle = SLE.Expression.Constant(new Error($"{GotoGotoLabelInstruction.Location.Line}: no entry for goto label"));
            SLE.GotoExpression ReturnErrorSle = SLE.Expression.Return(ReturnTarget, ErrorSle);
            //
            SLE.Expression.IfThenElse(HasGotoLabelIndexSle, GetGotoLabelIndexSle, ReturnErrorSle);

            throw new NotImplementedException();
        }
        // Not implemented
        else {
            return new Error($"unhandled instruction: '{Instruction.GetType()}'");
        }

        // Add conditional instruction SLE
        if (InstructionSle is not null && ConditionSle is not null) {
            SLE.ConditionalExpression ConditionalSle = SLE.Expression.IfThen(ConditionSle, InstructionSle);
            return ConditionalSle;
        }
        // Add instruction SLE
        else if (InstructionSle is not null) {
            return InstructionSle;
        }
        // Add condition SLE
        else if (ConditionSle is not null) {
            return ConditionSle;
        }
        // Pass
        else {
            return (SLE.Expression?)null;
        }
    }
    [RequiresDynamicCode(DelegateCreationRequiresDynamicCode)]
    private static Result<SLE.Expression> CompileExpression(Expression Expression, Dictionary<string, SLE.ParameterExpression> VariableSles) {
        // Constant
        if (Expression is ConstantExpression ConstantExpression) {
            // Create constant SLE
            return SLE.Expression.Constant(ConstantExpression.Value, typeof(Thingie));
        }
        // Get variable
        else if (Expression is GetVariableExpression GetVariableExpression) {
            // Get or create variable SLE
            if (!VariableSles.TryGetValue(GetVariableExpression.TargetVariable, out SLE.ParameterExpression? VariableSle)) {
                VariableSle = SLE.Expression.Variable(typeof(Thingie), GetVariableExpression.TargetVariable);
                VariableSles[GetVariableExpression.TargetVariable] = VariableSle;
            }
            return VariableSle;
        }
        // Unary
        else if (Expression is UnaryExpression UnaryExpression) {
            // Compile expression
            if (CompileExpression(UnaryExpression.Expression, VariableSles).TryGetError(out Error ExpressionError, out SLE.Expression? ValueSle)) {
                return ExpressionError;
            }

            // Plus
            if (UnaryExpression.Operator is UnaryOperator.Plus) {
                // Store value in temporary variable
                SLE.ParameterExpression ValueTempSle = SLE.Expression.Variable(typeof(Thingie));

                // Number
                SLE.LambdaExpression IsNumberSle = SLE.Expression.Lambda((Thingie Value) => Value.Type == ThingieType.Number);
                SLE.LambdaExpression WhenNumberSle = SLE.Expression.Lambda((Thingie Value) => Thingie.Number(+Value.CastNumber()));

                //
                SLE.Expression Condition =
                    SLE.Expression.IfThenElse(IsNumberSle, WhenNumberSle,
                        SLE.Expression.Lambda);

                //SLE.Expression.PropertyOrField(ValueTempSle, nameof(Thingie.Type));
                //SLE.TypeBinaryExpression IsNumberSle = SLE.Expression.TypeIs(ValueTempSle,);

                // Number
                if (Value.Type is ThingieType.Number) {
                    return Thingie.Number(+Value.CastNumber());
                }
                // Invalid
                else {
                    return new Error($"{Expression.Location.Line}: invalid type for '{UnaryExpression.Operator}': '{Value.Type}'");
                }
            }
            // Minus
            else if (UnaryExpression.Operator is UnaryOperator.Minus) {
                // Number
                if (ValueSle.Type is ThingieType.Number) {
                    return Thingie.Number(-ValueSle.CastNumber());
                }
                // Invalid
                else {
                    return new Error($"{Expression.Location.Line}: invalid type for '{UnaryExpression.Operator}': '{ValueSle.Type}'");
                }
            }
            // Invalid
            else {
                return new Error($"{Expression.Location.Line}: invalid unary operator: '{UnaryExpression.Operator}'");
            }
        }
        // Binary
        else if (Expression is BinaryExpression BinaryExpression) {
            // Compile expressions
            if (CompileExpression(BinaryExpression.Expression1, VariableSles).TryGetError(out Error Expression1Error, out SLE.Expression? Value1Sle)) {
                return Expression1Error;
            }
            if (CompileExpression(BinaryExpression.Expression2, VariableSles).TryGetError(out Error Expression2Error, out SLE.Expression? Value2Sle)) {
                return Expression2Error;
            }

            // Add
            if (BinaryExpression.Operator is BinaryOperator.Add) {
                // Evaluate expression 1
                SLE.ParameterExpression Temp1 = SLE.Expression.Variable(typeof(Thingie));
                SLE.ParameterExpression Temp2 = SLE.Expression.Variable(typeof(Thingie));
                //
                SLE.Expression.PropertyOrField(BinaryExpression.Expression1, );*/

    /*// Number, Number
    if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
        return Thingie.Number(Value1.CastNumber() + Value2.CastNumber());
    }
    // String, Thingie
    else if (Value1.Type is ThingieType.String) {
        return Thingie.String(Value1.CastString() + Value2.ToString());
    }
    // Invalid
    else {
        return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1.Type}', '{Value2.Type}'");
    }*/
    /*}
    // Subtract
    else if (BinaryExpression.Operator is BinaryOperator.Subtract) {
        // Number, Number
        if (Value1Sle.Type is ThingieType.Number && Value2Sle.Type is ThingieType.Number) {
            return Thingie.Number(Value1Sle.CastNumber() - Value2Sle.CastNumber());
        }
        // Invalid
        else {
            return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1Sle.Type}', '{Value2Sle.Type}'");
        }
    }
    // Multiply
    else if (BinaryExpression.Operator is BinaryOperator.Multiply) {
        // Number, Number
        if (Value1Sle.Type is ThingieType.Number && Value2Sle.Type is ThingieType.Number) {
            return Thingie.Number(Value1Sle.CastNumber() * Value2Sle.CastNumber());
        }
        // String, Number
        else if (Value1Sle.Type is ThingieType.String && Value2Sle.Type is ThingieType.Number) {
            double Number2 = Value2Sle.CastNumber();
            int Int2 = (int)Number2;
            if (Int2 < 0 || Number2 != Int2) {
                return new Error($"{Expression.Location.Line}: number must be positive integer to multiply string");
            }
            return Thingie.String(string.Concat(Enumerable.Repeat(Value1Sle.CastString(), Int2)));
        }
        // Invalid
        else {
            return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1Sle.Type}', '{Value2Sle.Type}'");
        }
    }
    // Divide
    else if (BinaryExpression.Operator is BinaryOperator.Divide) {
        // Number, Number
        if (Value1Sle.Type is ThingieType.Number && Value2Sle.Type is ThingieType.Number) {
            return Thingie.Number(Value1Sle.CastNumber() / Value2Sle.CastNumber());
        }
        // Invalid
        else {
            return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1Sle.Type}', '{Value2Sle.Type}'");
        }
    }
    // Modulo
    else if (BinaryExpression.Operator is BinaryOperator.Modulo) {
        // Number, Number
        if (Value1Sle.Type is ThingieType.Number && Value2Sle.Type is ThingieType.Number) {
            return Thingie.Number(Value1Sle.CastNumber() % Value2Sle.CastNumber());
        }
        // Invalid
        else {
            return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1Sle.Type}', '{Value2Sle.Type}'");
        }
    }
    // Exponentiate
    else if (BinaryExpression.Operator is BinaryOperator.Exponentiate) {
        // Number, Number
        if (Value1Sle.Type is ThingieType.Number && Value2Sle.Type is ThingieType.Number) {
            return Thingie.Number(double.Pow(Value1Sle.CastNumber(), Value2Sle.CastNumber()));
        }
        // Invalid
        else {
            return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1Sle.Type}', '{Value2Sle.Type}'");
        }
    }
    // Equals
    else if (BinaryExpression.Operator is BinaryOperator.Equals) {
        // Thingie, Thingie
        return Thingie.Flag(Value1Sle == Value2Sle);
    }
    // Not equals
    else if (BinaryExpression.Operator is BinaryOperator.NotEquals) {
        // Thingie, Thingie
        return Thingie.Flag(Value1Sle != Value2Sle);
    }
    // Greater than
    else if (BinaryExpression.Operator is BinaryOperator.GreaterThan) {
        // Number, Number
        if (Value1Sle.Type is ThingieType.Number && Value2Sle.Type is ThingieType.Number) {
            return Thingie.Flag(Value1Sle.CastNumber() > Value2Sle.CastNumber());
        }
        // Invalid
        else {
            return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1Sle.Type}', '{Value2Sle.Type}'");
        }
    }
    // Less than
    else if (BinaryExpression.Operator is BinaryOperator.LessThan) {
        // Number, Number
        if (Value1Sle.Type is ThingieType.Number && Value2Sle.Type is ThingieType.Number) {
            return Thingie.Flag(Value1Sle.CastNumber() < Value2Sle.CastNumber());
        }
        // Invalid
        else {
            return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1Sle.Type}', '{Value2Sle.Type}'");
        }
    }
    // Greater than or equal to
    else if (BinaryExpression.Operator is BinaryOperator.GreaterThanOrEqualTo) {
        // Number, Number
        if (Value1Sle.Type is ThingieType.Number && Value2Sle.Type is ThingieType.Number) {
            return Thingie.Flag(Value1Sle.CastNumber() >= Value2Sle.CastNumber());
        }
        // Invalid
        else {
            return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1Sle.Type}', '{Value2Sle.Type}'");
        }
    }
    // Less than or equal to
    else if (BinaryExpression.Operator is BinaryOperator.LessThanOrEqualTo) {
        // Number, Number
        if (Value1Sle.Type is ThingieType.Number && Value2Sle.Type is ThingieType.Number) {
            return Thingie.Flag(Value1Sle.CastNumber() <= Value2Sle.CastNumber());
        }
        // Invalid
        else {
            return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1Sle.Type}', '{Value2Sle.Type}'");
        }
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
}*/

    private static string CreateStringLiteral(string Input) {
        return $"@\"{Input.Replace("\"", "\"\"")}\"";
    }
    private static string CreateLocationLiteral(SourceLocation Location) {
        return $"new {nameof(SourceLocation)}({CreateStringLiteral(Location.Source)}, {Location.Index}, {Location.Line})";
    }

    private struct CompilerState() {
        public Dictionary<string, int> Variables { get; set; } = [];
        public Dictionary<string, int> Labels { get; set; } = [];
    }
}

public class CompileResult {
    public required string Source { get; init; }
    public required string Output { get; init; }
}