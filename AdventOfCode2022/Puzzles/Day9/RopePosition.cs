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
    
    public Position[] Rope { get; set; }

    public RopePosition(Position startPosition, int knotLength)
    {
        _knotLength = knotLength;
        Rope = Enumerable.Repeat(startPosition, knotLength).ToArray();
    }
    /// <summary>
    /// We declare rope movement if next item movement in array is not "Near" the previous item
    /// </summary>
    /// <returns></returns>
    public void MoveRestOfRope(Position oldPositionv2)
    {
        // Starting on index 1
        for (int i = 1; i < Rope.Length - 1; i++)
        {
            Position oldPositionOfPreviousIndex = Rope[i];
            if (!IsBackOfTheRopeNear(i, out var oldPosition))
            {
                Rope[i + 1] = oldPositionv2;
            }
        }
        
    }

    public bool IsBackOfTheRopeNear(int index, out Position frontRopePart)
    {
        frontRopePart = Rope[index];
        Position otherRopeBody = Rope[index + 1];

        // Same Position
        if (frontRopePart.Equals(otherRopeBody))
            return true;

        // Diagonally
        if (frontRopePart.IsNear(otherRopeBody, -1, -1) || frontRopePart.IsNear(otherRopeBody, -1, 1) ||
            frontRopePart.IsNear(otherRopeBody, 1, 1) || frontRopePart.IsNear(otherRopeBody, 1, -1))
            return true;
        
        // Vertically
        if (frontRopePart.IsNear(otherRopeBody, row: -1) || frontRopePart.IsNear(otherRopeBody, row: 1))
            return true;
        
        // Horizontally
        if (frontRopePart.IsNear(otherRopeBody, column: -1) || frontRopePart.IsNear(otherRopeBody, column: 1))
            return true;
        
        return false; 
    }
    
}