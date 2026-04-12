// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.Text.Json.Serialization;

namespace McpBooking.Domain.Entities;

/// <summary>
/// Represents a bookable resource (booking type) in the WP Booking Calendar system.
/// </summary>
public class Resource
{
    /// <summary>
    /// Gets or sets the unique identifier of the resource.
    /// </summary>
    [JsonPropertyName("booking_type_id")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the display name of the resource.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the cost of the resource, or <see langword="null"/> if not set.
    /// </summary>
    [JsonPropertyName("cost")]
    public string? Cost { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of visitors for the resource, or <see langword="null"/> if not set.
    /// </summary>
    [JsonPropertyName("visitors")]
    public int? Visitors { get; set; }
}
