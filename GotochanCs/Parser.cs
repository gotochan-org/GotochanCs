using System.Runtime.InteropServices;
using System.Text;
using ResultZero;

namespace GotochanCs;

public static class Parser {
    private static ReadOnlySpan<char> NewlineChars => ['\n', '\r', '\u2028', '\u2029'];

    public static Result<List<Instruction>> Parse(TextReader Source) {
        List<Instruction> Instructions = [];

        List<string> CurrentTokens = [];
        StringBuilder CurrentToken = new();
        int CurrentLine = 0;

        bool TrySubmitToken() {
            if (CurrentToken.Length <= 0) {
                return false;
            }
            CurrentTokens.Add(CurrentToken.ToString());
            CurrentToken.Clear();
            return true;
        }
        Result TrySubmitTokens() {
            TrySubmitToken();
            if (CurrentTokens.Count <= 0) {
                return Result.Success;
            }
            if (ParseInstruction(CurrentLine, CollectionsMarshal.AsSpan(CurrentTokens)).TryGetError(out Error ParseInstructionError, out Instruction? Instruction)) {
                return ParseInstructionError;
            }
            Instructions.Add(Instruction);
            CurrentTokens.Clear();
            return Result.Success;
        }

        while (true) {
            // Read next char
            if (Read(Source, ref CurrentLine) is not char Next) {
                break;
            }
            JoinCrLf(Source, Next);

            // Escape
            if (Next is '\\') {
                // Ensure escape is not within string
                if (CurrentToken.Length >= 1 && CurrentToken[0] is '~') {
                    continue;
                }

                // Read escaped char
                if (Read(Source, ref CurrentLine) is not char Escaped) {
                    return new Error($"{CurrentLine}: incomplete escape sequence");
                }

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
                CurrentToken.Append(Next);
            }
        }

        // Try submit last tokens
        if (TrySubmitTokens().TryGetError(out Error SubmitLastTokensError)) {
            return SubmitLastTokensError;
        }

        return Instructions;
    }
    public static Result<List<Instruction>> Parse(string Source) {
        return Parse(new StringReader(Source));
    }
    public static Result<Instruction> ParseInstruction(int Line, scoped ReadOnlySpan<string> Tokens) {
        if (Tokens.Length is 2) {
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
                    };
                }
                // Goto label
                else {
                    // Create instruction
                    return new GotoLabelInstruction() {
                        Line = Line,
                        TargetLabel = Token2,
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
                };
            }
            // Set variable
            else {
                Result<BinaryOperator?> SetOperator = Token2 switch {
                    "=" => (BinaryOperator?)null,
                    "+=" => BinaryOperator.Add,
                    "-=" => BinaryOperator.Subtract,
                    "*=" => BinaryOperator.Multiply,
                    "/=" => BinaryOperator.Divide,
                    "%=" => BinaryOperator.Modulo,
                    "^=" => BinaryOperator.Exponentiate,
                    _ => new Error($"{Line}: invalid operator")
                };
                if (SetOperator.IsError) {
                    return SetOperator.Error;
                }
                // Parse expression
                if (ParseExpression(Line, Tokens[2..]).TryGetError(out Error ParseExpressionError, out Expression? Expression)) {
                    return ParseExpressionError;
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
        // Invalid
        return new Error($"{Line}: invalid expression");
    }

    private static string EscapeString(string String) {
        return String;
    }
    /*private static int? FindLine(int Line, int CurrentLine, List<LineInfo> LineInfos) {
        if (Line < 0 || Line > LineInfos.Count) {

        }


        if (Line < 0 || Line > CurrentLine) {
            return null;
        }
        for (int Index = LineInfos.Count - 1; Index >= 0; Index--) {
            LineInfo LineInfo = LineInfos[Index];

            if (Instruction.Line == Line) {
                return Index;
            }
            else if (Instruction.Line > Line) {
                if (Index + 1 >= Instructions.Count) {
                    return null;
                }
                return Index + 1;
            }
        }
    }
    private static int? FindLabel(string Label, int CurrentLine, List<LineInfo> LineInfos) {

    }*/
    private static char? Read(TextReader Source, ref int CurrentLine) {
        // Read next char
        int NextAsInt = Source.Read();
        if (NextAsInt < 0) {
            return null;
        }
        char Next = (char)NextAsInt;

        // Increment line
        if (NewlineChars.Contains(Next)) {
            CurrentLine++;
        }

        // Return next char
        return Next;
    }
    private static bool JoinCrLf(TextReader Source, char FirstChar) {
        if (FirstChar is '\r') {
            if (Source.Peek() is '\n') {
                Source.Read();
                return true;
            }
        }
        return false;
    }

    //private readonly record struct LineInfo(Instruction? Instruction, string Label);
}