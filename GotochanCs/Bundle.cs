namespace GotochanCs;

public abstract class Bundle {
    public string Name { get; }
    public Dictionary<string, Thingie> Options { get; set; }
    public List<Bundle> Dependencies { get; set; }
    public Dictionary<string, Action<Actor>> ExternalLabels { get; set; }

    public Bundle() {
        Name = GetName();
        Options = GetOptions();
        Dependencies = GetDependencies();
        ExternalLabels = GetExternalLabels();
    }

    public Thingie GetOption(string Key) {
        return Options[Key];
    }
    public void SetOption(string Key, Thingie Value) {
        Options[Key] = Value;
    }

    protected abstract string GetName();
    protected virtual Dictionary<string, Thingie> GetOptions() => [];
    protected virtual List<Bundle> GetDependencies() => [];
    protected virtual Dictionary<string, Action<Actor>> GetExternalLabels() => [];
}