// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
namespace McpBooking.Infrastructure.Properties;

/// <summary>
/// Provides obfuscated string constants used by the infrastructure layer.
/// </summary>
internal static class Strings
{
    /// <summary>
    /// Gets the default base URL of the WP Booking Calendar REST API.
    /// </summary>
    // "https://kv-milowerland.de/wp-json/wpbc/v1"
    internal static string DefaultApiBaseUrl => Decode("aHR0cHM6Ly9rdi1taWxvd2VybGFuZC5kZS93cC1qc29uL3dwYmMvdjE=");

    /// <summary>
    /// Gets the relative path of the resources endpoint.
    /// </summary>
    // "resources"
    internal static string ApiResourcesPath => Decode("cmVzb3VyY2Vz");

    /// <summary>
    /// Gets the query string format for pagination parameters (page and per_page).
    /// </summary>
    // "?page={0}&per_page={1}"
    internal static string QueryPageFormat => Decode("P3BhZ2U9ezB9JnBlcl9wYWdlPXsxfQ==");

    /// <summary>
    /// Gets the relative path of the bookings endpoint.
    /// </summary>
    // "bookings"
    internal static string ApiBookingsPath => Decode("Ym9va2luZ3M=");

    /// <summary>
    /// Gets the format string for a single booking by ID.
    /// </summary>
    // "bookings/{0}"
    internal static string ApiBookingByIdPath => Decode("Ym9va2luZ3MvezB9");

    /// <summary>
    /// Gets the format string for the booking approve endpoint.
    /// </summary>
    // "bookings/{0}/approve"
    internal static string ApiBookingApprovePath => Decode("Ym9va2luZ3MvezB9L2FwcHJvdmU=");

    /// <summary>
    /// Gets the format string for the booking pending endpoint.
    /// </summary>
    // "bookings/{0}/pending"
    internal static string ApiBookingPendingPath => Decode("Ym9va2luZ3MvezB9L3BlbmRpbmc=");

    /// <summary>
    /// Gets the format string for the booking note endpoint.
    /// </summary>
    // "bookings/{0}/note"
    internal static string ApiBookingNotePath => Decode("Ym9va2luZ3MvezB9L25vdGU=");

    /// <summary>
    /// Gets the query string format for booking list filters with ampersand prefix.
    /// </summary>
    // "&amp;resource_id={0}"
    internal static string QueryResourceIdFormat => Decode("JnJlc291cmNlX2lkPXswfQ==");

    /// <summary>
    /// Gets the query string format for status filter.
    /// </summary>
    // "&amp;status={0}"
    internal static string QueryStatusFormat => Decode("JnN0YXR1cz17MH0=");

    /// <summary>
    /// Gets the query string format for date_from filter.
    /// </summary>
    // "&amp;date_from={0}"
    internal static string QueryDateFromFormat => Decode("JmRhdGVfZnJvbT17MH0=");

    /// <summary>
    /// Gets the query string format for date_to filter.
    /// </summary>
    // "&amp;date_to={0}"
    internal static string QueryDateToFormat => Decode("JmRhdGVfdG89ezB9");

    /// <summary>
    /// Gets the query string format for is_new filter.
    /// </summary>
    // "&amp;is_new={0}"
    internal static string QueryIsNewFormat => Decode("JmlzX25ldz17MH0=");

    /// <summary>
    /// Gets the query string format for search filter.
    /// </summary>
    // "&amp;search={0}"
    internal static string QuerySearchFormat => Decode("JnNlYXJjaD17MH0=");

    /// <summary>
    /// Gets the query string format for orderby filter.
    /// </summary>
    // "&amp;orderby={0}"
    internal static string QueryOrderByFormat => Decode("Jm9yZGVyYnk9ezB9");

    /// <summary>
    /// Gets the query string format for order filter.
    /// </summary>
    // "&amp;order={0}"
    internal static string QueryOrderFormat => Decode("Jm9yZGVyPXswfQ==");

    private static string Decode(string base64) =>
        System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64));
}
