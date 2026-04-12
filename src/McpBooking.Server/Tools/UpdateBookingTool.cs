// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.ComponentModel;
using System.Net;
using System.Text.Json;
using McpBooking.Application.UseCases;
using McpBooking.Server.Properties;
using ModelContextProtocol.Server;

namespace McpBooking.Server.Tools;

/// <summary>
/// MCP tool that updates an existing booking.
/// </summary>
[McpServerToolType]
public class UpdateBookingTool
{
    private readonly UpdateBookingUseCase _useCase;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBookingTool"/> class.
    /// </summary>
    /// <param name="useCase">The use case that updates a booking.</param>
    public UpdateBookingTool(UpdateBookingUseCase useCase)
    {
        _useCase = useCase;
    }

    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    /// <summary>
    /// Updates an existing booking in the WP Booking Calendar API.
    /// </summary>
    [McpServerTool(Name = "update_booking"), Description("Aktualisiert eine bestehende Buchung.")]
    public async Task<string> ExecuteAsync(
        [Description("Buchungs-ID")] int id,
        [Description("Formulardaten als JSON-String (optional)")] string? formDataJson = null,
        [Description("Ressourcen-/Buchungstyp-ID (optional)")] int? bookingType = null,
        [Description("Buchungsstatus (optional)")] string? status = null,
        CancellationToken ct = default)
    {
        if (id < 1) return Messages.ErrorInvalidId;
        if (bookingType.HasValue && bookingType.Value < 1) return Messages.ErrorInvalidBookingType;
        if (formDataJson is not null && !IsValidJson(formDataJson))
            return string.Format(Messages.ErrorInvalidJson, "formDataJson");

        try
        {
            var booking = await _useCase.ExecuteAsync(id, formDataJson, bookingType, status, ct);
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
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return string.Format(Messages.ErrorBookingNotFound, id);
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
