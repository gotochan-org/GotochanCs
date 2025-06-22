namespace GotochanCs;

/// <summary>
/// A Gotochan token. Tokens represent code in a processed way.
/// </summary>
public readonly record struct Token {
    /// <summary>
    /// The location of the token in the original source code.
    /// </summary>
    public SourceLocation Location { get; }
    /// <summary>
    /// The type of token.
    /// </summary>
    public TokenType Type { get; }
    /// <summary>
    /// The string data of the token.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Constructs a token with a location, a type and a value.
    /// </summary>
    public Token(SourceLocation Location, TokenType Type, string Value) {
        this.Location = Location;
        this.Type = Type;
        this.Value = Value;
    }
    /// <summary>
    /// Constructs a token with an index in a string, a type and a value.
    /// </summary>
    public Token(string Source, int Index, TokenType Type, string Value)
        : this(new SourceLocation(Source, Index), Type, Value) {
    }
}

/// <summary>
/// Types of <see cref="Token"/>.
/// </summary>
public enum TokenType {
    /// <summary>
    /// A separator between instructions.
    /// </summary>
    EndOfInstruction = 0,
    /// <summary>
    /// A constant nothing (null) value.
    /// </summary>
    Nothing = 1,
    /// <summary>
    /// A constant flag (boolean) value.
    /// </summary>
    Flag = 2,
    /// <summary>
    /// A constant number (64-bit float) value.
    /// </summary>
    Number = 3,
    /// <summary>
    /// A constant string (UTF-16 char sequence) value.
    /// </summary>
    String = 4,
    /// <summary>
    /// An assignment or compound assignment operator.
    /// </summary>
    SetOperator = 5,
    /// <summary>
    /// A unary or binary operator.
    /// </summary>
    Operator = 6,
    /// <summary>
    /// A name for a variable or label.
    /// </summary>
    Identifier = 7,
    /// <summary>
    /// A goto keyword.
    /// </summary>
    Goto = 8,
    /// <summary>
    /// A label keyword.
    /// </summary>
    Label = 9,
    /// <summary>
    /// An if keyword.
    /// </summary>
    If = 10,
}