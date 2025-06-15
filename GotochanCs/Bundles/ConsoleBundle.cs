namespace GotochanCs.Bundles;

public class ConsoleBundle : Bundle {
    protected override string GetName() => "console";
    protected override List<Bundle> GetDependencies() => [
        new StringSageBundle(),
        new NumberSageBundle(),
    ];
    protected override Dictionary<string, Action<Actor>> GetExternalLabels() => new() {
        ["say"] = Actor => {
            string What = Actor.GetVariable("what").ToString();

            Console.Write(What);
        },
        ["complain"] = Actor => {
            string What = Actor.GetVariable("what").ToString();
            double Intensity = Actor.GetVariable("intensity").CastNumber();

            if (Intensity % 1 != 0) {
                throw new ArgumentException("complain intensity must be whole");
            }

            ConsoleColor ComplainColor = Intensity switch {
                1 => ConsoleColor.Yellow,
                2 => ConsoleColor.DarkYellow,
                3 => ConsoleColor.Red,
                4 => ConsoleColor.DarkRed,
                _ => throw new ArgumentOutOfRangeException("complain intensity must be 1 to 4")
            };

            ConsoleColor OriginalColor = Console.ForegroundColor;
            Console.ForegroundColor = ComplainColor;
            Console.Write(What);
            Console.ForegroundColor = OriginalColor;
        },
        ["fail"] = Actor => {
            string Failure = Actor.GetVariable("failure").ToString();

            throw new Exception(Failure);
        },
        ["clear"] = Actor => {
            Console.Clear();
        },
        ["stamptime"] = Actor => {
            Actor.SetVariable("timestamp", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() / 1000.0);
        },
        ["wait"] = Actor => {
            Thread.Sleep(TimeSpan.FromSeconds(Actor.GetVariable("seconds").CastNumber()));
        },
        ["eatkey"] = Actor => {
            while (true) {
                ConsoleKeyInfo KeyPressed = Console.ReadKey(true);
                if (KeyPressed.Key is ConsoleKey.Enter) {
                    Actor.SetVariable("result", "\n");
                    break;
                }
                else {
                    Actor.SetVariable("result", KeyPressed.KeyChar.ToString());
                    break;
                }
            }
        },
        ["peekkey"] = Actor => {
            Actor.SetVariable("result", Console.KeyAvailable);
        },
        ["roll"] = Actor => {
            double Lowest = Actor.GetVariable("lowest").CastNumber();
            double Highest = Actor.GetVariable("highest").CastNumber();

            Actor.SetVariable("result", Random.Shared.NextDouble() * (Highest - Lowest) + Lowest);
        },
        ["describe"] = Actor => {
            Thingie What = Actor.GetVariable("what");

            Actor.SetVariable("result", What.Type.ToString().ToLowerInvariant());
        },
    };
}