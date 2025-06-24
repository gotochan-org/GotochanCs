#if !NET9_0_OR_GREATER
using Lock = object;
#endif

using ResultZero;
using System.Runtime.InteropServices;

namespace GotochanCs;

/// <summary>
/// A runtime state for Gotochan. Actors use collaborative multithreading using a lock.
/// </summary>
public class Actor {
    /// <summary>
    /// A global actor instance.
    /// </summary>
    public static Actor Shared { get; } = new();

    /// <summary>
    /// A lock used to prevent multithreading.
    /// </summary>
    public Lock Lock { get; } = new();

    /// <summary>
    /// The index of the last executed goto instruction to each label.
    /// </summary>
    private Dictionary<string, int> GotoLabelIndexes { get; } = [];
    /// <summary>
    /// The value of each global variable.
    /// </summary>
    private Dictionary<string, Thingie> Variables { get; } = [];
    /// <summary>
    /// The external delegates accessible as labels.
    /// </summary>
    private Dictionary<string, Action<Actor>> ExternalLabels { get; } = [];

    /// <summary>
    /// Constructs an empty actor.
    /// </summary>
    public Actor() {
    }
    /// <summary>
    /// Constructs an actor with the given bundles included.
    /// </summary>
    public Actor(params IEnumerable<Bundle> Bundles)
        : this() {
        foreach (Bundle Bundle in Bundles) {
            IncludeBundle(Bundle);
        }
    }
    /// <summary>
    /// Evaluates each instruction, returning an error on failure.
    /// </summary>
    /// <param name="CancelToken">
    /// Checked before each instruction is evaluated.
    /// </param>
    /// <exception cref="OperationCanceledException"/>
    public Result Interpret(ParseResult ParseResult, CancellationToken CancelToken = default) {
        lock (Lock) {
            // Get instructions as span
            ReadOnlySpan<Instruction> InstructionsSpan = CollectionsMarshal.AsSpan(ParseResult.Instructions);

            // Interpret each instruction
            for (int Index = 0; Index < InstructionsSpan.Length; Index++) {
                Instruction Instruction = InstructionsSpan[Index];

                // Check cancel token
                CancelToken.ThrowIfCancellationRequested();

                // Condition
                if (Instruction.Condition is not null) {
                    // Evaluate condition
                    if (InterpretExpression(Instruction.Condition).TryGetError(out Error ConditionError, out Thingie ConditionResult)) {
                        return ConditionError;
                    }
                    // Ensure condition is flag
                    if (ConditionResult.Type is not ThingieType.Flag) {
                        return new Error($"{Instruction.Condition.Location.Line}: condition must be flag, not '{ConditionResult.Type}'");
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
                    SetVariable(SetVariableInstruction.VariableName, Value);
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
                    SetGotoLabelIndex(GotoLabelInstruction.LabelName, Index);
                    // Go to index
                    Index = TargetIndex;
                    Index--;
                }
                // Goto goto
                else if (Instruction is GotoGotoLabelInstruction GotoGotoLabelInstruction) {
                    // Get index of goto label
                    int TargetIndex = GetGotoLabelIndex(GotoGotoLabelInstruction.LabelName);
                    if (TargetIndex < 0) {
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
    /// <summary>
    /// Evaluates the expression.
    /// </summary>
    public Result<Thingie> InterpretExpression(Expression Expression) {
        lock (Lock) {
            // Constant
            if (Expression is ConstantExpression ConstantExpression) {
                return ConstantExpression.Value;
            }
            // Get variable
            else if (Expression is GetVariableExpression GetVariableExpression) {
                return GetVariable(GetVariableExpression.VariableName);
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
                // Not
                else if (UnaryExpression.Operator is UnaryOperator.Not) {
                    return Thingie.Not(UnaryExpression.Location, Value);
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
    /// <summary>
    /// Returns the value of the given variable, or <see cref="Thingie.Nothing()"/>.
    /// </summary>
    public Thingie GetVariable(string VariableName) {
        lock (Lock) {
            return Variables.GetValueOrDefault(VariableName);
        }
    }
    /// <summary>
    /// Returns a snapshot of the value of each variable.
    /// </summary>
    public Dictionary<string, Thingie> GetVariables() {
        lock (Lock) {
            return Variables.ToDictionary();
        }
    }
    /// <summary>
    /// Sets the value of the given variable.
    /// </summary>
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
    /// <summary>
    /// Returns the index of the last executed goto instruction to the given label, or -1.
    /// </summary>
    public int GetGotoLabelIndex(string LabelName) {
        lock (Lock) {
            return GotoLabelIndexes.GetValueOrDefault(LabelName, -1);
        }
    }
    /// <summary>
    /// Returns a snapshot of the index of the last executed goto instruction for each label.
    /// </summary>
    public Dictionary<string, int> GetGotoLabelIndexes() {
        lock (Lock) {
            return GotoLabelIndexes.ToDictionary();
        }
    }
    /// <summary>
    /// Sets the index of the last executed goto instruction to the given label.
    /// </summary>
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
    /// <summary>
    /// Returns the given external delegate accessible as a label, or <see langword="null"/>.
    /// </summary>
    public Delegate? GetExternalLabel(string LabelName) {
        lock (Lock) {
            return ExternalLabels.GetValueOrDefault(LabelName);
        }
    }
    /// <summary>
    /// Returns a snapshot of each external delegate accessible as a label.
    /// </summary>
    public Dictionary<string, Action<Actor>> GetExternalLabels() {
        lock (Lock) {
            return ExternalLabels.ToDictionary();
        }
    }
    /// <summary>
    /// Sets the given external delegate accessible as a label.
    /// </summary>
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
    /// <summary>
    /// Calls the external delegate accessible as a label.
    /// </summary>
    public Result GotoExternalLabel(SourceLocation Location, string LabelName) {
        // Get external label
        if (GetExternalLabel(LabelName) is Action<Actor> ExternalLabel) {
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
    /// <summary>
    /// Applies the contents of the given bundle and its dependencies.
    /// </summary>
    public void IncludeBundle(Bundle Bundle) {
        HashSet<Bundle> IncludedBundles = [];

        void IncludeBundleRecursive(Bundle CurrentBundle) {
            // Skip cyclic dependencies
            if (!IncludedBundles.Add(CurrentBundle)) {
                return;
            }

            // Include dependencies first
            foreach (Bundle Dependency in CurrentBundle.Dependencies) {
                IncludeBundleRecursive(Dependency);
            }

            // Set external labels
            foreach (KeyValuePair<string, Action<Actor>> ExternalLabel in CurrentBundle.ExternalLabels) {
                SetExternalLabel(ExternalLabel.Key, ExternalLabel.Value);
            }
        }

        IncludeBundleRecursive(Bundle);
    }
}