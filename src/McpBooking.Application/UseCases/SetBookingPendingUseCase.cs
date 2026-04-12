// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.DTOs;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

/// <summary>
/// Use case for setting a booking to pending status.
/// </summary>
public class SetBookingPendingUseCase
{
    private readonly IBookingRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetBookingPendingUseCase"/> class.
    /// </summary>
    /// <param name="repository">The booking repository used to set bookings to pending.</param>
    public SetBookingPendingUseCase(IBookingRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Sets a booking to pending status via the repository and returns it as a DTO.
    /// </summary>
    public virtual async Task<BookingDto> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var booking = await _repository.SetPendingAsync(id, ct);
        return ToDto(booking);
    }

    private static BookingDto ToDto(Booking b) => new(
        b.Id, b.BookingType, b.Dates, b.FormData, b.Status,
        b.SortDate, b.ModificationDate, b.IsNew, b.Note);
}
