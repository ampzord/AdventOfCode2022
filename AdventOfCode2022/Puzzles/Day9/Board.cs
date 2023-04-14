using System.ComponentModel;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;

namespace AdventOfCode2022.Puzzles.Day9;

public class Board
{
    private readonly int _rows;
    private readonly int _columns;
    public RopePosition RopePosition { get; set; }
    public Position StartingPosition { get; set; }
    public char[,] Grid { get; set; }
    public int[,] VisitedGrid { get; set; } 

    public Board(int rows, int columns)
    {
        Grid = new char[rows, columns];
        VisitedGrid = new int[rows, columns];
        StartingPosition = new Position(rows - 1, 0);
        RopePosition = new RopePosition(StartingPosition, StartingPosition);
        _rows = rows;
        _columns = columns;
    }

    public void Move(Tuple<Direction, int> move)
    {
        //move head
        MoveHead(move.Item1);

        //move tail
        MoveTail();

        //update VisitedGrid
    }
    
    public bool IsValidMove(Direction direction)
    {
        Position oldPosition = RopePosition.Head;
        Position newPosition = direction switch
        {
            Direction.Up => new Position(oldPosition.Row - 1, oldPosition.Column),
            Direction.Down => new Position(oldPosition.Row + 1, oldPosition.Column),
            Direction.Right => new Position(oldPosition.Row, oldPosition.Column + 1),
            Direction.Left => new Position(oldPosition.Row, oldPosition.Column - 1),
            _ => throw new InvalidEnumArgumentException("Direction is not valid.")
        };
        
        return IsOutOfBounds(newPosition);
    }

    private bool IsOutOfBounds(Position newPosition)
    {
        return newPosition.Row < 0 || newPosition.Row > _rows - 1 || 
               newPosition.Column < 0 || newPosition.Column > _columns - 1;
    }

    private void MoveHead(Direction direction)
    {
        Position oldPosition = RopePosition.Head;
        
        Position newPosition = direction switch
        {
            Direction.Up => new Position(oldPosition.Row - 1, oldPosition.Column),
            Direction.Down => new Position(oldPosition.Row + 1, oldPosition.Column),
            Direction.Right => new Position(oldPosition.Row, oldPosition.Column + 1),
            Direction.Left => new Position(oldPosition.Row, oldPosition.Column - 1),
            _ => throw new InvalidEnumArgumentException("Direction is not valid.")
        };

        RopePosition.Head = newPosition;
    }
    
    private void MoveTail()
    {
        throw new NotImplementedException();
    }
    

    
    
}