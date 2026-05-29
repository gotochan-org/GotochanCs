using System.Buffers;
using System.Text;

namespace GotochanCs.Bundles;

/// <summary>
/// A bundle of string helper methods. The great sage of strings.
/// </summary>
public class StringSageBundle : Bundle {
    /// <inheritdoc/>
    protected override string GetName() => "stringsage";
    /// <inheritdoc/>
    protected override Dictionary<string, Action<Actor>> GetExternalLabels() => new() {
        // Returns the number of runes in a string.
        ["measure"] = Actor => {
            string What = Actor.GetVariable("what").CastString();

            int RuneCount = GetRuneCount(What);

            Actor.SetVariable("result", RuneCount);
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
        // Returns the Nth rune in a string.
        ["peekat"] = Actor => {
            string What = Actor.GetVariable("what").CastString();
            int Where = (int)Actor.GetVariable("where").CastNumber();

            if (Where < 0) {
                throw new ArgumentException("where must be positive or zero");
            }

            Actor.SetVariable("result", GetRuneAtRunePosition(What, Where)?.ToString());
        },
        // Finds every appearance of a substring in a string and replaces it with another substring.
        ["swap"] = Actor => {
            string What = Actor.GetVariable("what").CastString();
            string Target = Actor.GetVariable("target").CastString();
            string Replace = Actor.GetVariable("replace").CastString();

            Actor.SetVariable("result", What.Replace(Target, Replace, StringComparison.Ordinal));
        },
        // Returns the rune position of a substring in a string, or nothing.
        ["find"] = Actor => {
            string What = Actor.GetVariable("what").CastString();
            string Target = Actor.GetVariable("target").CastString();

            int RunePositionOfTarget = GetRunePositionOfSubstring(What, Target);

            if (RunePositionOfTarget >= 0) {
                Actor.SetVariable("result", RunePositionOfTarget);
            }
            else {
                Actor.SetVariable("result", (int?)null);
            }
        },
    };

    private static int GetRuneCount(scoped ReadOnlySpan<char> Input) {
        int RuneCount = 0;
        int Index = 0;
        while (true) {
            if (Rune.DecodeFromUtf16(Input[Index..], out Rune _, out int CharsConsumed) is not OperationStatus.Done) {
                return RuneCount;
            }
            Index += CharsConsumed;
            RuneCount++;
        }
    }
    private static int GetRunePositionOfSubstring(scoped ReadOnlySpan<char> Input, scoped ReadOnlySpan<char> Target) {
        int RunePosition = 0;
        int Index = 0;
        while (true) {
            if (Input[Index..].StartsWith(Target, StringComparison.Ordinal)) {
                return RunePosition;
            }
            if (Rune.DecodeFromUtf16(Input[Index..], out Rune _, out int CharsConsumed) is not OperationStatus.Done) {
                return -1;
            }
            Index += CharsConsumed;
            RunePosition++;
        }
    }
    private static Rune? GetRuneAtRunePosition(scoped ReadOnlySpan<char> Input, int TargetRunePosition) {
        ArgumentOutOfRangeException.ThrowIfNegative(TargetRunePosition);

        int RunePosition = 0;
        int Index = 0;
        while (true) {
            if (Rune.DecodeFromUtf16(Input[Index..], out Rune CurrentRune, out int CharsConsumed) is not OperationStatus.Done) {
                return null;
            }
            if (RunePosition == TargetRunePosition) {
                return CurrentRune;
            }
            Index += CharsConsumed;
            RunePosition++;
        }
    }
}