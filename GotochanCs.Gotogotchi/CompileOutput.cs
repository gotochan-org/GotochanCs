#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable IDE0005 // Using directive is unnecessary
#pragma warning disable CS0162 // Unreachable code detected
#pragma warning disable CS0168 // Variable is declared but never used
#pragma warning disable IDE0059 // Unnecessary assignment of a value
#pragma warning restore IDE0079 // Remove unnecessary suppression

using System;
using System.Collections.Generic;
using GotochanCs;
using ResultZero;

public static partial class CompileOutput {
    public static Result Execute(Actor Actor) {
        int GotoLabelSwitchIdentifier;

        lock (Actor.Lock) {
        Index1:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(15);
                Actor.SetVariable(@"screenwidth", Temporary1.Value);
            }

        Index2:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(10);
                Actor.SetVariable(@"screenheight", Temporary1.Value);
            }

        Index3:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(8);
                Actor.SetVariable(@"dialogueline", Temporary1.Value);
            }

        Index4:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(9);
                Actor.SetVariable(@"dialoguetwoline", Temporary1.Value);
            }

        Index5:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(5);
                Actor.SetVariable(@"petline", Temporary1.Value);
            }

        Index6:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(6);
                Actor.SetVariable(@"pettwoline", Temporary1.Value);
            }

        Index7:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(1);
                Actor.SetVariable(@"hungerline", Temporary1.Value);
            }

        Index8:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(2);
                Actor.SetVariable(@"thirstline", Temporary1.Value);
            }

        Index9:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(3);
                Actor.SetVariable(@"funline", Temporary1.Value);
            }

        Index10:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Flag(true);
                Actor.SetVariable(@"intutorial", Temporary1.Value);
            }

        Index11:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(100);
                Actor.SetVariable(@"hunger", Temporary1.Value);
            }

        Index12:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(100);
                Actor.SetVariable(@"thirst", Temporary1.Value);
            }

        Index13:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(100);
                Actor.SetVariable(@"fun", Temporary1.Value);
            }

        Index14:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"");
                Actor.SetVariable(@"pet", Temporary1.Value);
            }

        Index15:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"");
                Actor.SetVariable(@"pettwo", Temporary1.Value);
            }

        Index16:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"welcome.");
                Actor.SetVariable(@"dialogue", Temporary1.Value);
            }

        Index17:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"v2.0");
                Actor.SetVariable(@"dialoguetwo", Temporary1.Value);
            }

        Index18:
            {
                Actor.SetGotoLabelIndex(@"displaytutorialmessage", 17);
                goto Label18;
            }

        Index19:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"take care of");
                Actor.SetVariable(@"dialogue", Temporary1.Value);
            }

        Index20:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"your neko.");
                Actor.SetVariable(@"dialoguetwo", Temporary1.Value);
            }

        Index21:
            {
                Actor.SetGotoLabelIndex(@"displaytutorialmessage", 20);
                goto Label18;
            }

        Index22:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"press A to");
                Actor.SetVariable(@"dialogue", Temporary1.Value);
            }

        Index23:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"feed neko.");
                Actor.SetVariable(@"dialoguetwo", Temporary1.Value);
            }

        Index24:
            {
                Actor.SetGotoLabelIndex(@"displaytutorialmessage", 23);
                goto Label18;
            }

        Index25:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"press B to");
                Actor.SetVariable(@"dialogue", Temporary1.Value);
            }

        Index26:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"water neko.");
                Actor.SetVariable(@"dialoguetwo", Temporary1.Value);
            }

        Index27:
            {
                Actor.SetGotoLabelIndex(@"displaytutorialmessage", 26);
                goto Label18;
            }

        Index28:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"press C to");
                Actor.SetVariable(@"dialogue", Temporary1.Value);
            }

        Index29:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"play w/ neko.");
                Actor.SetVariable(@"dialoguetwo", Temporary1.Value);
            }

        Index30:
            {
                Actor.SetGotoLabelIndex(@"displaytutorialmessage", 29);
                goto Label18;
            }

        Index31:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"keep numbers");
                Actor.SetVariable(@"dialogue", Temporary1.Value);
            }

        Index32:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"above 0.");
                Actor.SetVariable(@"dialoguetwo", Temporary1.Value);
            }

        Index33:
            {
                Actor.SetGotoLabelIndex(@"displaytutorialmessage", 32);
                goto Label18;
            }

        Index34:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Flag(false);
                Actor.SetVariable(@"intutorial", Temporary1.Value);
            }

        Index35:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"^ ^");
                Actor.SetVariable(@"pet", Temporary1.Value);
            }

        Index36:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"(>.<)");
                Actor.SetVariable(@"pettwo", Temporary1.Value);
            }

        Index37:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"");
                Actor.SetVariable(@"dialogue", Temporary1.Value);
            }

        Index38:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@" A  B  C ");
                Actor.SetVariable(@"dialoguetwo", Temporary1.Value);
            }

        Index39:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(54, 1), @"stamptime");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index40:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"timestamp");
                Actor.SetVariable(@"timelastreducedstats", Temporary1.Value);
            }

        Index41:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Flag(true);
                Actor.SetVariable(@"redraw", Temporary1.Value);
            }

        Index42:
        Label1:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"redraw");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"58: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    Result Temporary2;
                    Temporary2 = Actor.GotoExternalLabel(new SourceLocation(58, 3), @"clear");
                    if (Temporary2.IsError) {
                        return Temporary2.Error;
                    }
                }
            }

        Index43:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"redraw");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"59: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    Actor.SetGotoLabelIndex(@"drawgrid", 42);
                    goto Label3;
                }
            }

        Index44:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(0.15);
                Actor.SetVariable(@"seconds", Temporary1.Value);
            }

        Index45:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(62, 3), @"wait");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index46:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Flag(false);
                Actor.SetVariable(@"redraw", Temporary1.Value);
            }

        Index47:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(66, 3), @"stamptime");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index48:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"timestamp");
                Actor.SetVariable(@"currenttime", Temporary1.Value);
            }

        Index49:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"timestamp");
                Temporary3 = Actor.GetVariable(@"timelastreducedstats");
                Temporary1 = Thingie.Subtract(new SourceLocation(68, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"timestamp", Temporary1.Value);
            }

        Index50:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"timestamp");
                Temporary3 = Thingie.Number(5);
                Temporary1 = Thingie.LessThan(new SourceLocation(69, 26), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"nottimetoreducestats", Temporary1.Value);
            }

        Index51:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"nottimetoreducestats");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"70: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index57;
                }
            }

        Index52:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"currenttime");
                Actor.SetVariable(@"timelastreducedstats", Temporary1.Value);
            }

        Index53:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Flag(true);
                Actor.SetVariable(@"redraw", Temporary1.Value);
            }

        Index54:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"hunger");
                Temporary3 = Thingie.Number(2);
                Temporary1 = Thingie.Subtract(new SourceLocation(73, 13), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"hunger", Temporary1.Value);
            }

        Index55:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"thirst");
                Temporary3 = Thingie.Number(3);
                Temporary1 = Thingie.Subtract(new SourceLocation(74, 13), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"thirst", Temporary1.Value);
            }

        Index56:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"fun");
                Temporary3 = Thingie.Number(1);
                Temporary1 = Thingie.Subtract(new SourceLocation(75, 10), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"fun", Temporary1.Value);
            }

        Index57:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(77, 3), @"peekkey");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index58:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"result");
                Temporary3 = Thingie.Flag(false);
                Temporary1 = Thingie.Equals(new SourceLocation(78, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"hasnoinput", Temporary1.Value);
            }

        Index59:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"hasnoinput");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"80: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    Actor.SetGotoLabelIndex(@"endofinput", 58);
                    goto Label2;
                }
            }

        Index60:
            {
                Actor.SetGotoLabelIndex(@"lastinput", 59);
                goto Label16;
            }

        Index61:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"result");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index62:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(83, 5), @"casedown");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index63:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"result");
                Temporary3 = Thingie.String(@"a");
                Temporary1 = Thingie.NotEquals(new SourceLocation(85, 22), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"inputisnotfeed", Temporary1.Value);
            }

        Index64:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"inputisnotfeed");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"86: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index67;
                }
            }

        Index65:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Flag(true);
                Actor.SetVariable(@"redraw", Temporary1.Value);
            }

        Index66:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"hunger");
                Temporary3 = Thingie.Number(5);
                Temporary1 = Thingie.Add(new SourceLocation(88, 15), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"hunger", Temporary1.Value);
            }

        Index67:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"result");
                Temporary3 = Thingie.String(@"b");
                Temporary1 = Thingie.NotEquals(new SourceLocation(89, 23), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"inputisnotwater", Temporary1.Value);
            }

        Index68:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"inputisnotwater");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"90: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index71;
                }
            }

        Index69:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Flag(true);
                Actor.SetVariable(@"redraw", Temporary1.Value);
            }

        Index70:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"thirst");
                Temporary3 = Thingie.Number(8);
                Temporary1 = Thingie.Add(new SourceLocation(92, 15), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"thirst", Temporary1.Value);
            }

        Index71:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"result");
                Temporary3 = Thingie.String(@"c");
                Temporary1 = Thingie.NotEquals(new SourceLocation(93, 22), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"inputisnotplay", Temporary1.Value);
            }

        Index72:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"inputisnotplay");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"94: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index75;
                }
            }

        Index73:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Flag(true);
                Actor.SetVariable(@"redraw", Temporary1.Value);
            }

        Index74:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"fun");
                Temporary3 = Thingie.Number(15);
                Temporary1 = Thingie.Add(new SourceLocation(96, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"fun", Temporary1.Value);
            }

        Index75:
        Label2:
            {
                Actor.SetGotoLabelIndex(@"clampstats", 74);
                goto Label13;
            }

        Index76:
            {
                Actor.SetGotoLabelIndex(@"checkdeath", 75);
                goto Label14;
            }

        Index77:
            {
                Actor.SetGotoLabelIndex(@"gameloop", 76);
                goto Label1;
            }

        Index78:
            {
                goto EndLabel;
            }

        Index79:
        Label3:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(0);
                Actor.SetVariable(@"y", Temporary1.Value);
            }

        Index80:
        Label4:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"y");
                Temporary3 = Thingie.Number(1);
                Temporary1 = Thingie.Add(new SourceLocation(110, 10), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"y", Temporary1.Value);
            }

        Index81:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(0);
                Actor.SetVariable(@"x", Temporary1.Value);
            }

        Index82:
            {
                Actor.SetGotoLabelIndex(@"displaytitle", 81);
                goto Label17;
            }

        Index83:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"y");
                Temporary3 = Actor.GetVariable(@"dialogueline");
                Temporary1 = Thingie.NotEquals(new SourceLocation(113, 28), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"yisnotatdialogueline", Temporary1.Value);
            }

        Index84:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"yisnotatdialogueline");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"114: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index88;
                }
            }

        Index85:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"dialogue");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index86:
            {
                Actor.SetGotoLabelIndex(@"centertext", 85);
                goto Label7;
            }

        Index87:
            {
                Actor.SetGotoLabelIndex(@"endofforx", 86);
                goto Label6;
            }

        Index88:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"y");
                Temporary3 = Actor.GetVariable(@"dialoguetwoline");
                Temporary1 = Thingie.NotEquals(new SourceLocation(118, 31), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"yisnotatdialoguetwoline", Temporary1.Value);
            }

        Index89:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"yisnotatdialoguetwoline");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"119: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index93;
                }
            }

        Index90:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"dialoguetwo");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index91:
            {
                Actor.SetGotoLabelIndex(@"centertext", 90);
                goto Label7;
            }

        Index92:
            {
                Actor.SetGotoLabelIndex(@"endofforx", 91);
                goto Label6;
            }

        Index93:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"y");
                Temporary3 = Actor.GetVariable(@"petline");
                Temporary1 = Thingie.NotEquals(new SourceLocation(123, 23), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"yisnotatpetline", Temporary1.Value);
            }

        Index94:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"yisnotatpetline");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"124: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index98;
                }
            }

        Index95:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"pet");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index96:
            {
                Actor.SetGotoLabelIndex(@"centertext", 95);
                goto Label7;
            }

        Index97:
            {
                Actor.SetGotoLabelIndex(@"endofforx", 96);
                goto Label6;
            }

        Index98:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"y");
                Temporary3 = Actor.GetVariable(@"pettwoline");
                Temporary1 = Thingie.NotEquals(new SourceLocation(128, 26), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"yisnotatpettwoline", Temporary1.Value);
            }

        Index99:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"yisnotatpettwoline");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"129: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index103;
                }
            }

        Index100:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"pettwo");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index101:
            {
                Actor.SetGotoLabelIndex(@"centertext", 100);
                goto Label7;
            }

        Index102:
            {
                Actor.SetGotoLabelIndex(@"endofforx", 101);
                goto Label6;
            }

        Index103:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"intutorial");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"133: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    Actor.SetGotoLabelIndex(@"forx", 102);
                    goto Label5;
                }
            }

        Index104:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"y");
                Temporary3 = Actor.GetVariable(@"hungerline");
                Temporary1 = Thingie.NotEquals(new SourceLocation(134, 28), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"yisnotathungerline", Temporary1.Value);
            }

        Index105:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"yisnotathungerline");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"135: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index111;
                }
            }

        Index106:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"o-<: ");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index107:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"what");
                Temporary3 = Actor.GetVariable(@"hunger");
                Temporary1 = Thingie.Add(new SourceLocation(137, 17), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index108:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"what");
                Temporary3 = Thingie.String(@" ");
                Temporary1 = Thingie.Add(new SourceLocation(138, 17), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index109:
            {
                Actor.SetGotoLabelIndex(@"leftaligntext", 108);
                goto Label8;
            }

        Index110:
            {
                Actor.SetGotoLabelIndex(@"endofforx", 109);
                goto Label6;
            }

        Index111:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"y");
                Temporary3 = Actor.GetVariable(@"thirstline");
                Temporary1 = Thingie.NotEquals(new SourceLocation(141, 28), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"yisnotatthirstline", Temporary1.Value);
            }

        Index112:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"yisnotatthirstline");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"142: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index118;
                }
            }

        Index113:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"|_|: ");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index114:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"what");
                Temporary3 = Actor.GetVariable(@"thirst");
                Temporary1 = Thingie.Add(new SourceLocation(144, 17), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index115:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"what");
                Temporary3 = Thingie.String(@" ");
                Temporary1 = Thingie.Add(new SourceLocation(145, 17), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index116:
            {
                Actor.SetGotoLabelIndex(@"leftaligntext", 115);
                goto Label8;
            }

        Index117:
            {
                Actor.SetGotoLabelIndex(@"endofforx", 116);
                goto Label6;
            }

        Index118:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"y");
                Temporary3 = Actor.GetVariable(@"funline");
                Temporary1 = Thingie.NotEquals(new SourceLocation(148, 25), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"yisnotatfunline", Temporary1.Value);
            }

        Index119:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"yisnotatfunline");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"149: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index125;
                }
            }

        Index120:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"^_^: ");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index121:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"what");
                Temporary3 = Actor.GetVariable(@"fun");
                Temporary1 = Thingie.Add(new SourceLocation(151, 17), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index122:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"what");
                Temporary3 = Thingie.String(@" ");
                Temporary1 = Thingie.Add(new SourceLocation(152, 17), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index123:
            {
                Actor.SetGotoLabelIndex(@"leftaligntext", 122);
                goto Label8;
            }

        Index124:
            {
                Actor.SetGotoLabelIndex(@"endofforx", 123);
                goto Label6;
            }

        Index125:
        Label5:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"x");
                Temporary3 = Thingie.Number(1);
                Temporary1 = Thingie.Add(new SourceLocation(156, 14), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"x", Temporary1.Value);
            }

        Index126:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"#");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index127:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(158, 9), @"say");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index128:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"x");
                Temporary3 = Actor.GetVariable(@"screenwidth");
                Temporary1 = Thingie.NotEquals(new SourceLocation(159, 25), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"xisnotatlimit", Temporary1.Value);
            }

        Index129:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"xisnotatlimit");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"160: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    Actor.SetGotoLabelIndex(@"forx", 128);
                    goto Label5;
                }
            }

        Index130:
        Label6:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"
