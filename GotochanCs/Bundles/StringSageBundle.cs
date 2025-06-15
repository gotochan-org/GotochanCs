namespace GotochanCs.Bundles;

public class StringSageBundle : Bundle {
    protected override string GetName() => "stringsage";
    protected override Dictionary<string, Action<Actor>> GetExternalLabels() => new() {
        ["measure"] = Actor => {
            string What = Actor.GetVariable("what").CastString();

            Actor.SetVariable("result", What.Length);
        },
        ["caseup"] = Actor => {
            string What = Actor.GetVariable("what").CastString();

            Actor.SetVariable("result", What.ToUpperInvariant());
        },
        ["casedown"] = Actor => {
            string What = Actor.GetVariable("what").CastString();

            Actor.SetVariable("result", What.ToLowerInvariant());
        },
    };
}