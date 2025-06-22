using ResultZero;

namespace GotochanCs;

/// <summary>
/// A Gotochan object.
/// </summary>
public readonly struct Thingie : IEquatable<Thingie> {
    /// <summary>
    /// The type of Gotochan object.
    /// </summary>
    public ThingieType Type { get; }

    /// <summary>
    /// The 8 byte data for flags and numbers.
    /// </summary>
    private double NumberData { get; }
    /// <summary>
    /// The 4-8 byte data for strings.
    /// </summary>
    private string? StringData { get; }

    /// <summary>
    /// Constructs a thingie with the given type and data.
    /// </summary>
    private Thingie(ThingieType Type, double NumberData = default, string? StringData = default) {
        this.Type = Type;
        this.NumberData = NumberData;
        this.StringData = StringData;
    }

    /// <summary>
    /// Creates a nothing (null) thingie.
    /// </summary>
    public static Thingie Nothing() => new(ThingieType.Nothing);
    /// <summary>
    /// Creates a flag (boolean) thingie, or a nothing (null) thingie.
    /// </summary>
    public static Thingie Flag(bool? Value) => Value is null ? Nothing() : new(ThingieType.Flag, NumberData: Value.Value ? 1 : 0);
    /// <summary>
    /// Creates a number (64-bit signed float) thingie, or a nothing (null) thingie.
    /// </summary>
    public static Thingie Number(double? Value) => Value is null ? Nothing() : new(ThingieType.Number, NumberData: Value.Value);
    /// <summary>
    /// Creates a string (UTF-16 char sequence) thingie, or a nothing (null) thingie.
    /// </summary>
    public static Thingie String(string? Value) => Value is null ? Nothing() : new(ThingieType.String, StringData: Value);

    /// <inheritdoc cref="Flag(bool?)"/>
    public static implicit operator Thingie(bool? Value) => Flag(Value);
    /// <inheritdoc cref="Number(double?)"/>
    public static implicit operator Thingie(double? Value) => Number(Value);
    /// <inheritdoc cref="String(string?)"/>
    public static implicit operator Thingie(string? Value) => String(Value);

    /// <summary>
    /// Casts the thingie to type nothing (null), or throws an exception.
    /// </summary>
    /// <exception cref="InvalidCastException"/>
    public object? CastNothing() => Type is ThingieType.Nothing ? null : throw new InvalidCastException("thingie is not nothing");
    /// <summary>
    /// Casts the thingie to type flag (boolean), or throws an exception.
    /// </summary>
    /// <exception cref="InvalidCastException"/>
    public bool CastFlag() => Type is ThingieType.Flag ? NumberData is not 0 : throw new InvalidCastException("thingie is not flag");
    /// <summary>
    /// Casts the thingie to type number (64-bit signed float), or throws an exception.
    /// </summary>
    /// <exception cref="InvalidCastException"/>
    public double CastNumber() => Type is ThingieType.Number ? NumberData : throw new InvalidCastException("thingie is not number");
    /// <summary>
    /// Casts the thingie to type string (UTF-16 char sequence), or throws an exception.
    /// </summary>
    /// <exception cref="InvalidCastException"/>
    public string CastString() => Type is ThingieType.String ? StringData! : throw new InvalidCastException("thingie is not string");

    /// <summary>
    /// Casts the thingie to type flag (boolean) or type nothing (null), or throws an exception.
    /// </summary>
    /// <exception cref="InvalidCastException"/>
    public bool? CastFlagOrNothing() => Type is ThingieType.Nothing ? null : CastFlag();
    /// <summary>
    /// Casts the thingie to type number (64-bit signed float) or type nothing (null), or throws an exception.
    /// </summary>
    /// <exception cref="InvalidCastException"/>
    public double? CastNumberOrNothing() => Type is ThingieType.Nothing ? null : CastNumber();
    /// <summary>
    /// Casts the thingie to type string (UTF-16 char sequence) or type nothing (null), or throws an exception.
    /// </summary>
    /// <exception cref="InvalidCastException"/>
    public string? CastStringOrNothing() => Type is ThingieType.Nothing ? null : CastString();

    /// <summary>
    /// Casts the thingie to type nothing (null), or returns an error.
    /// </summary>
    public Result<object?> AsNothing() => Type is ThingieType.Nothing ? CastNothing() : new Error("thingie is not nothing");
    /// <summary>
    /// Casts the thingie to type flag (boolean), or returns an error.
    /// </summary>
    public Result<bool> AsFlag() => Type is ThingieType.Flag ? CastFlag() : new Error("thingie is not flag");
    /// <summary>
    /// Casts the thingie to type number (64-bit signed float), or returns an error.
    /// </summary>
    public Result<double> AsNumber() => Type is ThingieType.Number ? CastNumber() : new Error("thingie is not number");
    /// <summary>
    /// Casts the thingie to type string (UTF-16 char sequence), or returns an error.
    /// </summary>
    public Result<string> AsString() => Type is ThingieType.String ? CastString() : new Error("thingie is not string");

    /// <summary>
    /// Casts the thingie to type flag (boolean) or type nothing (null), or returns an error.
    /// </summary>
    public Result<bool?> AsFlagOrNothing() => Type is ThingieType.Nothing ? (bool?)null : AsFlag().Try(Flag => (bool?)Flag);
    /// <summary>
    /// Casts the thingie to type number (64-bit signed float) or type nothing (null), or returns an error.
    /// </summary>
    public Result<double?> AsNumberOrNothing() => Type is ThingieType.Nothing ? (double?)null : AsNumber().Try(Number => (double?)Number);
    /// <summary>
    /// Casts the thingie to type string (UTF-16 char sequence) or type nothing (null), or returns an error.
    /// </summary>
    public Result<string?> AsStringOrNothing() => Type is ThingieType.Nothing ? null : CastString();

    /// <inheritdoc cref="CastFlag()"/>
    public static explicit operator bool(Thingie Thingie) => Thingie.CastFlag();
    /// <inheritdoc cref="CastFlagOrNothing()"/>
    public static explicit operator bool?(Thingie Thingie) => Thingie.CastFlagOrNothing();
    /// <inheritdoc cref="CastNumber()"/>
    public static explicit operator double(Thingie Thingie) => Thingie.CastNumber();
    /// <inheritdoc cref="CastNumberOrNothing()"/>
    public static explicit operator double?(Thingie Thingie) => Thingie.CastNumberOrNothing();
    /// <inheritdoc cref="CastStringOrNothing()"/>
    public static explicit operator string?(Thingie Thingie) => Thingie.CastStringOrNothing();

    /// <summary>
    /// Converts the thingie to a CLR object appropriate for the type.
    /// </summary>
    public object? AsObject() => Type switch {
        ThingieType.Nothing => CastNothing(),
        ThingieType.Flag => CastFlag(),
        ThingieType.Number => CastNumber(),
        ThingieType.String => CastString(),
        _ => throw new NotImplementedException($"type not handled: '{Type}'")
    };

    /// <summary>
    /// Stringifies the thingie.
    /// </summary>
    public override string ToString() => Type switch {
        ThingieType.Nothing => "nothing",
        ThingieType.Flag => CastFlag() ? "yes" : "no",
        ThingieType.Number => CastNumber().ToString(),
        ThingieType.String => CastString(),
        _ => throw new NotImplementedException($"type not handled: '{Type}'")
    };

    /// <summary>
    /// Returns whether the other thingie is equal to this thingie.
    /// </summary>
    public bool Equals(Thingie Other) {
        return Type == Other.Type
            && NumberData == Other.NumberData
            && StringData == Other.StringData;
    }
    /// <summary>
    /// Returns whether the other object is a thingie and equal to this thingie.
    /// </summary>
    public override bool Equals(object? Other) {
        return Other is Thingie OtherThingie && Equals(OtherThingie);
    }
    /// <summary>
    /// Returns a hash code for this thingie.
    /// </summary>
    public override int GetHashCode() {
        return Type switch {
            ThingieType.Nothing => 0,
            ThingieType.Flag => CastFlag().GetHashCode(),
            ThingieType.Number => CastNumber().GetHashCode(),
            ThingieType.String => CastString().GetHashCode(),
            _ => throw new NotImplementedException($"type not handled: '{Type}'")
        };
    }

    /// <summary>
    /// Returns whether the thingies are equal.
    /// </summary>
    public static bool operator ==(Thingie A, Thingie B) => A.Equals(B);
    /// <summary>
    /// Returns whether the thingies are not equal.
    /// </summary>
    public static bool operator !=(Thingie A, Thingie B) => !A.Equals(B);

    /// <summary>
    /// Performs a unary plus (+) operation on <paramref name="Value"/>.
    /// </summary>
    public static Result<Thingie> Plus(SourceLocation Location, Thingie Value) {
        // Number
        if (Value.Type is ThingieType.Number) {
            return Number(+Value.CastNumber());
        }
        // Invalid
        else {
            return new Error($"{Location.Line}: invalid type for '+': '{Value.Type}'");
        }
    }
    /// <summary>
    /// Performs a unary minus (-) operation on <paramref name="Value"/>.
    /// </summary>
    public static Result<Thingie> Minus(SourceLocation Location, Thingie Value) {
        // Number
        if (Value.Type is ThingieType.Number) {
            return Number(-Value.CastNumber());
        }
        // Invalid
        else {
            return new Error($"{Location.Line}: invalid type for '-': '{Value.Type}'");
        }
    }
    /// <summary>
    /// Performs a binary add (+) operation on <paramref name="Value1"/> and <paramref name="Value2"/>.
    /// </summary>
    public static Result<Thingie> Add(SourceLocation Location, Thingie Value1, Thingie Value2) {
        // Number, Number
        if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
            return Number(Value1.CastNumber() + Value2.CastNumber());
        }
        // String, Thingie
        else if (Value1.Type is ThingieType.String) {
            return String(Value1.CastString() + Value2.ToString());
        }
        // Invalid
        else {
            return new Error($"{Location.Line}: invalid types for '+': '{Value1.Type}', '{Value2.Type}'");
        }
    }
    /// <summary>
    /// Performs a binary subtract (-) operation on <paramref name="Value1"/> and <paramref name="Value2"/>.
    /// </summary>
    public static Result<Thingie> Subtract(SourceLocation Location, Thingie Value1, Thingie Value2) {
        // Number, Number
        if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
            return Number(Value1.CastNumber() - Value2.CastNumber());
        }
        // Invalid
        else {
            return new Error($"{Location.Line}: invalid types for '-': '{Value1.Type}', '{Value2.Type}'");
        }
    }
    /// <summary>
    /// Performs a binary multiply (*) operation on <paramref name="Value1"/> and <paramref name="Value2"/>.
    /// </summary>
    public static Result<Thingie> Multiply(SourceLocation Location, Thingie Value1, Thingie Value2) {
        // Number, Number
        if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
            return Number(Value1.CastNumber() * Value2.CastNumber());
        }
        // String, Number
        else if (Value1.Type is ThingieType.String && Value2.Type is ThingieType.Number) {
            double Number2 = Value2.CastNumber();
            int Int2 = (int)Number2;
            if (Int2 < 0 || Number2 != Int2) {
                return new Error($"{Location.Line}: number must be positive integer to multiply string");
            }
            return String(string.Concat(Enumerable.Repeat(Value1.CastString(), Int2)));
        }
        // Invalid
        else {
            return new Error($"{Location.Line}: invalid types for '*': '{Value1.Type}', '{Value2.Type}'");
        }
    }
    /// <summary>
    /// Performs a binary divide (/) operation on <paramref name="Value1"/> and <paramref name="Value2"/>.
    /// </summary>
    public static Result<Thingie> Divide(SourceLocation Location, Thingie Value1, Thingie Value2) {
        // Number, Number
        if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
            return Number(Value1.CastNumber() / Value2.CastNumber());
        }
        // Invalid
        else {
            return new Error($"{Location.Line}: invalid types for '/': '{Value1.Type}', '{Value2.Type}'");
        }
    }
    /// <summary>
    /// Performs a binary modulo (%) operation on <paramref name="Value1"/> and <paramref name="Value2"/>.
    /// </summary>
    public static Result<Thingie> Modulo(SourceLocation Location, Thingie Value1, Thingie Value2) {
        // Number, Number
        if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
            return Number(Value1.CastNumber() % Value2.CastNumber());
        }
        // Invalid
        else {
            return new Error($"{Location.Line}: invalid types for '%': '{Value1.Type}', '{Value2.Type}'");
        }
    }
    /// <summary>
    /// Performs a binary exponentiate (^) operation on <paramref name="Value1"/> and <paramref name="Value2"/>.
    /// </summary>
    public static Result<Thingie> Exponentiate(SourceLocation Location, Thingie Value1, Thingie Value2) {
        // Number, Number
        if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
            return Number(double.Pow(Value1.CastNumber(), Value2.CastNumber()));
        }
        // Invalid
        else {
            return new Error($"{Location.Line}: invalid types for '^': '{Value1.Type}', '{Value2.Type}'");
        }
    }
