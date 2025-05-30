using System.Text;
using System.Runtime.InteropServices;
using ResultZero;

namespace GotochanCs;

public static class Parser {
    private static ReadOnlySpan<char> NewlineChars => ['\n', '\r', '\u2028', '\u2029'];

    public static Result<Script> Parse(string Source) {
        List<Instruction> Instructions = [];
        Dictionary<int, int> LineIndexes = [];
        Dictionary<string, int> LabelIndexes = [];

        List<string> CurrentTokens = [];
        StringBuilder CurrentToken = new();
        int CurrentLine = 1;

        bool TrySubmitToken() {
            // Ensure token was built
            if (CurrentToken.Length <= 0) {
                return false;
            }
            // Submit token to list
            CurrentTokens.Add(CurrentToken.ToString());
            CurrentToken.Clear();
            return true;
        }
        Result<bool> TrySubmitTokens() {
            // Submit last token to list
            TrySubmitToken();
            // Ensure tokens were built
            if (CurrentTokens.Count <= 0) {
                return false;
            }
            // Parse instruction from tokens
            if (ParseInstruction(CurrentLine, CollectionsMarshal.AsSpan(CurrentTokens)).TryGetError(out Error ParseInstructionError, out Instruction? Instruction)) {
                return ParseInstructionError;
            }
            // Submit instruction to list
            CurrentTokens.Clear();
            Instructions.Add(Instruction);
            int InstructionIndex = Instructions.Count - 1;
            // Track index of first instruction on each line
            LineIndexes.TryAdd(CurrentLine, InstructionIndex);
            // Track index of labels
            if (Instruction is LabelInstruction LabelInstruction) {
                if (!LabelIndexes.TryAdd(LabelInstruction.Name, InstructionIndex)) {
                    return new Error($"duplicate label: '{LabelInstruction.Name}'");
                }
            }
            return true;
        }

        for (int Index = 0; Index < Source.Length; Index++) {
            char Next = Source[Index];

            // Escape
            if (Next is '\\') {
                // Ensure escape is not within string
                if (CurrentToken.Length >= 1 && CurrentToken[0] is '~') {
                    continue;
                }

                // Read escaped char
                Index++;
                if (Index >= Source.Length) {
                    return new Error($"{CurrentLine}: incomplete escape sequence");
                }
                char Escaped = Source[Index];

                // Escape newline
                if (NewlineChars.Contains(Escaped)) {
                    // Pass (don't end instruction)
                }
                // Invalid escape
                else {
                    return new Error($"{CurrentLine}: invalid escape sequence");
                }
            }
            // End of instruction
            else if (Next is ';' || NewlineChars.Contains(Next)) {
                // Try submit tokens
                if (TrySubmitTokens().TryGetError(out Error SubmitTokensError)) {
                    return SubmitTokensError;
                }
            }
            // End of token
            else if (char.IsWhiteSpace(Next)) {
                // Try submit token
                TrySubmitToken();
            }
            // Part of token
            else {
                // Build token
                CurrentToken.Append(Next);
            }

            // Increment line
            if (NewlineChars.Contains(Next)) {
                CurrentLine++;
            }
            // Join CR LF
            if (Next is '\r' && Index + 1 < Source.Length && Source[Index + 1] is '\n') {
                Index++;
            }
        }

        // Try submit last tokens
        if (TrySubmitTokens().TryGetError(out Error SubmitLastTokensError)) {
            return SubmitLastTokensError;
        }

        // Convert goto line/label to goto index
        for (int Index = 0; Index < Instructions.Count; Index++) {
            Instruction Instruction = Instructions[Index];

            // Goto line
            if (Instruction is GotoLineInstruction GotoLineInstruction) {
                // Get index of first instruction on line
                if (!LineIndexes.TryGetValue(GotoLineInstruction.TargetLine, out int TargetIndex)) {
                    return new Error($"{Instruction.Line}: invalid label");
                }
                // Replace with goto index
                Instructions[Index] = new GotoIndexInstruction() {
                    Line = GotoLineInstruction.Line,
                    TargetIndex = TargetIndex,
                    Condition = GotoLineInstruction.Condition,
                };
            }
            // Goto label
            else if (Instruction is GotoLabelInstruction GotoLabelInstruction) {
                // Get index of label
                if (!LabelIndexes.TryGetValue(GotoLabelInstruction.TargetLabel, out int TargetIndex)) {
                    return new Error($"{Instruction.Line}: invalid label");
                }
                // Replace with goto index
                Instructions[Index] = new GotoIndexInstruction() {
                    Line = GotoLabelInstruction.Line,
                    TargetIndex = TargetIndex,
                    Condition = GotoLabelInstruction.Condition,
                };
            }
        }

        // Create script from results
        return new Script() {
            Source = Source,
            Instructions = Instructions,
            LineIndexes = LineIndexes,
            LabelIndexes = LabelIndexes,
        };
    }
    public static Result<Instruction> ParseInstruction(int Line, scoped ReadOnlySpan<string> Tokens) {
        if (Tokens.Length == 2) {
            string Token1 = Tokens[0];
            string Token2 = Tokens[1];

            // Goto
            if (Token1 is "goto") {
                // Goto line
                if (Token2[0] is '-' or '+' or (>= '0' and <= '9')) {
                    // Parse line number
                    int TargetLine = int.Parse(Token2[1..], System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowExponent);

                    // Apply offset from current line
                    if (Token2[0] is '-') {
                        TargetLine = Line - TargetLine;
                    }
                    else if (Token2[1] is '+') {
                        TargetLine = Line + TargetLine;
                    }

                    // Create instruction
                    return new GotoLineInstruction() {
                        Line = Line,
                        TargetLine = TargetLine,
                        Condition = new ConstantExpression() {
                            Line = Line,
                            Value = Thingie.Flag(true),
                        },
                    };
                }
                // Goto label
                else {
                    // Create instruction
                    return new GotoLabelInstruction() {
                        Line = Line,
                        TargetLabel = Token2,
                        Condition = new ConstantExpression() {
                            Line = Line,
                            Value = Thingie.Flag(true),
                        },
                    };
                }
            }
            // Label
            else if (Token1 is "label") {
                // Create instruction
                return new LabelInstruction() {
                    Line = Line,
                    Name = Token2,
                };
            }
        }
        else if (Tokens.Length >= 3) {
            string Token1 = Tokens[0];
            string Token2 = Tokens[1];
            string Token3 = Tokens[2];

            // Goto goto
            if (Token1 is "goto" && Token2 is "goto") {
                // Create instruction
                return new GotoGotoLabelInstruction() {
                    Line = Line,
                    TargetLabel = Token3,
                    Condition = new ConstantExpression() {
                        Line = Line,
                        Value = Thingie.Flag(true),
                    },
                };
            }
            // Set variable
            else {
                // Get compound operator
                Result<BinaryOperator?> CompoundOperator = Token2 switch {
                    "=" => (BinaryOperator?)null,
                    "+=" => BinaryOperator.Add,
                    "-=" => BinaryOperator.Subtract,
                    "*=" => BinaryOperator.Multiply,
                    "/=" => BinaryOperator.Divide,
                    "%=" => BinaryOperator.Modulo,
                    "^=" => BinaryOperator.Exponentiate,
                    _ => new Error($"{Line}: invalid set operator")
                };
                if (CompoundOperator.IsError) {
                    return CompoundOperator.Error;
                }
                // Parse expression
                if (ParseExpression(Line, Tokens[2..]).TryGetError(out Error ParseExpressionError, out Expression? Expression)) {
                    return ParseExpressionError;
                }
                // Apply compound operator
                if (CompoundOperator.Value is not null) {
                    Expression = new BinaryExpression() {
                        Line = Line,
                        Operator = CompoundOperator.Value.Value,
                        Expression1 = new GetVariableExpression() {
                            Line = Line,
                            TargetVariable = Token1,
                        },
                        Expression2 = Expression,
                    };
                }
                // Create instruction
                return new SetVariableInstruction() {
                    Line = Line,
                    TargetVariable = Token1,
                    Expression = Expression,
                };
            }
        }
        // Invalid
        return new Error($"{Line}: invalid instruction");
    }
    public static Result<Expression> ParseExpression(int Line, scoped ReadOnlySpan<string> Tokens) {
        if (Tokens.Length == 1) {
            string Token1 = Tokens[0];

            // Nothing
            if (Token1 is "nothing") {
                return new ConstantExpression() {
                    Line = Line,
                    Value = Thingie.Nothing(),
                };
            }
            // Flag
            else if (Token1 is "yes") {
                return new ConstantExpression() {
                    Line = Line,
                    Value = Thingie.Flag(true),
                };
            }
            else if (Token1 is "no") {
                return new ConstantExpression() {
                    Line = Line,
                    Value = Thingie.Flag(false),
                };
            }
            // String
            else if (Token1.StartsWith('~')) {
                return new ConstantExpression() {
                    Line = Line,
                    Value = Thingie.String(EscapeString(Token1[1..].Replace('~', ' '))),
                };
            }
            // Double
            else if (double.TryParse(Token1, out double Double)) {
                return new ConstantExpression() {
                    Line = Line,
                    Value = Thingie.Number(Double),
                };
            }
            // Get Variable
            else {
                return new GetVariableExpression() {
                    Line = Line,
                    TargetVariable = Token1,
                };
            }
        }
        else if (Tokens.Length == 2) {
            string Token1 = Tokens[0];
            string Token2 = Tokens[1];

            // Parse expression
            if (ParseExpression(Line, [Token2]).TryGetError(out Error ParseExpressionError, out Expression? Expression)) {
                return ParseExpressionError;
            }

            // Get unary operator
            Result<UnaryOperator> Operator = Token1 switch {
                "+" => UnaryOperator.Plus,
                "-" => UnaryOperator.Minus,
                _ => new Error($"invalid unary operator: '{Token1}'")
            };
            if (Operator.IsError) {
                return Operator.Error;
            }

            // Unary
            return new UnaryExpression() {
                Line = Line,
                Operator = Operator.Value,
                Expression = Expression,
            };
        }
        else if (Tokens.Length == 3) {
            string Token1 = Tokens[0];
            string Token2 = Tokens[1];
            string Token3 = Tokens[2];

            // Parse expressions
            if (ParseExpression(Line, [Token1]).TryGetError(out Error ParseExpression1Error, out Expression? Expression1)) {
                return ParseExpression1Error;
            }
            if (ParseExpression(Line, [Token3]).TryGetError(out Error ParseExpression2Error, out Expression? Expression2)) {
                return ParseExpression2Error;
            }

            // Get binary operator
            Result<BinaryOperator> Operator = Token2 switch {
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
                _ => new Error($"invalid binary operator: '{Token2}'")
            };
            if (Operator.IsError) {
                return Operator.Error;
            }

            // Binary
            return new BinaryExpression() {
                Line = Line,
                Operator = Operator.Value,
                Expression1 = Expression1,
                Expression2 = Expression2,
            };
        }
        // Invalid
        return new Error($"{Line}: invalid expression");
    }

    private static string EscapeString(string String) {
        return String;
    }
}

public class Script {
    public required string Source { get; init; }
    public required List<Instruction> Instructions { get; init; }
    public required Dictionary<int, int> LineIndexes { get; init; }
    public required Dictionary<string, int> LabelIndexes { get; init; }
}