using LinkDotNet.StringBuilder;
using ResultZero;

namespace GotochanCs;

public static class Lexer {
    private static ReadOnlySpan<char> NewlineChars => ['\n', '\r', '\u2028', '\u2029'];

    public static Result<List<Token>> Lex(string Source) {
        List<Token> CurrentTokens = [];
        int CurrentLine = 1;

        // Build tokens
        for (int Index = 0; Index < Source.Length; Index++) {
            char Next = Source[Index];
            char? NextNext = Index + 1 < Source.Length ? Source[Index + 1] : null;

            // String
            if (Next is '~') {
                // Lex string
                if (LexString(Source, ref Index, ref CurrentLine).TryGetError(out Error StringError, out string? String)) {
                    return StringError;
                }
                // Create token
                CurrentTokens.Add(new Token(CurrentLine, TokenType.String, String));
            }
            // Number
            else if (Next is >= '0' and <= '9') {
                // Lex number
                if (LexNumber()) {

                }
            }
            // Arithmetic operator
            else if (Next is '+' or '-' or '*' or '/' or '%' or '^') {
                if (NextNext is '=') {
                    // Move forward
                    Index++;
                    // Create token
                    CurrentTokens.Add(new Token(CurrentLine, TokenType.AssignmentOperator, $"{Next}="));
                }
                else {
                    // Create token
                    CurrentTokens.Add(new Token(CurrentLine, TokenType.ArithmeticOperator, $"{Next}"));
                }
            }
            // Equals
            else if (Next is '=') {
                if (NextNext is '=') {
                    // Move forward
                    Index++;
                    // Create token
                    CurrentTokens.Add(new Token(CurrentLine, TokenType.ComparisonOperator, "=="));
                }
                else {
                    // Create token
                    CurrentTokens.Add(new Token(CurrentLine, TokenType.AssignmentOperator, "="));
                }
            }
            // Not equals
            else if (Next is '!') {
                if (NextNext is '=') {
                    // Move forward
                    Index++;
                    // Create token
                    CurrentTokens.Add(new Token(CurrentLine, TokenType.ComparisonOperator, "!="));
                }
                else {
                    // Invalid
                    return new Error($"{CurrentLine}: invalid '!'");
                }
            }
            // Greater than, less than
            else if (Next is '>' or '<') {
                if (NextNext is '=') {
                    // Move forward
                    Index++;
                    // Create token
                    CurrentTokens.Add(new Token(CurrentLine, TokenType.ComparisonOperator, $"{Next}="));
                }
                else {
                    // Create token
                    CurrentTokens.Add(new Token(CurrentLine, TokenType.AssignmentOperator, $"{Next}"));
                }
            }
            // Identifier
            else {

            }
            /*// Plus
            else if (Next is '+') {
                if (NextNext is '=') {
                    // Move forward
                    Index++;
                    // Create token
                    CurrentTokens.Add(new Token(CurrentLine, TokenType.Operator, "+="));
                }
                else {
                    // Create token
                    CurrentTokens.Add(new Token(CurrentLine, TokenType.Operator, "+"));
                }
            }
            // Minus
            else if (Next is '-') {
                if (NextNext is '=') {
                    // Move forward
                    Index++;
                    // Create token
                    CurrentTokens.Add(new Token(CurrentLine, TokenType.Operator, "-="));
                }
                else {
                    // Create token
                    CurrentTokens.Add(new Token(CurrentLine, TokenType.Operator, "-"));
                }
            }
            // Multiply
            else if (Next is '*') {
                if (NextNext is '=') {
                    // Move forward
                    Index++;
                    // Create token
                    CurrentTokens.Add(new Token(CurrentLine, TokenType.Operator, "*="));
                }
                else {
                    // Create token
                    CurrentTokens.Add(new Token(CurrentLine, TokenType.Operator, "*"));
                }
            }
            // Divide
            else if (Next is '/') {
                if (NextNext is '=') {
                    // Move forward
                    Index++;
                    // Create token
                    CurrentTokens.Add(new Token(CurrentLine, TokenType.Operator, "/="));
                }
                else {
                    // Create token
                    CurrentTokens.Add(new Token(CurrentLine, TokenType.Operator, "/"));
                }
            }
            // Modulo
            else if (Next is '%') {
                if (NextNext is '=') {
                    // Move forward
                    Index++;
                    // Create token
                    CurrentTokens.Add(new Token(CurrentLine, TokenType.Operator, "%="));
                }
                else {
                    // Create token
                    CurrentTokens.Add(new Token(CurrentLine, TokenType.Operator, "%"));
                }
            }
            // Exponentiate
            else if (Next is '^') {
                if (NextNext is '=') {
                    // Move forward
                    Index++;
                    // Create token
                    CurrentTokens.Add(new Token(CurrentLine, TokenType.Operator, "^="));
                }
                else {
                    // Create token
                    CurrentTokens.Add(new Token(CurrentLine, TokenType.Operator, "^"));
                }
            }*/
        }
    }

    private static Result<string> LexString(string Source, ref int Index, ref int CurrentLine) {
        using ValueStringBuilder StringBuilder = new(stackalloc char[64]);

        // Move past first '~'
        Index++;

        // Build string
        for (; Index < Source.Length; Index++) {
            char Next = Source[Index];

            // Escape
            if (Next is '\\') {
                // Read escaped char
                Index++;
                if (Index >= Source.Length) {
                    return new Error($"{CurrentLine}: incomplete escape sequence");
                }
                char Escaped = Source[Index];

                // Newline
                if (NewlineChars.Contains(Escaped)) {
                    // Pass (don't end string)
                }
                // Character
                else {
                    StringBuilder.Append(Escaped);
                }
            }
            // Space
            else if (Next is '~') {
                StringBuilder.Append(' ');
            }
            // Character
            else {
                StringBuilder.Append(Next);
            }
        }

        // Finish
        return StringBuilder.ToString();
    }
    private static Result<string> LexNumber(string Source, ref int Index, ref int CurrentLine) {
        using ValueStringBuilder NumberBuilder = new(stackalloc char[64]);

        // Build number
        for (; Index < Source.Length; Index++) {
            char Next = Source[Index];

            // Digit
            if (Next is >= '0' and <= '9') {
                NumberBuilder.Append(Next);
            }
            // Underscore
            else if (Next is '_') {
                NumberBuilder.Append(Next);
            }
        }

        // Disallow trailing underscore
        if (NumberBuilder[^1] is '_') {
            return new Error($"{CurrentLine}: trailing underscore in number");
        }

        // Finish
        return NumberBuilder.ToString();
    }
}