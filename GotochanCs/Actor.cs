using ResultZero;

namespace GotochanCs;

public class Actor {
    public static Actor Default { get; } = new();

    public Lock Lock { get; } = new();

    private readonly Dictionary<string, Thingie> Variables = [];

    public Result Interpret(scoped ReadOnlySpan<Instruction> Instructions) {
        lock (Lock) {
            Dictionary<string, int> LabelIndexes = FindLabels();

            for (int Index = 0; Index < Instructions.Length; Index++) {
                Instruction Instruction = Instructions[Index];

                // Set variable
                if (Instruction is SetVariableInstruction SetVariableInstruction) {
                    if (Interpret(SetVariableInstruction.Expression).TryGetError(out Error Error, out Thingie Value)) {
                        return Error;
                    }
                    Variables[SetVariableInstruction.TargetVariable] = Value;
                }
                // Goto line
                else if (Instruction is GotoLineInstruction GotoLineInstruction) {
                    int? TargetInstructionIndex = FindFirstInstructionOnLine(Instructions, GotoLineInstruction.Line);
                    if (TargetInstructionIndex is null) {
                        return new Error($"{Instruction.Line}: invalid line");
                    }
                    Index = TargetInstructionIndex.Value;
                    Index--;
                }
                // Goto label
                else if (Instruction is GotoLabelInstruction GotoLabelInstruction) {

                }
                // Invalid
                else {
                    return new Error($"{Instruction.Line}: invalid instruction: '{Instruction}'");
                }
            }
            return Result.Success;
        }
    }
    public Result<Thingie> Interpret(Expression Expression) {
        lock (Lock) {
            // Constant
            if (Expression is ConstantExpression ConstantExpression) {
                return ConstantExpression.Value;
            }
            // Get variable
            else if (Expression is GetVariableExpression GetVariableExpression) {
                return Variables.GetValueOrDefault(GetVariableExpression.TargetVariable);
            }
            // Unary
            else if (Expression is UnaryExpression UnaryExpression) {
                // Evaluate expression
                if (Interpret(UnaryExpression.Expression).TryGetError(out Error ExpressionError, out Thingie Value)) {
                    return ExpressionError;
                }

                // Plus
                if (UnaryExpression.Operator is UnaryOperator.Plus) {
                    // Number
                    if (Value.Type is ThingieType.Number) {
                        return Thingie.Number(+Value.CastNumber());
                    }
                    // Invalid
                    else {
                        return new Error($"invalid type for '{UnaryExpression.Operator}': '{Value.Type}'");
                    }
                }
                // Minus
                else if (UnaryExpression.Operator is UnaryOperator.Minus) {
                    // Number
                    if (Value.Type is ThingieType.Number) {
                        return Thingie.Number(-Value.CastNumber());
                    }
                    // Invalid
                    else {
                        return new Error($"invalid type for '{UnaryExpression.Operator}': '{Value.Type}'");
                    }
                }
                // Invalid
                else {
                    return new Error($"invalid unary operator: '{UnaryExpression.Operator}'");
                }
            }
            // Binary
            else if (Expression is BinaryExpression BinaryExpression) {
                // Evaluate expressions
                if (Interpret(BinaryExpression.Expression1).TryGetError(out Error Expression1Error, out Thingie Value1)) {
                    return Expression1Error;
                }
                if (Interpret(BinaryExpression.Expression2).TryGetError(out Error Expression2Error, out Thingie Value2)) {
                    return Expression2Error;
                }

                // Add
                if (BinaryExpression.Operator is BinaryOperator.Add) {
                    // Number, Number
                    if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
                        return Thingie.Number(Value1.CastNumber() + Value2.CastNumber());
                    }
                    // String, Thingie
                    else if (Value1.Type is ThingieType.String) {
                        return Thingie.String(Value1.CastString() + Value2.ToString());
                    }
                    // Invalid
                    else {
                        return new Error($"invalid types for '{BinaryExpression.Operator}': '{Value1.Type}', '{Value2.Type}'");
                    }
                }
                // Subtract
                else if (BinaryExpression.Operator is BinaryOperator.Subtract) {
                    // Number, Number
                    if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
                        return Thingie.Number(Value1.CastNumber() - Value2.CastNumber());
                    }
                    // Invalid
                    else {
                        return new Error($"invalid types for '{BinaryExpression.Operator}': '{Value1.Type}', '{Value2.Type}'");
                    }
                }
                // Multiply
                else if (BinaryExpression.Operator is BinaryOperator.Multiply) {
                    // Number, Number
                    if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
                        return Thingie.Number(Value1.CastNumber() * Value2.CastNumber());
                    }
                    // String, Number
                    else if (Value1.Type is ThingieType.String && Value2.Type is ThingieType.Number) {
                        double Number2 = Value2.CastNumber();
                        int Int2 = (int)Number2;
                        if (Int2 < 0 || Number2 != Int2) {
                            return new Error("number must be positive integer to multiply string");
                        }
                        return Thingie.String(string.Concat(Enumerable.Repeat(Value1.CastString(), Int2)));
                    }
                    // Invalid
                    else {
                        return new Error($"invalid types for '{BinaryExpression.Operator}': '{Value1.Type}', '{Value2.Type}'");
                    }
                }
                // Divide
                else if (BinaryExpression.Operator is BinaryOperator.Divide) {
                    // Number, Number
                    if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
                        return Thingie.Number(Value1.CastNumber() / Value2.CastNumber());
                    }
                    // Invalid
                    else {
                        return new Error($"invalid types for '{BinaryExpression.Operator}': '{Value1.Type}', '{Value2.Type}'");
                    }
                }
                // Modulo
                else if (BinaryExpression.Operator is BinaryOperator.Modulo) {
                    // Number, Number
                    if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
                        return Thingie.Number(Value1.CastNumber() % Value2.CastNumber());
                    }
                    // Invalid
                    else {
                        return new Error($"invalid types for '{BinaryExpression.Operator}': '{Value1.Type}', '{Value2.Type}'");
                    }
                }
                // Exponentiate
                else if (BinaryExpression.Operator is BinaryOperator.Exponentiate) {
                    // Number, Number
                    if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
                        return Thingie.Number(double.Pow(Value1.CastNumber(), Value2.CastNumber()));
                    }
                    // Invalid
                    else {
                        return new Error($"invalid types for '{BinaryExpression.Operator}': '{Value1.Type}', '{Value2.Type}'");
                    }
                }
                // Invalid
                else {
                    return new Error($"invalid binary operator: '{BinaryExpression.Operator}'");
                }
            }
            // Invalid
            else {
                return new Error($"invalid expression: '{Expression}'");
            }
        }
    }

    private static int? FindFirstInstructionOnLine(scoped ReadOnlySpan<Instruction> Instructions, int Line) {
        int LeftPointer = 0;
        int RightPointer = Instructions.Length - 1;

        while (true) {
            if (LeftPointer > RightPointer) {
                return null;
            }

            int MidPointer = (LeftPointer + RightPointer) / 2;

            if (Instructions[MidPointer].Line < Line) {
                LeftPointer = MidPointer + 1;
            }
            else if (Instructions[MidPointer].Line > Line) {
                RightPointer = MidPointer - 1;
            }
            else {
                while (MidPointer > 0 && Instructions[MidPointer - 1].Line == Line) {
                    MidPointer--;
                }
                return MidPointer;
            }
        }
    }
    /*private static int? FindLabel(scoped ReadOnlySpan<Instruction> Instructions) {
        foreach (Instruction Instruction in Instructions) {

        }
    }*/
    private static Result<Dictionary<string, int>> FindLabels(scoped ReadOnlySpan<Instruction> Instructions) {
        Dictionary<string, int> LabelIndexes = [];

        for (int Index = 0; Index < Instructions.Length; Index++) {
            Instruction Instruction = Instructions[Index];

            if (Instruction is LabelInstruction LabelInstruction) {
                if (!LabelIndexes.TryAdd(LabelInstruction.Name, Index)) {
                    return new Error($"duplicate label: '{LabelInstruction.Name}'");
                }
            }
        }

        return LabelIndexes;
    }
}