namespace GotochanCs;

public abstract class Bundle {
    public string Name { get; }
    public IDictionary<string, Thingie> Options { get; set; }
    public IList<Bundle> Dependencies { get; set; }
    public IDictionary<string, Action<Actor>> ExternalLabels { get; set; }

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
    protected virtual IDictionary<string, Thingie> GetOptions() => new Dictionary<string, Thingie>();
    protected virtual IList<Bundle> GetDependencies() => [];
    protected virtual IDictionary<string, Action<Actor>> GetExternalLabels() => new Dictionary<string, Action<Actor>>();
}