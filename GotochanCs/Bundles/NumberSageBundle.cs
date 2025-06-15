namespace GotochanCs.Bundles;

public class NumberSageBundle : Bundle {
    protected override string GetName() => "numbersage";
    protected override Dictionary<string, Action<Actor>> GetExternalLabels() => new() {
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