using LinkDotNet.StringBuilder;
using ResultZero;

namespace GotochanCs;

public static class Lexer {
    public static ReadOnlySpan<char> NewlineChars => ['\n', '\r', '\u2028', '\u2029'];
    public static ReadOnlySpan<char> ReservedChars => ['+', '-', '*', '/', '%', '^', '=', '!', '>', '<', '\\', ';', '~'];

    public static Result<LexResult> Lex(string Source) {
        List<Token> CurrentTokens = [];

        // Build tokens
        for (int Index = 0; Index < Source.Length; Index++) {
            char Next = Source[Index];
            char? NextNext = Index + 1 < Source.Length ? Source[Index + 1] : null;

            // Escape
            if (Next is '\\') {
                // Read escaped char
                Index++;
                if (Index >= Source.Length) {
                    return new Error($"{SourceLocation.GetLine(Source, Index)}: incomplete escape sequence");
                }
                char Escaped = Source[Index];

                // Escape newline
                if (NewlineChars.Contains(Escaped)) {
                    // Pass
                }
                // Invalid escape
                else {
                    return new Error($"{SourceLocation.GetLine(Source, Index)}: invalid escape sequence");
                }
            }
            // End of instruction
            else if (Next is ';' || NewlineChars.Contains(Next)) {
                // Create token
                CurrentTokens.Add(new Token(Source, Index, TokenType.EndOfInstruction, $"{Next}"));
            }
            // String
            else if (Next is '~') {
                // Lex string
                if (LexString(Source, ref Index).TryGetError(out Error StringError, out string? String)) {
                    return StringError;
                }
                // Create token
                CurrentTokens.Add(new Token(Source, Index, TokenType.String, String));
            }
            // Number
            else if (Next is >= '0' and <= '9') {
                // Lex number
                if (LexNumber(Source, ref Index).TryGetError(out Error NumberError, out string? Number)) {
                    return NumberError;
                }
                // Create token
                CurrentTokens.Add(new Token(Source, Index, TokenType.Number, Number));
            }
            // Arithmetic operator
            else if (Next is '+' or '-' or '*' or '/' or '%' or '^') {
                if (NextNext is '=') {
                    // Move forward
                    Index++;
                    // Create token
                    CurrentTokens.Add(new Token(Source, Index, TokenType.SetOperator, $"{Next}="));
                }
                else {
                    // Create token
                    CurrentTokens.Add(new Token(Source, Index, TokenType.Operator, $"{Next}"));
                }
            }
            // Equals
            else if (Next is '=') {
                if (NextNext is '=') {
                    // Move forward
                    Index++;
                    // Create token
                    CurrentTokens.Add(new Token(Source, Index, TokenType.Operator, "=="));
                }
                else {
                    // Create token
                    CurrentTokens.Add(new Token(Source, Index, TokenType.SetOperator, "="));
                }
            }
            // Not equals
            else if (Next is '!') {
                if (NextNext is '=') {
                    // Move forward
                    Index++;
                    // Create token
                    CurrentTokens.Add(new Token(Source, Index, TokenType.Operator, "!="));
                }
                else {
                    // Invalid
                    return new Error($"{SourceLocation.GetLine(Source, Index)}: invalid '!'");
                }
            }
            // Greater than, less than
            else if (Next is '>' or '<') {
                if (NextNext is '=') {
                    // Move forward
                    Index++;
                    // Create token
                    CurrentTokens.Add(new Token(Source, Index, TokenType.Operator, $"{Next}="));
                }
                else {
                    // Create token
                    CurrentTokens.Add(new Token(Source, Index, TokenType.Operator, $"{Next}"));
                }
            }
            // Whitespace
            else if (char.IsWhiteSpace(Next)) {
                // Pass
            }
            // Identifier
            else {
                // Lex identifier
                if (LexIdentifier(Source, ref Index).TryGetError(out Error IdentifierError, out string? Identifier)) {
                    return IdentifierError;
                }

                // Keyword
                if (Identifier is "goto") {
                    CurrentTokens.Add(new Token(Source, Index, TokenType.Goto, Identifier));
                }
                else if (Identifier is "label") {
                    CurrentTokens.Add(new Token(Source, Index, TokenType.Label, Identifier));
                }
                else if (Identifier is "if") {
                    CurrentTokens.Add(new Token(Source, Index, TokenType.If, Identifier));
                }
                // Nothing
                else if (Identifier is "nothing") {
                    CurrentTokens.Add(new Token(Source, Index, TokenType.Nothing, Identifier));
                }
                // Flag
                else if (Identifier is "yes" or "no") {
                    CurrentTokens.Add(new Token(Source, Index, TokenType.Flag, Identifier));
                }
                // Identifier
                else {
                    CurrentTokens.Add(new Token(Source, Index, TokenType.Identifier, Identifier));
                }
            }
        }

        // Finish
        return new LexResult() {
            Source = Source,
            Tokens = CurrentTokens,
        };
    }

