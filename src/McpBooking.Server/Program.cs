using McpBooking.Application.UseCases;
using McpBooking.Infrastructure;
using McpBooking.Infrastructure.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

var options = new BookingApiOptions
{
    BaseUrl = Environment.GetEnvironmentVariable("WPBC_API_URL")
              ?? "https://kv-milowerland.de/wp-json/wpbc/v1",
    Username = Environment.GetEnvironmentVariable("WPBC_USERNAME")
              ?? throw new InvalidOperationException("Umgebungsvariable WPBC_USERNAME ist nicht gesetzt."),
    Password = Environment.GetEnvironmentVariable("WPBC_PASSWORD")
              ?? throw new InvalidOperationException("Umgebungsvariable WPBC_PASSWORD ist nicht gesetzt.")
};

builder.Services.AddInfrastructure(options);
builder.Services.AddScoped<ListResourcesUseCase>();

builder.Services.AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly(typeof(Program).Assembly);

var app = builder.Build();
await app.RunAsync();
