using System.Reflection;
using Application;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Persistence;
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

app.Run();

public partial class Program { }