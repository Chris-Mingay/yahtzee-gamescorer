using System;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Users.Commands.CreateUser;
using Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Application.Integration.Tests.Users.Commands;

using static Testing;

[Collection("Sequential")]
public class CreateUserCommandTests
{

    [Fact]
    public async Task Should_ThrowException_WhenFullnameNotProvided()
    {

        var command = new CreateUserCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();

    }

    [Fact]
    public async Task Should_CreateUser_WhenDetailsAreValid()
    {
        var command = new CreateUserCommand()
        {
            Fullname = "Testy McTestFace"
        };

        var response = await SendAsync(command);
        var actual = await FindAsync<User>(response.Id);

        Console.WriteLine(response.Id);
        actual.Name.Should().Be(command.Fullname);

    }
}