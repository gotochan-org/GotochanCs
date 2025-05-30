#if !NET9_0_OR_GREATER
using Lock = object;
#endif

using ResultZero;

namespace GotochanCs;

public class Actor {
    public static Actor Default { get; } = new();

    public Lock Lock { get; } = new();

    private readonly Dictionary<string, Thingie> Variables = [];

    public Result Interpret(Script Script) {
        lock (Lock) {
            for (int Index = 0; Index < Script.Instructions.Count; Index++) {
                Instruction Instruction = Script.Instructions[Index];

                // Set variable
                if (Instruction is SetVariableInstruction SetVariableInstruction) {
                    // Evaluate value
                    if (InterpretExpression(SetVariableInstruction.Expression).TryGetError(out Error ValueError, out Thingie Value)) {
                        return ValueError;
                    }
                    // Set variable to value
                    Variables[SetVariableInstruction.TargetVariable] = Value;
                }
                // Goto
                else if (Instruction is GotoInstruction GotoInstruction) {
                    // Evaluate condition
                    if (InterpretExpression(GotoInstruction.Condition).TryGetError(out Error ConditionError, out Thingie ConditionResult)) {
                        return ConditionError;
                    }
                    // Ensure condition is flag
                    if (ConditionResult.Type is not ThingieType.Flag) {
                        return new Error($"{Instruction.Line}: condition must be flag, not '{ConditionResult.Type}'");
                    }
                    // Skip instruction if not condition
                    if (!ConditionResult.CastFlag()) {
                        continue;
                    }

                    // Goto line
                    if (GotoInstruction is GotoIndexInstruction GotoIndexInstruction) {
                        // Go to index
                        Index = GotoIndexInstruction.TargetIndex;
                        Index--;
                    }
                    // Goto line
                    else if (GotoInstruction is GotoLineInstruction GotoLineInstruction) {
                        /*// Get index of first instruction on line
                        if (!Script.LineIndexes.TryGetValue(GotoLineInstruction.TargetLine, out int TargetIndex)) {
                            return new Error($"{Instruction.Line}: invalid line");
                        }
                        // Go to index
                        Index = TargetIndex;
                        Index--;*/
                    }
                    // Goto label
                    else if (GotoInstruction is GotoLabelInstruction GotoLabelInstruction) {
                        /*// Get index of label
                        if (!Script.LabelIndexes.TryGetValue(GotoLabelInstruction.TargetLabel, out int TargetIndex)) {
                            return new Error($"{Instruction.Line}: invalid label");
                        }
                        // Go to index
                        Index = TargetIndex;
                        Index--;*/
                    }
                    // Goto goto
                    else if (GotoInstruction is GotoGotoLabelInstruction GotoGotoLabelInstruction) {
                        throw new NotImplementedException();
                    }
                }
                // Invalid
                else {
                    return new Error($"{Instruction.Line}: invalid instruction: '{Instruction}'");
                }
            }
            return Result.Success;
        }
    }
    public Result<Thingie> InterpretExpression(Expression Expression) {
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
                if (InterpretExpression(UnaryExpression.Expression).TryGetError(out Error ExpressionError, out Thingie Value)) {
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
                if (InterpretExpression(BinaryExpression.Expression1).TryGetError(out Error Expression1Error, out Thingie Value1)) {
                    return Expression1Error;
                }
                if (InterpretExpression(BinaryExpression.Expression2).TryGetError(out Error Expression2Error, out Thingie Value2)) {
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
    public Thingie GetVariable(string TargetVariable) {
        lock (Lock) {
            return Variables.GetValueOrDefault(TargetVariable);
        }
    }
    public void SetVariable(string TargetVariable, Thingie Value) {
        lock (Lock) {
            if (Value.Type is ThingieType.Nothing) {
                Variables.Remove(TargetVariable);
            }
            else {
                Variables[TargetVariable] = Value;
            }
        }
    }
}