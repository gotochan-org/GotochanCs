using System.Globalization;

namespace GotochanCs.Bundles;

/// <summary>
/// A bundle of string helper methods.
/// </summary>
public class StringSageBundle : Bundle {
    /// <inheritdoc/>
    protected override string GetName() => "stringsage";
    /// <inheritdoc/>
    protected override Dictionary<string, Action<Actor>> GetExternalLabels() => new() {
        // Returns the number of graphemes in a string.
        ["measure"] = Actor => {
            string What = Actor.GetVariable("what").CastString();

            Actor.SetVariable("result", new StringInfo(What).LengthInTextElements);
        },
        // Converts a string to uppercase.
        ["caseup"] = Actor => {
            string What = Actor.GetVariable("what").CastString();

            Actor.SetVariable("result", What.ToUpperInvariant());
        },
        // Converts a string to lowercase.
        ["casedown"] = Actor => {
            string What = Actor.GetVariable("what").CastString();

            Actor.SetVariable("result", What.ToLowerInvariant());
        },
        // Returns the Nth grapheme in a string.
        ["peekat"] = Actor => {
            string What = Actor.GetVariable("what").CastString();
            int Where = (int)Actor.GetVariable("where").CastNumber();

            Actor.SetVariable("result", new StringInfo(What).SubstringByTextElements(Where));
        },
        // Finds every appearance of a substring in a string and replaces it with another substring.
        ["swap"] = Actor => {
            string What = Actor.GetVariable("what").CastString();
            string Target = Actor.GetVariable("target").CastString();
            string Replace = Actor.GetVariable("replace").CastString();

            Actor.SetVariable("result", What.Replace(Target, Replace));
        },
        // Returns the index of a substring in a string, or nothing.
        ["find"] = Actor => {
            string What = Actor.GetVariable("what").CastString();
            string Target = Actor.GetVariable("target").CastString();

            int? Index = What.IndexOf(Target);
            if (Index < 0) {
                Index = null;
            }
            Actor.SetVariable("result", Index);
        },
    };
}