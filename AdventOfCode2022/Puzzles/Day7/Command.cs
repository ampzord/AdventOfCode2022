namespace AdventOfCode2022.Puzzles.Day7;

public enum CommandType
{
    ListDirectory,
    GoToDirectory,
    GoToParentDirectory,
    GoToRoot
}

public static class StringExtension 
{
    public static bool IsCommand(this string s)
    {
        if (s[0].Equals('$'))
        {
            return true;
        }

        return false;
    }
}

