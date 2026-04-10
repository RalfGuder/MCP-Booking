using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;
using McpBooking.Infrastructure.Http;

namespace McpBooking.Infrastructure.Repositories;

public class ResourceRepository : IResourceRepository
{
    private readonly BookingApiClient _client;

    public ResourceRepository(BookingApiClient client)
    {
        _client = client;
    }

    public async Task<IReadOnlyList<Resource>> ListAsync(
        int page = 1, int perPage = 20, CancellationToken ct = default)
    {
        var resources = await _client.GetAsync<List<Resource>>(
            $"/resources?page={page}&per_page={perPage}", ct);
        return resources ?? [];
    }
}
