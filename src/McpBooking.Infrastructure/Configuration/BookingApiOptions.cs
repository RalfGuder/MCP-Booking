namespace McpBooking.Infrastructure.Configuration;

public class BookingApiOptions
{
    public string BaseUrl { get; set; } = "https://kv-milowerland.de/wp-json/wpbc/v1";
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
