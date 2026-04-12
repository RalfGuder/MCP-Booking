// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;
using McpBooking.Infrastructure.Http;
using McpBooking.Infrastructure.Properties;

namespace McpBooking.Infrastructure.Repositories;

/// <summary>
/// Retrieves and manages bookings via the WP Booking Calendar REST API.
/// </summary>
public class BookingRepository : IBookingRepository
{
    private readonly BookingApiClient _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookingRepository"/> class.
    /// </summary>
    /// <param name="client">The API client used to send HTTP requests.</param>
    public BookingRepository(BookingApiClient client)
    {
        _client = client;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<Booking>> ListAsync(int page, int perPage,
        int? resourceId = null, string? status = null,
        string? dateFrom = null, string? dateTo = null,
        bool? isNew = null, string? search = null,
        string? orderBy = null, string? order = null,
        CancellationToken ct = default)
    {
        var query = $"{Strings.ApiBookingsPath}{string.Format(Strings.QueryPageFormat, page, perPage)}";
        if (resourceId.HasValue)
            query += string.Format(Strings.QueryResourceIdFormat, resourceId.Value);
        if (status != null)
            query += string.Format(Strings.QueryStatusFormat, Uri.EscapeDataString(status));
        if (dateFrom != null)
            query += string.Format(Strings.QueryDateFromFormat, Uri.EscapeDataString(dateFrom));
        if (dateTo != null)
            query += string.Format(Strings.QueryDateToFormat, Uri.EscapeDataString(dateTo));
        if (isNew.HasValue)
            query += string.Format(Strings.QueryIsNewFormat, isNew.Value.ToString().ToLowerInvariant());
        if (search != null)
            query += string.Format(Strings.QuerySearchFormat, Uri.EscapeDataString(search));
        if (orderBy != null)
            query += string.Format(Strings.QueryOrderByFormat, Uri.EscapeDataString(orderBy));
        if (order != null)
            query += string.Format(Strings.QueryOrderFormat, Uri.EscapeDataString(order));

        return await _client.GetAsync<List<Booking>>(query, ct) ?? [];
    }

    /// <inheritdoc />
    public async Task<Booking?> GetAsync(int id, CancellationToken ct = default)
    {
        return await _client.GetAsync<Booking>(
            string.Format(Strings.ApiBookingByIdPath, id), ct);
    }

    /// <inheritdoc />
    public async Task<Booking> CreateAsync(int bookingType, string formDataJson,
        string datesJson, CancellationToken ct = default)
    {
        var body = new
        {
            booking_type = bookingType,
            form_data = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(formDataJson),
            dates = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(datesJson)
        };
        return (await _client.PostAsync<Booking>(Strings.ApiBookingsPath, body, ct))!;
    }

    /// <inheritdoc />
    public async Task<Booking> UpdateAsync(int id, string? formDataJson = null,
        int? bookingType = null, string? status = null, CancellationToken ct = default)
    {
        var dict = new Dictionary<string, object>();
        if (formDataJson != null)
            dict["form_data"] = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(formDataJson);
        if (bookingType.HasValue)
            dict["booking_type"] = bookingType.Value;
        if (status != null)
            dict["status"] = status;

        return (await _client.PutAsync<Booking>(
            string.Format(Strings.ApiBookingByIdPath, id), dict, ct))!;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        await _client.DeleteAsync(
            string.Format(Strings.ApiBookingByIdPath, id), ct);
    }

    /// <inheritdoc />
    public async Task<Booking> ApproveAsync(int id, CancellationToken ct = default)
    {
        return (await _client.PostAsync<Booking>(
            string.Format(Strings.ApiBookingApprovePath, id), ct))!;
    }

    /// <inheritdoc />
    public async Task<Booking> SetPendingAsync(int id, CancellationToken ct = default)
    {
        return (await _client.PostAsync<Booking>(
            string.Format(Strings.ApiBookingPendingPath, id), ct))!;
    }

    /// <inheritdoc />
    public async Task<Booking> UpdateNoteAsync(int id, string note, CancellationToken ct = default)
    {
        return (await _client.PutAsync<Booking>(
            string.Format(Strings.ApiBookingNotePath, id), new { note }, ct))!;
    }
}
