using System;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Rounds.Commands.RecordRound;
using Application.Users.Commands.CreateUser;
using Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Application.Integration.Tests.Rounds.Commands;

using static Testing;

[Collection("Sequential")]
public class RecordRoundCommandTests
{
    [Fact]
    public async Task Should_ThrowException_WhenDetailsAreNotValid()
    {
        
        await ClearAsync();

        var command = new RecordRoundCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();

        var user = new User()
        {
            Name = "Test User",
            CreatedAt = DateTime.Now
        };

        await AddAsync(user);

        command.UserId = user.Id;
        command.InputString = null;
        
        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
        
        command.UserId = Guid.Empty;
        command.InputString = "(6, 6, 6, 6, 6) sixes";
        
        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
        
    }

    [Fact]
    public async Task Should_CreateRound_WhenDetailsAreValid()
    {

        await ClearAsync();
        
        var user = new User()
        {
            Name = "Test User",
            CreatedAt = DateTime.Now
        };

        await AddAsync(user);
        
        var command = new RecordRoundCommand()
        {
            UserId = user.Id,
            InputString = "(1, 1, 1, 1, 1) ones"
        };

        var response = await SendAsync(command);

        response.InputString.Should().Be(command.InputString);
        response.Score.Should().Be(5);
    }
}