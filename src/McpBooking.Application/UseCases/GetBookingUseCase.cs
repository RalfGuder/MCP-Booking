// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.DTOs;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

/// <summary>
/// Use case for retrieving a single booking by its identifier.
/// </summary>
public class GetBookingUseCase
{
    private readonly IBookingRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBookingUseCase"/> class.
    /// </summary>
    /// <param name="repository">The booking repository used to retrieve data.</param>
    public GetBookingUseCase(IBookingRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Retrieves a single booking from the repository.
    /// </summary>
    public virtual async Task<BookingDto?> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var booking = await _repository.GetAsync(id, ct);
        return booking is null ? null : ToDto(booking);
    }

    private static BookingDto ToDto(Booking b) => new(
        b.Id, b.BookingType, b.Dates, b.FormData, b.Status,
        b.SortDate, b.ModificationDate, b.IsNew, b.Note);
}
