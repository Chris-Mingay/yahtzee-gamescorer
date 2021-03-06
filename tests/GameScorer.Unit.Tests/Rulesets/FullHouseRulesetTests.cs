using FluentAssertions;
using GameScorer.Rulesets;
using Xunit;

namespace GameScorer.Unit.Tests.Rulesets;

public class FullHouseRulsetTests
{

    [Theory]
    [InlineData(1, 1, 1, 1, 1, 0)]
    [InlineData(1, 2, 3, 4, 5, 0)]
    [InlineData(1, 1, 1, 1, 2, 0)]
    [InlineData(1, 1, 1, 2, 2, 7)]
    [InlineData(5, 5, 6, 6, 6, 28)]
    public void CalculateScore_ShouldReturn_ExpectedScore(int d1, int d2, int d3, int d4, int d5, int expectedOutput)
    {
        var _sut = new FullHouseRuleset();

        var actualOutput = _sut.CalculateScore(new[] {d1, d2, d3, d4, d5});

        actualOutput.Should().Be(expectedOutput);
    }
}