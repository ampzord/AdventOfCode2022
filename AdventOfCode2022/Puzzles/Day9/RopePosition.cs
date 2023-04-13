namespace AdventOfCode2022.Puzzles.Day9;

public class RopePosition
{
    public Position Head { get; set; }
    public Position Tail { get; set; }

    public RopePosition(Position head, Position tail)
    {
        Head = head;
        Tail = tail;
    }
}