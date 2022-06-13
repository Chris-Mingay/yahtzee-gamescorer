using Infrastructure.Monitoring;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence
{
    public class SqliteApplicationDbContext : ApplicationDbContext
    {
        private readonly IConfiguration _configuration;

        public SqliteApplicationDbContext(
            IConfiguration configuration )
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(_configuration.GetConnectionString("DefaultSqliteConnection"));
            options.AddInterceptors(new EFLogger());
        }

    }

}
