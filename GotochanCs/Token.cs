namespace GotochanCs;

public readonly record struct Token {
    public int Line { get; }
    public TokenType Type { get; }
    public string Value { get; }

    public Token(int Line, TokenType Type, string Value) {
        this.Line = Line;
        this.Type = Type;
        this.Value = Value;
    }
}

public enum TokenType {
    Nothing = 0,
    Flag = 1,
    Number = 2,
    String = 3,
    AssignmentOperator = 4,
    ArithmeticOperator = 5,
    ComparisonOperator = 6,
}