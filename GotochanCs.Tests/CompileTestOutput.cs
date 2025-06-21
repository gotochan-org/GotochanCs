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

namespace GotochanCs.Tests;

public static partial class CompileOutput {
    public static Result Execute(Actor Actor) {
        int GotoLabelSwitchIdentifier;

        lock (Actor.Lock) {
        Index1:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Thingie.Number(1);
                Actor.SetVariable(@"counter", Temporary1.Value);
            }

        Index2:
        Label1:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"counter");
                Temporary3 = Thingie.Number(1);
                Temporary1 = Thingie.Add(new SourceLocation(3, 12), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                Actor.SetVariable(@"counter", Temporary1.Value);
            }

        Index3:
            {
                Actor.SetGotoLabelIndex(@"saycounter", 2);
                goto Label2;
            }

        Index4:
            {
                Result<Thingie> Temporary1;
                Result<Thingie> Temporary2;
                Result<Thingie> Temporary3;
                Temporary2 = Actor.GetVariable(@"counter");
                Temporary3 = Thingie.Number(3);
                Temporary1 = Thingie.LessThanOrEqualTo(new SourceLocation(5, 14), Temporary2.Value, Temporary3.Value);
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
                if (Temporary1.Value.Type is not ThingieType.Flag) {
                    return new Error($"5: condition must be flag, not '{Temporary1.Value.Type}'");
                }
                if (Temporary1.Value.CastFlag()) {
                    Actor.SetGotoLabelIndex(@"loop", 3);
                    goto Label1;
                }
            }

        Index5:
            {
                goto EndLabel;
            }

        Index6:
        Label2:
            {
                Result<Thingie> Temporary1;
                Temporary1 = Actor.GetVariable(@"counter");
                Actor.SetVariable(@"what", Temporary1.Value);
            }

        Index7:
            {
                Result Temporary1;
                Temporary1 = Actor.GotoExternalLabel(new SourceLocation(11, 1), @"say");
                if (Temporary1.IsError) {
                    return Temporary1.Error;
                }
            }

        Index8:
            {
                int Temporary1;
                Temporary1 = Actor.GetGotoLabelIndex(@"saycounter");
                if (Temporary1 < 0) {
                    return new Error(@"12: no entry for goto label: 'saycounter'");
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
                default:
                    throw new InvalidProgramException();
            }

        EndLabel:
            ;
        }

        return Result.Success;
    }
}