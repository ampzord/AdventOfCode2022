using AdventOfCode2022.Utilities;

namespace AdventOfCode2022.Puzzles;

public static class Day1
{
    public static void Solution()
    {
        string path = PuzzleUtils.GetFilePath("Day1.txt");
        var linesInFile = File.ReadAllLines(path);

        int currentCalories = 0;
        List<int> currCalories = new List<int>();

        foreach (var line in linesInFile)
        {
            if (line != String.Empty)
            {
                currentCalories += int.Parse(line);
            }
            else
            {
                currCalories.Add(currentCalories);
                currentCalories = 0;
            }
        }

        var _currCalories = currCalories.OrderByDescending(i => i);
        Console.WriteLine(String.Join(", ", _currCalories));

        int top3Calories = 0;
        for (int i = 0; i < 3; i++)
        {
            top3Calories += _currCalories.ElementAt(i);
        }

        Console.WriteLine("\n" + top3Calories);
        Console.Read();
    }
}
