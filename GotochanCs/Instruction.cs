namespace GotochanCs;

public abstract record Instruction {
    public required SourceLocation Location { get; init; }
    public required Expression? Condition { get; init; }
}

public record SetVariableInstruction : Instruction {
    public required string VariableName { get; init; }
    public required Expression Value { get; init; }
}

public record LabelInstruction : Instruction {
    public required string LabelName { get; init; }
}

public record GotoLineInstruction : Instruction {
    public required int LineNumber { get; init; }
    public int? InstructionIndex { get; init; }
}

public record GotoLabelInstruction : Instruction {
    public required string LabelName { get; init; }
    public int? InstructionIndex { get; init; }
}

public record GotoGotoLabelInstruction : Instruction {
    public required string LabelName { get; init; }
}