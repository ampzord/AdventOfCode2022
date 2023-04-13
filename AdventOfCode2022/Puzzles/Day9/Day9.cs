namespace AdventOfCode2022.Puzzles.Day9;

public static class Day9
{
    private static string[] userInput = File.ReadAllLines(@"Input\Day9.txt");
    public static void SolutionPart1()
    {
        Board board = new Board(5, 6);

        foreach (var input in userInput)
        {
            var parsedInput = ParseInput(input);
            board.MoveRope(parsedInput);
        }
        
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
