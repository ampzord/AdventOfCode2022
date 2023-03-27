using AdventOfCode2022.Utilities;

namespace AdventOfCode2022.Puzzles;

public static class Day6
{
    private static string path = PuzzleUtils.GetFilePath("Day6.txt");
    private const int PROTOCOL_SEQUENCE_VALUE = 14; // solution2
    
    public static void SolutionPart1()
    {
        var input = File.ReadAllText(path);

        int characterLengthSignal = CalculateSignal(input);

        Console.WriteLine("Character Length Signal: " + characterLengthSignal);
        Console.ReadKey();
    }

    private static int CalculateSignal(string input)
    {
        List<char> possibleSignal = new List<char>();
        int characterLengthSignal = 0;
        for(int i = 0; i < input.Length; i++) 
        {
            if (i < PROTOCOL_SEQUENCE_VALUE - 1)
            {
                possibleSignal.Add(input[i]);
                continue;
            }

            possibleSignal.Add(input[i]);

            var signalSet = possibleSignal.ToHashSet();

            if (signalSet.Count == PROTOCOL_SEQUENCE_VALUE)
            {
                characterLengthSignal = i + 1;
                break;
            }

            possibleSignal.Remove(input[i - (PROTOCOL_SEQUENCE_VALUE - 1)]);
        }

        return characterLengthSignal;
    }
     
}

