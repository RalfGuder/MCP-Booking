// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.ComponentModel;
using System.Net;
using System.Text.Json;
using McpBooking.Application.UseCases;
using McpBooking.Server.Properties;
using ModelContextProtocol.Server;

namespace McpBooking.Server.Tools;

/// <summary>
/// MCP tool that retrieves a single booking by its ID.
/// </summary>
[McpServerToolType]
public class GetBookingTool
{
    private readonly GetBookingUseCase _useCase;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBookingTool"/> class.
    /// </summary>
    /// <param name="useCase">The use case that retrieves a booking.</param>
    public GetBookingTool(GetBookingUseCase useCase)
    {
        _useCase = useCase;
    }

    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    /// <summary>
    /// Retrieves a single booking from the WP Booking Calendar API.
    /// </summary>
    [McpServerTool(Name = "get_booking"), Description("Ruft eine einzelne Buchung ab.")]
    public async Task<string> ExecuteAsync(
        [Description("Buchungs-ID")] int id,
        CancellationToken ct = default)
    {
        if (id < 1) return Messages.ErrorInvalidId;

        try
        {
            var booking = await _useCase.ExecuteAsync(id, ct);
            if (booking is null)
                return string.Format(Messages.ErrorBookingNotFound, id);
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
