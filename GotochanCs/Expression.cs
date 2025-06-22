namespace GotochanCs;

/// <summary>
/// A Gotochan expression.
/// </summary>
public abstract record Expression {
    /// <summary>
    /// The location of the start of expression in the original source code.
    /// </summary>
    public required SourceLocation Location { get; init; }
}

/// <summary>
/// An expression with a constant value.
/// </summary>
public record ConstantExpression : Expression {
    /// <summary>
    /// The constant value.
    /// </summary>
    public required Thingie Value { get; init; }
}

/// <summary>
/// An expression that gets the value of a variable.
/// </summary>
public record GetVariableExpression : Expression {
    /// <summary>
    /// The name of the variable to get.
    /// </summary>
    public required string VariableName { get; init; }
}

/// <summary>
/// An expression with an operator on one value.
/// </summary>
public record UnaryExpression : Expression {
    /// <summary>
    /// The unary operator.
    /// </summary>
    public required UnaryOperator Operator { get; init; }
    /// <summary>
    /// The value.
    /// </summary>
    public required Expression Expression { get; init; }
}

/// <summary>
/// Types of operators for <see cref="UnaryExpression"/>.
/// </summary>
public enum UnaryOperator {
    /// <summary>
    /// A unary plus (+) operation.
    /// </summary>
    Plus,
    /// <summary>
    /// A unary minus (-) operation.
    /// </summary>
    Minus,
}

/// <summary>
/// An expression with an operator on two values.
/// </summary>
public record BinaryExpression : Expression {
    /// <summary>
    /// The binary operator.
    /// </summary>
    public required BinaryOperator Operator { get; init; }
    /// <summary>
    /// The first value.
    /// </summary>
    public required Expression Expression1 { get; init; }
    /// <summary>
    /// The second value.
    /// </summary>
    public required Expression Expression2 { get; init; }
}

/// <summary>
/// Types of operators for <see cref="BinaryExpression"/>.
/// </summary>
public enum BinaryOperator {
    /// <summary>
    /// A binary add (+) operation.
    /// </summary>
    Add,
    /// <summary>
    /// A binary subtract (-) operation.
    /// </summary>
    Subtract,
    /// <summary>
    /// A binary multiply (*) operation.
    /// </summary>
    Multiply,
    /// <summary>
    /// A binary divide (/) operation.
    /// </summary>
    Divide,
    /// <summary>
    /// A binary modulo (%) operation.
    /// </summary>
    Modulo,
    /// <summary>
    /// A binary exponentiate (^) operation.
    /// </summary>
    Exponentiate,

    /// <summary>
    /// A binary equals (==) operation.
    /// </summary>
    Equals,
    /// <summary>
    /// A binary not equals (!=) operation.
    /// </summary>
    NotEquals,
    /// <summary>
    /// A binary greater than (&gt;) operation.
    /// </summary>
    GreaterThan,
    /// <summary>
    /// A binary less than (&lt;) operation.
    /// </summary>
    LessThan,
    /// <summary>
    /// A binary greater than or equal to (&gt;=) operation.
    /// </summary>
    GreaterThanOrEqualTo,
    /// <summary>
    /// A binary less than or equal to (&lt;=) operation.
    /// </summary>
    LessThanOrEqualTo,
}