#pragma warning disable IDE0060 // Remove unused parameter
    /// <summary>
    /// Performs a binary equals (==) operation on <paramref name="Value1"/> and <paramref name="Value2"/>.
    /// </summary>
    public static Result<Thingie> Equals(SourceLocation Location, Thingie Value1, Thingie Value2) {
        // Thingie, Thingie
        return Flag(Value1 == Value2);
    }
    /// <summary>
    /// Performs a binary not equals (!=) operation on <paramref name="Value1"/> and <paramref name="Value2"/>.
    /// </summary>
    public static Result<Thingie> NotEquals(SourceLocation Location, Thingie Value1, Thingie Value2) {
        // Thingie, Thingie
        return Flag(Value1 != Value2);
    }
#pragma warning restore IDE0060 // Remove unused parameter
    /// <summary>
    /// Performs a greater than (&gt;) operation on <paramref name="Value1"/> and <paramref name="Value2"/>.
    /// </summary>
    public static Result<Thingie> GreaterThan(SourceLocation Location, Thingie Value1, Thingie Value2) {
        // Number, Number
        if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
            return Flag(Value1.CastNumber() > Value2.CastNumber());
        }
        // Invalid
        else {
            return new Error($"{Location.Line}: invalid types for '>': '{Value1.Type}', '{Value2.Type}'");
        }
    }
    /// <summary>
    /// Performs a less than (&lt;) operation on <paramref name="Value1"/> and <paramref name="Value2"/>.
    /// </summary>
    public static Result<Thingie> LessThan(SourceLocation Location, Thingie Value1, Thingie Value2) {
        // Number, Number
        if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
            return Flag(Value1.CastNumber() < Value2.CastNumber());
        }
        // Invalid
        else {
            return new Error($"{Location.Line}: invalid types for '<': '{Value1.Type}', '{Value2.Type}'");
        }
    }
    /// <summary>
    /// Performs a greater than or equal to (&gt;=) operation on <paramref name="Value1"/> and <paramref name="Value2"/>.
    /// </summary>
    public static Result<Thingie> GreaterThanOrEqualTo(SourceLocation Location, Thingie Value1, Thingie Value2) {
        // Number, Number
        if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
            return Flag(Value1.CastNumber() >= Value2.CastNumber());
        }
        // Invalid
        else {
            return new Error($"{Location.Line}: invalid types for '>=': '{Value1.Type}', '{Value2.Type}'");
        }
    }
    /// <summary>
    /// Performs a less than or equal to (&lt;=) operation on <paramref name="Value1"/> and <paramref name="Value2"/>.
    /// </summary>
    public static Result<Thingie> LessThanOrEqualTo(SourceLocation Location, Thingie Value1, Thingie Value2) {
        // Number, Number
        if (Value1.Type is ThingieType.Number && Value2.Type is ThingieType.Number) {
            return Flag(Value1.CastNumber() <= Value2.CastNumber());
        }
        // Invalid
        else {
            return new Error($"{Location.Line}: invalid types for '<=': '{Value1.Type}', '{Value2.Type}'");
        }
    }
}

/// <summary>
/// Types of <see cref="Thingie"/>.
/// </summary>
public enum ThingieType {
    /// <summary>
    /// No data, represented by <see langword="null"/>.
    /// </summary>
    Nothing = 0,
    /// <summary>
    /// True/false data, represented by <see cref="bool"/>.
    /// </summary>
    Flag = 1,
    /// <summary>
    /// 64-bit float data, represented by <see cref="double"/>.
    /// </summary>
    Number = 2,
    /// <summary>
    /// UTF-16 char sequence data, represented by <see cref="string"/>.
    /// </summary>
    String = 3,
}