using ResultZero;
using GotochanCs.Bundles;

namespace GotochanCs.Gotogotchi;

public static class Program {
    public static void Main() {
        string Source = File.ReadAllText("gotogotchi.gotochan");

        ParseResult ParseResult = Parser.Parse(Source).Value;
        Parser.Optimize(ParseResult);

        Actor Actor = new(new ConsoleBundle());

        Result<CompileResult> CompileResult = Compiler.Compile(ParseResult);
        _ = CompileResult;

        Result Result = CompileOutput.Execute(Actor);
        //Result Result = Actor.Interpret(ParseResult);

        Console.WriteLine(new string('-', 10));
        Console.WriteLine($"Result: {Result}");
        Console.ReadLine();
    }
}