");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index131:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(163, 5), @"say");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index132:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"y");
                Temporary3 = Actor.GetVariable(@"screenheight");
                Temporary1 = Thingie.NotEquals(new SourceLocation(164, 21), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"yisnotatlimit", Temporary1.Value);
            }

        Index133:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"yisnotatlimit");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"165: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    Actor.SetGotoLabelIndex(@"fory", 132);
                    goto Label4;
                }
            }

        Index134:
            {
                int Temporary1;
                Temporary1 = Actor.GetGotoLabelIndex(@"drawgrid");
                if (Temporary1 < 0) {
                    return new Error(@"166: no entry for goto label: 'drawgrid'");
                }
                GotoLabelSwitchIdentifier = Temporary1 + 1 + 1;
                goto GotoLabelSwitch;
            }

        Index135:
        Label7:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"what");
                Actor.SetVariable(@"texttocenter", Temporary1.Value);
            }

        Index136:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"screenwidth");
                Actor.SetVariable(@"numberofhashtags", Temporary1.Value);
            }

        Index137:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(171, 3), @"measure");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index138:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"result");
                Actor.SetVariable(@"lengthoftext", Temporary1.Value);
            }

        Index139:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"numberofhashtags");
                Temporary3 = Actor.GetVariable(@"lengthoftext");
                Temporary1 = Thingie.Subtract(new SourceLocation(173, 23), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"numberofhashtags", Temporary1.Value);
            }

        Index140:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"numberofhashtags");
                Temporary3 = Thingie.Number(2);
                Temporary1 = Thingie.Divide(new SourceLocation(174, 23), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"numberofhashtags", Temporary1.Value);
            }

        Index141:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"numberofhashtags");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index142:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"chop");
                Actor.SetVariable(@"how", Temporary1.Value);
            }

        Index143:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(177, 3), @"round");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index144:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"result");
                Actor.SetVariable(@"numberofhashtags", Temporary1.Value);
            }

        Index145:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(0);
                Actor.SetVariable(@"numberofhashtagsprinted", Temporary1.Value);
            }

        Index146:
            {
                Actor.SetGotoLabelIndex(@"wraphashtags", 145);
                goto Label9;
            }

        Index147:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"texttocenter");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index148:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(183, 3), @"say");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index149:
            {
                Actor.SetGotoLabelIndex(@"wraphashtags", 148);
                goto Label9;
            }

        Index150:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"lengthoftext");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index151:
            {
                Actor.SetGotoLabelIndex(@"iseven", 150);
                goto Label12;
            }

        Index152:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"result");
                Temporary3 = Thingie.Flag(false);
                Temporary1 = Thingie.Equals(new SourceLocation(189, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"result", Temporary1.Value);
            }

        Index153:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"result");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"190: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index156;
                }
            }

        Index154:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"#");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index155:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(192, 3), @"say");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index156:
            {
                int Temporary1;
                Temporary1 = Actor.GetGotoLabelIndex(@"centertext");
                if (Temporary1 < 0) {
                    return new Error(@"193: no entry for goto label: 'centertext'");
                }
                GotoLabelSwitchIdentifier = Temporary1 + 1 + 1;
                goto GotoLabelSwitch;
            }

        Index157:
        Label8:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(196, 3), @"measure");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index158:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"result");
                Actor.SetVariable(@"lengthoftext", Temporary1.Value);
            }

        Index159:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(198, 3), @"say");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index160:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"screenwidth");
                Actor.SetVariable(@"numberofhashtags", Temporary1.Value);
            }

        Index161:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"numberofhashtags");
                Temporary3 = Actor.GetVariable(@"lengthoftext");
                Temporary1 = Thingie.Subtract(new SourceLocation(200, 23), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"numberofhashtags", Temporary1.Value);
            }

        Index162:
            {
                Actor.SetGotoLabelIndex(@"wraphashtags", 161);
                goto Label9;
            }

        Index163:
            {
                int Temporary1;
                Temporary1 = Actor.GetGotoLabelIndex(@"leftaligntext");
                if (Temporary1 < 0) {
                    return new Error(@"202: no entry for goto label: 'leftaligntext'");
                }
                GotoLabelSwitchIdentifier = Temporary1 + 1 + 1;
                goto GotoLabelSwitch;
            }

        Index164:
        Label9:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(0);
                Actor.SetVariable(@"counter", Temporary1.Value);
            }

        Index165:
        Label10:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"counter");
                Temporary3 = Thingie.Number(1);
                Temporary1 = Thingie.Add(new SourceLocation(207, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"counter", Temporary1.Value);
            }

        Index166:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"#");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index167:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(209, 5), @"say");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index168:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"numberofhashtagsprinted");
                Temporary3 = Thingie.Number(1);
                Temporary1 = Thingie.Add(new SourceLocation(210, 32), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"numberofhashtagsprinted", Temporary1.Value);
            }

        Index169:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"counter");
                Temporary3 = Actor.GetVariable(@"numberofhashtags");
                Temporary1 = Thingie.Equals(new SourceLocation(211, 33), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"counterisnumberofhashtags", Temporary1.Value);
            }

        Index170:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"counterisnumberofhashtags");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"212: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    Actor.SetGotoLabelIndex(@"endofforcounter", 169);
                    goto Label11;
                }
            }

        Index171:
            {
                Actor.SetGotoLabelIndex(@"forcounter", 170);
                goto Label10;
            }

        Index172:
        Label11:
            {
                int Temporary1;
                Temporary1 = Actor.GetGotoLabelIndex(@"wraphashtags");
                if (Temporary1 < 0) {
                    return new Error(@"215: no entry for goto label: 'wraphashtags'");
                }
                GotoLabelSwitchIdentifier = Temporary1 + 1 + 1;
                goto GotoLabelSwitch;
            }

        Index173:
        Label12:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"what");
                Temporary3 = Thingie.Number(2);
                Temporary1 = Thingie.Divide(new SourceLocation(218, 11), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index174:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"what");
                Actor.SetVariable(@"nontruncated", Temporary1.Value);
            }

        Index175:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"chop");
                Actor.SetVariable(@"how", Temporary1.Value);
            }

        Index176:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(221, 3), @"round");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index177:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"result");
                Temporary3 = Actor.GetVariable(@"nontruncated");
                Temporary1 = Thingie.NotEquals(new SourceLocation(222, 24), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"truncatedisnotsame", Temporary1.Value);
            }

        Index178:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"truncatedisnotsame");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"223: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index181;
                }
            }

        Index179:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Flag(true);
                Actor.SetVariable(@"result", Temporary1.Value);
            }

        Index180:
            {
                goto Index182;
            }

        Index181:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Flag(false);
                Actor.SetVariable(@"result", Temporary1.Value);
            }

        Index182:
            {
                int Temporary1;
                Temporary1 = Actor.GetGotoLabelIndex(@"iseven");
                if (Temporary1 < 0) {
                    return new Error(@"227: no entry for goto label: 'iseven'");
                }
                GotoLabelSwitchIdentifier = Temporary1 + 1 + 1;
                goto GotoLabelSwitch;
            }

        Index183:
        Label13:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"hunger");
                Temporary3 = Thingie.Number(0);
                Temporary1 = Thingie.GreaterThanOrEqualTo(new SourceLocation(230, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"comparison", Temporary1.Value);
            }

        Index184:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"comparison");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"231: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index186;
                }
            }

        Index185:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(0);
                Actor.SetVariable(@"hunger", Temporary1.Value);
            }

        Index186:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"hunger");
                Temporary3 = Thingie.Number(100);
                Temporary1 = Thingie.LessThanOrEqualTo(new SourceLocation(233, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"comparison", Temporary1.Value);
            }

        Index187:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"comparison");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"234: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index189;
                }
            }

        Index188:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(100);
                Actor.SetVariable(@"hunger", Temporary1.Value);
            }

        Index189:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"thirst");
                Temporary3 = Thingie.Number(0);
                Temporary1 = Thingie.GreaterThanOrEqualTo(new SourceLocation(236, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"comparison", Temporary1.Value);
            }

        Index190:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"comparison");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"237: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index192;
                }
            }

        Index191:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(0);
                Actor.SetVariable(@"thirst", Temporary1.Value);
            }

        Index192:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"thirst");
                Temporary3 = Thingie.Number(100);
                Temporary1 = Thingie.LessThanOrEqualTo(new SourceLocation(239, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"comparison", Temporary1.Value);
            }

        Index193:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"comparison");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"240: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index195;
                }
            }

        Index194:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(100);
                Actor.SetVariable(@"thirst", Temporary1.Value);
            }

        Index195:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"fun");
                Temporary3 = Thingie.Number(0);
                Temporary1 = Thingie.GreaterThanOrEqualTo(new SourceLocation(242, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"comparison", Temporary1.Value);
            }

        Index196:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"comparison");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"243: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index198;
                }
            }

        Index197:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(0);
                Actor.SetVariable(@"fun", Temporary1.Value);
            }

        Index198:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"fun");
                Temporary3 = Thingie.Number(100);
                Temporary1 = Thingie.LessThanOrEqualTo(new SourceLocation(245, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"comparison", Temporary1.Value);
            }

        Index199:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"comparison");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"246: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index201;
                }
            }

        Index200:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(100);
                Actor.SetVariable(@"fun", Temporary1.Value);
            }

        Index201:
            {
                int Temporary1;
                Temporary1 = Actor.GetGotoLabelIndex(@"clampstats");
                if (Temporary1 < 0) {
                    return new Error(@"248: no entry for goto label: 'clampstats'");
                }
                GotoLabelSwitchIdentifier = Temporary1 + 1 + 1;
                goto GotoLabelSwitch;
            }

        Index202:
        Label14:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"hunger");
                Temporary3 = Thingie.Number(0);
                Temporary1 = Thingie.Equals(new SourceLocation(251, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"comparison", Temporary1.Value);
            }

        Index203:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"comparison");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"252: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    Actor.SetGotoLabelIndex(@"death", 202);
                    goto Label15;
                }
            }

        Index204:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"thirst");
                Temporary3 = Thingie.Number(0);
                Temporary1 = Thingie.Equals(new SourceLocation(253, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"comparison", Temporary1.Value);
            }

        Index205:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"comparison");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"254: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    Actor.SetGotoLabelIndex(@"death", 204);
                    goto Label15;
                }
            }

        Index206:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"fun");
                Temporary3 = Thingie.Number(0);
                Temporary1 = Thingie.Equals(new SourceLocation(255, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"comparison", Temporary1.Value);
            }

        Index207:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"comparison");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"256: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    Actor.SetGotoLabelIndex(@"death", 206);
                    goto Label15;
                }
            }

        Index208:
            {
                int Temporary1;
                Temporary1 = Actor.GetGotoLabelIndex(@"checkdeath");
                if (Temporary1 < 0) {
                    return new Error(@"257: no entry for goto label: 'checkdeath'");
                }
                GotoLabelSwitchIdentifier = Temporary1 + 1 + 1;
                goto GotoLabelSwitch;
            }

        Index209:
        Label15:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(260, 3), @"clear");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index210:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"your gotogotchi died. :C");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index211:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(262, 3), @"say");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index212:
            {
                goto EndLabel;
            }

        Index213:
            {
                int Temporary1;
                Temporary1 = Actor.GetGotoLabelIndex(@"death");
                if (Temporary1 < 0) {
                    return new Error(@"264: no entry for goto label: 'death'");
                }
                GotoLabelSwitchIdentifier = Temporary1 + 1 + 1;
                goto GotoLabelSwitch;
            }

        Index214:
        Label16:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(267, 3), @"eatkey");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index215:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"result");
                Actor.SetVariable(@"lastinputresult", Temporary1.Value);
            }

        Index216:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(269, 3), @"peekkey");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index217:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"result");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"270: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index214;
                }
            }

        Index218:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"lastinputresult");
                Actor.SetVariable(@"result", Temporary1.Value);
            }

        Index219:
            {
                int Temporary1;
                Temporary1 = Actor.GetGotoLabelIndex(@"lastinput");
                if (Temporary1 < 0) {
                    return new Error(@"272: no entry for goto label: 'lastinput'");
                }
                GotoLabelSwitchIdentifier = Temporary1 + 1 + 1;
                goto GotoLabelSwitch;
            }

        Index220:
        Label17:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Nothing();
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index221:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"y");
                Temporary3 = Thingie.Number(1);
                Temporary1 = Thingie.NotEquals(new SourceLocation(277, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"checky", Temporary1.Value);
            }

        Index222:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"checky");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"278: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index224;
                }
            }

        Index223:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"g");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index224:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"y");
                Temporary3 = Thingie.Number(2);
                Temporary1 = Thingie.NotEquals(new SourceLocation(280, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"checky", Temporary1.Value);
            }

        Index225:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"checky");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"281: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index227;
                }
            }

        Index226:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"o");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index227:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"y");
                Temporary3 = Thingie.Number(3);
                Temporary1 = Thingie.NotEquals(new SourceLocation(283, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"checky", Temporary1.Value);
            }

        Index228:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"checky");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"284: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index230;
                }
            }

        Index229:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"t");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index230:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"y");
                Temporary3 = Thingie.Number(4);
                Temporary1 = Thingie.NotEquals(new SourceLocation(286, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"checky", Temporary1.Value);
            }

        Index231:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"checky");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"287: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index233;
                }
            }

        Index232:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"o");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index233:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"y");
                Temporary3 = Thingie.Number(5);
                Temporary1 = Thingie.NotEquals(new SourceLocation(289, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"checky", Temporary1.Value);
            }

        Index234:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"checky");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"290: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index236;
                }
            }

        Index235:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"g");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index236:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"y");
                Temporary3 = Thingie.Number(6);
                Temporary1 = Thingie.NotEquals(new SourceLocation(292, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"checky", Temporary1.Value);
            }

        Index237:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"checky");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"293: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index239;
                }
            }

        Index238:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"o");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index239:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"y");
                Temporary3 = Thingie.Number(7);
                Temporary1 = Thingie.NotEquals(new SourceLocation(295, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"checky", Temporary1.Value);
            }

        Index240:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"checky");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"296: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index242;
                }
            }

        Index241:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"t");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index242:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"y");
                Temporary3 = Thingie.Number(8);
                Temporary1 = Thingie.NotEquals(new SourceLocation(298, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"checky", Temporary1.Value);
            }

        Index243:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"checky");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"299: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index245;
                }
            }

        Index244:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"c");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index245:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"y");
                Temporary3 = Thingie.Number(9);
                Temporary1 = Thingie.NotEquals(new SourceLocation(301, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"checky", Temporary1.Value);
            }

        Index246:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"checky");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"302: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index248;
                }
            }

        Index247:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"h");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index248:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"y");
                Temporary3 = Thingie.Number(10);
                Temporary1 = Thingie.NotEquals(new SourceLocation(304, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"checky", Temporary1.Value);
            }

        Index249:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"checky");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"305: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index251;
                }
            }

        Index250:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"i");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index251:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"what");
                Temporary3 = Thingie.Nothing();
                Temporary1 = Thingie.Equals(new SourceLocation(308, 19), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"whatisnothing", Temporary1.Value);
            }

        Index252:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"whatisnothing");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"309: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index255;
                }
            }

        Index253:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"what");
                Temporary3 = Thingie.String(@"|");
                Temporary1 = Thingie.Add(new SourceLocation(310, 11), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index254:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(311, 3), @"say");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index255:
            {
                int Temporary1;
                Temporary1 = Actor.GetGotoLabelIndex(@"displaytitle");
                if (Temporary1 < 0) {
                    return new Error(@"312: no entry for goto label: 'displaytitle'");
                }
                GotoLabelSwitchIdentifier = Temporary1 + 1 + 1;
                goto GotoLabelSwitch;
            }

        Index256:
        Label18:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(315, 3), @"clear");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index257:
            {
                Actor.SetGotoLabelIndex(@"drawgrid", 256);
                goto Label3;
            }

        Index258:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(317, 3), @"eatkey");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index259:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"result");
                Temporary3 = Thingie.String(@"enter");
                Temporary1 = Thingie.NotEquals(new SourceLocation(318, 19), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"keyisnotenter", Temporary1.Value);
            }

        Index260:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"keyisnotenter");
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"319: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index258;
                }
            }

        Index261:
            {
                int Temporary1;
                Temporary1 = Actor.GetGotoLabelIndex(@"displaytutorialmessage");
                if (Temporary1 < 0) {
                    return new Error(@"320: no entry for goto label: 'displaytutorialmessage'");
                }
                GotoLabelSwitchIdentifier = Temporary1 + 1 + 1;
                goto GotoLabelSwitch;
            }


            goto EndLabel;
        GotoLabelSwitch:
            switch (GotoLabelSwitchIdentifier) {
                case 1:
                    goto Index1;
                case 2:
                    goto Index2;
                case 3:
                    goto Index3;
                case 4:
                    goto Index4;
                case 5:
                    goto Index5;
                case 6:
                    goto Index6;
                case 7:
                    goto Index7;
                case 8:
                    goto Index8;
                case 9:
                    goto Index9;
                case 10:
                    goto Index10;
                case 11:
                    goto Index11;
                case 12:
                    goto Index12;
                case 13:
                    goto Index13;
                case 14:
                    goto Index14;
                case 15:
                    goto Index15;
                case 16:
                    goto Index16;
                case 17:
                    goto Index17;
                case 18:
                    goto Index18;
                case 19:
                    goto Index19;
                case 20:
                    goto Index20;
                case 21:
                    goto Index21;
                case 22:
                    goto Index22;
                case 23:
                    goto Index23;
                case 24:
                    goto Index24;
                case 25:
                    goto Index25;
                case 26:
                    goto Index26;
                case 27:
                    goto Index27;
                case 28:
                    goto Index28;
                case 29:
                    goto Index29;
                case 30:
                    goto Index30;
                case 31:
                    goto Index31;
                case 32:
                    goto Index32;
                case 33:
                    goto Index33;
                case 34:
                    goto Index34;
                case 35:
                    goto Index35;
                case 36:
                    goto Index36;
                case 37:
                    goto Index37;
                case 38:
                    goto Index38;
                case 39:
                    goto Index39;
                case 40:
                    goto Index40;
                case 41:
                    goto Index41;
                case 42:
                    goto Index42;
                case 43:
                    goto Index43;
                case 44:
                    goto Index44;
                case 45:
                    goto Index45;
                case 46:
                    goto Index46;
                case 47:
                    goto Index47;
                case 48:
                    goto Index48;
                case 49:
                    goto Index49;
                case 50:
                    goto Index50;
                case 51:
                    goto Index51;
                case 52:
                    goto Index52;
                case 53:
                    goto Index53;
                case 54:
                    goto Index54;
                case 55:
                    goto Index55;
                case 56:
                    goto Index56;
                case 57:
                    goto Index57;
                case 58:
                    goto Index58;
                case 59:
                    goto Index59;
                case 60:
                    goto Index60;
                case 61:
                    goto Index61;
                case 62:
                    goto Index62;
                case 63:
                    goto Index63;
                case 64:
                    goto Index64;
                case 65:
                    goto Index65;
                case 66:
                    goto Index66;
                case 67:
                    goto Index67;
                case 68:
                    goto Index68;
                case 69:
                    goto Index69;
                case 70:
                    goto Index70;
                case 71:
                    goto Index71;
                case 72:
                    goto Index72;
                case 73:
                    goto Index73;
                case 74:
                    goto Index74;
                case 75:
                    goto Index75;
                case 76:
                    goto Index76;
                case 77:
                    goto Index77;
                case 78:
                    goto Index78;
                case 79:
                    goto Index79;
                case 80:
                    goto Index80;
                case 81:
                    goto Index81;
                case 82:
                    goto Index82;
                case 83:
                    goto Index83;
                case 84:
                    goto Index84;
                case 85:
                    goto Index85;
                case 86:
                    goto Index86;
                case 87:
                    goto Index87;
                case 88:
                    goto Index88;
                case 89:
                    goto Index89;
                case 90:
                    goto Index90;
                case 91:
                    goto Index91;
                case 92:
                    goto Index92;
                case 93:
                    goto Index93;
                case 94:
                    goto Index94;
                case 95:
                    goto Index95;
                case 96:
                    goto Index96;
                case 97:
                    goto Index97;
                case 98:
                    goto Index98;
                case 99:
                    goto Index99;
                case 100:
                    goto Index100;
                case 101:
                    goto Index101;
                case 102:
                    goto Index102;
                case 103:
                    goto Index103;
                case 104:
                    goto Index104;
                case 105:
                    goto Index105;
                case 106:
                    goto Index106;
                case 107:
                    goto Index107;
                case 108:
                    goto Index108;
                case 109:
                    goto Index109;
                case 110:
                    goto Index110;
                case 111:
                    goto Index111;
                case 112:
                    goto Index112;
                case 113:
                    goto Index113;
                case 114:
                    goto Index114;
                case 115:
                    goto Index115;
                case 116:
                    goto Index116;
                case 117:
                    goto Index117;
                case 118:
                    goto Index118;
                case 119:
                    goto Index119;
                case 120:
                    goto Index120;
                case 121:
                    goto Index121;
                case 122:
                    goto Index122;
                case 123:
                    goto Index123;
                case 124:
                    goto Index124;
                case 125:
                    goto Index125;
                case 126:
                    goto Index126;
                case 127:
                    goto Index127;
                case 128:
                    goto Index128;
                case 129:
                    goto Index129;
                case 130:
                    goto Index130;
                case 131:
                    goto Index131;
                case 132:
                    goto Index132;
                case 133:
                    goto Index133;
                case 134:
                    goto Index134;
                case 135:
                    goto Index135;
                case 136:
                    goto Index136;
                case 137:
                    goto Index137;
                case 138:
                    goto Index138;
                case 139:
                    goto Index139;
                case 140:
                    goto Index140;
                case 141:
                    goto Index141;
                case 142:
                    goto Index142;
                case 143:
                    goto Index143;
                case 144:
                    goto Index144;
                case 145:
                    goto Index145;
                case 146:
                    goto Index146;
                case 147:
                    goto Index147;
                case 148:
                    goto Index148;
                case 149:
                    goto Index149;
                case 150:
                    goto Index150;
                case 151:
                    goto Index151;
                case 152:
                    goto Index152;
                case 153:
                    goto Index153;
                case 154:
                    goto Index154;
                case 155:
                    goto Index155;
                case 156:
                    goto Index156;
                case 157:
                    goto Index157;
                case 158:
                    goto Index158;
                case 159:
                    goto Index159;
                case 160:
                    goto Index160;
                case 161:
                    goto Index161;
                case 162:
                    goto Index162;
                case 163:
                    goto Index163;
                case 164:
                    goto Index164;
                case 165:
                    goto Index165;
                case 166:
                    goto Index166;
                case 167:
                    goto Index167;
                case 168:
                    goto Index168;
                case 169:
                    goto Index169;
                case 170:
                    goto Index170;
                case 171:
                    goto Index171;
                case 172:
                    goto Index172;
                case 173:
                    goto Index173;
                case 174:
                    goto Index174;
                case 175:
                    goto Index175;
                case 176:
                    goto Index176;
                case 177:
                    goto Index177;
                case 178:
                    goto Index178;
                case 179:
                    goto Index179;
                case 180:
                    goto Index180;
                case 181:
                    goto Index181;
                case 182:
                    goto Index182;
                case 183:
                    goto Index183;
                case 184:
                    goto Index184;
                case 185:
                    goto Index185;
                case 186:
                    goto Index186;
                case 187:
                    goto Index187;
                case 188:
                    goto Index188;
                case 189:
                    goto Index189;
                case 190:
                    goto Index190;
                case 191:
                    goto Index191;
                case 192:
                    goto Index192;
                case 193:
                    goto Index193;
                case 194:
                    goto Index194;
                case 195:
                    goto Index195;
                case 196:
                    goto Index196;
                case 197:
                    goto Index197;
                case 198:
                    goto Index198;
                case 199:
                    goto Index199;
                case 200:
                    goto Index200;
                case 201:
                    goto Index201;
                case 202:
                    goto Index202;
                case 203:
                    goto Index203;
                case 204:
                    goto Index204;
                case 205:
                    goto Index205;
                case 206:
                    goto Index206;
                case 207:
                    goto Index207;
                case 208:
                    goto Index208;
                case 209:
                    goto Index209;
                case 210:
                    goto Index210;
                case 211:
                    goto Index211;
                case 212:
                    goto Index212;
                case 213:
                    goto Index213;
                case 214:
                    goto Index214;
                case 215:
                    goto Index215;
                case 216:
                    goto Index216;
                case 217:
                    goto Index217;
                case 218:
                    goto Index218;
                case 219:
                    goto Index219;
                case 220:
                    goto Index220;
                case 221:
                    goto Index221;
                case 222:
                    goto Index222;
                case 223:
                    goto Index223;
                case 224:
                    goto Index224;
                case 225:
                    goto Index225;
                case 226:
                    goto Index226;
                case 227:
                    goto Index227;
                case 228:
                    goto Index228;
                case 229:
                    goto Index229;
                case 230:
                    goto Index230;
                case 231:
                    goto Index231;
                case 232:
                    goto Index232;
                case 233:
                    goto Index233;
                case 234:
                    goto Index234;
                case 235:
                    goto Index235;
                case 236:
                    goto Index236;
                case 237:
                    goto Index237;
                case 238:
                    goto Index238;
                case 239:
                    goto Index239;
                case 240:
                    goto Index240;
                case 241:
                    goto Index241;
                case 242:
                    goto Index242;
                case 243:
                    goto Index243;
                case 244:
                    goto Index244;
                case 245:
                    goto Index245;
                case 246:
                    goto Index246;
                case 247:
                    goto Index247;
                case 248:
                    goto Index248;
                case 249:
                    goto Index249;
                case 250:
                    goto Index250;
                case 251:
                    goto Index251;
                case 252:
                    goto Index252;
                case 253:
                    goto Index253;
                case 254:
                    goto Index254;
                case 255:
                    goto Index255;
                case 256:
                    goto Index256;
                case 257:
                    goto Index257;
                case 258:
                    goto Index258;
                case 259:
                    goto Index259;
                case 260:
                    goto Index260;
                case 261:
                    goto Index261;
                default:
                    throw new InvalidProgramException();
            }

        EndLabel:
            ;
        }

        return Result.Success;
    }
}