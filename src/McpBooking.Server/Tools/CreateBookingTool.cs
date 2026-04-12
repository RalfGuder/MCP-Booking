// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.ComponentModel;
using System.Net;
using System.Text.Json;
using McpBooking.Application.UseCases;
using McpBooking.Server.Properties;
using ModelContextProtocol.Server;

namespace McpBooking.Server.Tools;

/// <summary>
/// MCP tool that creates a new booking.
/// </summary>
[McpServerToolType]
public class CreateBookingTool
{
    private readonly CreateBookingUseCase _useCase;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBookingTool"/> class.
    /// </summary>
    /// <param name="useCase">The use case that creates a booking.</param>
    public CreateBookingTool(CreateBookingUseCase useCase)
    {
        _useCase = useCase;
    }

    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    /// <summary>
    /// Creates a new booking in the WP Booking Calendar API.
    /// </summary>
    [McpServerTool(Name = "create_booking"), Description("Erstellt eine neue Buchung.")]
    public async Task<string> ExecuteAsync(
        [Description("Ressourcen-/Buchungstyp-ID")] int bookingType,
        [Description("Formulardaten als JSON-String")] string formDataJson,
        [Description("Buchungsdaten als JSON-Array-String")] string datesJson,
        CancellationToken ct = default)
    {
        if (bookingType < 1) return Messages.ErrorInvalidBookingType;
        if (!IsValidJson(formDataJson))
            return string.Format(Messages.ErrorInvalidJson, "formDataJson");
        if (!IsValidJson(datesJson))
            return string.Format(Messages.ErrorInvalidJson, "datesJson");

        try
        {
            var booking = await _useCase.ExecuteAsync(bookingType, formDataJson, datesJson, ct);
            return JsonSerializer.Serialize(booking, s_jsonOptions);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            return Messages.ErrorAuthenticationFailed;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Forbidden)
        {
            return Messages.ErrorForbidden;
        }
        catch (HttpRequestException ex) when (ex.StatusCode >= HttpStatusCode.InternalServerError)
        {
            return Messages.ErrorServerError;
        }
        catch (HttpRequestException)
        {
            return Messages.ErrorApiUnreachable;
        }
    }

    private static bool IsValidJson(string json)
    {
        try
        {
            JsonDocument.Parse(json);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}
