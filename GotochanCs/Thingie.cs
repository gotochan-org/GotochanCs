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
    public static Thingie Flag(bool Value) => new(ThingieType.Flag, NumberData: Value ? 1 : 0);
    public static Thingie Number(double Value) => new(ThingieType.Number, NumberData: Value);
    public static Thingie String(string? Value) => Value is null ? Nothing() : new(ThingieType.String, StringData: Value);

    public static implicit operator Thingie(bool Value) => Flag(Value);
    public static implicit operator Thingie(double Value) => Number(Value);
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
    public static explicit operator double(Thingie Thingie) => Thingie.CastNumber();
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
}

public enum ThingieType {
    Nothing = 0,
    Flag = 1,
    Number = 2,
    String = 3,
}