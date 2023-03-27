using AdventOfCode2022.Utilities;

namespace AdventOfCode2022.Puzzles;
public static class Day4
{
    private static string path = PuzzleUtils.GetFilePath("Day4.txt");
    
    public static void SolutionPart1()
    {
        var linesInFile = File.ReadAllLines(path);

        int totalOverridenActivities = 0;
        foreach (string line in linesInFile)
        {
            List<KeyValuePair<int, int>> pairs = ParsePairs(line);

            if (IsOverridenActivity(pairs))
                totalOverridenActivities++;

        }

        Console.WriteLine("Total Overriden Activities: " + totalOverridenActivities);
        Console.ReadKey();
    }

    public static void SolutionPart2()
    {
        var linesInFile = File.ReadAllLines(path);

        int totalOverlappingActivities = 0;
        foreach (string line in linesInFile)
        {
            List<KeyValuePair<int, int>> pairs = ParsePairs(line);

            if (IsOverlappingActivity(pairs))
                totalOverlappingActivities++;

        }

        Console.WriteLine("Total Overlapping Activities: " + totalOverlappingActivities);
        Console.ReadKey();
    }

    private static List<KeyValuePair<int,int>> ParsePairs(string pair)
    {
        List<KeyValuePair<int, int>> outputPairs = new List<KeyValuePair<int, int>>();
        List<string> sepparatedPairs = pair.Split(',').ToList();
        List<string> firstPair = sepparatedPairs[0].Split('-').ToList();
        List<string> secondPair = sepparatedPairs[1].Split('-').ToList();

        int lowerValueFirstPair = Int32.Parse(firstPair[0]);
        int higherValueFirstPair = Int32.Parse(firstPair[1]);

        int lowerValueSecondPair = Int32.Parse(secondPair[0]);
        int higherValueSecondPair = Int32.Parse(secondPair[1]);

        KeyValuePair<int, int> _firstPair = new KeyValuePair<int, int>(lowerValueFirstPair, higherValueFirstPair);
        KeyValuePair<int, int> _secondPair = new KeyValuePair<int, int>(lowerValueSecondPair, higherValueSecondPair);

        outputPairs.Add(_firstPair);
        outputPairs.Add(_secondPair);

        return outputPairs;
    }
    
    private static bool IsOverridenActivity(List<KeyValuePair<int,int>> pairs)
    {
        KeyValuePair<int, int> firstPair = pairs[0];
        KeyValuePair<int, int> secondPair = pairs[1];

        HashSet<int> firstSet = new HashSet<int>();
        HashSet<int> secondSet = new HashSet<int>();

        for (int i = firstPair.Key; i < firstPair.Value + 1; i++)
        {
            firstSet.Add(i);
        }

        for (int i = secondPair.Key; i < secondPair.Value + 1; i++)
        {
            secondSet.Add(i);
        }

        if (firstSet.IsSubsetOf(secondSet) || secondSet.IsSubsetOf(firstSet))
            return true;

        return false;
    }

    private static bool IsOverlappingActivity(List<KeyValuePair<int, int>> pairs)
    {
        KeyValuePair<int, int> firstPair = pairs[0];
        KeyValuePair<int, int> secondPair = pairs[1];

        HashSet<int> firstSet = new HashSet<int>();
        HashSet<int> secondSet = new HashSet<int>();

        for (int i = firstPair.Key; i < firstPair.Value + 1; i++)
        {
            firstSet.Add(i);
        }

        for (int i = secondPair.Key; i < secondPair.Value + 1; i++)
        {
            secondSet.Add(i);
        }

        var result = firstSet.Intersect(secondSet);

        if (result.Any())
            return true;

        return false;
    }

}
