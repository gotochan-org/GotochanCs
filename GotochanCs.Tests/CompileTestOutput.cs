using System;
using System.Collections.Generic;
using GotochanCs;
using ResultZero;

namespace GotochanCs.Tests;

public static partial class CompileOutput {
    public static Result Execute(Actor Actor) {
        int GotoLabelSwitchIdentifier = -1;

        int GotoLabelIndex1 = -1;
        int GotoLabelIndex2 = -1;

        Thingie Variable1 = Thingie.Nothing();
        Thingie Variable2 = Thingie.Nothing();

    Index1:
        {
            Result<Thingie> Temporary1;
            Temporary1 = Thingie.Number(1);
            Variable1 = Temporary1.Value;
        }

    Index2:
    Label1:
        {
            Result<Thingie> Temporary1;
            Result<Thingie> Temporary2;
            Result<Thingie> Temporary3;
            Temporary2 = Variable1;
            Temporary3 = Thingie.Number(1);
            Temporary1 = Thingie.Add(new SourceLocation(3, 12), Temporary2.Value, Temporary3.Value);
            if (Temporary1.IsError) {
                return Temporary1.Error;
            }
            Variable1 = Temporary1.Value;
        }

    Index3:
        {
            GotoLabelIndex2 = 4;
            goto Label2;
        }

    Index4:
        {
            Result<Thingie> Temporary1;
            Result<Thingie> Temporary2;
            Result<Thingie> Temporary3;
            Temporary2 = Variable1;
            Temporary3 = Thingie.Number(3);
            Temporary1 = Thingie.LessThanOrEqualTo(new SourceLocation(5, 14), Temporary2.Value, Temporary3.Value);
            if (Temporary1.IsError) {
                return Temporary1.Error;
            }
            if (Temporary1.Value.Type is not ThingieType.Flag) {
                return new Error($"5: condition must be flag, not '{Temporary1.Value.Type}'");
            }
            if (Temporary1.Value.CastFlag()) {
                GotoLabelIndex1 = 5;
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
            Temporary1 = Variable1;
            Variable2 = Temporary1.Value;
        }

    Index7:
        {
            if (Actor.GotoExternalLabel(new SourceLocation(11, 1), @"say").TryGetError(out Error GotoExternalLabelError)) {
                return GotoExternalLabelError;
            }
        }

    Index8:
        {
            if (GotoLabelIndex2 < 0) {
                return new Error($"12: no entry for goto label: 'saycounter'");
            }
            GotoLabelSwitchIdentifier = GotoLabelIndex2;
            goto GotoLabelSwitch;
        }


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


        return Result.Success;
    }
}