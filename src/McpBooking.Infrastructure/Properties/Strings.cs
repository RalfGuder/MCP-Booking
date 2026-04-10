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
    // "/resources"
    internal static string ApiResourcesPath => Decode("L3Jlc291cmNlcw==");

    /// <summary>
    /// Gets the query string format for pagination parameters (page and per_page).
    /// </summary>
    // "?page={0}&per_page={1}"
    internal static string QueryPageFormat => Decode("P3BhZ2U9ezB9JnBlcl9wYWdlPXsxfQ==");

    private static string Decode(string base64) =>
        System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64));
}
