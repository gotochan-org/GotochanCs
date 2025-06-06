using System.Runtime.InteropServices;
using ResultZero;

namespace GotochanCs;

public static class Parser {
    public static Result<ParseResult> Parse(string Source) {
        if (Lexer.Lex(Source).TryGetError(out Error LexError, out LexResult? LexResult)) {
            return LexError;
        }
        return Parse(LexResult);
    }
    public static Result<ParseResult> Parse(LexResult LexResult) {
        List<Instruction> Instructions = [];
        Dictionary<int, int> LineIndexes = [];
        Dictionary<string, int> LabelIndexes = [];

        int CurrentInstructionIndex = 0;

        // Get tokens as span
        ReadOnlySpan<Token> TokensSpan = CollectionsMarshal.AsSpan(LexResult.Tokens);

        // Split instructions at end of instruction token
        for (int Index = 0; Index < TokensSpan.Length; Index++) {
            Token Next = TokensSpan[Index];

            bool EndOfInstruction = false;
            // End of instruction
            if (Next.Type is TokenType.EndOfInstruction) {
                EndOfInstruction = true;
            }
            // Last instruction
            else if (Index + 1 >= TokensSpan.Length) {
                Index++;
                EndOfInstruction = true;
            }

            // End of instruction
            if (EndOfInstruction) {
                // Get span of tokens in instruction
                ReadOnlySpan<Token> InstructionTokens = TokensSpan[CurrentInstructionIndex..Index];

                // Trim end of instruction tokens
                while (!InstructionTokens.IsEmpty && InstructionTokens[0].Type is TokenType.EndOfInstruction) {
                    InstructionTokens = InstructionTokens[1..];
                }
                while (!InstructionTokens.IsEmpty && InstructionTokens[^1].Type is TokenType.EndOfInstruction) {
                    InstructionTokens = InstructionTokens[..^1];
                }

                // Ensure any tokens
                if (!InstructionTokens.IsEmpty) {
                    // Parse tokens as instruction
                    if (ParseInstruction(InstructionTokens).TryGetError(out Error InstructionError, out Instruction? Instruction)) {
                        return InstructionError;
                    }
                    int InstructionIndex = Instructions.Count;
                    Instructions.Add(Instruction);

                    // Track line indexes
                    LineIndexes.TryAdd(Instruction.Location.Line, InstructionIndex);

                    // Fill in empty line indexes
                    int CurrentFillInLine = Instruction.Location.Line - 1;
                    while (CurrentFillInLine >= 0 && !LineIndexes.ContainsKey(CurrentFillInLine)) {
                        LineIndexes.TryAdd(CurrentFillInLine, InstructionIndex);
                        CurrentFillInLine--;
                    }

                    // Track label indexes
                    if (Instruction is LabelInstruction LabelInstruction) {
                        if (!LabelIndexes.TryAdd(LabelInstruction.Label, InstructionIndex)) {
                            return new Error($"{LabelInstruction.Location.Line}: duplicate label");
                        }
                    }
                }

                // Start instruction at next token
                CurrentInstructionIndex = Index + 1;
            }
        }

        // Finish
        return new ParseResult() {
            Source = LexResult.Source,
            Instructions = Instructions,
            LineIndexes = LineIndexes,
            LabelIndexes = LabelIndexes,
        };
    }
    public static Result<Instruction> ParseInstruction(scoped ReadOnlySpan<Token> Tokens) {
        // Ensure any token present
        if (Tokens.Length <= 0) {
            throw new ArgumentException("no tokens for instruction", nameof(Tokens));
        }

        // Condition
        Expression? Condition = null;
        for (int Index = 0; Index < Tokens.Length; Index++) {
            Token Next = Tokens[Index];

            // If
            if (Next.Type is TokenType.If) {
                // Ensure condition present
                if (Tokens.Length < Index + 1) {
                    return new Error($"{Next.Location.Line}: expected condition");
                }

                // Parse condition
                if (ParseExpression(Tokens[(Index + 1)..]).TryGetError(out Error ExpressionError, out Condition)) {
                    return ExpressionError;
                }

                // Remove condition tokens
                Tokens = Tokens[..Index];
                break;
            }
        }

        // Identifier
        if (Tokens.Length > 0 && Tokens[0].Type is TokenType.Identifier) {
            // Set
            if (Tokens.Length > 1 && Tokens[1].Type is TokenType.SetOperator) {
                // Ensure value present
                if (Tokens.Length < 2) {
                    return new Error($"{Tokens[1].Location.Line}: expected value");
                }

                // Parse value
                if (ParseExpression(Tokens[2..]).TryGetError(out Error ExpressionError, out Expression? Value)) {
                    return ExpressionError;
                }

                // Compound operator
                if (Tokens[1].Value is not "=") {
                    // Get operator
                    BinaryOperator CompoundOperator = Tokens[1].Value switch {
                        "+=" => BinaryOperator.Add,
                        "-=" => BinaryOperator.Subtract,
                        "*=" => BinaryOperator.Multiply,
                        "/=" => BinaryOperator.Divide,
                        "%=" => BinaryOperator.Modulo,
                        "^=" => BinaryOperator.Exponentiate,
                        _ => throw new NotImplementedException($"{Tokens[1].Location.Line}: unhandled operator: '{Tokens[1].Value}'")
                    };

                    // Wrap expression
                    Value = new BinaryExpression() {
                        Location = Value.Location,
                        Operator = CompoundOperator,
                        Expression1 = new GetVariableExpression() {
                            Location = Tokens[0].Location,
                            TargetVariable = Tokens[0].Value,
                        },
                        Expression2 = Value,
                    };
                }

                // Create instruction
                return new SetVariableInstruction() {
                    Location = Tokens[0].Location,
                    Condition = Condition,
                    TargetVariable = Tokens[0].Value,
                    Expression = Value,
                };
            }
            // Invalid
            else {
                return new Error($"{Tokens[0].Location.Line}: unexpected identifier");
            }
        }
        // Label
        else if (Tokens.Length > 0 && Tokens[0].Type is TokenType.Label) {
            // Identifier
            if (Tokens.Length > 1 && Tokens[1].Type is TokenType.Identifier) {
                // Ensure no tokens remaining
                if (Tokens.Length > 2) {
                    return new Error($"{Tokens[2].Location.Line}: unexpected token");
                }

                // Create instruction
                return new LabelInstruction() {
                    Location = Tokens[0].Location,
                    Condition = Condition,
                    Label = Tokens[1].Value,
                };
            }
            // Invalid
            else {
                return new Error($"{Tokens[0].Location.Line}: expected label identifier");
            }
        }
        // Goto
        else if (Tokens.Length > 0 && Tokens[0].Type is TokenType.Goto) {
            // Goto
            if (Tokens.Length > 1 && Tokens[1].Type is TokenType.Goto) {
                // Identifier
                if (Tokens.Length > 2 && Tokens[2].Type is TokenType.Identifier) {
                    // Ensure no tokens remaining
                    if (Tokens.Length > 3) {
                        return new Error($"{Tokens[3].Location.Line}: unexpected token");
                    }

                    // Create instruction
                    return new GotoGotoLabelInstruction() {
                        Location = Tokens[0].Location,
                        Condition = Condition,
                        TargetLabel = Tokens[2].Value,
                    };
                }
                // Invalid
                else {
                    return new Error($"{Tokens[1].Location.Line}: expected goto goto target");
                }
            }
            // Identifier
            else if (Tokens.Length > 1 && Tokens[1].Type is TokenType.Identifier) {
                // Ensure no tokens remaining
                if (Tokens.Length > 2) {
                    return new Error($"{Tokens[2].Location.Line}: unexpected token");
                }

                // Create instruction
                return new GotoLabelInstruction() {
                    Location = Tokens[0].Location,
                    Condition = Condition,
                    TargetLabel = Tokens[1].Value,
                };
            }
            // Number
            else if (Tokens.Length > 1 && Tokens[1].Type is TokenType.Number) {
                // Ensure no tokens remaining
                if (Tokens.Length > 2) {
                    return new Error($"{Tokens[2].Location.Line}: unexpected token");
                }

                // Parse line number
                if (!int.TryParse(Tokens[1].Value, out int TargetLine)) {
                    return new Error($"{Tokens[1].Location.Line}: invalid line number");
                }

                // Create instruction
                return new GotoLineInstruction() {
                    Location = Tokens[0].Location,
                    Condition = Condition,
                    TargetLine = TargetLine,
                };
            }
            // Operator
            else if (Tokens.Length > 1 && Tokens[1].Type is TokenType.Operator) {
                // Plus / Minus
                if (Tokens[1].Value is "+" or "-") {
                    // Number
                    if (Tokens.Length > 2 && Tokens[2].Type is TokenType.Number) {
                        // Ensure no tokens remaining
                        if (Tokens.Length > 3) {
                            return new Error($"{Tokens[3].Location.Line}: unexpected token");
                        }

                        // Parse line number offset
                        if (!int.TryParse(Tokens[2].Value, out int TargetLineOffset)) {
                            return new Error($"{Tokens[2].Location.Line}: invalid line number offset");
                        }

                        // Apply sign
                        if (Tokens[1].Value is "-") {
                            TargetLineOffset *= -1;
                        }

                        // Offset from current line
                        int TargetLine = Tokens[1].Location.Line + TargetLineOffset;

                        // Create instruction
                        return new GotoLineInstruction() {
                            Location = Tokens[0].Location,
                            Condition = Condition,
                            TargetLine = TargetLine,
                        };
                    }
                    // Invalid
                    else {
                        return new Error($"{Tokens[1].Location.Line}: unexpected operator");
                    }
                }
                // Invalid
                else {
                    return new Error($"{Tokens[1].Location.Line}: unexpected operator");
                }
            }
            // Invalid
            else {
                return new Error($"{Tokens[0].Location.Line}: expected goto target");
            }
        }

        // Invalid
        return new Error($"{Tokens[0].Location.Line}: invalid instruction");
    }
    public static Result<Expression> ParseExpression(scoped ReadOnlySpan<Token> Tokens) {
        // Ensure any token present
        if (Tokens.Length <= 0) {
            throw new ArgumentException("no tokens for expression", nameof(Tokens));
        }

        // Value
        if (Tokens.Length > 0 && Tokens[0].Type is TokenType.Identifier or TokenType.Nothing or TokenType.Flag or TokenType.Number or TokenType.String) {
            // Operator
            if (Tokens.Length > 1 && Tokens[1].Type is TokenType.Operator) {
                // Value
                if (Tokens.Length > 2 && Tokens[2].Type is TokenType.Identifier or TokenType.Nothing or TokenType.Flag or TokenType.Number or TokenType.String) {
                    // Ensure no tokens remaining
                    if (Tokens.Length > 3) {
                        return new Error($"{Tokens[3].Location.Line}: unexpected token");
                    }

                    // Get binary operator
                    BinaryOperator BinaryOperator = Tokens[1].Value switch {
                        "+" => BinaryOperator.Add,
                        "-" => BinaryOperator.Subtract,
                        "*" => BinaryOperator.Multiply,
                        "/" => BinaryOperator.Divide,
                        "%" => BinaryOperator.Modulo,
                        "^" => BinaryOperator.Exponentiate,

                        "==" => BinaryOperator.Equals,
                        "!=" => BinaryOperator.NotEquals,
                        ">" => BinaryOperator.GreaterThan,
                        "<" => BinaryOperator.LessThan,
                        ">=" => BinaryOperator.GreaterThanOrEqualTo,
                        "<=" => BinaryOperator.LessThanOrEqualTo,
                        _ => throw new NotImplementedException($"{Tokens[1].Location.Line}: unhandled binary operator: '{Tokens[1].Value}'")
                    };

                    // Get left value
                    if (ParseExpression([Tokens[0]]).TryGetError(out Error Value1Error, out Expression? Value1)) {
                        return Value1Error;
                    }
                    // Get right value
                    if (ParseExpression([Tokens[2]]).TryGetError(out Error Value2Error, out Expression? Value2)) {
                        return Value2Error;
                    }

                    // Create expression
                    return new BinaryExpression() {
                        Location = Tokens[0].Location,
                        Operator = BinaryOperator,
                        Expression1 = Value1,
                        Expression2 = Value2,
                    };
                }
                // Invalid
                else {
                    return new Error($"{Tokens[1].Location.Line}: unexpected operator");
                }
            }
            // Value
            else {
                // Ensure no tokens remaining
                if (Tokens.Length > 1) {
                    return new Error($"{Tokens[1].Location.Line}: unexpected token");
                }

                // Nothing
                if (Tokens[0].Type is TokenType.Nothing) {
                    return new ConstantExpression() {
                        Location = Tokens[0].Location,
                        Value = Thingie.Nothing(),
                    };
                }
                // Flag
                else if (Tokens[0].Type is TokenType.Flag && Tokens[0].Value is "yes") {
                    return new ConstantExpression() {
                        Location = Tokens[0].Location,
                        Value = Thingie.Flag(true),
                    };
                }
                else if (Tokens[0].Type is TokenType.Flag && Tokens[0].Value is "no") {
                    return new ConstantExpression() {
                        Location = Tokens[0].Location,
                        Value = Thingie.Flag(false),
                    };
                }
                // String
                else if (Tokens[0].Type is TokenType.String) {
                    return new ConstantExpression() {
                        Location = Tokens[0].Location,
                        Value = Thingie.String(Tokens[0].Value),
                    };
                }
                // Number
                else if (Tokens[0].Type is TokenType.Number) {
                    if (!double.TryParse(Tokens[0].Value, out double Number)) {
                        return new Error($"{Tokens[0].Location.Line}: invalid number");
                    }
                    return new ConstantExpression() {
                        Location = Tokens[0].Location,
                        Value = Thingie.Number(Number),
                    };
                }
                // Get Variable
                else if (Tokens[0].Type is TokenType.Identifier) {
                    return new GetVariableExpression() {
                        Location = Tokens[0].Location,
                        TargetVariable = Tokens[0].Value,
                    };
                }
                // Invalid
                else {
                    return new Error($"{Tokens[0].Location.Line}: unexpected token");
                }
            }
        }
        // Operator
        else if (Tokens.Length > 0 && Tokens[0].Type is TokenType.Operator) {
            // Value
            if (Tokens.Length > 1 && Tokens[1].Type is TokenType.Identifier or TokenType.Nothing or TokenType.Flag or TokenType.Number or TokenType.String) {
                // Ensure no tokens remaining
                if (Tokens.Length > 2) {
                    return new Error($"{Tokens[2].Location.Line}: unexpected token");
                }

                // Get unary operator
                UnaryOperator UnaryOperator = Tokens[0].Value switch {
                    "+" => UnaryOperator.Plus,
                    "-" => UnaryOperator.Minus,
                    _ => throw new NotImplementedException($"{Tokens[0].Location.Line}: unhandled unary operator: '{Tokens[0].Value}'")
                };

                // Get value
                if (ParseExpression([Tokens[1]]).TryGetError(out Error ValueError, out Expression? Value)) {
                    return ValueError;
                }

                // Create expression
                return new UnaryExpression() {
                    Location = Tokens[0].Location,
                    Operator = UnaryOperator,
                    Expression = Value,
                };
            }
            // Invalid
            else {
                return new Error($"{Tokens[0].Location.Line}: unexpected operator");
            }
        }

        // Invalid
        return new Error($"{Tokens[0].Location.Line}: invalid expression");
    }
    public static List<ParseAnalyzeResult> Analyze(ParseResult ParseResult, ParseAnalyses Analyses = ParseAnalyses.All) {
        List<ParseAnalyzeResult> Results = [];

        for (int Index = 0; Index < ParseResult.Instructions.Count; Index++) {
            Instruction Instruction = ParseResult.Instructions[Index];

            // Unused label
            if (Analyses.HasFlag(ParseAnalyses.UnusedLabel)) {
                // Label
                if (Instruction is LabelInstruction LabelInstruction) {
                    // Find goto label
                    bool FoundGotoLabel = false;
                    for (int Index2 = 0; Index2 < ParseResult.Instructions.Count; Index2++) {
                        Instruction Instruction2 = ParseResult.Instructions[Index2];

                        // Goto label
                        if (Instruction2 is GotoLabelInstruction GotoLabelInstruction) {
                            if (GotoLabelInstruction.TargetLabel == LabelInstruction.Label) {
                                FoundGotoLabel = true;
                                break;
                            }
                        }
                    }

                    // Label is unused
                    if (!FoundGotoLabel) {
                        // Create analysis
                        Results.Add(new ParseAnalyzeResult() {
                            Location = LabelInstruction.Location,
                            Analysis = ParseAnalyses.UnusedLabel,
                            Message = "unused label",
                        });
                    }
                }
            }
        }

        return Results;
    }
    public static void Optimize(ParseResult ParseResult, ParseOptimizations Optimizations = ParseOptimizations.All) {
        for (int Index = 0; Index < ParseResult.Instructions.Count; Index++) {
            Instruction Instruction = ParseResult.Instructions[Index];

            // Remove labels
            if (Optimizations.HasFlag(ParseOptimizations.RemoveLabels)) {
                // Label
                if (Instruction is LabelInstruction) {
                    // Remove instruction
                    ParseResult.Instructions.RemoveAt(Index);
                    Index--;
                    continue;
                }
            }
            // Calculate goto line index
            if (Optimizations.HasFlag(ParseOptimizations.CalculateGotoLineIndex)) {
                // Goto line
                if (Instruction is GotoLineInstruction GotoLineInstruction) {
                    // Get index of first instruction on line
                    if (ParseResult.LineIndexes.TryGetValue(GotoLineInstruction.TargetLine, out int TargetIndex)) {
                        // Replace with goto index
                        ParseResult.Instructions[Index] = GotoLineInstruction with { TargetIndex = TargetIndex };
                    }
                }
            }
            // Calculate goto label index
            if (Optimizations.HasFlag(ParseOptimizations.CalculateGotoLabelIndex)) {
                // Goto label
                if (Instruction is GotoLabelInstruction GotoLabelInstruction) {
                    // Get index of label
                    if (ParseResult.LabelIndexes.TryGetValue(GotoLabelInstruction.TargetLabel, out int TargetIndex)) {
                        // Replace with goto index
                        ParseResult.Instructions[Index] = GotoLabelInstruction with { TargetIndex = TargetIndex };
                    }
                }
            }
            // Remove constant conditions
            if (Optimizations.HasFlag(ParseOptimizations.RemoveConstantConditions)) {
                // Condition
                if (Instruction.Condition is not null) {
                    // Constant condition
                    if (Instruction.Condition is ConstantExpression ConstantCondition) {
                        // Constant flag condition
                        if (ConstantCondition.Value.Type is ThingieType.Flag) {
                            // Constant true condition
                            if (ConstantCondition.Value.CastFlag()) {
                                // Remove condition
                                ParseResult.Instructions[Index] = Instruction with { Condition = null };
                            }
                            // Constant false condition
                            else {
                                // Remove instruction
                                ParseResult.Instructions.RemoveAt(Index);
                                Index--;
                                continue;
                            }
                        }
                    }
                }
            }
        }
    }
}

public class ParseResult {
    public required string Source { get; init; }
    public required List<Instruction> Instructions { get; init; }
    public required Dictionary<int, int> LineIndexes { get; init; }
    public required Dictionary<string, int> LabelIndexes { get; init; }
}

public readonly record struct ParseAnalyzeResult {
    public required SourceLocation Location { get; init; }
    public required ParseAnalyses Analysis { get; init; }
    public required string Message { get; init; }
}

[Flags]
public enum ParseAnalyses : long {
    UnusedLabel = 1,

    None = 0,
    All = long.MaxValue,
}

[Flags]
public enum ParseOptimizations : long {
    RemoveLabels = 1,
    CalculateGotoLineIndex = 2,
    CalculateGotoLabelIndex = 4,
    RemoveConstantConditions = 8,

    None = 0,
    All = long.MaxValue,
}