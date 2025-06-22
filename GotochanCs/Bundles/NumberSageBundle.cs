namespace GotochanCs.Bundles;

/// <summary>
/// A bundle of number helper methods.
/// </summary>
public class NumberSageBundle : Bundle {
    /// <inheritdoc/>
    protected override string GetName() => "numbersage";
    /// <inheritdoc/>
    protected override Dictionary<string, Action<Actor>> GetExternalLabels() => new() {
        // Rounds a number using a rounding method.
        ["round"] = Actor => {
            double What = Actor.GetVariable("what").CastNumber();
            string? How = Actor.GetVariable("how").CastStringOrNothing();

            MidpointRounding RoundMode = How switch {
                // Round
                null or "" or "round" => MidpointRounding.AwayFromZero,
                // Ceiling
                "up" => MidpointRounding.ToPositiveInfinity,
                // Floor
                "down" => MidpointRounding.ToNegativeInfinity,
                // Truncate
                "chop" => MidpointRounding.ToZero,
                // Invalid
                _ => throw new ArgumentException($"unknown rounding method: '{How}'")
            };

            Actor.SetVariable("result", double.Round(What, RoundMode));
        },
    };
}