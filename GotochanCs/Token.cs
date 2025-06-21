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