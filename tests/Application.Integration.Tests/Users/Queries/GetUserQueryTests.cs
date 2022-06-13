using System;
using System.Threading.Tasks;
using Application.Users.Queries.GetAllUsers;
using Application.Users.Queries.GetUser;
using Bogus;
using Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Application.Integration.Tests.Users.Queries;

using static Testing;

[Collection("Sequential")]
public class GetUserQueryTests
{

    [Fact]
    public async Task Should_ReturnExpectedUser()
    {
        await ClearAsync();

        var userFaker = new Faker<User>()
            .RuleFor(x => x.Name, f => f.Person.FullName)
            .RuleFor(x => x.CreatedAt, f => f.Date.Between(DateTime.Now.AddDays(-30), DateTime.Now));
        var fakerUser = userFaker.Generate();
        await AddAsync(fakerUser);

        var command = new GetUserQuery()
        {
            UserId = fakerUser.Id
        };

        var actualUser = await FluentActions.Invoking(() => SendAsync(command)).Invoke();

        actualUser.Id.Should().Be(fakerUser.Id);
        actualUser.Name.Should().Be(fakerUser.Name);
    }
    
    [Fact]
    public async Task Should_ReturnNull_WhenNotFound()
    {
        await ClearAsync();

        var command = new GetUserQuery()
        {
            UserId = Guid.NewGuid()
        };

        var actualUser = await FluentActions.Invoking(() => SendAsync(command)).Invoke();

        actualUser.Should().BeNull();
    }

}