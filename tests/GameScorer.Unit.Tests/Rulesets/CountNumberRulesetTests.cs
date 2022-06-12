using System;
using FluentAssertions;
using GameScorer.Rulesets;
using Xunit;

namespace GameScorer.Unit.Tests.Rulesets;

public class CountNumberRulesetTests
{

    [Theory]
    [InlineData(1, 2, 3, 4, 5, 1, 1)]
    [InlineData(1, 1, 2, 3, 4, 1, 2)]
    [InlineData(2, 1, 1, 1, 1, 2, 2)]
    [InlineData(2, 2, 2, 1, 1, 2, 6)]
    [InlineData(3, 1, 1, 1, 1, 3, 3)]
    [InlineData(3, 3, 3, 3, 1, 3, 12)]
    public void CalculateScore_ShouldReturn_ExpectedScore(int d1, int d2, int d3, int d4, int d5, int targetDice, int expectedOutput)
    {
        var _sut = new CountNumberRuleset(targetDice);

        var actualOutput = _sut.CalculateScore(new[] {d1, d2, d3, d4, d5});

        actualOutput.Should().Be(expectedOutput);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(7)]
    public void Ctor_ShouldThrowException_WhenInvalidCountSet(int countNumber)
    {
        Action act = () =>
        {
            var _sut = new CountNumberRuleset(countNumber);
        };

        act.Should().Throw<ArgumentException>();
    }
}