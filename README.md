<img src="https://github.com/gotochan-org/GotochanCs/blob/main/Assets/GotochanIcon2x.png?raw=true">

[![NuGet](https://img.shields.io/nuget/v/GotochanCs.svg)](https://www.nuget.org/packages/GotochanCs)

# Gotochan for C#

A scripting language to power the universe.

Designed in 2023 and perfected in 2025.

## Getting Started

Let's create a basic 'hello world' program:

```gotochan
what = ~hello~world!
goto say
```

Gotochan does not use type annotations, curly braces, capital letters, or anything else that just gets in the way.

Gotochan is simple and elegant, so everything is accomplished with `goto`, `label` and `if`.

Now let's make a rocket launch.

```gotochan
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
```

It's aesthetic. It's easy. You use `goto` for everything.

#### But can you `goto` a `goto`?

Yes.

```gotochan
goto dosomething

label dosomething
goto goto dosomething
```

Goto goto goes to the last goto label for a label you've gone to.
So you can goto the line after the line of the goto for the label you wanted to go to.

#### What if you have a lot of `goto`?

That's okay. Become best friends with `goto`.

## Thingies

There are only four types of thing in Gotochan:

- `nothing`: the absence of all things
- `flag`: something that's either there or not there
- `number`: a numeric value
- `string`: a list of graphemes

These are the four thingies that make up Gotochan.

## Comments

Since Gotochan is a stylistic, elegant language, there's no difficulty in understanding a Gotochan program, so you don't need comments to describe the code. However, comments can be useful for translating to another language.

```gotochan
goto heaven # 天国に行く
```

## Interpreting

Gotochan code can be interpreted with 3 simple steps:

```cs
// Read source code
string Source = File.ReadAllText("gotogotchi.gotochan");

// Parse as instructions
ParseResult ParseResult = Parser.Parse(Source).Value;

// Interpret instructions
Actor.Shared.Interpret(ParseResult);
```

All code runs in an actor which has a lock, variables and gotos.

## Compiling

Gotochan code can be compiled to C# code for better performance.

Parse it as normal and then compile it:

```cs
// Compile as C# code
CompileResult CompileResult = Compiler.Compile(ParseResult).Value;
```

Then create a file in your C# project and paste the resulting code.

## Footnote

Brought to you by the [Konekomi Castle Gotochan Committee](https://store.steampowered.com/app/3812300).