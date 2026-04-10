using System.Text.Json.Serialization;

namespace McpBooking.Domain.Entities;

public class Resource
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("cost")]
    public string? Cost { get; set; }

    [JsonPropertyName("visitors")]
    public int? Visitors { get; set; }
}
