namespace AdventOfCode2022.Puzzles.Day9;

public static class Day9
{
    private static readonly string[] _userInput = File.ReadAllLines(@"Input\Day9.txt");

    /// <summary>
    /// Solution Part 1 done with OutOfBounds verification
    /// </summary>
    public static void SolutionPart1()
    {
        var board = new Board(1000, 1000);

        foreach (var input in _userInput)
        {
            var parsedInput = ParseInput(input);
            board.Move(parsedInput);
        }
        
        Console.WriteLine($"Visited Positions by Tail: {board.VisitedPositionsByTail()}");
    }
    
    /// <summary>
    /// Solution Part 2 done with OutOfBounds verification
    /// </summary>
    public static void SolutionPart2()
    {
        int knotLevel = 9;
        var board = new Board(1000, 1000);

        foreach (var input in _userInput)
        {
            var parsedInput = ParseInput(input);
            board.Move_Part2(parsedInput, knotLevel);
        }
        
        Console.WriteLine($"Visited Positions by Tail: {board.VisitedPositionsByTail()}");
    }

    private static Tuple<Direction, int> ParseInput(string input)
    {
        var parsedString = input.Split(" ");
        string directionInput = parsedString[0];
        var dir = directionInput.ToCharArray();

        if (!int.TryParse(parsedString[1], out int steps))
        {
            throw new Exception("Not a valid step number.");
        }
        
        Direction direction = dir[0] switch
        {
            'U' => Direction.Up,
            'D' => Direction.Down,
            'R' => Direction.Right,
            'L' => Direction.Left,
            _ => throw new Exception("Not a valid direction.")
        };
        
        return Tuple.Create(direction, steps);
    }
}
