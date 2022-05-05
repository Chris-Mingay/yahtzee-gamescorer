using FluentAssertions;
using GameScorer.Rulesets;
using Xunit;

namespace Runner.Unit.Tests.Rulesets;

public class OfAKindRulsetTests
{

    [Theory]
    [InlineData(1, 1, 1, 1, 1, 3, 3)]
    [InlineData(1, 1, 1, 1, 1, 4, 4)]
    [InlineData(1, 1, 1, 1, 1, 5, 5)]
    [InlineData(6, 6, 6, 6, 6, 5, 30)]
    public void CalculateScore_ShouldReturn_ExpectedScore(int d1, int d2, int d3, int d4, int d5, int ofAKind, int expectedOutput)
    {
        var _sut = new OfAKindRuleset(ofAKind);

        var actualOutput = _sut.CalculateScore(new[] {d1, d2, d3, d4, d5});

        actualOutput.Should().Be(expectedOutput);
    }
}