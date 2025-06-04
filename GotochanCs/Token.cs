namespace GotochanCs;

public readonly record struct Token {
    public SourceLocation Location { get; }
    public TokenType Type { get; }
    public string Value { get; }

    public Token(SourceLocation Location, TokenType Type, string Value) {
        this.Location = Location;
        this.Type = Type;
        this.Value = Value;
    }
    public Token(string Source, int Index, TokenType Type, string Value)
        : this(new SourceLocation(Source, Index), Type, Value) {
    }
}

public enum TokenType {
    EndOfInstruction = 0,
    Nothing = 1,
    Flag = 2,
    Number = 3,
    String = 4,
    SetOperator = 5,
    Operator = 6,
    Identifier = 7,
    Goto = 8,
    Label = 9,
    If = 10,
}

public readonly record struct SourceLocation {
    public string Source { get; }
    public int Index { get; }
    public int Line { get; }

    public SourceLocation(string Source, int Index, int? Line = null) {
        this.Source = Source;
        this.Index = Index;
        this.Line = Line ?? GetLine(Source, Index);
    }

    public static int GetLine(string Source, int Index) {
        int Line = 1;
        for (int NextIndex = 0; NextIndex < Index; NextIndex++) {
            char Next = Source[NextIndex];

            // Newline
            if (Lexer.NewlineChars.Contains(Next)) {
                Line++;
                // Join CR LF
                if (Next is '\r' && NextIndex + 1 < Source.Length && Source[NextIndex + 1] is '\n') {
                    NextIndex++;
                }
            }
        }
        return Line;
    }
}