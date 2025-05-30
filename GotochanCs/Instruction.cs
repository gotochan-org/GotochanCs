namespace GotochanCs;

public abstract record Instruction {
    public required int Line { get; init; }
}

public record SetVariableInstruction : Instruction {
    public required string TargetVariable { get; init; }
    public required Expression Expression { get; init; }
}

public record LabelInstruction : Instruction {
    public required string Name { get; init; }
}

public abstract record GotoInstruction : Instruction {
    public required Expression Condition { get; init; }
}

public record GotoLineInstruction : GotoInstruction {
    public required int TargetLine { get; init; }
}

public record GotoLabelInstruction : GotoInstruction {
    public required string TargetLabel { get; init; }
}

public record GotoGotoLabelInstruction : GotoInstruction {
    public required string TargetLabel { get; init; }
}