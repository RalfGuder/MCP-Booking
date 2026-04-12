// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.Text.Json;
using System.Text.Json.Serialization;

namespace McpBooking.Domain.Entities;

/// <summary>
/// Represents a booking in the WP Booking Calendar system.
/// </summary>
public class Booking
{
    /// <summary>
    /// Gets or sets the unique identifier of the booking.
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the resource (booking type) ID.
    /// </summary>
    [JsonPropertyName("booking_type")]
    public int BookingType { get; set; }

    /// <summary>
    /// Gets or sets the list of booked dates.
    /// </summary>
    [JsonPropertyName("dates")]
    public List<string> Dates { get; set; } = [];

    /// <summary>
    /// Gets or sets the submitted form data as a raw JSON element.
    /// </summary>
    [JsonPropertyName("form_data")]
    public JsonElement? FormData { get; set; }

    /// <summary>
    /// Gets or sets the booking status (e.g. pending, approved, trash).
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sort date of the booking.
    /// </summary>
    [JsonPropertyName("sort_date")]
    public string? SortDate { get; set; }

    /// <summary>
    /// Gets or sets the last modification date of the booking.
    /// </summary>
    [JsonPropertyName("modification_date")]
    public string? ModificationDate { get; set; }

    /// <summary>
    /// Gets or sets whether the booking is new/unread.
    /// </summary>
    [JsonPropertyName("is_new")]
    public bool? IsNew { get; set; }

    /// <summary>
    /// Gets or sets the note attached to the booking.
    /// </summary>
    [JsonPropertyName("note")]
    public string? Note { get; set; }
}
