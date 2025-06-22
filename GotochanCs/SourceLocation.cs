namespace GotochanCs;

/// <summary>
/// A location in source code.
/// </summary>
public readonly record struct SourceLocation {
    /// <summary>
    /// The 1-based vertical position.
    /// </summary>
    public int Line { get; }
    /// <summary>
    /// The 1-based horizontal position.
    /// </summary>
    public int Column { get; }

    /// <summary>
    /// Constructs a source location with the given line and column.
    /// </summary>
    public SourceLocation(int Line, int Column) {
        this.Line = Line;
        this.Column = Column;
    }
    /// <summary>
    /// Constructs a source location with the given line and column.
    /// </summary>
    public SourceLocation((int Line, int Column) LineAndColumn)
        : this(LineAndColumn.Line, LineAndColumn.Column) {
    }
    /// <summary>
    /// Constructs a source location with a line and column calculated from the given index in the given source.
    /// </summary>
    public SourceLocation(string Source, int Index)
        : this(GetLineAndColumn(Source, Index)) {
    }

    /// <summary>
    /// Returns an invalid source location, useful for methods that require a source location.
    /// </summary>
    public static SourceLocation Invalid() {
        return new SourceLocation(-1, -1);
    }
    /// <summary>
    /// Calculates the line and column of the given index in the given source.
    /// </summary>
    public static (int Line, int Column) GetLineAndColumn(string Source, int Index) {
        int Line = 1;
        int Column = 1;
        for (int NextIndex = 0; NextIndex < Index; NextIndex++) {
            char Next = Source[NextIndex];

            // Newline
            if (Lexer.NewlineChars.Contains(Next)) {
                Line++;
                Column = 1;
                // Join CR LF
                if (Next is '\r' && NextIndex + 1 < Source.Length && Source[NextIndex + 1] is '\n') {
                    NextIndex++;
                }
            }
            // Character
            else {
                Column++;
            }
        }
        return (Line, Column);
    }
    /// <summary>
    /// Calculates the line of the given index in the given source.
    /// </summary>
    public static int GetLine(string Source, int Index) {
        return GetLineAndColumn(Source, Index).Line;
    }
    /// <summary>
    /// Calculates the column of the given index in the given source.
    /// </summary>
    public static int GetColumn(string Source, int Index) {
        return GetLineAndColumn(Source, Index).Column;
    }
}