namespace McpBooking.Server.Properties;

internal static class Strings
{
    // "https://kv-milowerland.de/wp-json/wpbc/v1"
    internal static string DefaultApiBaseUrl => Decode("aHR0cHM6Ly9rdi1taWxvd2VybGFuZC5kZS93cC1qc29uL3dwYmMvdjE=");

    // "WPBC_API_URL"
    internal static string EnvVarApiUrl => Decode("V1BCQ19BUElfVVJM");

    // "WPBC_USERNAME"
    internal static string EnvVarUsername => Decode("V1BCQ19VU0VSTkFNRQ==");

    // "WPBC_PASSWORD"
    internal static string EnvVarPassword => Decode("V1BCQ19QQVNTV09SRA==");

    // "Umgebungsvariable WPBC_USERNAME ist nicht gesetzt."
    internal static string ErrorMissingUsername => Decode("VW1nZWJ1bmdzdmFyaWFibGUgV1BCQ19VU0VSTkFNRSBpc3QgbmljaHQgZ2VzZXR6dC4=");

    // "Umgebungsvariable WPBC_PASSWORD ist nicht gesetzt."
    internal static string ErrorMissingPassword => Decode("VW1nZWJ1bmdzdmFyaWFibGUgV1BCQ19QQVNTV09SRCBpc3QgbmljaHQgZ2VzZXR6dC4=");

    private static string Decode(string base64) =>
        System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64));
}
