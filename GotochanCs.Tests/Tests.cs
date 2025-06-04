using ResultZero;

namespace GotochanCs.Tests;

public class Tests {
    [Fact]
    public void BasicTest() {
        string Source = """
            counter = 10
            param = ~value:~
            param += counter
            goto say
            """;

        LexResult LexResult = Lexer.Lex(Source).Value;
        _ = LexResult;

        ParseResult ParseResult = Parser.Parse(LexResult).Value;
        ParseResult.Instructions.Count.ShouldBe(4);

        Actor Actor = new();
        Actor.Interpret(ParseResult).ShouldBe(Result.Success);
        Actor.GetVariable("param").ShouldBe("value: 10");
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