// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Infrastructure.Properties;

namespace McpBooking.Infrastructure.Configuration;

/// <summary>
/// Configuration options for the WP Booking Calendar REST API.
/// </summary>
public class BookingApiOptions
{
    /// <summary>
    /// Gets or sets the base URL of the booking API.
    /// </summary>
    public string BaseUrl { get; set; } = Strings.DefaultApiBaseUrl;

    /// <summary>
    /// Gets or sets the username used for basic authentication.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password used for basic authentication.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
