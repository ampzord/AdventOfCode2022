using AdventOfCode2022.Utilities;

namespace AdventOfCode2022.Puzzles;

public static class Day3
{
    private static string _path = PuzzleUtils.GetFilePath("Day3.txt");
    
    public static void SolutionPart1()
    {
        var linesInFile = File.ReadAllLines(_path);

        int totalPriority = 0;
        foreach (string line in linesInFile)
        {
            ISet<char> compartment = new HashSet<char>();
            int sizeOfCompartment = line.Length / 2;

            string firstCompartment = line.Substring(0, sizeOfCompartment);
            string secondCompartment = line.Substring(sizeOfCompartment, sizeOfCompartment);

            totalPriority += CalculatePriority(compartment, firstCompartment, secondCompartment);
        }

        Console.WriteLine("Total Priority: " + totalPriority);
        Console.ReadKey();
    }

    public static void SolutionPart2()
    {
        string[] linesInFile = File.ReadAllLines(_path);

        int totalPriority = 0;
        for (int i = 0; i < linesInFile.Length - 2; i = i + 3)
        {
            string line1 = linesInFile[i];
            string line2 = linesInFile[i + 1];
            string line3 = linesInFile[i + 2];

            string[] group = new string[] { line1, line2, line3 };
            totalPriority += CalculateGroupPriority(group);
        }

        Console.WriteLine("Total Priority: " + totalPriority);
        Console.ReadKey();
    }

    private static int CalculatePriority(ISet<char> compartment, string firstCompartment, string secondCompartment)
    {
        int priority = 0;
        foreach (char item in firstCompartment)
        {
            compartment.Add(item);
        }

        ISet<char> checkedItems = new HashSet<char>();
        foreach (char item in secondCompartment)
        {
            if (compartment.Contains(item) && checkedItems.Add(item))
            {
                if (char.IsUpper(item))
                {
                    int count = (int)item - 'A' + 27;
                    priority += count;
                }
                else
                {
                    int count = (int)item - 'a' + 1;
                    priority += count;
                }
            }
        }

        return priority;
    }

    private static int CalculateGroupPriority(string[] group)
    {
        ISet<char> firstRuckSack = new HashSet<char>();
        ISet<char> secondRuckSack = new HashSet<char>();
        ISet<char> thirdRuckSack = new HashSet<char>();
        List<ISet<char>> ruckSackGroup = new List<ISet<char>>();

        for (int i = 0; i < group.Length; i++)
        {
            foreach(char item in group[i])
            {
                if (i == 0)
                {
                    firstRuckSack.Add(item);
                }
                else if (i == 1)
                {
                    secondRuckSack.Add(item);
                }
                else if (i == 2)
                {
                    thirdRuckSack.Add(item);
                }
            }
        }

        ruckSackGroup.Add(firstRuckSack);
        ruckSackGroup.Add(secondRuckSack);
        ruckSackGroup.Add(thirdRuckSack);

        char value = GetRepeatedChar(ruckSackGroup);
        int priority = GetPriorityFromChar(value);

        return priority;
    }

    private static char GetRepeatedChar(List<ISet<char>> ruckSackGroup)
    {
        char value = ' ';
        foreach (var item in ruckSackGroup[0])
        {
            if (ruckSackGroup[1].Contains(item) && ruckSackGroup[2].Contains(item))
            {
                value = item;
                break;
            }
        }
        return value;
    }

    private static int GetPriorityFromChar(char value)
    {
        int priority = 0;

        if (char.IsUpper(value))
        {
            priority = (int)value - 'A' + 27;
        }
        else
        {
            priority = (int)value - 'a' + 1;
        }

        return priority;
    }
}