    private static Result<string> LexString(string Source, ref int Index) {
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
                    return new Error($"{SourceLocation.GetLine(Source, Index)}: incomplete escape sequence");
                }
                char Escaped = Source[Index];

                // Newline
                if (NewlineChars.Contains(Escaped)) {
                    // Pass
                }
                // Backspace
                else if (Escaped is 'b') {
                    StringBuilder.Append('\b');
                }
                // Form feed
                else if (Escaped is 'f') {
                    StringBuilder.Append('\f');
                }
                // Newline
                else if (Escaped is 'n') {
                    StringBuilder.Append('\n');
                }
                // Carriage return
                else if (Escaped is 'r') {
                    StringBuilder.Append('\r');
                }
                // Tab
                else if (Escaped is 't') {
                    StringBuilder.Append('\t');
                }
                // Vertical tab
                else if (Escaped is 'v') {
                    StringBuilder.Append('\v');
                }
                // Null
                else if (Escaped is '0') {
                    StringBuilder.Append('\0');
                }
                // Alert
                else if (Escaped is 'a') {
                    StringBuilder.Append('\a');
                }
                // Escape
                else if (Escaped is 'e') {
                    StringBuilder.Append('\e');
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
            // Whitespace
            else if (char.IsWhiteSpace(Next)) {
                Index--;
                break;
            }
            // Character
            else {
                StringBuilder.Append(Next);
            }
        }

        // Finish
        return StringBuilder.ToString();
    }
    private static Result<string> LexNumber(string Source, ref int Index) {
        using ValueStringBuilder NumberBuilder = new(stackalloc char[64]);

        // Build number
        for (; Index < Source.Length; Index++) {
            char Next = Source[Index];

            // Digit
            if (Next is >= '0' and <= '9') {
                NumberBuilder.Append(Next);
            }
            // Dot
            else if (Next is '.') {
                if (NumberBuilder.Contains(".")) {
                    Index--;
                    break;
                }
                NumberBuilder.Append(Next);
            }
            // Underscore
            else if (Next is '_') {
                NumberBuilder.Append(Next);
            }
            // Character
            else {
                Index--;
                break;
            }
        }

        // Disallow trailing underscore
        if (NumberBuilder[^1] is '_') {
            return new Error($"{SourceLocation.GetLine(Source, Index)}: trailing underscore in number");
        }

        // Remove underscores
        NumberBuilder.Replace("_", "");

        // Finish
        return NumberBuilder.ToString();
    }
    private static Result<string> LexIdentifier(string Source, ref int Index) {
        using ValueStringBuilder IdentifierBuilder = new(stackalloc char[64]);

        // Build identifier
        for (; Index < Source.Length; Index++) {
            char Next = Source[Index];

            // Reserved character
            if (ReservedChars.Contains(Next)) {
                Index--;
                break;
            }
            // Whitespace
            else if (char.IsWhiteSpace(Next)) {
                Index--;
                break;
            }
            // Character
            else {
                IdentifierBuilder.Append(Next);
            }
        }

        // Finish
        return IdentifierBuilder.ToString();
    }
}

public class LexResult {
    public required string Source { get; init; }
    public required List<Token> Tokens { get; init; }
}