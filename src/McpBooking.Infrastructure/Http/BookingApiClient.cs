using System.Net.Http.Json;

namespace McpBooking.Infrastructure.Http;

/// <summary>
/// HTTP client wrapper for communicating with the WP Booking Calendar REST API.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="BookingApiClient"/> class.
/// </remarks>
/// <param name="httpClient">The underlying HTTP client configured for the booking API.</param>
public class BookingApiClient(HttpClient httpClient)
{

    private readonly HttpClient _httpClient = httpClient;


    /// <summary>
    /// Sends a GET request to the specified path and deserializes the response body as JSON.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response into.</typeparam>
    /// <param name="path">The relative path and query string to request.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The deserialized response value, or <see langword="null"/> if the body is empty.</returns>
    public async Task<T?> GetAsync<T>(string path, CancellationToken ct = default)
    {
        var response = await _httpClient.GetAsync(path, ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>(ct);
    }

    /// <summary>
    /// Sends a POST request with a JSON body and deserializes the response.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response into.</typeparam>
    /// <param name="path">The relative path to request.</param>
    /// <param name="body">The object to serialize as JSON in the request body.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The deserialized response value, or <see langword="null"/> if the body is empty.</returns>
    public async Task<T?> PostAsync<T>(string path, object body, CancellationToken ct = default)
    {
        var response = await _httpClient.PostAsJsonAsync(path, body, ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>(ct);
    }

    /// <summary>
    /// Sends a POST request without a body and deserializes the response.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response into.</typeparam>
    /// <param name="path">The relative path to request.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The deserialized response value, or <see langword="null"/> if the body is empty.</returns>
    public async Task<T?> PostAsync<T>(string path, CancellationToken ct = default)
    {
        var response = await _httpClient.PostAsync(path, null, ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>(ct);
    }

    /// <summary>
    /// Sends a PUT request with a JSON body and deserializes the response.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response into.</typeparam>
    /// <param name="path">The relative path to request.</param>
    /// <param name="body">The object to serialize as JSON in the request body.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The deserialized response value, or <see langword="null"/> if the body is empty.</returns>
    public async Task<T?> PutAsync<T>(string path, object body, CancellationToken ct = default)
    {
        var response = await _httpClient.PutAsJsonAsync(path, body, ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>(ct);
    }

    /// <summary>
    /// Sends a DELETE request to the specified path.
    /// </summary>
    /// <param name="path">The relative path to request.</param>
    /// <param name="ct">A cancellation token.</param>
    public async Task DeleteAsync(string path, CancellationToken ct = default)
    {
        var response = await _httpClient.DeleteAsync(path, ct);
        response.EnsureSuccessStatusCode();
    }
}
