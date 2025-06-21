#if !NET9_0_OR_GREATER
using Lock = object;
#endif

using ResultZero;
using System.Runtime.InteropServices;

namespace GotochanCs;

public class Actor {
    public static Actor Default { get; } = new();

    public Lock Lock { get; } = new();
    public Dictionary<string, Thingie> Variables { get; } = [];
    public Dictionary<string, int> GotoLabelIndexes { get; } = [];
    public Dictionary<string, Action<Actor>> ExternalLabels { get; } = [];

    public Actor() {
    }
    public Actor(params IEnumerable<Bundle> Bundles)
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
                        return new Error($"{Instruction.Condition.Location}: condition must be flag, not '{ConditionResult.Type}'");
                    }
                    // Skip instruction if not condition
                    if (!ConditionResult.CastFlag()) {
                        continue;
                    }
                }

                // Set variable
                if (Instruction is SetVariableInstruction SetVariableInstruction) {
                    // Evaluate value
                    if (InterpretExpression(SetVariableInstruction.Value).TryGetError(out Error ValueError, out Thingie Value)) {
                        return ValueError;
                    }
                    // Set variable to value
                    Variables[SetVariableInstruction.VariableName] = Value;
                }
                // Label
                else if (Instruction is LabelInstruction) {
                    // Pass
                }
                // Goto line
                else if (Instruction is GotoLineInstruction GotoLineInstruction) {
                    // Get target index
                    int TargetIndex;
                    // Use precalculated index
                    if (GotoLineInstruction.InstructionIndex is not null) {
                        TargetIndex = GotoLineInstruction.InstructionIndex.Value;
                    }
                    // Check for goto end of program
                    else if (GotoLineInstruction.LineNumber > ParseResult.MaximumLine) {
                        break;
                    }
                    // Get index of first instruction on line
                    else if (!ParseResult.LineIndexes.TryGetValue(GotoLineInstruction.LineNumber, out TargetIndex)) {
                        return new Error($"{Instruction.Location.Line}: invalid line");
                    }
                    // Go to index
                    Index = TargetIndex;
                    Index--;
                }
                // Goto label
                else if (Instruction is GotoLabelInstruction GotoLabelInstruction) {
                    // Get target index
                    int TargetIndex;
                    // Use precalculated index
                    if (GotoLabelInstruction.InstructionIndex is not null) {
                        TargetIndex = GotoLabelInstruction.InstructionIndex.Value;
                    }
                    // Get index of label
                    else if (!ParseResult.LabelIndexes.TryGetValue(GotoLabelInstruction.LabelName, out TargetIndex)) {
                        // Go to external label
                        if (GotoExternalLabel(GotoLabelInstruction.Location, GotoLabelInstruction.LabelName).TryGetError(out Error GotoExternalLabelError)) {
                            return GotoExternalLabelError;
                        }
                        // Next instruction
                        continue;
                    }
                    // Set index of goto label
                    GotoLabelIndexes[GotoLabelInstruction.LabelName] = Index;
                    // Go to index
                    Index = TargetIndex;
                    Index--;
                }
                // Goto goto
                else if (Instruction is GotoGotoLabelInstruction GotoGotoLabelInstruction) {
                    // Get index of goto label
                    if (!GotoLabelIndexes.TryGetValue(GotoGotoLabelInstruction.LabelName, out int TargetIndex)) {
                        return new Error($"{Instruction.Location.Line}: no entry for goto label: '{GotoGotoLabelInstruction.LabelName}'");
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
                return Variables.GetValueOrDefault(GetVariableExpression.VariableName);
            }
            // Unary
            else if (Expression is UnaryExpression UnaryExpression) {
                // Evaluate expression
                if (InterpretExpression(UnaryExpression.Expression).TryGetError(out Error ExpressionError, out Thingie Value)) {
                    return ExpressionError;
                }

                // Plus
                if (UnaryExpression.Operator is UnaryOperator.Plus) {
                    return Thingie.Plus(UnaryExpression.Location, Value);
                }
                // Minus
                else if (UnaryExpression.Operator is UnaryOperator.Minus) {
                    return Thingie.Minus(UnaryExpression.Location, Value);
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
                    return Thingie.Add(BinaryExpression.Location, Value1, Value2);
                }
                // Subtract
                else if (BinaryExpression.Operator is BinaryOperator.Subtract) {
                    return Thingie.Subtract(BinaryExpression.Location, Value1, Value2);
                }
                // Multiply
                else if (BinaryExpression.Operator is BinaryOperator.Multiply) {
                    return Thingie.Multiply(BinaryExpression.Location, Value1, Value2);
                }
                // Divide
                else if (BinaryExpression.Operator is BinaryOperator.Divide) {
                    return Thingie.Divide(BinaryExpression.Location, Value1, Value2);
                }
                // Modulo
                else if (BinaryExpression.Operator is BinaryOperator.Modulo) {
                    return Thingie.Modulo(BinaryExpression.Location, Value1, Value2);
                }
                // Exponentiate
                else if (BinaryExpression.Operator is BinaryOperator.Exponentiate) {
                    return Thingie.Exponentiate(BinaryExpression.Location, Value1, Value2);
                }
                // Equals
                else if (BinaryExpression.Operator is BinaryOperator.Equals) {
                    return Thingie.Equals(BinaryExpression.Location, Value1, Value2);
                }
                // Not equals
                else if (BinaryExpression.Operator is BinaryOperator.NotEquals) {
                    return Thingie.NotEquals(BinaryExpression.Location, Value1, Value2);
                }
                // Greater than
                else if (BinaryExpression.Operator is BinaryOperator.GreaterThan) {
                    return Thingie.GreaterThan(BinaryExpression.Location, Value1, Value2);
                }
                // Less than
                else if (BinaryExpression.Operator is BinaryOperator.LessThan) {
                    return Thingie.LessThan(BinaryExpression.Location, Value1, Value2);
                }
                // Greater than or equal to
                else if (BinaryExpression.Operator is BinaryOperator.GreaterThanOrEqualTo) {
                    return Thingie.GreaterThanOrEqualTo(BinaryExpression.Location, Value1, Value2);
                }
                // Less than or equal to
                else if (BinaryExpression.Operator is BinaryOperator.LessThanOrEqualTo) {
                    return Thingie.LessThanOrEqualTo(BinaryExpression.Location, Value1, Value2);
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
    public Thingie GetVariable(string VariableName) {
        lock (Lock) {
            return Variables.GetValueOrDefault(VariableName);
        }
    }
    public Dictionary<string, Thingie> GetVariables() {
        lock (Lock) {
            return Variables.ToDictionary();
        }
    }
    public void SetVariable(string VariableName, Thingie Value) {
        lock (Lock) {
            if (Value.Type is ThingieType.Nothing) {
                Variables.Remove(VariableName);
            }
            else {
                Variables[VariableName] = Value;
            }
        }
    }
    public int GetGotoLabelIndex(string LabelName) {
        lock (Lock) {
            return GotoLabelIndexes.GetValueOrDefault(LabelName);
        }
    }
    public Dictionary<string, int> GetGotoLabelIndexes() {
        lock (Lock) {
            return GotoLabelIndexes.ToDictionary();
        }
    }
    public void SetGotoLabelIndex(string LabelName, int Value) {
        lock (Lock) {
            if (Value < 0) {
                GotoLabelIndexes.Remove(LabelName);
            }
            else {
                GotoLabelIndexes[LabelName] = Value;
            }
        }
    }
    public Delegate? GetExternalLabel(string LabelName) {
        lock (Lock) {
            return ExternalLabels.GetValueOrDefault(LabelName);
        }
    }
    public Dictionary<string, Action<Actor>> GetExternalLabels() {
        lock (Lock) {
            return ExternalLabels.ToDictionary();
        }
    }
    public void SetExternalLabel(string LabelName, Action<Actor>? Value) {
        lock (Lock) {
            if (Value is null) {
                ExternalLabels.Remove(LabelName);
            }
            else {
                ExternalLabels[LabelName] = Value;
            }
        }
    }
    public Result GotoExternalLabel(SourceLocation Location, string LabelName) {
        // Get external label
        if (ExternalLabels.TryGetValue(LabelName, out Action<Actor>? ExternalLabel)) {
            // Call external label
            try {
                ExternalLabel(this);
                return Result.Success;
            }
            // Return exceptions as errors
            catch (Exception Ex) {
                return new Error($"{Location.Line}: '{Ex.GetType()}': '{Ex.Message}'");
            }
        }
        // Invalid
        else {
            return new Error($"{Location.Line}: invalid label");
        }
    }
    public void IncludeBundle(Bundle Bundle) {
        HashSet<Bundle> IncludedBundles = [];
        Queue<Bundle> PendingBundles = [];

        PendingBundles.Enqueue(Bundle);

        while (PendingBundles.TryDequeue(out Bundle? CurrentBundle)) {
            // Skip cyclic dependencies
            if (!IncludedBundles.Add(CurrentBundle)) {
                continue;
            }
            // Add external labels
            foreach (KeyValuePair<string, Action<Actor>> ExternalLabel in CurrentBundle.ExternalLabels) {
                ExternalLabels[ExternalLabel.Key] = ExternalLabel.Value;
            }
            // Add dependencies
            foreach (Bundle Dependency in CurrentBundle.Dependencies) {
                PendingBundles.Enqueue(Dependency);
            }
        }
    }
}