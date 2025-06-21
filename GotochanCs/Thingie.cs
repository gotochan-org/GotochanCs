using ResultZero;

namespace GotochanCs;

public readonly struct Thingie : IEquatable<Thingie> {
    public ThingieType Type { get; }

    private double NumberData { get; }
    private string? StringData { get; }

    private Thingie(ThingieType Type, double NumberData = default, string? StringData = default) {
        this.Type = Type;
        this.NumberData = NumberData;
        this.StringData = StringData;
    }

    public static Thingie Nothing() => new(ThingieType.Nothing);
    public static Thingie Flag(bool? Value) => Value is null ? Nothing() : new(ThingieType.Flag, NumberData: Value.Value ? 1 : 0);
    public static Thingie Number(double? Value) => Value is null ? Nothing() : new(ThingieType.Number, NumberData: Value.Value);
    public static Thingie String(string? Value) => Value is null ? Nothing() : new(ThingieType.String, StringData: Value);

    public static implicit operator Thingie(bool? Value) => Flag(Value);
    public static implicit operator Thingie(double? Value) => Number(Value);
    public static implicit operator Thingie(string? Value) => String(Value);

    public object? CastNothing() => Type is ThingieType.Nothing ? null : throw new InvalidCastException("thingie is not nothing");
    public bool CastFlag() => Type is ThingieType.Flag ? NumberData is not 0 : throw new InvalidCastException("thingie is not flag");
    public double CastNumber() => Type is ThingieType.Number ? NumberData : throw new InvalidCastException("thingie is not number");
    public string CastString() => Type is ThingieType.String ? StringData! : throw new InvalidCastException("thingie is not string");

    public Result<object?> AsNothing() => Type is ThingieType.Nothing ? CastNothing() : new Error("thingie is not nothing");
    public Result<bool> AsFlag() => Type is ThingieType.Flag ? CastFlag() : new Error("thingie is not flag");
    public Result<double> AsNumber() => Type is ThingieType.Number ? CastNumber() : new Error("thingie is not number");
    public Result<string> AsString() => Type is ThingieType.String ? CastString() : new Error("thingie is not string");

    public static explicit operator bool(Thingie Thingie) => Thingie.CastFlag();
    public static explicit operator bool?(Thingie Thingie) => Thingie.Type is ThingieType.Nothing ? null : Thingie.CastFlag();
    public static explicit operator double(Thingie Thingie) => Thingie.CastNumber();
    public static explicit operator double?(Thingie Thingie) => Thingie.Type is ThingieType.Nothing ? null : Thingie.CastNumber();
    public static explicit operator string?(Thingie Thingie) => Thingie.Type is ThingieType.Nothing ? null : Thingie.CastString();

    public object? AsObject => Type switch {
        ThingieType.Nothing => CastNothing(),
        ThingieType.Flag => CastFlag(),
        ThingieType.Number => CastNumber(),
        ThingieType.String => CastString(),
        _ => throw new NotImplementedException($"type not handled: '{Type}'")
    };

    public override string ToString() => Type switch {
        ThingieType.Nothing => "nothing",
        ThingieType.Flag => CastFlag() ? "yes" : "no",
        ThingieType.Number => CastNumber().ToString(),
        ThingieType.String => CastString(),
        _ => throw new NotImplementedException($"type not handled: '{Type}'")
    };

    public override bool Equals(object? Other) {
        return Other is Thingie OtherThingie && Equals(OtherThingie);
    }
    public override int GetHashCode() {
        return Type switch {
            ThingieType.Nothing => 0,
            ThingieType.Flag => CastFlag().GetHashCode(),
            ThingieType.Number => CastNumber().GetHashCode(),
            ThingieType.String => CastString().GetHashCode(),
            _ => throw new NotImplementedException($"type not handled: '{Type}'")
        };
    }
    public bool Equals(Thingie Other) {
        return Type == Other.Type
            && NumberData == Other.NumberData
            && StringData == Other.StringData;
    }

    public static bool operator ==(Thingie A, Thingie B) => A.Equals(B);
    public static bool operator !=(Thingie A, Thingie B) => !A.Equals(B);

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
    public static Result<Thingie> Equals(SourceLocation Location, Thingie Value1, Thingie Value2) {
        // Thingie, Thingie
        return Flag(Value1 == Value2);
    }
    public static Result<Thingie> NotEquals(SourceLocation Location, Thingie Value1, Thingie Value2) {
        // Thingie, Thingie
        return Flag(Value1 != Value2);
    }
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

public enum ThingieType {
    Nothing = 0,
    Flag = 1,
    Number = 2,
    String = 3,
}