// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
namespace McpBooking.Server.Properties;

/// <summary>
/// Provides obfuscated string constants used by the server layer.
/// </summary>
internal static class Strings
{
    /// <summary>
    /// Gets the default base URL of the WP Booking Calendar REST API.
    /// </summary>
    // "https://kv-milowerland.de/wp-json/wpbc/v1"
    internal static string DefaultApiBaseUrl => Decode("aHR0cHM6Ly9rdi1taWxvd2VybGFuZC5kZS93cC1qc29uL3dwYmMvdjE=");

    /// <summary>
    /// Gets the name of the environment variable that overrides the API base URL.
    /// </summary>
    // "WPBC_API_URL"
    internal static string EnvVarApiUrl => Decode("V1BCQ19BUElfVVJM");

    /// <summary>
    /// Gets the name of the environment variable that supplies the API username.
    /// </summary>
    // "WPBC_USERNAME"
    internal static string EnvVarUsername => Decode("V1BCQ19VU0VSTkFNRQ==");

    /// <summary>
    /// Gets the name of the environment variable that supplies the API password.
    /// </summary>
    // "WPBC_PASSWORD"
    internal static string EnvVarPassword => Decode("V1BCQ19QQVNTV09SRA==");

    /// <summary>
    /// Gets the error message thrown when the WPBC_USERNAME environment variable is not set.
    /// </summary>
    // "Umgebungsvariable WPBC_USERNAME ist nicht gesetzt."
    internal static string ErrorMissingUsername => Decode("VW1nZWJ1bmdzdmFyaWFibGUgV1BCQ19VU0VSTkFNRSBpc3QgbmljaHQgZ2VzZXR6dC4=");

    /// <summary>
    /// Gets the error message thrown when the WPBC_PASSWORD environment variable is not set.
    /// </summary>
    // "Umgebungsvariable WPBC_PASSWORD ist nicht gesetzt."
    internal static string ErrorMissingPassword => Decode("VW1nZWJ1bmdzdmFyaWFibGUgV1BCQ19QQVNTV09SRCBpc3QgbmljaHQgZ2VzZXR6dC4=");

    private static string Decode(string base64) =>
        System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64));
}
