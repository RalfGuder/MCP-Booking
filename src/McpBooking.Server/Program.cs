// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.UseCases;
using McpBooking.Infrastructure;
using McpBooking.Infrastructure.Configuration;
using McpBooking.Server.Properties;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

// Remove console logging – stdout is reserved exclusively for MCP JSON-RPC
builder.Logging.ClearProviders();

var options = new BookingApiOptions
{
    BaseUrl = Environment.GetEnvironmentVariable(Strings.EnvVarApiUrl)
              ?? Strings.DefaultApiBaseUrl,
    Username = Environment.GetEnvironmentVariable(Strings.EnvVarUsername)
              ?? throw new InvalidOperationException(Strings.ErrorMissingUsername),
    Password = Environment.GetEnvironmentVariable(Strings.EnvVarPassword)
              ?? throw new InvalidOperationException(Strings.ErrorMissingPassword)
};

builder.Services.AddInfrastructure(options);
builder.Services.AddScoped<ListResourcesUseCase>();

builder.Services.AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly(typeof(Program).Assembly);

var app = builder.Build();
await app.RunAsync();
