// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.DTOs;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

/// <summary>
/// Use case for retrieving a filtered, paginated list of bookings.
/// </summary>
public class ListBookingsUseCase
{
    private readonly IBookingRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListBookingsUseCase"/> class.
    /// </summary>
    /// <param name="repository">The booking repository used to retrieve data.</param>
    public ListBookingsUseCase(IBookingRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Retrieves a filtered, paginated list of bookings from the repository.
    /// </summary>
    public virtual async Task<IReadOnlyList<BookingDto>> ExecuteAsync(
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

    private static BookingDto ToDto(Booking b) => new(
        b.Id, b.BookingType, b.Dates, b.FormData, b.Status,
        b.SortDate, b.ModificationDate, b.IsNew, b.Note);
}
