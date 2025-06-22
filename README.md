<img src="https://github.com/gotochan-org/GotochanCs/blob/main/Assets/GotochanIcon2x.png?raw=true">

# Gotochan for C#

Do you hate:

- Functions?
- Reading?
- Object oriented objects?

Look no further – Gotochan is everyone's *least* favorite programming language!

Designed in 2023 and perfected in 2025.

## Getting Started

Let's make a basic hello world program!

```gotochan
what = ~hello~world!
goto say
```

Gotochan does not use type annotations, curly braces, capital letters, or anything else that just gets in the way.

Gotochan is simple and elegant, so everything is accomplished with `goto`, `label` and `if`.

Now let's make a rocket launch!

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

It's pretty, easy and neat! You just use `goto` for everything.

But can you `goto` a `goto`?

Yes!

```gotochan
goto dosomething

label dosomething
goto goto dosomething
```

Goto goto goes to the last goto label for a label you've gone to.
So you can goto the line after the line you goto'd the label you wanted to go to.

What if you have a lot of `goto`?

That's okay. Become friends with `goto`.

## Thingies

The experts educated us that there are only four types of thing in existence:

- `nothing`: the absence of all things
- `flag`: something that's either there or not there
- `number`: a tally chart on a petition to ban water
- `string`: stuff that you can say or scream

These are the thingies that make up Gotochan.

## Comments

We have no idea what a "comment" is but we saw some other languages using it so we added it.

```gotochan
# hewwo (>.<)
```

## Interpreting

You can interpret Gotochan code with 3 simple steps:

```cs
// Read source code
string Source = File.ReadAllText("gotogotchi.gotochan");

// Parse as instructions
ParseResult ParseResult = Parser.Parse(Source).Value;

// Interpret instructions
Actor.Shared.Interpret(ParseResult);
```

All code runs in an actor which has a lock, variables and gotos!

## Compiling

You can compile Gotochan code to C# code for super performance!

Parse it as normal and then compile it:

```cs
// Compile as C# code
CompileResult CompileResult = Compiler.Compile(ParseResult).Value;
```

Then create a file in your C# project and paste the resulting code!

## Footnote

This language was brought to you by the Gotochan Committee of Konekomi Castle.
Come visit us on [Steam](https://store.steampowered.com/app/3812300)!