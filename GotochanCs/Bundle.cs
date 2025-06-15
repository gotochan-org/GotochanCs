namespace GotochanCs;

public abstract class Bundle {
    public string Name { get; }
    public Dictionary<string, Thingie> Options { get; }
    public Dictionary<string, Action<Actor>> ExternalLabels { get; }

    public Bundle() {
        Name = GetName();
        Options = [];
        ExternalLabels = GetExternalLabels();
    }

    public Thingie GetOption(string Key) {
        return Options[Key];
    }
    public void SetOption(string Key, Thingie Value) {
        Options[Key] = Value;
    }
    public void IncludeBundle(Bundle Bundle) {
        // Add external labels
        foreach (KeyValuePair<string, Action<Actor>> ExternalLabel in Bundle.ExternalLabels) {
            ExternalLabels[ExternalLabel.Key] = ExternalLabel.Value;
        }
    }

    protected abstract string GetName();
    protected abstract Dictionary<string, Action<Actor>> GetExternalLabels();
}