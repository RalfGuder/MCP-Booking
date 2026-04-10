using McpBooking.Infrastructure.Properties;

namespace McpBooking.Infrastructure.Configuration;

public class BookingApiOptions
{
    public string BaseUrl { get; set; } = Strings.DefaultApiBaseUrl;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
