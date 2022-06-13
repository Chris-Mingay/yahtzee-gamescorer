using System;
using System.Threading.Tasks;
using Application.Users.Queries.GetAllUsers;
using Bogus;
using Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Application.Integration.Tests.Users.Queries;

using static Testing;

[Collection("Sequential")]
public class GetAllUsersQueryTests
{

    [Fact]
    public async Task Should_ReturnExpectedRecordCount()
    {
        await ClearAsync();

        var userFaker = new Faker<User>()
            .RuleFor(x => x.Name, f => f.Person.FullName)
            .RuleFor(x => x.CreatedAt, f => f.Date.Between(DateTime.Now.AddDays(-30), DateTime.Now));

        var fakerUsers = userFaker.Generate(20);
        await AddRangeAsync(fakerUsers);
        
        var expectedCount = await CountAsync<User>();
        var command = new GetAllUsersQuery();

        var users = await FluentActions.Invoking(() => SendAsync(command)).Invoke();
        var actualCount = users.Count;

        actualCount.Should().Be(expectedCount);

    }

}