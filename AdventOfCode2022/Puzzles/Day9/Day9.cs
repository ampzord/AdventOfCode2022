namespace AdventOfCode2022.Puzzles.Day9;

public static class Day9
{
    private static readonly string[] _userInput = File.ReadAllLines(@"Input\Day9.txt");
    private const int knotDistancePart1 = 2;
    private const int knotDistancePart2 = 10;
    
    /// <summary>
    /// Solution done with OutOfBounds verification
    /// </summary>
    public static void SolutionPart2()
    {
        var board = new Board(2000, 2000, knotDistancePart2);

        foreach (var input in _userInput)
        {
            var parsedInput = ParseInput(input);
            board.Move(parsedInput);
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
