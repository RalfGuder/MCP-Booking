// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.Text.Json.Serialization;

namespace McpBooking.Domain.Entities;

/// <summary>
/// Represents a single date entry within a booking.
/// </summary>
public class BookingDate
{
    /// <summary>
    /// Gets or sets the booking date and time.
    /// </summary>
    [JsonPropertyName("booking_date")]
    public string BookingDateValue { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the approval status (1 = approved, 0 = pending).
    /// </summary>
    [JsonPropertyName("approved")]
    public int Approved { get; set; }
}
