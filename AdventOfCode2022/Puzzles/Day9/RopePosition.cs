namespace AdventOfCode2022.Puzzles.Day9;

public class RopePosition
{
    private int _knotLevel { get; }
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
    /// <summary>
    /// We declare rope movement if next item movement in array is not "Near" the previous item
    /// </summary>
    /// <returns></returns>
    public bool IsRopeMovement()
    {
        for (int i = 0; i < Knots.Length; i++)
        {
            // if (Knots[i].
        }

        return false;
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