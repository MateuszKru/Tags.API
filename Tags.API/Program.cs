using Tags.API.DependencyInjection;
using Tags.API.Middlewares;
using Tags.Application.DependencyInjection;
using Tags.Domain.DependencyInjection;
using Tags.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddPresentation()
    .AddApplication()
    .AddDomain()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

await MigrationExtension.ApplyMigrationsAsync(app);
await DataInitializeExtension.InitializeTagsAsync(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();

public partial class Program
{ }