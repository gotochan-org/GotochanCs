using System.Runtime.InteropServices;
using System.Text;
using ResultZero;

namespace GotochanCs;

public static class Parser {
    private static ReadOnlySpan<char> NewlineChars => ['\n', '\r', '\u2028', '\u2029'];

    public static Result<List<Instruction>> Parse(TextReader Source) {
        List<Instruction> Instructions = [];

        List<string> CurrentWords = [];
        StringBuilder CurrentWord = new();
        int CurrentLine = 0;

        bool TrySubmitWord() {
            if (CurrentWord.Length <= 0) {
                return false;
            }
            CurrentWords.Add(CurrentWord.ToString());
            CurrentWord.Clear();
            return true;
        }
        Result TrySubmitWords() {
            TrySubmitWord();
            if (CurrentWords.Count <= 0) {
                return Result.Success;
            }
            if (ParseInstruction(CurrentLine, CollectionsMarshal.AsSpan(CurrentWords)).TryGetError(out Error ParseInstructionError, out Instruction? Instruction)) {
                return ParseInstructionError;
            }
            Instructions.Add(Instruction);
            CurrentWords.Clear();
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
                if (CurrentWord.Length >= 1 && CurrentWord[0] is '~') {
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
                // Try submit words
                if (TrySubmitWords().TryGetError(out Error SubmitWordsError)) {
                    return SubmitWordsError;
                }
            }
            // End of word
            else if (char.IsWhiteSpace(Next)) {
                // Try submit word
                TrySubmitWord();
            }
            // Part of word
            else {
                CurrentWord.Append(Next);
            }
        }

        // Try submit last words
        if (TrySubmitWords().TryGetError(out Error SubmitLastWordsError)) {
            return SubmitLastWordsError;
        }

        return Instructions;
    }
    public static Result<List<Instruction>> Parse(string Source) {
        return Parse(new StringReader(Source));
    }
    public static Result<Instruction> ParseInstruction(int Line, scoped ReadOnlySpan<string> Words) {
        if (Words.Length is 2) {
            string Word1 = Words[0];
            string Word2 = Words[1];

            // Goto
            if (Word1 is "goto") {
                // Goto line
                if (Word2[0] is '-' or '+' or (>= '0' and <= '9')) {
                    // Parse line number
                    int TargetLine = int.Parse(Word2[1..], System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowExponent);

                    // Apply offset from current line
                    if (Word2[0] is '-') {
                        TargetLine = Line - TargetLine;
                    }
                    else if (Word2[1] is '+') {
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
                        TargetLabel = Word2,
                    };
                }
            }
            // Label
            else if (Word1 is "label") {
                // Create instruction
                return new LabelInstruction() {
                    Line = Line,
                    Name = Word2,
                };
            }
        }
        else if (Words.Length >= 3) {
            string Word1 = Words[0];
            string Word2 = Words[1];
            string Word3 = Words[2];

            // Goto goto
            if (Word1 is "goto" && Word2 is "goto") {
                // Create instruction
                return new GotoGotoLabelInstruction() {
                    Line = Line,
                    TargetLabel = Word3,
                };
            }
            // Set variable
            else {
                Result<BinaryOperator?> SetOperator = Word2 switch {
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
                if (ParseExpression(Line, Words[2..]).TryGetError(out Error ParseExpressionError, out Expression? Expression)) {
                    return ParseExpressionError;
                }
                // Create instruction
                return new SetVariableInstruction() {
                    Line = Line,
                    TargetVariable = Word1,
                    Expression = Expression,
                };
            }
        }
        // Invalid
        return new Error($"{Line}: invalid instruction");
    }
    public static Result<Expression> ParseExpression(int Line, scoped ReadOnlySpan<string> Words) {
        if (Words.Length == 1) {
            string Word1 = Words[0];

            // Nothing
            if (Word1 is "nothing") {
                return new ConstantExpression() {
                    Line = Line,
                    Value = Thingie.Nothing(),
                };
            }
            // Flag
            else if (Word1 is "yes") {
                return new ConstantExpression() {
                    Line = Line,
                    Value = Thingie.Flag(true),
                };
            }
            else if (Word1 is "no") {
                return new ConstantExpression() {
                    Line = Line,
                    Value = Thingie.Flag(false),
                };
            }
            // String
            else if (Word1.StartsWith('~')) {
                return new ConstantExpression() {
                    Line = Line,
                    Value = Thingie.String(EscapeString(Word1[1..].Replace('~', ' '))),
                };
            }
            // Double
            else if (double.TryParse(Word1, out double Double)) {
                return new ConstantExpression() {
                    Line = Line,
                    Value = Thingie.Number(Double),
                };
            }
            // Get Variable
            else {
                return new GetVariableExpression() {
                    Line = Line,
                    TargetVariable = Word1,
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