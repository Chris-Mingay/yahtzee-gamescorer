using System;
using System.Collections.Generic;
using FluentAssertions;
using GameScorer;
using GameScorer.Exceptions;
using GameScorer.Parsers;
using Xunit;

namespace Runner.Unit.Tests.Parsers;

public class RegexParserTests
{
    
    private RegexParser _sut { get; set; }

    public RegexParserTests()
    {
        _sut = new RegexParser();
    }
    
    
    [Theory]
    [MemberData(nameof(RegexParserTestsData.GoodData), MemberType = typeof(RegexParserTestsData))]
    public void Parse_ShouldReturnExpectedRound_WhenInputDataIsValid(string inputString, Round expectedOutput)
    {
        var actualOutput = _sut.Parse(inputString);

        actualOutput.Should().BeEquivalentTo(expectedOutput);
    }

    [Theory]
    [InlineData("")]
    [InlineData("(1, 1, 1, 1, 1)")]
    [InlineData("(1, 1, 1, 1, 1) ")]
    [InlineData("(1, 1, 1, 1) wrongcount1")]
    [InlineData("(1, 1, 1, 1, 1, 1) wrongcount2")]
    [InlineData("noarray")]
    [InlineData("(1, dog, 1, 1, 1) badelement")]
    public void Parse_ShouldThrowException_WhenInputDataIsInvalid(string inputString)
    {
        Action act = () =>
        {
            _sut.Parse(inputString);
        };

        act.Should().Throw<InvalidRoundDataException>();
    }
}

public class RegexParserTestsData
{
    public static IEnumerable<object[]> GoodData => _goodData();
    private static IEnumerable<object[]> _goodData()
    {
        yield return new object[]
        {
            "(1, 1, 1, 1, 1) ones",
            new Round()
            {
                Die = new []{ 1, 1, 1, 1, 1},
                Ruleset = "ones"
            }
        };
        
        yield return new object[]
        {
            "(1, 2, 3, 4, 5) twos",
            new Round()
            {
                Die = new []{ 1, 2, 3, 4, 5},
                Ruleset = "twos"
            }
        };
        
    }
        
}