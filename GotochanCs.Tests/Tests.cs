using ResultZero;

namespace GotochanCs.Tests;

public class Tests {
    [Fact]
    public void BasicTest() {
        string Source = """
            counter = 10
            what = ~value:~
            what += counter
            goto say
            """;

        LexResult LexResult = Lexer.Lex(Source).Value;

        ParseResult ParseResult = Parser.Parse(LexResult).Value;
        ParseResult.Instructions.Count.ShouldBe(4);

        Parser.Optimize(ParseResult);

        Actor Actor = new();
        Actor.IncludePackage(new ConsoleAppPackage());
        Actor.Interpret(ParseResult).ShouldBe(Result.Success);
        Actor.GetVariable("what").ShouldBe("value: 10");
    }
    /*[Fact]
    public void ReadmeTest() {
        string Source = """
            counter = 10
            param = ~countdown:~
            param += counter
            param += ~\n
            goto say
            counteriszero = counter == 0
            param = 0.5
            goto wait
            counter -= 1
            goto +2 if counteriszero
            goto -9
            param = ~blast~off!
            goto say
            """;

        List<Instruction> Instructions = Parser.Parse(Source).Value;

        Instructions.Count.ShouldBe(13);
    }*/
}