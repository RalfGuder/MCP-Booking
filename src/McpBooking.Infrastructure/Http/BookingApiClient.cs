using System.Net.Http.Json;

namespace McpBooking.Infrastructure.Http;

public class BookingApiClient
{
    private readonly HttpClient _httpClient;

    public BookingApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T?> GetAsync<T>(string path, CancellationToken ct = default)
    {
        var response = await _httpClient.GetAsync(path, ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>(ct);
    }
}
