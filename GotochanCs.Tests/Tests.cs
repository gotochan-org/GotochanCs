using ResultZero;
using GotochanCs.Bundles;

namespace GotochanCs.Tests;

public class Tests {
    [Fact]
    public void BasicTest() {
        string Source = """
            counter = 10 # the value
            what = ~value:~
            what += counter
            goto say
            """;

        LexResult LexResult = Lexer.Lex(Source).Value;

        ParseResult ParseResult = Parser.Parse(LexResult).Value;
        ParseResult.Instructions.Count.ShouldBe(4);

        Parser.Optimize(ParseResult);

        Actor Actor = new(new ConsoleBundle());
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

        Actor Actor = new(new ConsoleBundle());
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

        Actor Actor = new(new ConsoleBundle());
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
    [Fact]
    public void NotTest() {
        string Source = """
            invalid = no
            goto +2 if !no
            invalid = yes
            """;

        LexResult LexResult = Lexer.Lex(Source).Value;

        ParseResult ParseResult = Parser.Parse(LexResult).Value;

        Parser.Optimize(ParseResult);

        Actor Actor = new(new ConsoleBundle());
        Actor.Interpret(ParseResult).ShouldBe(Result.Success);
        Actor.GetVariable("invalid").ShouldBe(false);
    }
    [Fact]
    public void CompileTest() {
        string Source = """
            counter = 1
            label loop
            counter += 1
            goto saycounter
            goto loop if counter <= 3

            goto +99999

            label saycounter
            what = counter
            goto say
            goto goto saycounter
            """;

        LexResult LexResult = Lexer.Lex(Source).Value;

        ParseResult ParseResult = Parser.Parse(LexResult).Value;

        Parser.Optimize(ParseResult);

        CompileResult CompileResult = Compiler.Compile(ParseResult, new CompileOptions() {
            NamespaceName = "GotochanCs.Tests",
            ClassName = "CompileOutput",
            MethodName = "Execute",
        }).Value;

        _ = CompileResult.Output;
    }
    [Fact]
    public void CompileTestOutputTest() {
        Actor Actor = new(new ConsoleBundle());

        CompileOutput.Execute(Actor).ShouldBe(Result.Success);
        Actor.GetVariable("counter").ShouldBe(4);
    }
}