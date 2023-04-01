using System.Text;
using AdventOfCode2022.Utilities;

namespace AdventOfCode2022.Puzzles;

public static class Day8
{
    private static string _path = PuzzleUtils.GetFilePath("Day8.txt");
    private static int outerTrees = 0;
    private static int innerTrees = 0;
    private static int[,] grid;

    public static void SolutionPart1()
    {
        var userInput = File.ReadAllLines(_path);
        int gridSize = userInput[0].Length;
        
        grid = FillGrid(gridSize, userInput);

        int visibleTrees = GetVisibleTrees(gridSize);
        
        Console.WriteLine($"Outer Trees: {outerTrees}");
        Console.WriteLine($"Inner Trees: {innerTrees}");
        Console.WriteLine($"Total Trees: {visibleTrees}");
    }

    private static int[,] FillGrid(int gridSize, string[] userInput)
    {
        grid = new int[gridSize, gridSize];
        
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                var input = userInput[i].ToCharArray();
                var inputNumber = int.Parse(input[j].ToString());
                grid[i, j] = inputNumber;
            }
        }

        return grid;
    }

    private static int GetVisibleTrees(int gridSize)
    {
        int visibleTrees = 0;
        
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (IsTreeVisibleInIndex(gridSize, i, j))
                {
                    visibleTrees++;
                }
            }
        }

        return visibleTrees;
    }

    private static bool IsTreeVisibleInIndex(int gridSize,int row, int column)
    {
        if (IsOuterTree(row, column, gridSize))
            return true;
        
        if (IsTreeVisibleFromTop(gridSize, row, column) || IsTreeVisibleFromRight(gridSize, row, column) || 
            IsTreeVisibleFromLeft(gridSize, row, column) || IsTreeVisibleFromBottom(gridSize, row, column))
        {
            return true;
        }

        return false;
    }

    private static bool IsTreeVisibleFromBottom(int gridSize, int row, int column)
    {
        int currentValue = grid[row, column];
        
        for (int i = row + 1; i < gridSize; i++)
        {
            if (grid[i, column] >= currentValue)
            {
                return false;
            }
        }
        
        innerTrees++;
        return true;
    }

    private static bool IsTreeVisibleFromLeft(int gridSize, int row, int column)
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

    private static bool IsTreeVisibleFromRight(int gridSize, int row, int column)
    {
        int currentValue = grid[row, column];
        
        for (int i = column + 1; i < gridSize; i++)
        {
            if (grid[row, i] >= currentValue)
            {
                return false;
            }
        }
        
        innerTrees++;
        return true;
    }

    private static bool IsTreeVisibleFromTop(int gridSize, int row, int column)
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

    // Outer trees visible is n + n-1 + n-1 + n-2
    private static int GetOuterTreesVisible(int n)
    {
        return n + n - 1 + n - 1 + n - 2;
    }

    private static bool IsOuterTree(int row, int column, int gridSize)
    {
        if (row == 0 || column == 0)
        {
            outerTrees++;
            return true;
        }

        if (row == gridSize - 1 || column == gridSize - 1)
        {
            outerTrees++;
            return true;
        }

        return false;
    }
}

