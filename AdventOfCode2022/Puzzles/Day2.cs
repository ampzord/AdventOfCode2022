using AdventOfCode2022.Utilities;

namespace AdventOfCode2022.Puzzles;
public static class Day2
{
    private static Dictionary<string, string> possiblePlays = new();
    private static Dictionary<Dictionary<string, string>, string> newPossiblePlays = new();

    private enum MyPlayScore
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    public static void Solution()
    {
        var linesInFile = File.ReadAllLines(@"Input\Day2.txt");

        newPossiblePlays = new Dictionary<Dictionary<string, string>, string>
        {
            { new Dictionary<string, string>() { { "A", "X" } }, "C" }, //Rock + Lose -> Scissors
            { new Dictionary<string, string>() { { "A", "Y" } }, "A" }, //Rock + Draw -> Rock
            { new Dictionary<string, string>() { { "A", "Z" } }, "B" }, //Rock + Win -> Paper
            { new Dictionary<string, string>() { { "B", "X" } }, "A" }, //Paper + Lose -> Rock
            { new Dictionary<string, string>() { { "B", "Y" } }, "B" }, //Paper + Draw -> Paper
            { new Dictionary<string, string>() { { "B", "Z" } }, "C" }, //Paper + Win -> Scissors
            { new Dictionary<string, string>() { { "C", "X" } }, "B" }, //Scissors + Lose -> Paper
            { new Dictionary<string, string>() { { "C", "Y" } }, "C" }, //Scissors + Draw -> Scissors
            { new Dictionary<string, string>() { { "C", "Z" } }, "A" } //Scissors + Win -> Rock
        };

        possiblePlays = new Dictionary<string, string>
        {
            { "A", "Rock" },
            { "B", "Paper" },
            { "C", "Scissors" }
        };

        Dictionary<string, string> possibleScore = new Dictionary<string, string>
        {
            { "X", "Lose" },
            { "Y", "Draw" },
            { "Z", "Win" }
        };

        int currentScore = 0;
        foreach (var line in linesInFile)
        {
            string[] play = line.Split(' ');
            var opponentPlay = play[0];
            var scoreToPlayFor = possibleScore[play[1]];

            var myPlay = GetMyPlay(opponentPlay, play[1]);
            currentScore += ResolveHand(scoreToPlayFor, myPlay);
        }

        Console.WriteLine("Score: " + currentScore);
        Console.ReadKey();

    }
    private static int ResolveHand(string scoreToPlayFor, string myPlay)
    {
        int score = 0;

        if (scoreToPlayFor.Equals("Draw"))
        {
            score += 3;
        }

        else if (scoreToPlayFor.Equals("Win"))
        {
            score += 6;
        }

        if (possiblePlays[myPlay].Equals("Rock"))
        {
            score += (int)MyPlayScore.Rock;
        }
        else if (possiblePlays[myPlay].Equals("Paper"))
        {
            score += (int)MyPlayScore.Paper;
        }
        else if (possiblePlays[myPlay].Equals("Scissors"))
        {
            score += (int)MyPlayScore.Scissors;
        }

        return score;
       
    }

    private static string GetMyPlay(string opponentPlay, string myPlay)
    {
        Dictionary<string, string> play = new Dictionary<string, string>()
        {
            { opponentPlay, myPlay }
        };

        string _myPlay = String.Empty;
        foreach (var possiblePlay in newPossiblePlays)
        {
            if (possiblePlay.Key.ElementAt(0).Equals(play.ElementAt(0)))
            {
                _myPlay = possiblePlay.Value;
                break;
            }
        }

        return _myPlay;
    }
}
