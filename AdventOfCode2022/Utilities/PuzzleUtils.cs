namespace AdventOfCode2022.Utilities;
public static class PuzzleUtils
{
    public static string GetFilePath(string filename)
    {
        string projectPath = Directory.GetCurrentDirectory();
        string fileTargetPath = @"Input\" + filename;
        string path = Path.GetFullPath(Path.Combine(projectPath, @"..\..\..\"));
        string currentPath = Path.GetFullPath(Path.Combine(path, fileTargetPath));

        return currentPath;
    }
}

