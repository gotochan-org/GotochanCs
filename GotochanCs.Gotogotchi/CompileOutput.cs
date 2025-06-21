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

        int GotoLabelIndex1 = -1;
        int GotoLabelIndex2 = -1;
        int GotoLabelIndex3 = -1;
        int GotoLabelIndex4 = -1;
        int GotoLabelIndex5 = -1;
        int GotoLabelIndex6 = -1;
        int GotoLabelIndex7 = -1;
        int GotoLabelIndex8 = -1;
        int GotoLabelIndex9 = -1;
        int GotoLabelIndex10 = -1;
        int GotoLabelIndex11 = -1;
        int GotoLabelIndex12 = -1;
        int GotoLabelIndex13 = -1;
        int GotoLabelIndex14 = -1;
        int GotoLabelIndex15 = -1;
        int GotoLabelIndex16 = -1;
        int GotoLabelIndex17 = -1;
        int GotoLabelIndex18 = -1;

        Thingie Variable1 = Thingie.Nothing();
        Thingie Variable2 = Thingie.Nothing();
        Thingie Variable3 = Thingie.Nothing();
        Thingie Variable4 = Thingie.Nothing();
        Thingie Variable5 = Thingie.Nothing();
        Thingie Variable6 = Thingie.Nothing();
        Thingie Variable7 = Thingie.Nothing();
        Thingie Variable8 = Thingie.Nothing();
        Thingie Variable9 = Thingie.Nothing();
        Thingie Variable10 = Thingie.Nothing();
        Thingie Variable11 = Thingie.Nothing();
        Thingie Variable12 = Thingie.Nothing();
        Thingie Variable13 = Thingie.Nothing();
        Thingie Variable14 = Thingie.Nothing();
        Thingie Variable15 = Thingie.Nothing();
        Thingie Variable16 = Thingie.Nothing();
        Thingie Variable17 = Thingie.Nothing();
        Thingie Variable18 = Thingie.Nothing();
        Thingie Variable19 = Thingie.Nothing();
        Thingie Variable20 = Thingie.Nothing();
        Thingie Variable21 = Thingie.Nothing();
        Thingie Variable22 = Thingie.Nothing();
        Thingie Variable23 = Thingie.Nothing();
        Thingie Variable24 = Thingie.Nothing();
        Thingie Variable25 = Thingie.Nothing();
        Thingie Variable26 = Thingie.Nothing();
        Thingie Variable27 = Thingie.Nothing();
        Thingie Variable28 = Thingie.Nothing();
        Thingie Variable29 = Thingie.Nothing();
        Thingie Variable30 = Thingie.Nothing();
        Thingie Variable31 = Thingie.Nothing();
        Thingie Variable32 = Thingie.Nothing();
        Thingie Variable33 = Thingie.Nothing();
        Thingie Variable34 = Thingie.Nothing();
        Thingie Variable35 = Thingie.Nothing();
        Thingie Variable36 = Thingie.Nothing();
        Thingie Variable37 = Thingie.Nothing();
        Thingie Variable38 = Thingie.Nothing();
        Thingie Variable39 = Thingie.Nothing();
        Thingie Variable40 = Thingie.Nothing();
        Thingie Variable41 = Thingie.Nothing();
        Thingie Variable42 = Thingie.Nothing();
        Thingie Variable43 = Thingie.Nothing();
        Thingie Variable44 = Thingie.Nothing();
        Thingie Variable45 = Thingie.Nothing();
        Thingie Variable46 = Thingie.Nothing();
        Thingie Variable47 = Thingie.Nothing();
        Thingie Variable48 = Thingie.Nothing();
        Thingie Variable49 = Thingie.Nothing();
        Thingie Variable50 = Thingie.Nothing();
        Thingie Variable51 = Thingie.Nothing();
        Thingie Variable52 = Thingie.Nothing();
        Thingie Variable53 = Thingie.Nothing();

        void LoadActor() {
            GotoLabelIndex1 = Actor.GetGotoLabelIndex(@"gameloop");
            GotoLabelIndex2 = Actor.GetGotoLabelIndex(@"endofinput");
            GotoLabelIndex3 = Actor.GetGotoLabelIndex(@"drawgrid");
            GotoLabelIndex4 = Actor.GetGotoLabelIndex(@"fory");
            GotoLabelIndex5 = Actor.GetGotoLabelIndex(@"forx");
            GotoLabelIndex6 = Actor.GetGotoLabelIndex(@"endofforx");
            GotoLabelIndex7 = Actor.GetGotoLabelIndex(@"centertext");
            GotoLabelIndex8 = Actor.GetGotoLabelIndex(@"leftaligntext");
            GotoLabelIndex9 = Actor.GetGotoLabelIndex(@"wraphashtags");
            GotoLabelIndex10 = Actor.GetGotoLabelIndex(@"forcounter");
            GotoLabelIndex11 = Actor.GetGotoLabelIndex(@"endofforcounter");
            GotoLabelIndex12 = Actor.GetGotoLabelIndex(@"iseven");
            GotoLabelIndex13 = Actor.GetGotoLabelIndex(@"clampstats");
            GotoLabelIndex14 = Actor.GetGotoLabelIndex(@"checkdeath");
            GotoLabelIndex15 = Actor.GetGotoLabelIndex(@"death");
            GotoLabelIndex16 = Actor.GetGotoLabelIndex(@"lastinput");
            GotoLabelIndex17 = Actor.GetGotoLabelIndex(@"displaytitle");
            GotoLabelIndex18 = Actor.GetGotoLabelIndex(@"displaytutorialmessage");
            Variable1 = Actor.GetVariable(@"screenwidth");
            Variable2 = Actor.GetVariable(@"screenheight");
            Variable3 = Actor.GetVariable(@"dialogueline");
            Variable4 = Actor.GetVariable(@"dialoguetwoline");
            Variable5 = Actor.GetVariable(@"petline");
            Variable6 = Actor.GetVariable(@"pettwoline");
            Variable7 = Actor.GetVariable(@"hungerline");
            Variable8 = Actor.GetVariable(@"thirstline");
            Variable9 = Actor.GetVariable(@"funline");
            Variable10 = Actor.GetVariable(@"intutorial");
            Variable11 = Actor.GetVariable(@"hunger");
            Variable12 = Actor.GetVariable(@"thirst");
            Variable13 = Actor.GetVariable(@"fun");
            Variable14 = Actor.GetVariable(@"pet");
            Variable15 = Actor.GetVariable(@"pettwo");
            Variable16 = Actor.GetVariable(@"dialogue");
            Variable17 = Actor.GetVariable(@"dialoguetwo");
            Variable18 = Actor.GetVariable(@"timelastreducedstats");
            Variable19 = Actor.GetVariable(@"timestamp");
            Variable20 = Actor.GetVariable(@"redraw");
            Variable21 = Actor.GetVariable(@"seconds");
            Variable22 = Actor.GetVariable(@"currenttime");
            Variable23 = Actor.GetVariable(@"nottimetoreducestats");
            Variable24 = Actor.GetVariable(@"hasnoinput");
            Variable25 = Actor.GetVariable(@"result");
            Variable26 = Actor.GetVariable(@"what");
            Variable27 = Actor.GetVariable(@"inputisnotfeed");
            Variable28 = Actor.GetVariable(@"inputisnotwater");
            Variable29 = Actor.GetVariable(@"inputisnotplay");
            Variable30 = Actor.GetVariable(@"y");
            Variable31 = Actor.GetVariable(@"x");
            Variable32 = Actor.GetVariable(@"yisnotatdialogueline");
            Variable33 = Actor.GetVariable(@"yisnotatdialoguetwoline");
            Variable34 = Actor.GetVariable(@"yisnotatpetline");
            Variable35 = Actor.GetVariable(@"yisnotatpettwoline");
            Variable36 = Actor.GetVariable(@"yisnotathungerline");
            Variable37 = Actor.GetVariable(@"yisnotatthirstline");
            Variable38 = Actor.GetVariable(@"yisnotatfunline");
            Variable39 = Actor.GetVariable(@"xisnotatlimit");
            Variable40 = Actor.GetVariable(@"yisnotatlimit");
            Variable41 = Actor.GetVariable(@"texttocenter");
            Variable42 = Actor.GetVariable(@"numberofhashtags");
            Variable43 = Actor.GetVariable(@"lengthoftext");
            Variable44 = Actor.GetVariable(@"numberofhashtagsprinted");
            Variable45 = Actor.GetVariable(@"counter");
            Variable46 = Actor.GetVariable(@"counterisnumberofhashtags");
            Variable47 = Actor.GetVariable(@"nontruncated");
            Variable48 = Actor.GetVariable(@"truncatedisnotsame");
            Variable49 = Actor.GetVariable(@"comparison");
            Variable50 = Actor.GetVariable(@"lastinputresult");
            Variable51 = Actor.GetVariable(@"checky");
            Variable52 = Actor.GetVariable(@"whatisnothing");
            Variable53 = Actor.GetVariable(@"keyisnotenter");
        }

        void SaveActor() {
            Actor.SetGotoLabelIndex(@"gameloop", GotoLabelIndex1);
            Actor.SetGotoLabelIndex(@"endofinput", GotoLabelIndex2);
            Actor.SetGotoLabelIndex(@"drawgrid", GotoLabelIndex3);
            Actor.SetGotoLabelIndex(@"fory", GotoLabelIndex4);
            Actor.SetGotoLabelIndex(@"forx", GotoLabelIndex5);
            Actor.SetGotoLabelIndex(@"endofforx", GotoLabelIndex6);
            Actor.SetGotoLabelIndex(@"centertext", GotoLabelIndex7);
            Actor.SetGotoLabelIndex(@"leftaligntext", GotoLabelIndex8);
            Actor.SetGotoLabelIndex(@"wraphashtags", GotoLabelIndex9);
            Actor.SetGotoLabelIndex(@"forcounter", GotoLabelIndex10);
            Actor.SetGotoLabelIndex(@"endofforcounter", GotoLabelIndex11);
            Actor.SetGotoLabelIndex(@"iseven", GotoLabelIndex12);
            Actor.SetGotoLabelIndex(@"clampstats", GotoLabelIndex13);
            Actor.SetGotoLabelIndex(@"checkdeath", GotoLabelIndex14);
            Actor.SetGotoLabelIndex(@"death", GotoLabelIndex15);
            Actor.SetGotoLabelIndex(@"lastinput", GotoLabelIndex16);
            Actor.SetGotoLabelIndex(@"displaytitle", GotoLabelIndex17);
            Actor.SetGotoLabelIndex(@"displaytutorialmessage", GotoLabelIndex18);
            Actor.SetVariable(@"screenwidth", Variable1);
            Actor.SetVariable(@"screenheight", Variable2);
            Actor.SetVariable(@"dialogueline", Variable3);
            Actor.SetVariable(@"dialoguetwoline", Variable4);
            Actor.SetVariable(@"petline", Variable5);
            Actor.SetVariable(@"pettwoline", Variable6);
            Actor.SetVariable(@"hungerline", Variable7);
            Actor.SetVariable(@"thirstline", Variable8);
            Actor.SetVariable(@"funline", Variable9);
            Actor.SetVariable(@"intutorial", Variable10);
            Actor.SetVariable(@"hunger", Variable11);
            Actor.SetVariable(@"thirst", Variable12);
            Actor.SetVariable(@"fun", Variable13);
            Actor.SetVariable(@"pet", Variable14);
            Actor.SetVariable(@"pettwo", Variable15);
            Actor.SetVariable(@"dialogue", Variable16);
            Actor.SetVariable(@"dialoguetwo", Variable17);
            Actor.SetVariable(@"timelastreducedstats", Variable18);
            Actor.SetVariable(@"timestamp", Variable19);
            Actor.SetVariable(@"redraw", Variable20);
            Actor.SetVariable(@"seconds", Variable21);
            Actor.SetVariable(@"currenttime", Variable22);
            Actor.SetVariable(@"nottimetoreducestats", Variable23);
            Actor.SetVariable(@"hasnoinput", Variable24);
            Actor.SetVariable(@"result", Variable25);
            Actor.SetVariable(@"what", Variable26);
            Actor.SetVariable(@"inputisnotfeed", Variable27);
            Actor.SetVariable(@"inputisnotwater", Variable28);
            Actor.SetVariable(@"inputisnotplay", Variable29);
            Actor.SetVariable(@"y", Variable30);
            Actor.SetVariable(@"x", Variable31);
            Actor.SetVariable(@"yisnotatdialogueline", Variable32);
            Actor.SetVariable(@"yisnotatdialoguetwoline", Variable33);
            Actor.SetVariable(@"yisnotatpetline", Variable34);
            Actor.SetVariable(@"yisnotatpettwoline", Variable35);
            Actor.SetVariable(@"yisnotathungerline", Variable36);
            Actor.SetVariable(@"yisnotatthirstline", Variable37);
            Actor.SetVariable(@"yisnotatfunline", Variable38);
            Actor.SetVariable(@"xisnotatlimit", Variable39);
            Actor.SetVariable(@"yisnotatlimit", Variable40);
            Actor.SetVariable(@"texttocenter", Variable41);
            Actor.SetVariable(@"numberofhashtags", Variable42);
            Actor.SetVariable(@"lengthoftext", Variable43);
            Actor.SetVariable(@"numberofhashtagsprinted", Variable44);
            Actor.SetVariable(@"counter", Variable45);
            Actor.SetVariable(@"counterisnumberofhashtags", Variable46);
            Actor.SetVariable(@"nontruncated", Variable47);
            Actor.SetVariable(@"truncatedisnotsame", Variable48);
            Actor.SetVariable(@"comparison", Variable49);
            Actor.SetVariable(@"lastinputresult", Variable50);
            Actor.SetVariable(@"checky", Variable51);
            Actor.SetVariable(@"whatisnothing", Variable52);
            Actor.SetVariable(@"keyisnotenter", Variable53);
        }

        lock (Actor.Lock) {
            LoadActor();
        Index1:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(15);
                Variable1 = Temporary1.Value;
            }

        Index2:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(10);
                Variable2 = Temporary1.Value;
            }

        Index3:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(8);
                Variable3 = Temporary1.Value;
            }

        Index4:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(9);
                Variable4 = Temporary1.Value;
            }

        Index5:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(5);
                Variable5 = Temporary1.Value;
            }

        Index6:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(6);
                Variable6 = Temporary1.Value;
            }

        Index7:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(1);
                Variable7 = Temporary1.Value;
            }

        Index8:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(2);
                Variable8 = Temporary1.Value;
            }

        Index9:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(3);
                Variable9 = Temporary1.Value;
            }

        Index10:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Flag(true);
                Variable10 = Temporary1.Value;
            }

        Index11:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(100);
                Variable11 = Temporary1.Value;
            }

        Index12:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(100);
                Variable12 = Temporary1.Value;
            }

        Index13:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(100);
                Variable13 = Temporary1.Value;
            }

        Index14:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"");
                Variable14 = Temporary1.Value;
            }

        Index15:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"");
                Variable15 = Temporary1.Value;
            }

        Index16:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"welcome.");
                Variable16 = Temporary1.Value;
            }

        Index17:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"v2.0");
                Variable17 = Temporary1.Value;
            }

        Index18:
            {
                GotoLabelIndex18 = 19;
                goto Label18;
            }

        Index19:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"take care of");
                Variable16 = Temporary1.Value;
            }

        Index20:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"your neko.");
                Variable17 = Temporary1.Value;
            }

        Index21:
            {
                GotoLabelIndex18 = 22;
                goto Label18;
            }

        Index22:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"press A to");
                Variable16 = Temporary1.Value;
            }

        Index23:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"feed neko.");
                Variable17 = Temporary1.Value;
            }

        Index24:
            {
                GotoLabelIndex18 = 25;
                goto Label18;
            }

        Index25:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"press B to");
                Variable16 = Temporary1.Value;
            }

        Index26:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"water neko.");
                Variable17 = Temporary1.Value;
            }

        Index27:
            {
                GotoLabelIndex18 = 28;
                goto Label18;
            }

        Index28:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"press C to");
                Variable16 = Temporary1.Value;
            }

        Index29:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"play w/ neko.");
                Variable17 = Temporary1.Value;
            }

        Index30:
            {
                GotoLabelIndex18 = 31;
                goto Label18;
            }

        Index31:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"keep numbers");
                Variable16 = Temporary1.Value;
            }

        Index32:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"above 0.");
                Variable17 = Temporary1.Value;
            }

        Index33:
            {
                GotoLabelIndex18 = 34;
                goto Label18;
            }

        Index34:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Flag(false);
                Variable10 = Temporary1.Value;
            }

        Index35:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"^ ^");
                Variable14 = Temporary1.Value;
            }

        Index36:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"(>.<)");
                Variable15 = Temporary1.Value;
            }

        Index37:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"");
                Variable16 = Temporary1.Value;
            }

        Index38:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@" A  B  C ");
                Variable17 = Temporary1.Value;
            }

        Index39:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(54, 1), @"stamptime");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index40:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable19;
                Variable18 = Temporary1.Value;
            }

        Index41:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Flag(true);
                Variable20 = Temporary1.Value;
            }

        Index42:
        Label1:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable20;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"58: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    SaveActor();
                    Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(58, 3), @"clear");
                    if (GotoExternalLabelResult.IsError) {
                        return GotoExternalLabelResult.Error;
                    }
                }
            }

        Index43:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable20;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"59: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    GotoLabelIndex3 = 44;
                    goto Label3;
                }
            }

        Index44:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(0.15);
                Variable21 = Temporary1.Value;
            }

        Index45:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(62, 3), @"wait");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index46:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Flag(false);
                Variable20 = Temporary1.Value;
            }

        Index47:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(66, 3), @"stamptime");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index48:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable19;
                Variable22 = Temporary1.Value;
            }

        Index49:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable19;
                Temporary3 = Variable18;
                Temporary1 = Thingie.Subtract(new SourceLocation(68, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable19 = Temporary1.Value;
            }

        Index50:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable19;
                Temporary3 = Thingie.Number(5);
                Temporary1 = Thingie.LessThan(new SourceLocation(69, 26), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable23 = Temporary1.Value;
            }

        Index51:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable23;
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
                Temporary1 = Variable22;
                Variable18 = Temporary1.Value;
            }

        Index53:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Flag(true);
                Variable20 = Temporary1.Value;
            }

        Index54:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable11;
                Temporary3 = Thingie.Number(2);
                Temporary1 = Thingie.Subtract(new SourceLocation(73, 13), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable11 = Temporary1.Value;
            }

        Index55:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable12;
                Temporary3 = Thingie.Number(3);
                Temporary1 = Thingie.Subtract(new SourceLocation(74, 13), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable12 = Temporary1.Value;
            }

        Index56:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable13;
                Temporary3 = Thingie.Number(1);
                Temporary1 = Thingie.Subtract(new SourceLocation(75, 10), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable13 = Temporary1.Value;
            }

        Index57:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(77, 3), @"peekkey");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index58:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable25;
                Temporary3 = Thingie.Flag(false);
                Temporary1 = Thingie.Equals(new SourceLocation(78, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable24 = Temporary1.Value;
            }

        Index59:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable24;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"80: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    GotoLabelIndex2 = 60;
                    goto Label2;
                }
            }

        Index60:
            {
                GotoLabelIndex16 = 61;
                goto Label16;
            }

        Index61:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable25;
                Variable26 = Temporary1.Value;
            }

        Index62:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(83, 5), @"casedown");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index63:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable25;
                Temporary3 = Thingie.String(@"a");
                Temporary1 = Thingie.NotEquals(new SourceLocation(85, 22), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable27 = Temporary1.Value;
            }

        Index64:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable27;
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
                Variable20 = Temporary1.Value;
            }

        Index66:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable11;
                Temporary3 = Thingie.Number(5);
                Temporary1 = Thingie.Add(new SourceLocation(88, 15), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable11 = Temporary1.Value;
            }

        Index67:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable25;
                Temporary3 = Thingie.String(@"b");
                Temporary1 = Thingie.NotEquals(new SourceLocation(89, 23), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable28 = Temporary1.Value;
            }

        Index68:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable28;
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
                Variable20 = Temporary1.Value;
            }

        Index70:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable12;
                Temporary3 = Thingie.Number(8);
                Temporary1 = Thingie.Add(new SourceLocation(92, 15), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable12 = Temporary1.Value;
            }

        Index71:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable25;
                Temporary3 = Thingie.String(@"c");
                Temporary1 = Thingie.NotEquals(new SourceLocation(93, 22), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable29 = Temporary1.Value;
            }

        Index72:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable29;
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
                Variable20 = Temporary1.Value;
            }

        Index74:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable13;
                Temporary3 = Thingie.Number(15);
                Temporary1 = Thingie.Add(new SourceLocation(96, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable13 = Temporary1.Value;
            }

        Index75:
        Label2:
            {
                GotoLabelIndex13 = 76;
                goto Label13;
            }

        Index76:
            {
                GotoLabelIndex14 = 77;
                goto Label14;
            }

        Index77:
            {
                GotoLabelIndex1 = 78;
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
                Variable30 = Temporary1.Value;
            }

        Index80:
        Label4:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable30;
                Temporary3 = Thingie.Number(1);
                Temporary1 = Thingie.Add(new SourceLocation(110, 10), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable30 = Temporary1.Value;
            }

        Index81:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(0);
                Variable31 = Temporary1.Value;
            }

        Index82:
            {
                GotoLabelIndex17 = 83;
                goto Label17;
            }

        Index83:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable30;
                Temporary3 = Variable3;
                Temporary1 = Thingie.NotEquals(new SourceLocation(113, 28), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable32 = Temporary1.Value;
            }

        Index84:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable32;
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
                Temporary1 = Variable16;
                Variable26 = Temporary1.Value;
            }

        Index86:
            {
                GotoLabelIndex7 = 87;
                goto Label7;
            }

        Index87:
            {
                GotoLabelIndex6 = 88;
                goto Label6;
            }

        Index88:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable30;
                Temporary3 = Variable4;
                Temporary1 = Thingie.NotEquals(new SourceLocation(118, 31), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable33 = Temporary1.Value;
            }

        Index89:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable33;
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
                Temporary1 = Variable17;
                Variable26 = Temporary1.Value;
            }

        Index91:
            {
                GotoLabelIndex7 = 92;
                goto Label7;
            }

        Index92:
            {
                GotoLabelIndex6 = 93;
                goto Label6;
            }

        Index93:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable30;
                Temporary3 = Variable5;
                Temporary1 = Thingie.NotEquals(new SourceLocation(123, 23), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable34 = Temporary1.Value;
            }

        Index94:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable34;
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
                Temporary1 = Variable14;
                Variable26 = Temporary1.Value;
            }

        Index96:
            {
                GotoLabelIndex7 = 97;
                goto Label7;
            }

        Index97:
            {
                GotoLabelIndex6 = 98;
                goto Label6;
            }

        Index98:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable30;
                Temporary3 = Variable6;
                Temporary1 = Thingie.NotEquals(new SourceLocation(128, 26), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable35 = Temporary1.Value;
            }

        Index99:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable35;
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
                Temporary1 = Variable15;
                Variable26 = Temporary1.Value;
            }

        Index101:
            {
                GotoLabelIndex7 = 102;
                goto Label7;
            }

        Index102:
            {
                GotoLabelIndex6 = 103;
                goto Label6;
            }

        Index103:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable10;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"133: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    GotoLabelIndex5 = 104;
                    goto Label5;
                }
            }

        Index104:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable30;
                Temporary3 = Variable7;
                Temporary1 = Thingie.NotEquals(new SourceLocation(134, 28), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable36 = Temporary1.Value;
            }

        Index105:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable36;
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
                Variable26 = Temporary1.Value;
            }

        Index107:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable26;
                Temporary3 = Variable11;
                Temporary1 = Thingie.Add(new SourceLocation(137, 17), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable26 = Temporary1.Value;
            }

        Index108:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable26;
                Temporary3 = Thingie.String(@" ");
                Temporary1 = Thingie.Add(new SourceLocation(138, 17), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable26 = Temporary1.Value;
            }

        Index109:
            {
                GotoLabelIndex8 = 110;
                goto Label8;
            }

        Index110:
            {
                GotoLabelIndex6 = 111;
                goto Label6;
            }

        Index111:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable30;
                Temporary3 = Variable8;
                Temporary1 = Thingie.NotEquals(new SourceLocation(141, 28), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable37 = Temporary1.Value;
            }

        Index112:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable37;
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
                Variable26 = Temporary1.Value;
            }

        Index114:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable26;
                Temporary3 = Variable12;
                Temporary1 = Thingie.Add(new SourceLocation(144, 17), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable26 = Temporary1.Value;
            }

        Index115:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable26;
                Temporary3 = Thingie.String(@" ");
                Temporary1 = Thingie.Add(new SourceLocation(145, 17), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable26 = Temporary1.Value;
            }

        Index116:
            {
                GotoLabelIndex8 = 117;
                goto Label8;
            }

        Index117:
            {
                GotoLabelIndex6 = 118;
                goto Label6;
            }

        Index118:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable30;
                Temporary3 = Variable9;
                Temporary1 = Thingie.NotEquals(new SourceLocation(148, 25), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable38 = Temporary1.Value;
            }

        Index119:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable38;
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
                Variable26 = Temporary1.Value;
            }

        Index121:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable26;
                Temporary3 = Variable13;
                Temporary1 = Thingie.Add(new SourceLocation(151, 17), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable26 = Temporary1.Value;
            }

        Index122:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable26;
                Temporary3 = Thingie.String(@" ");
                Temporary1 = Thingie.Add(new SourceLocation(152, 17), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable26 = Temporary1.Value;
            }

        Index123:
            {
                GotoLabelIndex8 = 124;
                goto Label8;
            }

        Index124:
            {
                GotoLabelIndex6 = 125;
                goto Label6;
            }

        Index125:
        Label5:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable31;
                Temporary3 = Thingie.Number(1);
                Temporary1 = Thingie.Add(new SourceLocation(156, 14), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable31 = Temporary1.Value;
            }

        Index126:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"#");
                Variable26 = Temporary1.Value;
            }

        Index127:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(158, 9), @"say");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index128:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable31;
                Temporary3 = Variable1;
                Temporary1 = Thingie.NotEquals(new SourceLocation(159, 25), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable39 = Temporary1.Value;
            }

        Index129:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable39;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"160: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    GotoLabelIndex5 = 130;
                    goto Label5;
                }
            }

        Index130:
        Label6:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"
