using AdventOfCode2022.Utilities;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.VisualBasic.FileIO;

namespace AdventOfCode2022.Puzzles;

public static class Day7
{
    private static string path = PuzzleUtils.GetFilePath("Day7_Example.txt");
    private static IEnumerator<string> _enumerator;
    private static IEnumerator<string> _enumeratorTest;
    private static List<string> _allLines = System.IO.File.ReadLines(path).ToList();
    private static List<string> _auxiliarLines = System.IO.File.ReadLines(path).ToList();
    public static void SolutionPart1()
    {
        Directory topRoot = new Directory("topRoot");
        Directory root = new Directory("/", topRoot);
        
        //root has 10 files created, not creating new directories..
        
        //use list outsi
        
        _enumerator = _allLines.GetEnumerator();
        _enumeratorTest = _allLines.GetEnumerator();
        while (_enumerator.MoveNext())
        {
            string line = _enumerator.Current;
            _enumeratorTest.MoveNext();
            
            // _allLines = _allLines.Skip(1).ToList();
            // _enumerator = _allLines.GetEnumerator();

            if (line.IsCommand())
            {
                CommandType commandType = GetCommandType(line);
                switch (commandType)
                {
                    case CommandType.ListDirectory:
                        GetAllValuesAfterListDirectory(root);
                        break;
                    case CommandType.GoToDirectory or CommandType.GoToParentDirectory:
                        ChangeDirectory(root, line);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine("LINE IS NOT COMMAND");
            }
            
        }
        
        // at most 100_000
        var atMost = root.SubDirectories.Where(d => d.CalculateSizeOfDirectory() <= 100_000).ToList();

        Console.WriteLine(atMost.Sum());
    }

    private static Directory ChangeDirectory(Directory directory, string line)
    {
        var splitCommands = line.Split(" ");
        
        //Go to ParentDirectory
        if (splitCommands[2].Equals(".."))
        {
            directory = directory.ParentDirectory;
        }
        else //Go to SubDirectory
        {
            foreach (var dir in directory.SubDirectories)
            {
                if (dir.Name.Equals(splitCommands[2], StringComparison.OrdinalIgnoreCase))
                {
                    directory = dir;
                }
            }
        }

        return directory;
    }

    private static Directory GetAllValuesAfterListDirectory(Directory directory)
    {
        while (_enumerator.MoveNext() && !_enumerator.Current.IsCommand())
        {
            _enumeratorTest.MoveNext();
            string line = _enumerator.Current;
            var splitCommands = line.Split(" ");
            
            // Add Subdirectory
            if (line.StartsWith("dir"))
            {
                directory.SubDirectories.Add(new Directory(splitCommands[1], directory));
            }
            else // Add files
            {
                string fileName = splitCommands[1];
                decimal fileSize = decimal.Parse(splitCommands[0]);
                    
                directory.Files.Add(new File(fileName, fileSize));
            }
            
            //_allLines = _allLines.Skip(1).ToList();
            //_auxiliarLines = _allLines;
            //_enumerator = _allLines.GetEnumerator();
        }

        _enumerator = _enumeratorTest;
        
        return directory;
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
    public class Directory
    {
        public Directory() { }
        public Directory(string name)
        {
            Name = name;
        }
        public Directory(string name, Directory parentDirectory)
        {
            Name = name;
            ParentDirectory = parentDirectory;
        }
        public string Name { get; set; }
        public decimal? Size { get; set; }
        public List<File> Files { get; set; } = new();
        public List<Directory> SubDirectories { get; set; } = new();
        public Directory ParentDirectory { get; set; }
        public decimal? CalculateSizeOfDirectory()
        {
            decimal? totalSize = 0M;

            if (Files.Any())
                totalSize += Files.GetSize();
            if (SubDirectories.Any())
                totalSize += SubDirectories.GetSize();

            Size = totalSize;
            return totalSize;
        }
    }
    public class File
    {
        public File(string name, decimal size)
        {
            Name = name;
            Size = size;
        }

        public readonly string Name;
        public readonly decimal Size;
    }
    internal enum CommandType
    {
        ListDirectory,
        GoToDirectory,
        GoToParentDirectory,
        GoToRoot
    }
    public static bool IsCommand(this string s)
    {
        if (s[0].Equals('$'))
        {
            return true;
        }

        return false;
    }
    public static decimal? GetSize(this List<Directory> directories)
    {
        decimal? totalSize = 0;
        foreach (var dir in directories)
        {
            totalSize += dir.CalculateSizeOfDirectory();
        }

        return totalSize;
    }
    public static decimal? GetSize(this List<File> files)
    {
        decimal? totalSize = 0;
        foreach (var file in files)
        {
            totalSize += file.Size;
        }

        return totalSize;
    }
    
    public static decimal Sum(this List<Directory> directories)
    {
        return (directories.Sum(d => d.Size).Value);
    }
    
}



