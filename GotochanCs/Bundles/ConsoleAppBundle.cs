namespace GotochanCs.Bundles;

public class ConsoleAppBundle : Bundle {
    public override string Name { get; } = "consoleapp";
    public override Dictionary<string, Thingie> Options { get; } = [];
    public override Dictionary<string, Action<Actor>> ExternalLabels { get; } = [];

    public ConsoleAppBundle() {
        ExternalLabels = GetExternalLabels();
    }

    private Dictionary<string, Action<Actor>> GetExternalLabels() => new() {
        ["say"] = Actor => {
            string What = Actor.GetVariable("what").ToString();

            Console.Write(What);
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
        ["fail"] = Actor => {
            string Failure = Actor.GetVariable("failure").ToString();

            throw new Exception(Failure);
        },
        ["describe"] = Actor => {
            Thingie What = Actor.GetVariable("what");

            Actor.SetVariable("result", What.Type.ToString().ToLowerInvariant());
        },
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
        ["truncate"] = Actor => {
            double What = Actor.GetVariable("what").CastNumber();

            Actor.SetVariable("result", double.Truncate(What));
        },
        ["round"] = Actor => {
            double What = Actor.GetVariable("what").CastNumber();

            Actor.SetVariable("result", double.Round(What, MidpointRounding.AwayFromZero));
        },
        ["floor"] = Actor => {
            double What = Actor.GetVariable("what").CastNumber();

            Actor.SetVariable("result", double.Floor(What));
        },
        ["ceiling"] = Actor => {
            double What = Actor.GetVariable("what").CastNumber();

            Actor.SetVariable("result", double.Ceiling(What));
        },
    };
}