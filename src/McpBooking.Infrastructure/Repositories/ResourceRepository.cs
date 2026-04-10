// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;
using McpBooking.Infrastructure.Http;
using McpBooking.Infrastructure.Properties;

namespace McpBooking.Infrastructure.Repositories;

/// <summary>
/// Retrieves resources from the WP Booking Calendar REST API.
/// </summary>
public class ResourceRepository : IResourceRepository
{
    private readonly BookingApiClient _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceRepository"/> class.
    /// </summary>
    /// <param name="client">The API client used to send HTTP requests.</param>
    public ResourceRepository(BookingApiClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Retrieves a paginated list of resources from the booking API.
    /// </summary>
    /// <param name="page">The page number (1-based).</param>
    /// <param name="perPage">The number of items per page (1-100).</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A read-only list of resources.</returns>
    public async Task<IReadOnlyList<Resource>> ListAsync(
        int page = 1, int perPage = 20, CancellationToken ct = default)
    {
        var resources = await _client.GetAsync<List<Resource>>(
            $"{Strings.ApiResourcesPath}{string.Format(Strings.QueryPageFormat, page, perPage)}", ct);
        return resources ?? [];
    }
}
