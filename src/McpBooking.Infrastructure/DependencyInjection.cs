// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.Net.Http.Headers;
using System.Text;
using McpBooking.Domain.Interfaces;
using McpBooking.Infrastructure.Configuration;
using McpBooking.Infrastructure.Http;
using McpBooking.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace McpBooking.Infrastructure;

/// <summary>
/// Provides extension methods for registering infrastructure services with the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers all infrastructure services required by the application.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="options">The booking API configuration options.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> for chaining.</returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, BookingApiOptions options)
    {
        services.AddSingleton(options);
        services.AddHttpClient<BookingApiClient>(client =>
        {
            client.BaseAddress = new Uri(options.BaseUrl.TrimEnd('/') + "/");
            var credentials = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{options.Username}:{options.Password}"));
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", credentials);
        });
        services.AddScoped<IResourceRepository, ResourceRepository>();
        return services;
    }
}
