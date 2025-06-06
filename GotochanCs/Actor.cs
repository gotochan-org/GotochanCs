#if !NET9_0_OR_GREATER
using Lock = object;
#endif

using ResultZero;
using System.Runtime.InteropServices;

namespace GotochanCs;

public class Actor {
    public static Actor Default { get; } = new();

    public Lock Lock { get; } = new();

    private readonly Dictionary<string, Thingie> Variables = [];
    private readonly Dictionary<string, int> GotoLabelIndexes = [];
    private readonly Dictionary<string, Action<Actor>> ExternalLabels = [];

    public Actor() {
    }
    public Actor(IEnumerable<Bundle> Bundles)
        : this() {
        foreach (Bundle Bundle in Bundles) {
            IncludeBundle(Bundle);
        }
    }
    public Result Interpret(ParseResult ParseResult) {
        lock (Lock) {
            // Get instructions as span
            ReadOnlySpan<Instruction> InstructionsSpan = CollectionsMarshal.AsSpan(ParseResult.Instructions);

            // Interpret each instruction
            for (int Index = 0; Index < InstructionsSpan.Length; Index++) {
                Instruction Instruction = InstructionsSpan[Index];

                // Condition
                if (Instruction.Condition is not null) {
                    // Evaluate condition
                    if (InterpretExpression(Instruction.Condition).TryGetError(out Error ConditionError, out Thingie ConditionResult)) {
                        return ConditionError;
                    }
                    // Ensure condition is flag
                    if (ConditionResult.Type is not ThingieType.Flag) {
                        return new Error($"{Instruction.Location}: condition must be flag, not '{ConditionResult.Type}'");
                    }
                    // Skip instruction if not condition
                    if (!ConditionResult.CastFlag()) {
                        continue;
                    }
                }

                // Set variable
                if (Instruction is SetVariableInstruction SetVariableInstruction) {
                    // Evaluate value
                    if (InterpretExpression(SetVariableInstruction.Expression).TryGetError(out Error ValueError, out Thingie Value)) {
                        return ValueError;
                    }
                    // Set variable to value
                    Variables[SetVariableInstruction.TargetVariable] = Value;
                }
                // Label
                else if (Instruction is LabelInstruction LabelInstruction) {
                    // Set label index
                    ParseResult.LabelIndexes[LabelInstruction.Label] = Index;
                }
                // Goto line
                else if (Instruction is GotoLineInstruction GotoLineInstruction) {
                    // Check for goto end of program
                    if (GotoLineInstruction.TargetLine > ParseResult.MaximumLine) {
                        break;
                    }
                    // Get index of first instruction on line
                    if (!ParseResult.LineIndexes.TryGetValue(GotoLineInstruction.TargetLine, out int TargetIndex)) {
                        return new Error($"{Instruction.Location.Line}: invalid line");
                    }
                    // Go to index
                    Index = TargetIndex;
                    Index--;
                }
                // Goto label
                else if (Instruction is GotoLabelInstruction GotoLabelInstruction) {
                    // Get index of label
                    if (!ParseResult.LabelIndexes.TryGetValue(GotoLabelInstruction.TargetLabel, out int TargetIndex)) {
                        // Get external label
                        if (ExternalLabels.TryGetValue(GotoLabelInstruction.TargetLabel, out Action<Actor>? ExternalLabel)) {
                            // Call external label
                            try {
                                ExternalLabel(this);
                                continue;
                            }
                            // Return exceptions as errors
                            catch (Exception Ex) {
                                return new Error($"{Instruction.Location.Line}: '{Ex.GetType()}': '{Ex.Message}'");
                            }
                        }
                        // Invalid
                        else {
                            return new Error($"{Instruction.Location.Line}: invalid label");
                        }
                    }
                    // Set index of goto label
                    GotoLabelIndexes[GotoLabelInstruction.TargetLabel] = Index;
                    // Go to index
                    Index = TargetIndex;
                    Index--;
                }
                // Goto goto
                else if (Instruction is GotoGotoLabelInstruction GotoGotoLabelInstruction) {
                    // Get index of goto label
                    if (!GotoLabelIndexes.TryGetValue(GotoGotoLabelInstruction.TargetLabel, out int TargetIndex)) {
                        return new Error($"{Instruction.Location.Line}: no entry for goto label");
                    }
                    // Go to index
                    Index = TargetIndex;
                }
                // Invalid
                else {
                    return new Error($"{Instruction.Location.Line}: invalid instruction: '{Instruction}'");
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
                        return new Error($"{Expression.Location.Line}: invalid type for '{UnaryExpression.Operator}': '{Value.Type}'");
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
                        return new Error($"{Expression.Location.Line}: invalid type for '{UnaryExpression.Operator}': '{Value.Type}'");
                    }
                }
                // Invalid
                else {
                    return new Error($"{Expression.Location.Line}: invalid unary operator: '{UnaryExpression.Operator}'");
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
                        return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1.Type}', '{Value2.Type}'");
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
                        return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1.Type}', '{Value2.Type}'");
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
                            return new Error($"{Expression.Location.Line}: number must be positive integer to multiply string");
                        }
                        return Thingie.String(string.Concat(Enumerable.Repeat(Value1.CastString(), Int2)));
                    }
                    // Invalid
                    else {
                        return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1.Type}', '{Value2.Type}'");
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
                        return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1.Type}', '{Value2.Type}'");
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
                        return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1.Type}', '{Value2.Type}'");
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
                        return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1.Type}', '{Value2.Type}'");
                    }
                }
                // Equals
                else if (BinaryExpression.Operator is BinaryOperator.Equals) {
                    // Thingie, Thingie
                    return Thingie.Flag(Value1 == Value2);
                }
                // Not equals
                else if (BinaryExpression.Operator is BinaryOperator.NotEquals) {
                    // Thingie, Thingie
                    return Thingie.Flag(Value1 != Value2);
                }
                // Greater than
                else if (BinaryExpression.Operator is BinaryOperator.GreaterThan) {
                    // Number, Number
                    if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
                        return Thingie.Flag(Value1.CastNumber() > Value2.CastNumber());
                    }
                    // Invalid
                    else {
                        return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1.Type}', '{Value2.Type}'");
                    }
                }
                // Less than
                else if (BinaryExpression.Operator is BinaryOperator.LessThan) {
                    // Number, Number
                    if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
                        return Thingie.Flag(Value1.CastNumber() < Value2.CastNumber());
                    }
                    // Invalid
                    else {
                        return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1.Type}', '{Value2.Type}'");
                    }
                }
                // Greater than or equal to
                else if (BinaryExpression.Operator is BinaryOperator.GreaterThanOrEqualTo) {
                    // Number, Number
                    if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
                        return Thingie.Flag(Value1.CastNumber() >= Value2.CastNumber());
                    }
                    // Invalid
                    else {
                        return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1.Type}', '{Value2.Type}'");
                    }
                }
                // Less than or equal to
                else if (BinaryExpression.Operator is BinaryOperator.LessThanOrEqualTo) {
                    // Number, Number
                    if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
                        return Thingie.Flag(Value1.CastNumber() <= Value2.CastNumber());
                    }
                    // Invalid
                    else {
                        return new Error($"{Expression.Location.Line}: invalid types for '{BinaryExpression.Operator}': '{Value1.Type}', '{Value2.Type}'");
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
    public Dictionary<string, Thingie> GetVariables() {
        lock (Lock) {
            return Variables.ToDictionary();
        }
    }
    public Delegate? GetExternalLabel(string TargetLabel) {
        lock (Lock) {
            return ExternalLabels.GetValueOrDefault(TargetLabel);
        }
    }
    public void SetExternalLabel(string TargetLabel, Action<Actor>? Value) {
        lock (Lock) {
            if (Value is null) {
                ExternalLabels.Remove(TargetLabel);
            }
            else {
                ExternalLabels[TargetLabel] = Value;
            }
        }
    }
    public Dictionary<string, Action<Actor>> GetExternalLabels() {
        lock (Lock) {
            return ExternalLabels.ToDictionary();
        }
    }
    public void IncludeBundle(Bundle Bundle) {
        // Add external labels
        foreach (KeyValuePair<string, Action<Actor>> ExternalLabel in Bundle.ExternalLabels) {
            ExternalLabels[ExternalLabel.Key] = ExternalLabel.Value;
        }
    }
}