// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Domain.Entities;

namespace McpBooking.Domain.Interfaces;

/// <summary>
/// Defines the contract for booking data access operations.
/// </summary>
public interface IBookingRepository
{
    /// <summary>
    /// Retrieves a filtered, paginated list of bookings.
    /// </summary>
    Task<IReadOnlyList<Booking>> ListAsync(int page, int perPage,
        int? resourceId = null, string? status = null,
        string? dateFrom = null, string? dateTo = null,
        bool? isNew = null, string? search = null,
        string? orderBy = null, string? order = null,
        CancellationToken ct = default);

    /// <summary>
    /// Retrieves a single booking by its identifier.
    /// </summary>
    Task<Booking?> GetAsync(int id, CancellationToken ct = default);

    /// <summary>
    /// Creates a new booking.
    /// </summary>
    Task<Booking> CreateAsync(int bookingType, string formDataJson,
        string datesJson, CancellationToken ct = default);

    /// <summary>
    /// Updates an existing booking.
    /// </summary>
    Task<Booking> UpdateAsync(int id, string? formDataJson = null,
        int? bookingType = null, string? status = null,
        CancellationToken ct = default);

    /// <summary>
    /// Deletes a booking by its identifier.
    /// </summary>
    Task DeleteAsync(int id, CancellationToken ct = default);

    /// <summary>
    /// Approves a booking.
    /// </summary>
    Task<Booking> ApproveAsync(int id, CancellationToken ct = default);

    /// <summary>
    /// Sets a booking to pending status.
    /// </summary>
    Task<Booking> SetPendingAsync(int id, CancellationToken ct = default);

    /// <summary>
    /// Updates the note attached to a booking.
    /// </summary>
    Task<Booking> UpdateNoteAsync(int id, string note,
        CancellationToken ct = default);
}
