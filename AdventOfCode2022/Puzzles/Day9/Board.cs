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
    public HashSet<Tuple<int,int>> VisitedGrid;

    public Board(int rows, int columns)
    {
        Grid = new char[rows, columns];
        StartingPosition = new Position(rows / 2, columns / 2);
        RopePosition = new RopePosition(StartingPosition, StartingPosition);
        _rows = rows;
        _columns = columns;
        VisitedGrid = new HashSet<Tuple<int, int>>();
    }

    public void Move(Tuple<Direction, int> move)
    {
        int moveAmount = move.Item2;
        while (moveAmount > 0)
        {
            if (!IsValidMove(move.Item1))
                break;
            
            var oldHeadPosition = MoveHead(move.Item1);
            if (!RopePosition.IsTailNearHead())
            {
                MoveTail(oldHeadPosition);
                UpdateTailVisited(); 
            }
            
            moveAmount--;
        }
    }
    
    public void Move_Part2(Tuple<Direction, int> move, int knotDistance)
    {
        int moveAmount = move.Item2;
        while (moveAmount > 0)
        {
            if (!IsValidMove(move.Item1))
                break;
            
            var oldHeadPosition = MoveHead(move.Item1);
            if (!RopePosition.IsTailNearHead())
            {
                MoveTail(oldHeadPosition);
                UpdateTailVisited(); 
            }
            
            moveAmount--;
        }
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
        
        return !IsOutOfBounds(newPosition);
    }

    private bool IsOutOfBounds(Position newPosition)
    {
        return newPosition.Row < 0 || newPosition.Row > _rows - 1 || 
               newPosition.Column < 0 || newPosition.Column > _columns - 1;
    }

    private Position MoveHead(Direction direction)
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
        return oldPosition;
    }
    
    private void MoveTail(Position oldHeadPosition)
    {
        RopePosition.Tail = oldHeadPosition;
    }
    public void UpdateTailVisited()
    {
        var tupleVisit = Tuple.Create(RopePosition.Tail.Row, RopePosition.Tail.Column);
        VisitedGrid.Add(tupleVisit);
    }

    public int VisitedPositionsByTail()
    {
        return VisitedGrid.Count;
    }
    
}