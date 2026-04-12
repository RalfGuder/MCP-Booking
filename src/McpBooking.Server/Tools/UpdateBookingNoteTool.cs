// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.ComponentModel;
using System.Net;
using System.Text.Json;
using McpBooking.Application.UseCases;
using McpBooking.Server.Properties;
using ModelContextProtocol.Server;

namespace McpBooking.Server.Tools;

/// <summary>
/// MCP tool that updates the note of an existing booking.
/// </summary>
[McpServerToolType]
public class UpdateBookingNoteTool
{
    private readonly UpdateBookingNoteUseCase _useCase;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBookingNoteTool"/> class.
    /// </summary>
    /// <param name="useCase">The use case that updates a booking note.</param>
    public UpdateBookingNoteTool(UpdateBookingNoteUseCase useCase)
    {
        _useCase = useCase;
    }

    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    /// <summary>
    /// Updates the note of a booking in the WP Booking Calendar API.
    /// </summary>
    [McpServerTool(Name = "update_booking_note"), Description("Aktualisiert die Notiz einer Buchung.")]
    public async Task<string> ExecuteAsync(
        [Description("Buchungs-ID")] int id,
        [Description("Notiztext")] string note,
        CancellationToken ct = default)
    {
        if (id < 1) return Messages.ErrorInvalidId;
        if (string.IsNullOrWhiteSpace(note)) return Messages.ErrorNoteEmpty;

        try
        {
            var booking = await _useCase.ExecuteAsync(id, note, ct);
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
}
