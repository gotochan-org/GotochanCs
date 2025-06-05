namespace GotochanCs;

public abstract class Package {
    public abstract string Name { get; }
    public abstract Dictionary<string, Thingie> Options { get; }
    public abstract Dictionary<string, Action<Actor>> ExternalLabels { get; }

    public Thingie GetOption(string Key) {
        return Options[Key];
    }
    public void SetOption(string Key, Thingie Value) {
        Options[Key] = Value;
    }
}