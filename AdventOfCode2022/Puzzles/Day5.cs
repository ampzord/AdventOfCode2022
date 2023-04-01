using AdventOfCode2022.Utilities;
using System.Collections;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Puzzles;

public static class Day5
{
    private static string _path = PuzzleUtils.GetFilePath("Day5.txt");
    
    public static void SolutionPart1()
    {
        var linesInFile = File.ReadAllLines(_path);

        List<Stack> stacks = ParseInput(linesInFile);
        foreach (string line in linesInFile)
        {
            if (!line.StartsWith("move"))
            {
                continue;
            }

            string[] numbers = Regex.Split(line, @"\D+");
            int quantityToMove = int.Parse(numbers[1]);
            int fromStack = int.Parse(numbers[2]) - 1;
            int toStack = int.Parse(numbers[3]) - 1;

            for(int i = 0; i < quantityToMove; i++)
            {
                var getTopValue = stacks[fromStack].Peek();
                stacks[fromStack].Pop();
                stacks[toStack].Push(getTopValue);
            }
        }

        for (int i = 0; i < stacks.Count; i++)
        {
            Console.WriteLine("Stack " + i + " : " + stacks[i].Peek());
        }
        Console.ReadKey();
    }

    public static void SolutionPart2()
    {
        var linesInFile = File.ReadAllLines(_path);

        List<Stack> stacks = ParseInput(linesInFile);
        foreach (string line in linesInFile)
        {
            if (!line.StartsWith("move"))
            {
                continue;
            }

            string[] numbers = Regex.Split(line, @"\D+");
            int quantityToMove = int.Parse(numbers[1]);
            int fromStack = int.Parse(numbers[2]) - 1;
            int toStack = int.Parse(numbers[3]) - 1;

            List<object> cratesToMove = new List<object>();
            for (int i = 0; i < quantityToMove; i++)
            {
                cratesToMove.Add(stacks[fromStack].Peek());
                stacks[fromStack].Pop();
            }

            for (int i = quantityToMove-1; i >= 0; i--)
            {
                stacks[toStack].Push(cratesToMove[i]);
            }

        }

        for (int i = 0; i < stacks.Count; i++)
        {
            Console.WriteLine("Stack " + i + " : " + stacks[i].Peek());
        }
        Console.ReadKey();
    }

    private static List<Stack> ParseInput(string[] linesInFile)
    {
        List<string> inputLines = new List<string>();
        foreach (string line in linesInFile)
        {
            inputLines.Add(line);

            if (line.StartsWith(" 1"))
            {
                break;
            }

        }

        char[,] arrayInput = GetTwoDimensionalArrayOfInput(inputLines);

        List<Stack> stacks = CreateStacks(arrayInput);

        return stacks;
    }

    private static char[,] GetTwoDimensionalArrayOfInput(List<string> inputLines)
    {
        char[,] input = new char[inputLines.Count, inputLines.Count];

        //1-5-9-13-17-21-25-29-33
        for (int i = 0; i < inputLines.Count; i++)
        {
            int denom = 0;
            for (int j = 1; j < 34; j = j + 4)
            {
                input[i, denom] = inputLines[i].ElementAt(j);
                denom++;
            }

        }

        return input;
    }

    private static List<Stack> CreateStacks(char[,] array)
    {
        List<Stack> stacks = new List<Stack>();
        for (int j = 0; j < array.GetLength(0); j++)
        {
            Stack stack = new Stack();
            for (int i = array.GetLength(0)-2; i >= 0; i--)
            {
                string currentValue = array[i, j].ToString();
                if (!String.IsNullOrWhiteSpace(currentValue)) 
                {
                    stack.Push(currentValue);
                }
            }
            stacks.Add(stack);
        }

        return stacks;
    }
}

