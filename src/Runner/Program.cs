// See https://aka.ms/new-console-template for more information

using GameScorer;
using GameScorer.Parsers;

Console.WriteLine("Hello, World!");

var gameScorer = new Scorer(new RegexParser());

gameScorer.PlayRound("(1,1,1,1,1) ones");
gameScorer.PlayRound("(2,2,2,2,2) twos");
gameScorer.PlayRound("(3,3,3,3,3) threes");
gameScorer.PlayRound("(4,4,4,4,4) fours");
gameScorer.PlayRound("(5,5,5,5,5) fives");
gameScorer.PlayRound("(6,6,6,6,6) sixes");
gameScorer.PlayRound("(6,6,6,1,1) threeofakind");
gameScorer.PlayRound("(6,6,6,6,1) fourofakind");
gameScorer.PlayRound("(6,6,6,6,6) fiveofakind");
gameScorer.PlayRound("(6,6,5,5,1) twopairs");
gameScorer.PlayRound("(6,6,6,5,5) fullhouse");

Console.WriteLine($"Total Score: {gameScorer.TotalScore}");

// To halt the output
Console.ReadKey();