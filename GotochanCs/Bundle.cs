namespace GotochanCs;

/// <summary>
/// A standardized library that can be included in an <see cref="Actor"/>.
/// </summary>
public abstract class Bundle {
    /// <summary>
    /// The display name of the bundle.
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// The value of each option used to configure the bundle.
    /// </summary>
    public Dictionary<string, Thingie> Options { get; set; }
    /// <summary>
    /// The bundles to be included before this bundle.
    /// </summary>
    public List<Bundle> Dependencies { get; set; }
    /// <summary>
    /// The external delegates accessible as labels.
    /// </summary>
    public Dictionary<string, Action<Actor>> ExternalLabels { get; set; }

    /// <summary>
    /// Constructs a bundle using the virtual methods.
    /// </summary>
    public Bundle() {
        Name = GetName();
        Options = GetOptions();
        Dependencies = GetDependencies();
        ExternalLabels = GetExternalLabels();
    }

    /// <summary>
    /// Returns the value of the given option used to configure the bundle, or <see cref="Thingie.Nothing()"/>.
    /// </summary>
    public Thingie GetOption(string OptionName) {
        return Options.GetValueOrDefault(OptionName);
    }
    /// <summary>
    /// Sets the value of the given option used to configure the bundle.
    /// </summary>
    public void SetOption(string OptionName, Thingie Value) {
        if (Value.Type is ThingieType.Nothing) {
            Options.Remove(OptionName);
        }
        else {
            Options[OptionName] = Value;
        }
    }

    /// <summary>
    /// Returns the display name of the bundle.
    /// </summary>
    protected abstract string GetName();
    /// <summary>
    /// Returns the initial value of each option used to configure the bundle.
    /// </summary>
    protected virtual Dictionary<string, Thingie> GetOptions() => [];
    /// <summary>
    /// Returns the initial bundles to be included before this bundle.
    /// </summary>
    protected virtual List<Bundle> GetDependencies() => [];
    /// <summary>
    /// Returns the initial external delegates accessible as labels.
    /// </summary>
    protected virtual Dictionary<string, Action<Actor>> GetExternalLabels() => [];
}