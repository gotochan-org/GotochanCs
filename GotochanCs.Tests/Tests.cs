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
    [Fact]
    public void ReadmeTest() {
        string Source = """
            counter = 10
            what = ~countdown:~
            what += counter
            what += ~\n
            goto say
            counteriszero = counter == 0
            seconds = 0.5
            goto wait
            counter -= 1
            goto +2 if counteriszero
            goto -9
            what = ~blast~off!
            goto say
            """;

        LexResult LexResult = Lexer.Lex(Source).Value;

        ParseResult ParseResult = Parser.Parse(LexResult).Value;
        ParseResult.Instructions.Count.ShouldBe(13);

        Parser.Optimize(ParseResult);

        Actor Actor = new();
        Actor.IncludePackage(new ConsoleAppPackage());
        Actor.Interpret(ParseResult).ShouldBe(Result.Success);
    }
    [Fact]
    public void LabelTest() {
        string Source = """
            counter = 1
            label loop
            counter += 1
            goto loop if counter <= 3
            """;

        LexResult LexResult = Lexer.Lex(Source).Value;

        ParseResult ParseResult = Parser.Parse(LexResult).Value;
        ParseResult.Instructions.Count.ShouldBe(4);

        Parser.Optimize(ParseResult);

        Actor Actor = new();
        Actor.IncludePackage(new ConsoleAppPackage());
        Actor.Interpret(ParseResult).ShouldBe(Result.Success);
        Actor.GetVariable("counter").ShouldBe(4);
    }
    [Fact]
    public void AnalyzeTest() {
        string Source = """
            goto iamused
            label iamused
            label iamnotused
            """;

        LexResult LexResult = Lexer.Lex(Source).Value;

        ParseResult ParseResult = Parser.Parse(LexResult).Value;
        ParseResult.Instructions.Count.ShouldBe(3);

        List<ParseAnalyzeResult> AnalyzeResults = Parser.Analyze(ParseResult);

        bool UnusedLabelSuccess = false;

        foreach (ParseAnalyzeResult AnalyzeResult in AnalyzeResults) {
            if (AnalyzeResult.Location.Line is 3 && AnalyzeResult.Analysis is ParseAnalyses.UnusedLabel) {
                UnusedLabelSuccess = true;
            }
        }

        UnusedLabelSuccess.ShouldBeTrue();
    }
}