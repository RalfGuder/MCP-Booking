// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.ComponentModel;
using System.Net;
using System.Text.Json;
using McpBooking.Application.UseCases;
using McpBooking.Server.Properties;
using ModelContextProtocol.Server;

namespace McpBooking.Server.Tools;

/// <summary>
/// MCP tool that lists bookings with optional filters.
/// </summary>
[McpServerToolType]
public class ListBookingsTool
{
    private readonly ListBookingsUseCase _useCase;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListBookingsTool"/> class.
    /// </summary>
    /// <param name="useCase">The use case that retrieves the booking list.</param>
    public ListBookingsTool(ListBookingsUseCase useCase)
    {
        _useCase = useCase;
    }

    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    /// <summary>
    /// Lists bookings from the WP Booking Calendar API with optional filters.
    /// </summary>
    [McpServerTool(Name = "list_bookings"), Description("Listet Buchungen mit optionalen Filtern auf.")]
    public async Task<string> ExecuteAsync(
        [Description("Seite (Standard: 1)")] int page = 1,
        [Description("Einträge pro Seite (Standard: 20, Max: 100)")] int perPage = 20,
        [Description("Filter nach Ressourcen-ID")] int? resourceId = null,
        [Description("Filter nach Status (pending/approved/trash)")] string? status = null,
        [Description("Filter ab Datum (ISO 8601)")] string? dateFrom = null,
        [Description("Filter bis Datum (ISO 8601)")] string? dateTo = null,
        [Description("Filter nach neu/ungelesen")] bool? isNew = null,
        [Description("Stichwortsuche")] string? search = null,
        [Description("Sortierung (booking_id/sort_date/modification_date)")] string? orderBy = null,
        [Description("Richtung (ASC/DESC)")] string? order = null,
        CancellationToken ct = default)
    {
        if (page < 1) return Messages.ErrorPageInvalid;
        if (perPage < 1 || perPage > 100) return Messages.ErrorPerPageInvalid;

        try
        {
            var bookings = await _useCase.ExecuteAsync(page, perPage,
                resourceId, status, dateFrom, dateTo,
                isNew, search, orderBy, order, ct);
            return JsonSerializer.Serialize(bookings, s_jsonOptions);
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
}
