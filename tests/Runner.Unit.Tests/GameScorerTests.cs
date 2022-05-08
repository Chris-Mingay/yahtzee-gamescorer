using System;
using System.Collections.Generic;
using FluentAssertions;
using GameScorer;
using GameScorer.Config;
using GameScorer.Exceptions;
using GameScorer.Interfaces;
using GameScorer.Parsers;
using GameScorer.Rulesets;
using Xunit;

namespace Runner.Unit.Tests;

public class GameScorerTests
{

    [Fact]
    public void Ctor1_ShouldSetExpectedParser()
    {
        var gameScorer = new Scorer();
        var expectedOutput = typeof(RoundParser);

        gameScorer.Parser.Should().BeOfType(expectedOutput);
        gameScorer.RulesetCount.Should().Be(11);
    }
    
    [Fact]
    public void Ctor2_ShouldSetExpectedParserAndRulsets()
    {
        var parser = new TestParser();
        var gameScorer = new Scorer(new GameScorerOptions{ RoundParser = parser});
        var expectedOutput = typeof(TestParser);
        
        gameScorer.Parser.Should().BeOfType(expectedOutput);
        gameScorer.RulesetCount.Should().Be(11);
    }
    
    [Fact]
    public void Ctor2_ShouldSetProvidedRulsets()
    {
        var rulesets = new Dictionary<string, IRuleset>();
        rulesets.Add("test1", new FullHouseRuleset());
        rulesets.Add("test2", new FullHouseRuleset());
        rulesets.Add("test3", new FullHouseRuleset());
        
        var gameScorer = new Scorer(new GameScorerOptions{ Rulesets = rulesets });

        gameScorer.RulesetCount.Should().Be(rulesets.Count);
    }
    
    [Fact]
    public void RemoveRuleset_ShouldRemoveRuleset_WhenPresent()
    {
        var gameScorer = new Scorer();
        var expectedOutput = gameScorer.RulesetCount - 1;
        
        gameScorer.RemoveRuleset("ones");

        gameScorer.RulesetCount.Should().Be(expectedOutput);
    }
    
    [Fact]
    public void RemoveRuleset_ShouldThrowException_WhenNotPresent()
    {
        var gameScorer = new Scorer();

        Action act = () =>
        {
            gameScorer.RemoveRuleset("badrequest");
        };

        act.Should().Throw<RulesetNotFoundException>();
    }
    
    [Fact]
    public void ClearRulesets_ShouldRemoveAllRulesets()
    {
        var gameScorer = new Scorer();
        
        gameScorer.ClearRulesets();

        gameScorer.RulesetCount.Should().Be(0);
    }

    [Fact]
    public void AddRuleset_ShouldAddNewRuleset_WhenNotPresent()
    {
        var gameScorer = new Scorer();
        var expectedOutput = gameScorer.RulesetCount + 1;
        
        gameScorer.AddRuleset("test", new TwoPairRuleset());

        gameScorer.RulesetCount.Should().Be(expectedOutput);
    }
    
    [Fact]
    public void AddRuleset_ShouldUpdateRuleset_WhenPresent()
    {
        var gameScorer = new Scorer();
        gameScorer.AddRuleset("test", new TwoPairRuleset());
        var expectedCountOutput = gameScorer.RulesetCount;
        var expectedTypeOutput = typeof(FullHouseRuleset);
        
        gameScorer.AddRuleset("test", new FullHouseRuleset());
        
        gameScorer.RulesetCount.Should().Be(expectedCountOutput);
        gameScorer.GetRuleset("test").Should().BeOfType(expectedTypeOutput);
    }

    [Fact]
    public void GetRuleset_ShouldReturnExpectedRuleset_WhenPresent()
    {
        var gameScorer = new Scorer();
        var testRuleset = new OfAKindRuleset(4);
        gameScorer.AddRuleset("test", testRuleset);

        var actualOutput = gameScorer.GetRuleset("test");

        actualOutput.Should().BeEquivalentTo(testRuleset);
    }
    
    [Fact]
    public void GetRuleset_ShouldThrowException_WhenNotPresent()
    {
        var gameScorer = new Scorer();

        Action act = () =>
        {
            gameScorer.GetRuleset("badrequest");
        };

        act.Should().Throw<RulesetNotFoundException>();
    }

    [Theory]
    [InlineData("(1, 1, 1, 1, 1) ones", 5)]
    [InlineData("(2, 2, 2, 2, 2) ones", 0)]
    [InlineData("(2, 2, 2, 2, 1) twos", 8)]
    [InlineData("(1, 1, 1, 1, 1) twos", 0)]
    [InlineData("(3, 3, 3, 1, 1) threes", 9)]
    [InlineData("(1, 1, 1, 1, 1) threes", 0)]
    [InlineData("(4, 4, 1, 1, 1) fours", 8)]
    [InlineData("(1, 1, 1, 1, 1) fours", 0)]
    [InlineData("(5, 1, 1, 1, 1) fives", 5)]
    [InlineData("(1, 1, 1, 1, 1) fives", 0)]
    [InlineData("(6, 1, 1, 1, 1) sixes", 6)]
    [InlineData("(1, 1, 1, 1, 1) sixes", 0)]
    [InlineData("(1, 1, 1, 2, 3) threeofakind", 3)]
    [InlineData("(2, 2, 2, 3, 4) threeofakind", 6)]
    [InlineData("(1, 1, 1, 1, 4) fourofakind", 4)]
    [InlineData("(2, 2, 2, 2, 3) fourofakind", 8)]
    [InlineData("(2, 2, 2, 2, 2) fiveofakind", 10)]
    [InlineData("(6, 6, 6, 6, 6) fiveofakind", 30)]
    [InlineData("(1, 1, 2, 2, 3) twopairs", 6)]
    [InlineData("(5, 5, 6, 6, 3) twopairs", 22)]
    [InlineData("(1, 1, 1, 2, 2) fullhouse", 7)]
    [InlineData("(1, 1, 2, 2, 2) fullhouse", 8)]
    public void PlayRound_ShouldReturnExpectedScore(string inputString, int expectedOutput)
    {
        var gameScorer = new Scorer();
        
        gameScorer.PlayRound(inputString);

        gameScorer.TotalScore.Should().Be(expectedOutput);
    }
    
}

public class TestParser : IRoundParser
{
    public Round Parse(string inputstring)
    {
        throw new NotImplementedException();
    }
}