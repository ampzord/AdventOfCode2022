namespace AdventOfCode2022.Puzzles.Day7;

public class File
{
    public File(string name, long size)
    {
        Name = name;
        Size = size;
    }

    public readonly string Name;
    public readonly long Size;
}

public static class FileExtension
{
    public static long? GetSize(this List<File> files)
    {
        long? totalSize = 0;
        foreach (var file in files)
        {
            totalSize += file.Size;
        }

        return totalSize;
    }
}