");
                Variable26 = Temporary1.Value;
            }

        Index131:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(163, 5), @"say");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index132:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable30;
                Temporary3 = Variable2;
                Temporary1 = Thingie.NotEquals(new SourceLocation(164, 21), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable40 = Temporary1.Value;
            }

        Index133:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable40;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"165: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    GotoLabelIndex4 = 134;
                    goto Label4;
                }
            }

        Index134:
            {
                if (GotoLabelIndex3 < 0) {
                    return new Error($"166: no entry for goto label: 'drawgrid'");
                }
                GotoLabelSwitchIdentifier = GotoLabelIndex3;
                goto GotoLabelSwitch;
            }

        Index135:
        Label7:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable26;
                Variable41 = Temporary1.Value;
            }

        Index136:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable1;
                Variable42 = Temporary1.Value;
            }

        Index137:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(171, 3), @"measure");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index138:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable25;
                Variable43 = Temporary1.Value;
            }

        Index139:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable42;
                Temporary3 = Variable43;
                Temporary1 = Thingie.Subtract(new SourceLocation(173, 23), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable42 = Temporary1.Value;
            }

        Index140:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable42;
                Temporary3 = Thingie.Number(2);
                Temporary1 = Thingie.Divide(new SourceLocation(174, 23), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable42 = Temporary1.Value;
            }

        Index141:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable42;
                Variable26 = Temporary1.Value;
            }

        Index142:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(176, 3), @"truncate");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index143:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable25;
                Variable42 = Temporary1.Value;
            }

        Index144:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(0);
                Variable44 = Temporary1.Value;
            }

        Index145:
            {
                GotoLabelIndex9 = 146;
                goto Label9;
            }

        Index146:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable41;
                Variable26 = Temporary1.Value;
            }

        Index147:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(182, 3), @"say");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index148:
            {
                GotoLabelIndex9 = 149;
                goto Label9;
            }

        Index149:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable43;
                Variable26 = Temporary1.Value;
            }

        Index150:
            {
                GotoLabelIndex12 = 151;
                goto Label12;
            }

        Index151:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable25;
                Temporary3 = Thingie.Flag(false);
                Temporary1 = Thingie.Equals(new SourceLocation(188, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable25 = Temporary1.Value;
            }

        Index152:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable25;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"189: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index155;
                }
            }

        Index153:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"#");
                Variable26 = Temporary1.Value;
            }

        Index154:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(191, 3), @"say");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index155:
            {
                if (GotoLabelIndex7 < 0) {
                    return new Error($"192: no entry for goto label: 'centertext'");
                }
                GotoLabelSwitchIdentifier = GotoLabelIndex7;
                goto GotoLabelSwitch;
            }

        Index156:
        Label8:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(195, 3), @"measure");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index157:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable25;
                Variable43 = Temporary1.Value;
            }

        Index158:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(197, 3), @"say");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index159:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable1;
                Variable42 = Temporary1.Value;
            }

        Index160:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable42;
                Temporary3 = Variable43;
                Temporary1 = Thingie.Subtract(new SourceLocation(199, 23), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable42 = Temporary1.Value;
            }

        Index161:
            {
                GotoLabelIndex9 = 162;
                goto Label9;
            }

        Index162:
            {
                if (GotoLabelIndex8 < 0) {
                    return new Error($"201: no entry for goto label: 'leftaligntext'");
                }
                GotoLabelSwitchIdentifier = GotoLabelIndex8;
                goto GotoLabelSwitch;
            }

        Index163:
        Label9:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(0);
                Variable45 = Temporary1.Value;
            }

        Index164:
        Label10:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable45;
                Temporary3 = Thingie.Number(1);
                Temporary1 = Thingie.Add(new SourceLocation(206, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable45 = Temporary1.Value;
            }

        Index165:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"#");
                Variable26 = Temporary1.Value;
            }

        Index166:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(208, 5), @"say");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index167:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable44;
                Temporary3 = Thingie.Number(1);
                Temporary1 = Thingie.Add(new SourceLocation(209, 32), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable44 = Temporary1.Value;
            }

        Index168:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable45;
                Temporary3 = Variable42;
                Temporary1 = Thingie.Equals(new SourceLocation(210, 33), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable46 = Temporary1.Value;
            }

        Index169:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable46;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"211: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    GotoLabelIndex11 = 170;
                    goto Label11;
                }
            }

        Index170:
            {
                GotoLabelIndex10 = 171;
                goto Label10;
            }

        Index171:
        Label11:
            {
                if (GotoLabelIndex9 < 0) {
                    return new Error($"214: no entry for goto label: 'wraphashtags'");
                }
                GotoLabelSwitchIdentifier = GotoLabelIndex9;
                goto GotoLabelSwitch;
            }

        Index172:
        Label12:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable26;
                Temporary3 = Thingie.Number(2);
                Temporary1 = Thingie.Divide(new SourceLocation(217, 11), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable26 = Temporary1.Value;
            }

        Index173:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable26;
                Variable47 = Temporary1.Value;
            }

        Index174:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(219, 3), @"truncate");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index175:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable25;
                Temporary3 = Variable47;
                Temporary1 = Thingie.NotEquals(new SourceLocation(220, 24), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable48 = Temporary1.Value;
            }

        Index176:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable48;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"221: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index179;
                }
            }

        Index177:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Flag(true);
                Variable25 = Temporary1.Value;
            }

        Index178:
            {
                goto Index180;
            }

        Index179:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Flag(false);
                Variable25 = Temporary1.Value;
            }

        Index180:
            {
                if (GotoLabelIndex12 < 0) {
                    return new Error($"225: no entry for goto label: 'iseven'");
                }
                GotoLabelSwitchIdentifier = GotoLabelIndex12;
                goto GotoLabelSwitch;
            }

        Index181:
        Label13:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable11;
                Temporary3 = Thingie.Number(0);
                Temporary1 = Thingie.GreaterThanOrEqualTo(new SourceLocation(228, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable49 = Temporary1.Value;
            }

        Index182:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable49;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"229: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index184;
                }
            }

        Index183:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(0);
                Variable11 = Temporary1.Value;
            }

        Index184:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable11;
                Temporary3 = Thingie.Number(100);
                Temporary1 = Thingie.LessThanOrEqualTo(new SourceLocation(231, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable49 = Temporary1.Value;
            }

        Index185:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable49;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"232: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index187;
                }
            }

        Index186:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(100);
                Variable11 = Temporary1.Value;
            }

        Index187:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable12;
                Temporary3 = Thingie.Number(0);
                Temporary1 = Thingie.GreaterThanOrEqualTo(new SourceLocation(234, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable49 = Temporary1.Value;
            }

        Index188:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable49;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"235: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index190;
                }
            }

        Index189:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(0);
                Variable12 = Temporary1.Value;
            }

        Index190:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable12;
                Temporary3 = Thingie.Number(100);
                Temporary1 = Thingie.LessThanOrEqualTo(new SourceLocation(237, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable49 = Temporary1.Value;
            }

        Index191:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable49;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"238: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index193;
                }
            }

        Index192:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(100);
                Variable12 = Temporary1.Value;
            }

        Index193:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable13;
                Temporary3 = Thingie.Number(0);
                Temporary1 = Thingie.GreaterThanOrEqualTo(new SourceLocation(240, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable49 = Temporary1.Value;
            }

        Index194:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable49;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"241: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index196;
                }
            }

        Index195:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(0);
                Variable13 = Temporary1.Value;
            }

        Index196:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable13;
                Temporary3 = Thingie.Number(100);
                Temporary1 = Thingie.LessThanOrEqualTo(new SourceLocation(243, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable49 = Temporary1.Value;
            }

        Index197:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable49;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"244: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index199;
                }
            }

        Index198:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(100);
                Variable13 = Temporary1.Value;
            }

        Index199:
            {
                if (GotoLabelIndex13 < 0) {
                    return new Error($"246: no entry for goto label: 'clampstats'");
                }
                GotoLabelSwitchIdentifier = GotoLabelIndex13;
                goto GotoLabelSwitch;
            }

        Index200:
        Label14:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable11;
                Temporary3 = Thingie.Number(0);
                Temporary1 = Thingie.Equals(new SourceLocation(249, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable49 = Temporary1.Value;
            }

        Index201:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable49;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"250: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    GotoLabelIndex15 = 202;
                    goto Label15;
                }
            }

        Index202:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable12;
                Temporary3 = Thingie.Number(0);
                Temporary1 = Thingie.Equals(new SourceLocation(251, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable49 = Temporary1.Value;
            }

        Index203:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable49;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"252: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    GotoLabelIndex15 = 204;
                    goto Label15;
                }
            }

        Index204:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable13;
                Temporary3 = Thingie.Number(0);
                Temporary1 = Thingie.Equals(new SourceLocation(253, 16), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable49 = Temporary1.Value;
            }

        Index205:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable49;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"254: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    GotoLabelIndex15 = 206;
                    goto Label15;
                }
            }

        Index206:
            {
                if (GotoLabelIndex14 < 0) {
                    return new Error($"255: no entry for goto label: 'checkdeath'");
                }
                GotoLabelSwitchIdentifier = GotoLabelIndex14;
                goto GotoLabelSwitch;
            }

        Index207:
        Label15:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(258, 3), @"clear");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index208:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"your gotogotchi died. :C");
                Variable26 = Temporary1.Value;
            }

        Index209:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(260, 3), @"say");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index210:
            {
                goto EndLabel;
            }

        Index211:
            {
                if (GotoLabelIndex15 < 0) {
                    return new Error($"262: no entry for goto label: 'death'");
                }
                GotoLabelSwitchIdentifier = GotoLabelIndex15;
                goto GotoLabelSwitch;
            }

        Index212:
        Label16:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(265, 3), @"eatkey");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index213:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable25;
                Variable50 = Temporary1.Value;
            }

        Index214:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(267, 3), @"peekkey");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index215:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable25;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"268: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index212;
                }
            }

        Index216:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable50;
                Variable25 = Temporary1.Value;
            }

        Index217:
            {
                if (GotoLabelIndex16 < 0) {
                    return new Error($"270: no entry for goto label: 'lastinput'");
                }
                GotoLabelSwitchIdentifier = GotoLabelIndex16;
                goto GotoLabelSwitch;
            }

        Index218:
        Label17:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Nothing();
                Variable26 = Temporary1.Value;
            }

        Index219:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable30;
                Temporary3 = Thingie.Number(1);
                Temporary1 = Thingie.NotEquals(new SourceLocation(275, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable51 = Temporary1.Value;
            }

        Index220:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable51;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"276: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index222;
                }
            }

        Index221:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"g");
                Variable26 = Temporary1.Value;
            }

        Index222:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable30;
                Temporary3 = Thingie.Number(2);
                Temporary1 = Thingie.NotEquals(new SourceLocation(278, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable51 = Temporary1.Value;
            }

        Index223:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable51;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"279: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index225;
                }
            }

        Index224:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"o");
                Variable26 = Temporary1.Value;
            }

        Index225:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable30;
                Temporary3 = Thingie.Number(3);
                Temporary1 = Thingie.NotEquals(new SourceLocation(281, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable51 = Temporary1.Value;
            }

        Index226:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable51;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"282: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index228;
                }
            }

        Index227:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"t");
                Variable26 = Temporary1.Value;
            }

        Index228:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable30;
                Temporary3 = Thingie.Number(4);
                Temporary1 = Thingie.NotEquals(new SourceLocation(284, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable51 = Temporary1.Value;
            }

        Index229:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable51;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"285: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index231;
                }
            }

        Index230:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"o");
                Variable26 = Temporary1.Value;
            }

        Index231:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable30;
                Temporary3 = Thingie.Number(5);
                Temporary1 = Thingie.NotEquals(new SourceLocation(287, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable51 = Temporary1.Value;
            }

        Index232:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable51;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"288: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index234;
                }
            }

        Index233:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"g");
                Variable26 = Temporary1.Value;
            }

        Index234:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable30;
                Temporary3 = Thingie.Number(6);
                Temporary1 = Thingie.NotEquals(new SourceLocation(290, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable51 = Temporary1.Value;
            }

        Index235:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable51;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"291: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index237;
                }
            }

        Index236:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"o");
                Variable26 = Temporary1.Value;
            }

        Index237:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable30;
                Temporary3 = Thingie.Number(7);
                Temporary1 = Thingie.NotEquals(new SourceLocation(293, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable51 = Temporary1.Value;
            }

        Index238:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable51;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"294: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index240;
                }
            }

        Index239:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"t");
                Variable26 = Temporary1.Value;
            }

        Index240:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable30;
                Temporary3 = Thingie.Number(8);
                Temporary1 = Thingie.NotEquals(new SourceLocation(296, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable51 = Temporary1.Value;
            }

        Index241:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable51;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"297: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index243;
                }
            }

        Index242:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"c");
                Variable26 = Temporary1.Value;
            }

        Index243:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable30;
                Temporary3 = Thingie.Number(9);
                Temporary1 = Thingie.NotEquals(new SourceLocation(299, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable51 = Temporary1.Value;
            }

        Index244:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable51;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"300: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index246;
                }
            }

        Index245:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"h");
                Variable26 = Temporary1.Value;
            }

        Index246:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable30;
                Temporary3 = Thingie.Number(10);
                Temporary1 = Thingie.NotEquals(new SourceLocation(302, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable51 = Temporary1.Value;
            }

        Index247:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable51;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"303: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index249;
                }
            }

        Index248:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.String(@"i");
                Variable26 = Temporary1.Value;
            }

        Index249:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable26;
                Temporary3 = Thingie.Nothing();
                Temporary1 = Thingie.Equals(new SourceLocation(306, 19), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable52 = Temporary1.Value;
            }

        Index250:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable52;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"307: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index253;
                }
            }

        Index251:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable26;
                Temporary3 = Thingie.String(@"|");
                Temporary1 = Thingie.Add(new SourceLocation(308, 11), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable26 = Temporary1.Value;
            }

        Index252:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(309, 3), @"say");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index253:
            {
                if (GotoLabelIndex17 < 0) {
                    return new Error($"310: no entry for goto label: 'displaytitle'");
                }
                GotoLabelSwitchIdentifier = GotoLabelIndex17;
                goto GotoLabelSwitch;
            }

        Index254:
        Label18:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(313, 3), @"clear");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index255:
            {
                GotoLabelIndex3 = 256;
                goto Label3;
            }

        Index256:
            {
                SaveActor();
                Result GotoExternalLabelResult = Actor.GotoExternalLabel(new SourceLocation(315, 3), @"eatkey");
                if (GotoExternalLabelResult.IsError) {
                    return GotoExternalLabelResult.Error;
                }
            }

        Index257:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Variable25;
                Temporary3 = Thingie.String(@"
");
                Temporary1 = Thingie.NotEquals(new SourceLocation(316, 19), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Variable53 = Temporary1.Value;
            }

        Index258:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Variable53;
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"317: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    goto Index256;
                }
            }

        Index259:
            {
                if (GotoLabelIndex18 < 0) {
                    return new Error($"318: no entry for goto label: 'displaytutorialmessage'");
                }
                GotoLabelSwitchIdentifier = GotoLabelIndex18;
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
                default:
                    throw new InvalidProgramException();
            }

        EndLabel:
            ;
            SaveActor();
        }

        return Result.Success;
    }
}