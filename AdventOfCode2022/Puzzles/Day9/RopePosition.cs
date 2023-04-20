namespace AdventOfCode2022.Puzzles.Day9;

public class RopePosition
{
    private int _knotLength { get; }

    public Position Head
    {
        get => Rope[0];
        set => Rope[0] = value;
    }
    
    public Position Tail
    {
        get => Rope[_knotLength - 1];
        set => Rope[_knotLength - 1] = value;
    }
    
    public Position[] Rope { get; }

    public RopePosition(Position startPosition, int knotLength)
    {
        _knotLength = knotLength;
        Rope = Enumerable.Repeat(startPosition, knotLength).ToArray();
    }

    public Position MoveTail(Position head, Position tail)
    {
        int dx = head.Column - tail.Column;
        int dy = head.Row - tail.Row;
        
        if (!IsRopeNear(head, tail))
        {
            tail = new Position(tail.Row + Math.Sign(dy), tail.Column + Math.Sign(dx));
        }

        return tail;
    }
    
    public bool IsRopeNear(Position front, Position back)
    {
        // Same Position
        if (front.Equals(back))
            return true;

        // Diagonally
        if (front.IsNear(back, -1, -1) || front.IsNear(back, -1, 1) ||
            front.IsNear(back, 1, 1) || front.IsNear(back, 1, -1))
            return true;
        
        // Vertically
        if (front.IsNear(back, row: -1) || front.IsNear(back, row: 1))
            return true;
        
        // Horizontally
        if (front.IsNear(back, column: -1) || front.IsNear(back, column: 1))
            return true;
        
        return false; 
    }
    
}