namespace AdventOfCode2022.Puzzles.Day9;

public class RopePosition
{
    private readonly int _knotLevel;
    public Position Head { get; set; }
    public Position Tail { get; set; }
    public Position[] Knots { get; set; }

    public RopePosition(Position head, Position tail, int knotLevel)
    {
        _knotLevel = knotLevel;
        Knots = new Position[knotLevel];
        Head = Knots[0];
        Tail = Knots[knotLevel - 1];
    }
    
    //verify in array if is Near
    //TODO 
    public bool IsTailNearHead()
    {
        // Same Position
        if (Head.Equals(Tail))
            return true;

        // Diagonally
        if (Head.IsNear(Tail, -1, -1) || Head.IsNear(Tail, -1, 1) ||
            Head.IsNear(Tail, 1, 1) || Head.IsNear(Tail, 1, -1))
            return true;
        
        // Vertically
        if (Head.IsNear(Tail, row: -1) || Head.IsNear(Tail, row: 1))
            return true;
        
        // Horizontally
        if (Head.IsNear(Tail, column: -1) || Head.IsNear(Tail, column: 1))
            return true;
        
        return false; 
    }
}