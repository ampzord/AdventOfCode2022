namespace AdventOfCode2022.Puzzles.Day9;

public class Position : IEquatable<Position>
{
    public int Row { get; set; }
    public int Column { get; set; }

    public Position(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public bool Equals(Position? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Row == other.Row && Column == other.Column;
    }

    public bool IsNear(Position? other, int row = 0, int column = 0)
    {
        if (other is null)
            return false;
        
        Position otherPosition = new Position(other.Row, other.Column);
        otherPosition.Row += row;
        otherPosition.Column += column;
        
        return this.Equals(otherPosition);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Position)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Row, Column);
    }
}