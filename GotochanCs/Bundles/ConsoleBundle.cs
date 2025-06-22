namespace GotochanCs.Bundles;

/// <summary>
/// A bundle of methods for a console application.
/// </summary>
public class ConsoleBundle : Bundle {
    /// <inheritdoc/>
    protected override string GetName() => "console";
    /// <inheritdoc/>
    protected override List<Bundle> GetDependencies() => [
        new StringSageBundle(),
        new NumberSageBundle(),
    ];
    /// <inheritdoc/>
    protected override Dictionary<string, Action<Actor>> GetExternalLabels() => new() {
        // Writes a thingie to the console.
        ["say"] = Actor => {
            string What = Actor.GetVariable("what").ToString();

            Console.Write(What);
        },
        // Writes a thingie to the console with a warning or error color.
        ["complain"] = Actor => {
            string What = Actor.GetVariable("what").ToString();
            double Intensity = Actor.GetVariable("intensity").CastNumber();

            ConsoleColor ComplainColor = Intensity switch {
                >= 4 => ConsoleColor.DarkRed,
                >= 3 => ConsoleColor.Red,
                >= 2 => ConsoleColor.DarkYellow,
                >= 1 => ConsoleColor.Yellow,
                _ => throw new ArgumentOutOfRangeException("complain intensity must be 1 to 4")
            };

            ConsoleColor OriginalColor = Console.ForegroundColor;
            Console.ForegroundColor = ComplainColor;
            Console.Write(What);
            Console.ForegroundColor = OriginalColor;
        },
        // Raises an error.
        ["fail"] = Actor => {
            string Failure = Actor.GetVariable("failure").ToString();

            throw new Exception(Failure);
        },
        // Clears the console.
        ["clear"] = Actor => {
            Console.Clear();
        },
        // Returns a UNIX timestamp (the number of seconds since 1970/01/01).
        ["stamptime"] = Actor => {
            Actor.SetVariable("timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000.0);
        },
        // Sleeps for a number of seconds.
        ["wait"] = Actor => {
            double Seconds = Actor.GetVariable("seconds").CastNumber();

            Thread.Sleep(TimeSpan.FromSeconds(Seconds));
        },
        // Waits for a key stroke and returns the name of the key.
        ["eatkey"] = Actor => {
            ConsoleKeyInfo KeyPressed = Console.ReadKey(true);
            Actor.SetVariable("result", KeyPressed.Key.ToString().ToLowerInvariant());
        },
        // Returns whether a key stroke is available.
        ["peekkey"] = Actor => {
            Actor.SetVariable("result", Console.KeyAvailable);
        },
        // Waits for a line of input and returns the input.
        ["eatline"] = Actor => {
            string LineEntered = Console.ReadLine() ?? "";
            Actor.SetVariable("result", LineEntered);
        },
        // Returns a random fractional number between lowest and highest.
        ["roll"] = Actor => {
            double Lowest = Actor.GetVariable("lowest").CastNumber();
            double Highest = Actor.GetVariable("highest").CastNumber();

            Actor.SetVariable("result", Random.Shared.NextDouble() * (Highest - Lowest) + Lowest);
        },
        // Returns the type of the thingie as a string.
        ["describe"] = Actor => {
            Thingie What = Actor.GetVariable("what");

            Actor.SetVariable("result", What.Type.ToString().ToLowerInvariant());
        },
    };
}