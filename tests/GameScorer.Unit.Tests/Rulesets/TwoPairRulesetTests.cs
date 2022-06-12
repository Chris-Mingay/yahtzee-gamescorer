using FluentAssertions;
using GameScorer.Rulesets;
using Xunit;

namespace GameScorer.Unit.Tests.Rulesets;

public class TwoPairRulesetTests
{
    [Theory]
    [InlineData(1, 1, 1, 1, 1, 0)]
    [InlineData(1, 1, 2, 2, 5, 6)]
    [InlineData(6, 6, 5, 5, 4, 22)]
    [InlineData(6, 6, 5, 5, 5, 0)]
    [InlineData(1, 2, 3, 4, 5, 0)]
    public void CalculateScore_ShouldReturn_ExpectedScore(int d1, int d2, int d3, int d4, int d5, int expectedOutput)
    {
        var _sut = new TwoPairRuleset();

        var actualOutput = _sut.CalculateScore(new[] {d1, d2, d3, d4, d5});

        actualOutput.Should().Be(expectedOutput);
    }
}