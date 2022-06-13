using System.Reflection;
using Application;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllersWithViews(options =>
        options.Filters.Add<ApiExceptionFilterAttribute>())
    .AddFluentValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds( type => type.ToString() );
    
    var filePath = Path.Combine(AppContext.BaseDirectory, $"{typeof(Program).GetTypeInfo().Assembly.GetName().Name}.xml");
    options.IncludeXmlComments(filePath);
});

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!dbContext.Database.IsInMemory())
    {
        dbContext.Database.Migrate();
    }

    DataSeeder.SeedDatabase(dbContext);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();

Console.WriteLine("Active Notifications:");
List<Type> notifications = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(x => x.GetTypes())
    .Where(x => typeof(INotification).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
    .ToList();
if (!notifications.Any()) Console.WriteLine("None");
else
{
    foreach (var notification in notifications)
    {
        Console.WriteLine(notification.Name);
    }
}
Console.WriteLine("");

Console.WriteLine("Active Notification Handlers:");
List<string> notificationHandlers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
    .Where(x => x.GetInterface(typeof(INotificationHandler<>).Name) is not null && !x.IsInterface && !x.IsAbstract)
    .Select(x => x.Name).ToList();
if (!notificationHandlers.Any()) Console.WriteLine("None");
else
{
    foreach (var type in notificationHandlers) Console.WriteLine(type);
}
Console.WriteLine("");

app.Run();

public partial class Program { }