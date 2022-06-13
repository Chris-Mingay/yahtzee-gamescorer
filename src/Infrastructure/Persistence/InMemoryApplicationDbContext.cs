using Infrastructure.Monitoring;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class InMemoryApplicationDbContext : ApplicationDbContext
{
   
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseInMemoryDatabase("YahtzeeGameScorer");
        options.AddInterceptors(new EFLogger());
    }

}