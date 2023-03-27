using AdventOfCode2022.Utilities;
using System;
using Microsoft.VisualBasic.FileIO;

namespace AdventOfCode2022.Puzzles;

public static class Day7
{
    private static string path = PuzzleUtils.GetFilePath("Day7.txt");
    
    //Find the directories with max size of 100_000
    //Then sum them up altogether

    public static void SolutionPart1()
    {
        
        // $ cd /
        // $ ls
        // dir gqlg
        // dir hchrwstr
        // dir lswlpt
        // 189381 mzsnhlf
        // dir plmdrbn

    }
    
    private static void ParseInput()
    {
        var lines = System.IO.File.ReadLines(path);
        
        Directory root = new Directory();
        
        foreach (var line in lines)
        {
            if (line.IsCommand())
            {
                CommandType commandType = GetCommandType(line);
                switch (commandType)
                {
                    case CommandType.ListDirectory:
                        
                        break;
                    case CommandType.GoToDirectory:
                        break;
                    case CommandType.GoToRoot:
                        break;
                    case CommandType.GoBackInDirectory:
                        break;
                }
            }



        }

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
            commandType = CommandType.GoBackInDirectory;
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

    private static bool IsCommand(char c)
    {
        if (c.Equals('$'))
        {
            return true;
        }

        return false;
    }
    
    
    

    internal class Directory
    {
        public string Name { get; set; }
        public decimal Size { get; set; }
        public List<File> Files { get; set; }
        public List<Directory> SubDirectories { get; set; }
        
    }

    internal class File
    {
        public string Name { get; set; }
        public decimal Size { get; set; }
    }
    
    internal enum CommandType
    {
        ListDirectory,
        GoToDirectory,
        GoBackInDirectory,
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

}



