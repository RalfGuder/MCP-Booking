// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.DTOs;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

/// <summary>
/// Use case class providing all booking operations.
/// </summary>
public class BookingUseCase
{
    private readonly IBookingRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookingUseCase"/> class.
    /// </summary>
    /// <param name="repository">The booking repository.</param>
    public BookingUseCase(IBookingRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Retrieves a filtered, paginated list of bookings.
    /// </summary>
    public virtual async Task<IReadOnlyList<BookingDto>> ListAsync(
        int page, int perPage,
        int? resourceId = null, string? status = null,
        string? dateFrom = null, string? dateTo = null,
        bool? isNew = null, string? search = null,
        string? orderBy = null, string? order = null,
        CancellationToken ct = default)
    {
        var bookings = await _repository.ListAsync(page, perPage,
            resourceId, status, dateFrom, dateTo,
            isNew, search, orderBy, order, ct);
        return bookings.Select(ToDto).ToList();
    }

    /// <summary>
    /// Retrieves a single booking by its identifier.
    /// </summary>
    public virtual async Task<BookingDto?> GetAsync(int id, CancellationToken ct = default)
    {
        var booking = await _repository.GetAsync(id, ct);
        return booking is null ? null : ToDto(booking);
    }

    /// <summary>
    /// Creates a new booking.
    /// </summary>
    public virtual async Task<BookingDto> CreateAsync(
        int bookingType, string formDataJson, string datesJson,
        CancellationToken ct = default)
    {
        var booking = await _repository.CreateAsync(bookingType, formDataJson, datesJson, ct);
        return ToDto(booking);
    }

    /// <summary>
    /// Updates an existing booking.
    /// </summary>
    public virtual async Task<BookingDto> UpdateAsync(
        int id, string? formDataJson = null, int? bookingType = null,
        string? status = null, CancellationToken ct = default)
    {
        var booking = await _repository.UpdateAsync(id, formDataJson, bookingType, status, ct);
        return ToDto(booking);
    }

    /// <summary>
    /// Deletes a booking.
    /// </summary>
    public virtual async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        await _repository.DeleteAsync(id, ct);
    }

    /// <summary>
    /// Approves a booking.
    /// </summary>
    public virtual async Task<BookingDto> ApproveAsync(int id, CancellationToken ct = default)
    {
        var booking = await _repository.ApproveAsync(id, ct);
        return ToDto(booking);
    }

    /// <summary>
    /// Sets a booking to pending status.
    /// </summary>
    public virtual async Task<BookingDto> SetPendingAsync(int id, CancellationToken ct = default)
    {
        var booking = await _repository.SetPendingAsync(id, ct);
        return ToDto(booking);
    }

    /// <summary>
    /// Updates the note attached to a booking.
    /// </summary>
    public virtual async Task<BookingDto> UpdateNoteAsync(
        int id, string note, CancellationToken ct = default)
    {
        var booking = await _repository.UpdateNoteAsync(id, note, ct);
        return ToDto(booking);
    }

    private static BookingDto ToDto(Booking b) => new(
        b.Id, b.BookingType, b.Dates, b.FormData, b.Status,
        b.SortDate, b.ModificationDate, b.IsNew, b.Note);
}
