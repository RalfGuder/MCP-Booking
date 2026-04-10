namespace McpBooking.Infrastructure.Properties;

internal static class Strings
{
    // "https://kv-milowerland.de/wp-json/wpbc/v1"
    internal static string DefaultApiBaseUrl => Decode("aHR0cHM6Ly9rdi1taWxvd2VybGFuZC5kZS93cC1qc29uL3dwYmMvdjE=");

    // "/resources"
    internal static string ApiResourcesPath => Decode("L3Jlc291cmNlcw==");

    // "?page={0}&per_page={1}"
    internal static string QueryPageFormat => Decode("P3BhZ2U9ezB9JnBlcl9wYWdlPXsxfQ==");

    private static string Decode(string base64) =>
        System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(base64));
}
