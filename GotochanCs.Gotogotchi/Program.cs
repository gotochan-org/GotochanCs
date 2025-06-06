using ResultZero;

namespace GotochanCs.Gotogotchi;

public static class Program {
    public static void Main() {
        string Source = File.ReadAllText("gotogotchi.gotochan");

        ParseResult ParseResult = Parser.Parse(Source).Value;
        //Parser.Optimize(ParseResult);

        Actor Actor = new([new ConsoleAppPackage()]);
        Result Result = Actor.Interpret(ParseResult);

        Console.WriteLine(new string('-', 10));
        Console.WriteLine($"Result: {Result}");
        Console.ReadLine();
    }
}