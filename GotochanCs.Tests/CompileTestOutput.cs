using System;
using System.Collections.Generic;
using GotochanCs;
using ResultZero;

namespace GotochanCs.Tests;

public static partial class CompileOutput {
    public static Result Execute(Actor Actor) {
        Dictionary<int, int> GotoLabelLines = [];
        int GotoGotoLabelIdentifier = -1;
        SourceLocation GotoGotoLabelLocation = default;
        string GotoGotoLabelName = "";

        Thingie Variable1 = Thingie.Nothing();
        Thingie Variable2 = Thingie.Nothing();

    Index1:
        {
            Result<Thingie> Temporary1;
            Temporary1 = Thingie.Number(1);
            Variable1 = Temporary1.Value;
        }

    Index2: Label1:
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
                goto Label1;
            }
        }

    Index5:
        {
            goto EndLabel;
        }

    Index6: Label2:
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
            GotoGotoLabelIdentifier = 2;
            GotoGotoLabelLocation = new SourceLocation(12, 1);
            GotoGotoLabelName = @"saycounter";
            goto GotoGotoLabel;
        }


    GotoGotoLabel: if (!GotoLabelLines.TryGetValue(GotoGotoLabelIdentifier, out int LabelIdentifier)) {
            return new Error($"{GotoGotoLabelLocation.Line}: no entry for goto label: '{GotoGotoLabelName}'");
        }
        switch (LabelIdentifier) {
            case 1:
                goto Label1;
            case 2:
                goto Label2;
        }
        GotoGotoLabelIdentifier = -1;
        GotoGotoLabelLocation = default;
        GotoGotoLabelName = "";

    EndLabel:;


        return Result.Success;
    }
}