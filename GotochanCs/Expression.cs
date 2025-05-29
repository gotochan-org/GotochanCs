namespace GotochanCs;

public abstract record Expression {
    public required int Line { get; init; }
}

public record ConstantExpression : Expression {
    public required Thingie Value { get; init; }
}

public record GetVariableExpression : Expression {
    public required string TargetVariable { get; init; }
}

public record UnaryExpression : Expression {
    public required UnaryOperator Operator { get; init; }
    public required Expression Expression { get; init; }
}

public enum UnaryOperator {
    Plus,
    Minus,
}

public record BinaryExpression : Expression {
    public required BinaryOperator Operator { get; init; }
    public required Expression Expression1 { get; init; }
    public required Expression Expression2 { get; init; }
}

public enum BinaryOperator {
    Add,
    Subtract,
    Multiply,
    Divide,
    Modulo,
    Exponentiate,

    Equals,
    NotEquals,
    GreaterThan,
    LessThan,
    GreaterThanOrEqualTo,
    LessThanOrEqualTo,
}