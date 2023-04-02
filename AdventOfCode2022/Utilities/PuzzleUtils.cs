using System.Reflection;

namespace AdventOfCode2022.Utilities;
public static class PuzzleUtils
{
    public static string GetFilePath(string filename)
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        string currentPath = Directory.GetCurrentDirectory();
        
        return currentPath;
    }
}

