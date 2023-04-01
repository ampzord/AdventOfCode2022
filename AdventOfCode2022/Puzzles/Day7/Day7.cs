using AdventOfCode2022.Utilities;

namespace AdventOfCode2022.Puzzles.Day7;

public static class Day7
{
    private static readonly string _path = PuzzleUtils.GetFilePath("Day7.txt");
    private static readonly List<string> _allLines = System.IO.File.ReadLines(_path).ToList();
    private const long MaxSize = 100_000;
    private const long TotalAvailableSpaceInDisk = 70_000_000;
    private const long AvailableSpaceInDiskNeeded = 30_000_000;
    
    public static void SolutionPart1()
    {
        Directory root = new Directory("/", null);
        Directory currentDir = root;
        for (int i = 0; i < _allLines.Count; i++)
        {
            string line = _allLines[i];
            if (line.IsCommand())
            {
                CommandType commandType = GetCommandType(line);
                switch (commandType)
                {
                    case CommandType.ListDirectory:
                        (currentDir, i) = GetInfoAfterListDirectory(currentDir, i + 1);
                        break;
                    case CommandType.GoToDirectory or CommandType.GoToParentDirectory:
                        currentDir = ChangeDirectory(currentDir, line);
                        break;
                    default:
                        break;
                }
            }
        }

        var smallDirectories = new List<Directory>();
        TraverseDirectoriesPart1(root, smallDirectories);
        
        Console.WriteLine(smallDirectories.Sum());
    }
    public static void SolutionPart2()
    {
        Directory root = new Directory("/", null);
        Directory currentDir = root;
        for (int i = 0; i < _allLines.Count; i++)
        {
            string line = _allLines[i];
            if (line.IsCommand())
            {
                CommandType commandType = GetCommandType(line);
                switch (commandType)
                {
                    case CommandType.ListDirectory:
                        (currentDir, i) = GetInfoAfterListDirectory(currentDir, i + 1);
                        break;
                    case CommandType.GoToDirectory or CommandType.GoToParentDirectory:
                        currentDir = ChangeDirectory(currentDir, line);
                        break;
                    default:
                        break;
                }
            }
        }
        
        TraverseDirectories(root);
        
        long? currentAvailableSpace = TotalAvailableSpaceInDisk - root.Size;
        long? neededSpace = AvailableSpaceInDiskNeeded - currentAvailableSpace;
        var directoriesToDelete = new List<Directory>();
        
        TraverseDirectoriesPart2(root, directoriesToDelete, neededSpace);
        directoriesToDelete.Sort();
        Console.WriteLine(directoriesToDelete.First().Size);
    }
    private static Directory ChangeDirectory(Directory directory, string line)
    {
        var splitCommands = line.Split(" ");
        
        //Go to ParentDirectory
        if (splitCommands[2].Equals(".."))
        {
            return directory.Parent;
        }
        else //Go to SubDirectory
        {
            string targetDirectoryName = splitCommands[2];
            foreach (var dir in directory.SubDirectories)
            {
                if (dir.Name.Equals(targetDirectoryName, StringComparison.OrdinalIgnoreCase))
                {
                    return dir;
                }
            }
        }
        
        return null;
    }
    private static (Directory, int) GetInfoAfterListDirectory(Directory directory, int index)
    {
        int currentIndex = index;
        for (int i = index; i < _allLines.Count; i++)
        {
            string line = _allLines[i];

            if (line.IsCommand())
            {
                currentIndex = i - 1;
                break;
            }

            var splitCommands = line.Split(" ");
            
            // Add Subdirectory
            if (line.StartsWith("dir"))
            {
                directory.SubDirectories.Add(new Directory(splitCommands[1], directory));
            }
            else // Add files
            {
                string fileName = splitCommands[1];
                long fileSize = long.Parse(splitCommands[0]);
                
                directory.Files.Add(new File(fileName, fileSize));
            }
        }

        return (directory, currentIndex);
    }
    private static CommandType GetCommandType(string line)
    {
        CommandType commandType = new();
        if (line.StartsWith(@"$ cd /"))
        {
            commandType = CommandType.GoToRoot;
        }
        else if (line.StartsWith(@"$ cd .."))
        {
            commandType = CommandType.GoToParentDirectory;
        }
        else if (line.StartsWith(@"$ ls"))
        {
            commandType = CommandType.ListDirectory; 
        }
        else if (line.StartsWith(@"$ cd"))
        {
            commandType = CommandType.GoToDirectory;     
        }

        return commandType;
    }
    public static void TraverseDirectoriesPart1(Directory source, List<Directory> smallDirectories)
    {
        if (source.Size <= MaxSize)
        {
            smallDirectories.Add(source);
        }

        foreach (Directory dir in source.SubDirectories)
        {
            source.SubDirectories.GetSize();
            TraverseDirectoriesPart1(dir, smallDirectories);
        }
    }
    public static void TraverseDirectories(Directory source)
    {
        source.CalculateSizeOfDirectory();
        foreach (Directory dir in source.SubDirectories)
        {
            source.SubDirectories.GetSize();
            TraverseDirectories(dir);
        }
    }
    public static void TraverseDirectoriesPart2(Directory source, List<Directory> enoughSpaceDirectories, long? neededSpace)
    {
        if (source.Size >= neededSpace)
        {
            enoughSpaceDirectories.Add(source);
        }
        
        foreach (Directory dir in source.SubDirectories)
        {
            source.SubDirectories.GetSize();
            TraverseDirectoriesPart2(dir, enoughSpaceDirectories, neededSpace);
        }
    }
}



