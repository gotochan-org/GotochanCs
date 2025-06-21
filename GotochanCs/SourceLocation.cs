namespace GotochanCs;

public readonly record struct SourceLocation {
    public int Line { get; }
    public int Column { get; }

    public SourceLocation(int Line, int Column) {
        this.Line = Line;
        this.Column = Column;
    }
    public SourceLocation((int Line, int Column) LineAndColumn)
        : this(LineAndColumn.Line, LineAndColumn.Column) {
    }
    public SourceLocation(string Source, int Index)
        : this(GetLineAndColumn(Source, Index)) {
    }

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
    public static int GetLine(string Source, int Index) {
        return GetLineAndColumn(Source, Index).Line;
    }
    public static int GetColumn(string Source, int Index) {
        return GetLineAndColumn(Source, Index).Column;
    }
}