using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            string DatabaseProvider = configuration.GetValue<string>("DatabaseProvider");
            switch(DatabaseProvider)
            {
                case "InMemory":
                    services.AddDbContext<ApplicationDbContext, InMemoryApplicationDbContext>();
                    break;
                default:
                    throw new Exception("Invalid DatabaseProvider specified in appsettings");
            }

            services.AddTransient<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            return services;
        }
    
    }