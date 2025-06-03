using ResultZero;
using System.Runtime.InteropServices;

namespace GotochanCs;

public static class Parser {
    public static Result<ParseResult> Parse(LexResult LexResult) {
        List<Instruction> Instructions = [];
        Dictionary<int, int> LineIndexes = [];
        Dictionary<string, int> LabelIndexes = [];

        int CurrentInstructionIndex = 0;

        for (int Index = 0; Index < LexResult.Tokens.Count; Index++) {
            Token Token = LexResult.Tokens[Index];

            if (Token.Type is TokenType.EndOfInstruction) {
                ReadOnlySpan<Token> InstructionTokens = CollectionsMarshal.AsSpan(LexResult.Tokens)[CurrentInstructionIndex..Index];
                if (ParseInstruction(InstructionTokens).TryGetError(out Error InstructionError, out Instruction? Instruction)) {
                    return InstructionError;
                }
                Instructions.Add(Instruction);
                CurrentInstructionIndex = Index + 1;
            }
        }

        /*Result<bool> TrySubmitTokens() {
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

        for (int Index = 0; Index < Tokens.Length; Index++) {
            Token Next = Tokens[Index];

            // End of instruction
            if (Next.Type is TokenType.EndOfInstruction) {
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
        }*/

        // Finish
        return new ParseResult() {
            Source = LexResult.Source,
            Instructions = Instructions,
            LineIndexes = LineIndexes,
            LabelIndexes = LabelIndexes,
        };
    }
    public static Result<Instruction> ParseInstruction(scoped ReadOnlySpan<Token> Tokens) {
        /*if (Tokens.Length == 2) {
            Token Token1 = Tokens[0];
            Token Token2 = Tokens[1];

            // Goto
            if (Token1.Type is TokenType.Goto) {
                // Goto line
                if (Token2.Type is TokenType.Operator or TokenType.Number) {
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
            else if (Token1.Type is TokenType.Label) {
                // Create instruction
                return new LabelInstruction() {
                    Line = Line,
                    Name = Token2,
                };
            }
        }
        else if (Tokens.Length >= 3) {
            Token Token1 = Tokens[0];
            Token Token2 = Tokens[1];
            Token Token3 = Tokens[2];

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
        }*/
        // Invalid
        return new Error($"{Tokens[0].Location.Line}: invalid instruction");
    }
    public static Result<Expression> ParseExpression(int Line, scoped ReadOnlySpan<Token> Tokens) {
        /*if (Tokens.Length == 1) {
            Token Token1 = Tokens[0];

            // Nothing
            if (Token1.Type is TokenType.Nothing) {
                return new ConstantExpression() {
                    Line = Line,
                    Value = Thingie.Nothing(),
                };
            }
            // Flag
            else if (Token1.Type is TokenType.Flag && Token1.Value is "yes") {
                return new ConstantExpression() {
                    Line = Line,
                    Value = Thingie.Flag(true),
                };
            }
            else if (Token1.Type is TokenType.Flag && Token1.Value is "no") {
                return new ConstantExpression() {
                    Line = Line,
                    Value = Thingie.Flag(false),
                };
            }
            // String
            else if (Token1.Type is TokenType.String) {
                return new ConstantExpression() {
                    Line = Line,
                    Value = Thingie.String(Token1.Value),
                };
            }
            // Double
            else if (Token1.Type is TokenType.Number) {
                if (!double.TryParse(Token1.Value, out double Number)) {
                    return new Error($"{Line}: invalid number");
                }
                return new ConstantExpression() {
                    Line = Line,
                    Value = Thingie.Number(Number),
                };
            }
            // Get Variable
            else if (Token1.Type is TokenType.Identifier) {
                return new GetVariableExpression() {
                    Line = Line,
                    TargetVariable = Token1.Value,
                };
            }
            // Not implemented
            else {
                throw new NotImplementedException(Token1.Type.ToString());
            }
        }
        else if (Tokens.Length == 2) {
            Token Token1 = Tokens[0];
            Token Token2 = Tokens[1];

            // Parse expression
            if (ParseExpression(Line, [Token2]).TryGetError(out Error ParseExpressionError, out Expression? Expression)) {
                return ParseExpressionError;
            }

            // Get unary operator
            Result<UnaryOperator> Operator = Token1.Value switch {
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
            Token Token1 = Tokens[0];
            Token Token2 = Tokens[1];
            Token Token3 = Tokens[2];

            // Parse expressions
            if (ParseExpression(Line, [Token1]).TryGetError(out Error ParseExpression1Error, out Expression? Expression1)) {
                return ParseExpression1Error;
            }
            if (ParseExpression(Line, [Token3]).TryGetError(out Error ParseExpression2Error, out Expression? Expression2)) {
                return ParseExpression2Error;
            }

            // Get binary operator
            Result<BinaryOperator> Operator = Token2.Value switch {
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
        }*/
        // Invalid
        return new Error($"{Line}: invalid expression");
    }

    private static string EscapeString(string String) {
        return String;
    }
}

public class ParseResult {
    public required string Source { get; init; }
    public required List<Instruction> Instructions { get; init; }
    public required Dictionary<int, int> LineIndexes { get; init; }
    public required Dictionary<string, int> LabelIndexes { get; init; }
}