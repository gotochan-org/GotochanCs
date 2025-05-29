namespace GotochanCs;

public readonly struct Thingie {
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

    public static explicit operator bool(Thingie Thingie) => Thingie.CastFlag();
    public static explicit operator double(Thingie Thingie) => Thingie.CastNumber();
    public static explicit operator string?(Thingie Thingie) => Thingie.Type is ThingieType.Nothing ? null : Thingie.CastString();
}

public enum ThingieType {
    Nothing,
    Flag,
    Number,
    String,
}