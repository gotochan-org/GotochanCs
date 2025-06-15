using System.Globalization;

namespace GotochanCs.Bundles;

public class StringSageBundle : Bundle {
    protected override string GetName() => "stringsage";
    protected override Dictionary<string, Action<Actor>> GetExternalLabels() => new() {
        ["measure"] = Actor => {
            string What = Actor.GetVariable("what").CastString();

            Actor.SetVariable("result", new StringInfo(What).LengthInTextElements);
        },
        ["caseup"] = Actor => {
            string What = Actor.GetVariable("what").CastString();

            Actor.SetVariable("result", What.ToUpperInvariant());
        },
        ["casedown"] = Actor => {
            string What = Actor.GetVariable("what").CastString();

            Actor.SetVariable("result", What.ToLowerInvariant());
        },
        ["peekat"] = Actor => {
            string What = Actor.GetVariable("what").CastString();
            int Where = (int)Actor.GetVariable("where").CastNumber();

            Actor.SetVariable("result", new StringInfo(What).SubstringByTextElements(Where));
        },
        ["swap"] = Actor => {
            string What = Actor.GetVariable("what").CastString();
            string Target = Actor.GetVariable("target").CastString();
            string Replace = Actor.GetVariable("replace").CastString();

            Actor.SetVariable("result", What.Replace(Target, Replace));
        },
        ["find"] = Actor => {
            string What = Actor.GetVariable("what").CastString();
            string Target = Actor.GetVariable("target").CastString();

            int? Index = What.IndexOf(Target);
            if (Index < 0) {
                Index = null;
            }
            Actor.SetVariable("target", Index);
        },
    };
}