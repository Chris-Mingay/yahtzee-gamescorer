using Application.Emails;
using Application.Interfaces;
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
                case "Sqlite":
                    services.AddDbContext<ApplicationDbContext, SqliteApplicationDbContext>();
                    break;
                default:
                    throw new Exception("Invalid DatabaseProvider specified in appsettings");
            }
            
            switch (configuration.GetValue<string>("EmailProvider"))
            {
                case "Smtp":
                    services.AddScoped<IEmailerService, SmtpEmailService>();
                    services.Configure<SmtpConfiguration>(configuration.GetSection("SmtpConfiguration"));
                    break;
                case "SendGrid":
                    services.AddScoped<IEmailerService, SendGridEmailService>();
                    services.Configure<SendGridConfiguration>(configuration.GetSection("SendGridConfiguration"));
                    break;
                case "Mock":
                    services.AddScoped<IEmailerService, MockEmailerService>();
                    break;
                default:
                    throw new Exception("appsettings.json EmailProvider must be set. Use 'Mock' to send emails to console log");

            }

            services.AddTransient<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            return services;
        }
    
    }