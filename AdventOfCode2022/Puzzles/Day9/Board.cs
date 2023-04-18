using System.ComponentModel;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;

namespace AdventOfCode2022.Puzzles.Day9;

public class Board
{
    private readonly int _rows;
    private readonly int _columns;
    public RopePosition RopePos { get; set; }
    public Position StartingPosition { get; set; }
    public char[,] Grid { get; set; }
    public HashSet<Tuple<int,int>> VisitedGrid;

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
        int moveAmount = move.Item2;
        while (moveAmount > 0)
        {
            if (!IsValidMove(move.Item1))
                break;
            
            // var oldHeadPosition = MoveRopeHead(move.Item1);
            // if (!RopePos.IsTailNearHead())
            // {
            //     MoveTail(oldHeadPosition);
            //     UpdateTailVisited(); 
            // }
            
            moveAmount--;
        }
    }
    
    public void Move_Part2(Tuple<Direction, int> move)
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
        // Move head always
        MoveRopeHead(direction, out var oldHeadPosition);
        
        // Verify if the next index close to Head is Near or not
        if (!RopePos.IsBackOfTheRopeNear(0, out _))
        {
            RopePos.Rope[1] = oldHeadPosition;
        }
        
        RopePos.MoveRestOfRope();
        UpdateTailVisited(); 
    }
    

    public bool IsValidMove(Direction direction)
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

    private void MoveRopeHead(Direction direction, out Position oldHeadPosition)
    {
        oldHeadPosition = RopePos.Head;
        
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