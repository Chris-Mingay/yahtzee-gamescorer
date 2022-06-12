using GameScorer;

Console.WriteLine("Play a number of example rounds of Yahtzee");
Console.WriteLine("");

List<string> rounds = new List<string>()
{
    "(1,1,1,1,1) ones",
    "(2,2,2,2,2) twos",
    "(3,3,3,3,3) threes",
    "(4,4,4,4,4) fours",
    "(5,5,5,5,5) fives",
    "(6,6,6,6,6) sixes",
    "(6,6,6,1,1) threeofakind",
    "(6,6,6,6,1) fourofakind",
    "(6,6,6,6,6) fiveofakind",
    "(6,6,5,5,1) twopairs",
    "(6,6,6,5,5) fullhouse"
};

var gameScorer = new Scorer();
foreach (var round in rounds)
{
    var score = gameScorer.PlayRound(round);
    Console.WriteLine($"Playing round '{round}' returned a score of {score}");
}

Console.WriteLine("");
Console.WriteLine($"Total Score: {gameScorer.TotalScore}");

// To halt the output
Console.WriteLine("");
Console.WriteLine("Press any key to quit");
Console.ReadKey();