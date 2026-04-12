// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.ComponentModel;
using System.Net;
using McpBooking.Application.UseCases;
using McpBooking.Server.Properties;
using ModelContextProtocol.Server;

namespace McpBooking.Server.Tools;

/// <summary>
/// MCP tool that deletes a booking by its ID.
/// </summary>
[McpServerToolType]
public class DeleteBookingTool
{
    private readonly DeleteBookingUseCase _useCase;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteBookingTool"/> class.
    /// </summary>
    /// <param name="useCase">The use case that deletes a booking.</param>
    public DeleteBookingTool(DeleteBookingUseCase useCase)
    {
        _useCase = useCase;
    }

    /// <summary>
    /// Deletes a booking from the WP Booking Calendar API.
    /// </summary>
    [McpServerTool(Name = "delete_booking"), Description("Löscht eine Buchung.")]
    public async Task<string> ExecuteAsync(
        [Description("Buchungs-ID")] int id,
        CancellationToken ct = default)
    {
        if (id < 1) return Messages.ErrorInvalidId;

        try
        {
            await _useCase.ExecuteAsync(id, ct);
            return string.Format(Messages.SuccessBookingDeleted, id);
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
