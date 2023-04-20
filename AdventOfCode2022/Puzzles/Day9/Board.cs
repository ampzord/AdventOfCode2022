using System.ComponentModel;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;

namespace AdventOfCode2022.Puzzles.Day9;

public class Board
{
    private readonly int _rows;
    private readonly int _columns;
    private RopePosition RopePos { get; }
    private Position StartingPosition { get; }
    private char[,] Grid { get; }
    private HashSet<Tuple<int,int>> VisitedGrid;

    public Board(int rows, int columns, int knotLength)
    {
        Grid = new char[rows, columns];
        StartingPosition = new Position(rows / 2, columns / 2);
        RopePos = new RopePosition(StartingPosition, knotLength);
        _rows = rows;
        _columns = columns;
        VisitedGrid = new HashSet<Tuple<int, int>>();
    }
    
    public void Move(Tuple<Direction, int> move)
    {
        int moveNumber = move.Item2;
        while (moveNumber > 0)
        {
            if (!IsValidMove(move.Item1))
                break;

            MoveWholeRope(move.Item1);
            moveNumber--;
        }
    }

    private void MoveWholeRope(Direction direction)
    {
        MoveRopeHead(direction);
        
        for (int i = 0; i < RopePos.Rope.Length - 1; i++)
        {
            RopePos.Rope[i + 1] = RopePos.MoveTail(RopePos.Rope[i], RopePos.Rope[i+1]);
        }
        
        UpdateTailVisited(); 
    }
    

    private bool IsValidMove(Direction direction)
    {
        Position oldPosition = RopePos.Head;
        Position newPosition = direction switch
        {
            Direction.Up => new Position(oldPosition.Row - 1, oldPosition.Column),
            Direction.Down => new Position(oldPosition.Row + 1, oldPosition.Column),
            Direction.Right => new Position(oldPosition.Row, oldPosition.Column + 1),
            Direction.Left => new Position(oldPosition.Row, oldPosition.Column - 1),
            _ => throw new InvalidEnumArgumentException("Direction is not valid.")
        };
        
        return !IsOutOfBounds(newPosition);
    }

    private bool IsOutOfBounds(Position newPosition)
    {
        return newPosition.Row < 0 || newPosition.Row > _rows - 1 || 
               newPosition.Column < 0 || newPosition.Column > _columns - 1;
    }

    private void MoveRopeHead(Direction direction)
    {
        Position oldHeadPosition = RopePos.Head;
        
        Position newPosition = direction switch
        {
            Direction.Up => new Position(oldHeadPosition.Row - 1, oldHeadPosition.Column),
            Direction.Down => new Position(oldHeadPosition.Row + 1, oldHeadPosition.Column),
            Direction.Right => new Position(oldHeadPosition.Row, oldHeadPosition.Column + 1),
            Direction.Left => new Position(oldHeadPosition.Row, oldHeadPosition.Column - 1),
            _ => throw new InvalidEnumArgumentException("Direction is not valid.")
        };
        
        RopePos.Head = newPosition;
    }
    
    public void UpdateTailVisited()
    {
        var tupleVisit = Tuple.Create(RopePos.Tail.Row, RopePos.Tail.Column);
        VisitedGrid.Add(tupleVisit);
    }

    public int VisitedPositionsByTail()
    {
        return VisitedGrid.Count;
    }
    
}