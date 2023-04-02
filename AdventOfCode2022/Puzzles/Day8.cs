using System.Text;
using AdventOfCode2022.Utilities;
using Iced.Intel;

namespace AdventOfCode2022.Puzzles;

public static class Day8
{
    private static string[] userInput = File.ReadAllLines(@"Input\Day8.txt");
    private static int _outerTrees = default;
    private static int _innerTrees = default;
    private static int[,] grid;
    private static int _gridLength;

    public static void SolutionPart1()
    {
        grid = FillGrid(userInput[0].Length);
        
        int visibleTrees = GetVisibleTrees();
        
        Console.WriteLine($"Outer Trees: {_outerTrees}");
        Console.WriteLine($"Inner Trees: {_innerTrees}");
        Console.WriteLine($"Total Trees: {visibleTrees}");
    }
    public static void SolutionPart2()
    {
        grid = FillGrid(userInput[0].Length);
        
        int bestScenicScore = GetTreesScenicScore();
        
        Console.WriteLine($"Best Scenic Score: {bestScenicScore}");
    }
    private static int[,] FillGrid(int gridSize)
    {
        grid = new int[gridSize, gridSize];
        _gridLength = grid.GetLength(0);
        
        for (int i = 0; i < _gridLength; i++)
        {
            for (int j = 0; j < _gridLength; j++)
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
        
        for (int i = 0; i < _gridLength; i++)
        {
            for (int j = 0; j < _gridLength; j++)
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

        for (int i = row + 1; i < _gridLength; i++)
        {
            if (grid[i, column] >= currentValue)
            {
                return false;
            }
        }
        
        _innerTrees++;
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

        _innerTrees++;
        return true;
    }
    private static bool IsTreeVisibleFromRight(int row, int column)
    {
        int currentValue = grid[row, column];
        
        for (int i = column + 1; i < _gridLength; i++)
        {
            if (grid[row, i] >= currentValue)
            {
                return false;
            }
        }
        
        _innerTrees++;
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
        
        _innerTrees++;
        return true;
    }
    private static bool IsOuterTree(int row, int column)
    {
        if (row == 0 || column == 0
            || row == _gridLength - 1 || column == _gridLength - 1)
        {
            _outerTrees++;
            return true;
        }
        
        return false;
    }
    private static int GetTreesScenicScore()
    {
        int bestScenicScore = 0;
        
        for (int i = 0; i < _gridLength; i++)
        {
            for (int j = 0; j < _gridLength; j++)
            {
                int tmpScenicScore = GetScenicScoreInIndex(i, j);
                Console.WriteLine($"Current Scenic Score: {tmpScenicScore}, [{i},{j}]");
                if (tmpScenicScore > bestScenicScore)
                    bestScenicScore = tmpScenicScore;
            }
        }

        return bestScenicScore;
    }
    private static int GetScenicScoreInIndex(int row, int column)
    {
        int scenicScoreFromTop = GetScenicScoreFromTop(row, column);
        int scenicScoreFromRight = GetScenicScoreFromRight(row, column);
        int scenicScoreFromLeft = GetScenicScoreFromLeft(row, column);
        int scenicScoreFromBottom = GetScenicScoreFromBottom(row, column);
        
        return CalculateScenicScore(scenicScoreFromTop, scenicScoreFromRight, 
            scenicScoreFromLeft, scenicScoreFromBottom);
    }
    private static int GetScenicScoreFromTop(int row, int column)
    {
        int scenicScore = 0;
        int currentValue = grid[row, column];
        
        for (int i = row - 1; i >= 0; i--)
        {
            scenicScore++;
            if (grid[i, column] >= currentValue)
            {
                break;
            }
        }
        
        return scenicScore;
    }
    private static int GetScenicScoreFromRight(int row, int column)
    {
        int scenicScore = 0;
        int currentValue = grid[row, column];
        
        for (int i = column + 1; i < _gridLength; i++)
        {
            scenicScore++;
            if (grid[row, i] >= currentValue)
            {
                break;
            }
        }

        return scenicScore;
    }
    private static int GetScenicScoreFromLeft(int row, int column)
    {
        int scenicScore = 0;
        int currentValue = grid[row, column];
        
        for (int i = column - 1; i >= 0; i--)
        {
            scenicScore++;

            if (grid[row, i] >= currentValue)
            {
                break;
            }
        }
        
        return scenicScore;
    }
    private static int GetScenicScoreFromBottom(int row, int column)
    {
        int scenicScore = 0;
        int currentValue = grid[row, column];

        for (int i = row + 1; i < _gridLength; i++)
        {
            scenicScore++;
            if (grid[i, column] >= currentValue)
            {
                break;
            }
        }
        
        return scenicScore;
    }
    private static int CalculateScenicScore(int score1, int score2, int score3, int score4)
    {
        int scenicScore = score1 * score2 * score3 * score4;
        return scenicScore;
    }
}



