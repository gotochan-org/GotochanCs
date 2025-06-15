using ResultZero;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using SLE = System.Linq.Expressions;

namespace GotochanCs;

public static class Compiler {
    internal const string DelegateCreationRequiresDynamicCode = "Delegate creation requires dynamic code generation.";

    [RequiresDynamicCode(DelegateCreationRequiresDynamicCode)]
    public static Result<CompileResult> Compile(ParseResult ParseResult) {
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

            // Set variable
            if (Instruction is SetVariableInstruction SetVariableInstruction) {
                // Get or create variable SLE
                if (!VariableSles.TryGetValue(SetVariableInstruction.TargetVariable, out SLE.ParameterExpression? VariableSle)) {
                    VariableSle = SLE.Expression.Variable(typeof(Thingie), SetVariableInstruction.TargetVariable);
                    VariableSles[SetVariableInstruction.TargetVariable] = VariableSle;
                }
                // Create value SLE
                if (CompileExpression(SetVariableInstruction.Expression).TryGetError(out Error ValueError, out SLE.Expression? ValueSle)) {
                    return ValueError;
                }
                // Create assign SLE
                SLE.BinaryExpression AssignSle = SLE.Expression.Assign(VariableSle, ValueSle);
                Sles.Add(AssignSle);
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
            }
            // Goto label
            else if (Instruction is GotoLabelInstruction GotoLabelInstruction) {
                // Get label target
                SLE.LabelTarget LabelTarget = LabelTargets[GotoLabelInstruction.TargetLabel];
                // Create goto SLE
                SLE.GotoExpression GotoSle = SLE.Expression.Goto(LabelTarget);
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
        }

        // Add successful return SLE
        Sles.Add(SLE.Expression.Return(ReturnTarget, SLE.Expression.Constant(Result.Success)));
        // Add return target SLE
        Sles.Add(SLE.Expression.Label(ReturnTarget));

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

    private static Result<SLE.Expression> CompileInstruction(Instruction Instruction) {
        return new Error();
    }
    private static Result<SLE.Expression> CompileExpression(Expression Expression) {
        return new Error();
    }
}

public class CompileResult {
    public required string Source { get; init; }
    public required Func<Actor, Result> Delegate { get; init; }
}