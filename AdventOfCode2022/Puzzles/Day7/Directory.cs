namespace AdventOfCode2022.Puzzles.Day7;

public class Directory : IComparable<Directory>
{
    public Directory(string name, Directory parent)
    {
        Name = name;
        Parent = parent;
    }
    public string Name { get; set; }
    public long? Size { get; set; }
    public List<File> Files { get; set; } = new();
    public List<Directory> SubDirectories { get; set; } = new();
    public Directory Parent { get; set; }
    public long? CalculateSizeOfDirectory()
    {
        if (Size is not null)
        {
            return Size;
        }
            
        long? totalSize = 0;

        if (Files.Any())
            totalSize += Files.GetSize();
        if (SubDirectories.Any())
            totalSize += SubDirectories.GetSize();

        Size = totalSize;
        return totalSize;
    }
    public int CompareTo(Directory? other)
    {
        if (other == null || Size > other.Size)
            return 1;
        if (Size < other.Size)
            return -1;
        return 0;
    }
}

public static class DirectoryExtension
{
    public static long? GetSize(this List<Directory> directories)
    {
        long? totalSize = 0;
        foreach (var dir in directories)
        {
            totalSize += dir.CalculateSizeOfDirectory();
        }

        return totalSize;
    }
    
    public static decimal Sum(this List<Directory> directories)
    {
        return (directories.Sum(d => d.Size).Value);
    }
}

