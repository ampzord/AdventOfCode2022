using System.Text;
using AdventOfCode2022.Utilities;

namespace AdventOfCode2022.Puzzles;

public static class Day8
{
    private static string _path = PuzzleUtils.GetFilePath("Day8.txt");
    private static int outerTrees = 0;
    private static int innerTrees = 0;
    private static int[,] grid;
    private static int gridLength;

    public static void SolutionPart1()
    {
        var userInput = File.ReadAllLines(_path);
        grid = FillGrid(userInput, userInput[0].Length);
        
        int visibleTrees = GetVisibleTrees();
        
        Console.WriteLine($"Outer Trees: {outerTrees}");
        Console.WriteLine($"Inner Trees: {innerTrees}");
        Console.WriteLine($"Total Trees: {visibleTrees}");
    }

    private static int[,] FillGrid(string[] userInput, int gridSize)
    {
        grid = new int[gridSize, gridSize];
        gridLength = grid.GetLength(0);
        
        for (int i = 0; i < gridLength; i++)
        {
            for (int j = 0; j < gridLength; j++)
            {
                var input = userInput[i].ToCharArray();
                var inputNumber = int.Parse(input[j].ToString());
                grid[i, j] = inputNumber;
            }
        }

        return grid;
    }

    private static int GetVisibleTrees()
    {
        int visibleTrees = 0;
        
        for (int i = 0; i < gridLength; i++)
        {
            for (int j = 0; j < gridLength; j++)
            {
                if (IsTreeVisibleInIndex( i, j))
                {
                    visibleTrees++;
                }
            }
        }

        return visibleTrees;
    }

    private static bool IsTreeVisibleInIndex(int row, int column)
    {
        if (IsOuterTree(row, column))
            return true;
        
        if (IsTreeVisibleFromTop(row, column) || IsTreeVisibleFromRight(row, column) || 
            IsTreeVisibleFromLeft(row, column) || IsTreeVisibleFromBottom(row, column))
        {
            return true;
        }

        return false;
    }

    private static bool IsTreeVisibleFromBottom(int row, int column)
    {
        int currentValue = grid[row, column];
        
        for (int i = row + 1; i < gridLength; i++)
        {
            if (grid[i, column] >= currentValue)
            {
                return false;
            }
        }
        
        innerTrees++;
        return true;
    }

    private static bool IsTreeVisibleFromLeft(int row, int column)
    {
        int currentValue = grid[row, column];
        
        for (int i = column - 1; i >= 0; i--)
        {
            if (grid[row, i] >= currentValue)
            {
                return false;
            }
        }

        innerTrees++;
        return true;
    }

    private static bool IsTreeVisibleFromRight(int row, int column)
    {
        int currentValue = grid[row, column];
        
        for (int i = column + 1; i < gridLength; i++)
        {
            if (grid[row, i] >= currentValue)
            {
                return false;
            }
        }
        
        innerTrees++;
        return true;
    }

    private static bool IsTreeVisibleFromTop(int row, int column)
    {
        int currentValue = grid[row, column];
        
        for (int i = row - 1; i >= 0; i--)
        {
            if (grid[i, column] >= currentValue)
            {
                return false;
            }
        }
        
        innerTrees++;
        return true;
    }
    
    private static bool IsOuterTree(int row, int column)
    {
        if (row == 0 || column == 0
            || row == gridLength - 1 || column == gridLength - 1)
        {
            outerTrees++;
            return true;
        }
        
        return false;
    }
}

