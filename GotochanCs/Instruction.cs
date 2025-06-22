namespace GotochanCs;

/// <summary>
/// A Gotochan instruction.
/// </summary>
public abstract record Instruction {
    /// <summary>
    /// The location of the start of instruction in the original source code.
    /// </summary>
    public required SourceLocation Location { get; init; }
    /// <summary>
    /// An optional condition flag expression that must evaluate to true for this instruction to be evaluated.
    /// </summary>
    public required Expression? Condition { get; init; }
}

/// <summary>
/// An instruction that sets the value of a variable.
/// </summary>
public record SetVariableInstruction : Instruction {
    /// <summary>
    /// The name of the variable to set.
    /// </summary>
    public required string VariableName { get; init; }
    /// <summary>
    /// The value to set the variable to.
    /// </summary>
    public required Expression Value { get; init; }
}

/// <summary>
/// An instruction that marks the target of a goto label instruction.
/// </summary>
public record LabelInstruction : Instruction {
    /// <summary>
    /// The name of the label.
    /// </summary>
    public required string LabelName { get; init; }
}

/// <summary>
/// An instruction that jumps to a line number.
/// </summary>
public record GotoLineInstruction : Instruction {
    /// <summary>
    /// The line number to jump to.
    /// </summary>
    public required int LineNumber { get; init; }
    /// <summary>
    /// The optional pre-calculated instruction index to jump to instead of the line number.
    /// </summary>
    public int? InstructionIndex { get; init; }
}

/// <summary>
/// An instruction that jumps to a label by name.
/// </summary>
public record GotoLabelInstruction : Instruction {
    /// <summary>
    /// The name of the label to jump to.
    /// </summary>
    public required string LabelName { get; init; }
    /// <summary>
    /// The optional pre-calculated instruction index to jump to instead of the label.
    /// </summary>
    public int? InstructionIndex { get; init; }
}

/// <summary>
/// An instruction that jumps to the instruction after the last evaluated goto label instruction for a label.
/// </summary>
public record GotoGotoLabelInstruction : Instruction {
    /// <summary>
    /// The name of the label to jump to the last evaluated goto label instruction for.
    /// </summary>
    public required string LabelName { get; init; }
}