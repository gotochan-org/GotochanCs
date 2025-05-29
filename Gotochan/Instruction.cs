namespace Gotochan;

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

public record GotoLineInstruction : Instruction {
    public required int TargetLine { get; init; }
}

public record GotoLabelInstruction : Instruction {
    public required string TargetLabel { get; init; }
}

public record BacktoInstruction : Instruction {
    public required string TargetLabel { get; init; }
}