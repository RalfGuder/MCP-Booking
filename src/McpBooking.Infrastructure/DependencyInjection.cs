using System.Net.Http.Headers;
using System.Text;
using McpBooking.Domain.Interfaces;
using McpBooking.Infrastructure.Configuration;
using McpBooking.Infrastructure.Http;
using McpBooking.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace McpBooking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, BookingApiOptions options)
    {
        services.AddSingleton(options);
        services.AddHttpClient<BookingApiClient>(client =>
        {
            client.BaseAddress = new Uri(options.BaseUrl);
            var credentials = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{options.Username}:{options.Password}"));
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", credentials);
        });
        services.AddScoped<IResourceRepository, ResourceRepository>();
        return services;
    }
}
