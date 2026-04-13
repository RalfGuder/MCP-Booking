// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.ComponentModel;
using System.Net;
using System.Text.Json;
using McpBooking.Application.UseCases;
using McpBooking.Server.Properties;
using ModelContextProtocol.Server;

namespace McpBooking.Server.Tools;

/// <summary>
/// MCP tool class providing all booking operations.
/// </summary>
[McpServerToolType]
public class BookingTool
{
    private readonly BookingUseCase _useCase;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookingTool"/> class.
    /// </summary>
    /// <param name="useCase">The use case providing all booking operations.</param>
    public BookingTool(BookingUseCase useCase)
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
    public async Task<string> ListBookingsAsync(
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
            var bookings = await _useCase.ListAsync(page, perPage,
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
        catch (HttpRequestException ex) when (ex.StatusCode.HasValue)
        {
            return ex.Message;
        }
        catch (HttpRequestException)
        {
            return Messages.ErrorApiUnreachable;
        }
        catch (JsonException ex)
        {
            return $"Deserialisierungsfehler: {ex.Message}";
        }
    }

    /// <summary>
    /// Retrieves a single booking from the WP Booking Calendar API.
    /// </summary>
    [McpServerTool(Name = "get_booking"), Description("Ruft eine einzelne Buchung ab.")]
    public async Task<string> GetBookingAsync(
        [Description("Buchungs-ID")] int id,
        CancellationToken ct = default)
    {
        if (id < 1) return Messages.ErrorInvalidId;

        try
        {
            var booking = await _useCase.GetAsync(id, ct);
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
        catch (HttpRequestException ex) when (ex.StatusCode.HasValue)
        {
            return ex.Message;
        }
        catch (HttpRequestException)
        {
            return Messages.ErrorApiUnreachable;
        }
    }

    /// <summary>
    /// Creates a new booking in the WP Booking Calendar API.
    /// </summary>
    [McpServerTool(Name = "create_booking"), Description("Erstellt eine neue Buchung.")]
    public async Task<string> CreateBookingAsync(
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
            var booking = await _useCase.CreateAsync(bookingType, formDataJson, datesJson, ct);
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
        catch (HttpRequestException ex) when (ex.StatusCode.HasValue)
        {
            return ex.Message;
        }
        catch (HttpRequestException)
        {
            return Messages.ErrorApiUnreachable;
        }
    }

    /// <summary>
    /// Updates an existing booking in the WP Booking Calendar API.
    /// </summary>
    [McpServerTool(Name = "update_booking"), Description("Aktualisiert eine bestehende Buchung.")]
    public async Task<string> UpdateBookingAsync(
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
            var booking = await _useCase.UpdateAsync(id, formDataJson, bookingType, status, ct);
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
        catch (HttpRequestException ex) when (ex.StatusCode.HasValue)
        {
            return ex.Message;
        }
        catch (HttpRequestException)
        {
            return Messages.ErrorApiUnreachable;
        }
    }

    /// <summary>
    /// Deletes a booking from the WP Booking Calendar API.
    /// </summary>
    [McpServerTool(Name = "delete_booking"), Description("Löscht eine Buchung.")]
    public async Task<string> DeleteBookingAsync(
        [Description("Buchungs-ID")] int id,
        CancellationToken ct = default)
    {
        if (id < 1) return Messages.ErrorInvalidId;

        try
        {
            await _useCase.DeleteAsync(id, ct);
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
        catch (HttpRequestException ex) when (ex.StatusCode.HasValue)
        {
            return ex.Message;
        }
        catch (HttpRequestException)
        {
            return Messages.ErrorApiUnreachable;
        }
    }

    /// <summary>
    /// Approves a booking in the WP Booking Calendar API.
    /// </summary>
    [McpServerTool(Name = "approve_booking"), Description("Genehmigt eine Buchung.")]
    public async Task<string> ApproveBookingAsync(
        [Description("Buchungs-ID")] int id,
        CancellationToken ct = default)
    {
        if (id < 1) return Messages.ErrorInvalidId;

        try
        {
            var booking = await _useCase.ApproveAsync(id, ct);
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
        catch (HttpRequestException ex) when (ex.StatusCode.HasValue)
        {
            return ex.Message;
        }
        catch (HttpRequestException)
        {
            return Messages.ErrorApiUnreachable;
        }
    }

    /// <summary>
    /// Sets a booking to pending status in the WP Booking Calendar API.
    /// </summary>
    [McpServerTool(Name = "set_booking_pending"), Description("Setzt eine Buchung auf ausstehend.")]
    public async Task<string> SetBookingPendingAsync(
        [Description("Buchungs-ID")] int id,
        CancellationToken ct = default)
    {
        if (id < 1) return Messages.ErrorInvalidId;

        try
        {
            var booking = await _useCase.SetPendingAsync(id, ct);
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
        catch (HttpRequestException ex) when (ex.StatusCode.HasValue)
        {
            return ex.Message;
        }
        catch (HttpRequestException)
        {
            return Messages.ErrorApiUnreachable;
        }
    }

    /// <summary>
    /// Updates the note of a booking in the WP Booking Calendar API.
    /// </summary>
    [McpServerTool(Name = "update_booking_note"), Description("Aktualisiert die Notiz einer Buchung.")]
    public async Task<string> UpdateBookingNoteAsync(
        [Description("Buchungs-ID")] int id,
        [Description("Notiztext")] string note,
        CancellationToken ct = default)
    {
        if (id < 1) return Messages.ErrorInvalidId;
        if (string.IsNullOrWhiteSpace(note)) return Messages.ErrorNoteEmpty;

        try
        {
            var booking = await _useCase.UpdateNoteAsync(id, note, ct);
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
        catch (HttpRequestException ex) when (ex.StatusCode.HasValue)
        {
            return ex.Message;
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
