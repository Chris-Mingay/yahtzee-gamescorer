using Bogus;
using Domain.Entities;
using GameScorer;
using Infrastructure.Persistence;
using Round = Domain.Entities.Round;

namespace Infrastructure.Data;

public static class DataSeeder
{

    public static void SeedDatabase(ApplicationDbContext context)
    {
        
        var rnd = new Random();
        
        if (!context.Users.Any())
        {

            var userFaker = new Faker<User>()
                .RuleFor(x => x.Name, f => f.Person.FullName)
                .RuleFor(x => x.CreatedAt, f => f.Date.Between(DateTime.Now.AddMonths(-3), DateTime.Now));

            var fakeUsers = userFaker.Generate(20);
            
            context.Users.AddRange(fakeUsers);

            context.SaveChanges();

        }

        var users = context.Users.ToList();

        if (!context.Rounds.Any())
        {
            var roundFaker = new Faker<Round>()
                .RuleFor(x => x.UserId, f => users[f.Random.Number(users.Count() - 1)].Id)
                .RuleFor(x => x.RecordedAt, f => f.Date.Between(DateTime.Now.AddMonths(-3), DateTime.Now))
                .RuleFor(x => x.Ruleset, f => GenerateRoundType(rnd))
                .RuleFor(x => x.InputString, (f, u) => $"{GenerateDieString(rnd)} {u.Ruleset}");

            var fakeRounds = roundFaker.Generate(300);
            var scorer = new Scorer();
            foreach (var round in fakeRounds)
            {
                round.Score = scorer.PlayRound(round.InputString).Score;
            }
            context.Rounds.AddRange(fakeRounds);
            context.SaveChanges();
        }
    }
    
    

    private static string GenerateDieString(Random rnd)
    {
        var die = new int[5];
        for (int i = 0; i < 5; i++)
        {
            die[i] = rnd.Next(1, 7);
        }
        return $"({String.Join(", ", die)})";
    }

    private static string GenerateRoundType(Random rnd)
    {
        var options = new string[]
        {
            "ones", "twos", "threes", "fours", "fives", "sixes", "threeofakind", "fourofakind", "fiveofakind",
            "twopairs", "fullhouse"
        };
        return options[rnd.Next(options.Length)];
    }
    
}