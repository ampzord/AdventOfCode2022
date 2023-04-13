using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;

namespace AdventOfCode2022.Puzzles.Day9;

public class Board
{
    public RopePosition RopePosition { get; set; }
    public Position StartingPosition { get; set; }
    public char[,] Grid { get; set; }
    public int[,] VisitedGrid { get; set; } 

    public Board(int rows, int columns)
    {
        Grid = new char[rows - 1, columns - 1];
        VisitedGrid = new int[rows - 1, columns - 1];
        StartingPosition = new Position(rows - 1, 0);
        RopePosition = new RopePosition(StartingPosition, StartingPosition);
    }

    public void MoveRope(Tuple<Direction, int> move)
    {
        //move head

        //move tail
        
        //update VisitedGrid
    }
    

    
    
}