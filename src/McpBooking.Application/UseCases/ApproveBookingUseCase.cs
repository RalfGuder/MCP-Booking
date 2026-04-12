// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.DTOs;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

/// <summary>
/// Use case for approving a booking.
/// </summary>
public class ApproveBookingUseCase
{
    private readonly IBookingRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApproveBookingUseCase"/> class.
    /// </summary>
    /// <param name="repository">The booking repository used to approve bookings.</param>
    public ApproveBookingUseCase(IBookingRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Approves a booking via the repository and returns it as a DTO.
    /// </summary>
    public virtual async Task<BookingDto> ExecuteAsync(int id, CancellationToken ct = default)
    {
        var booking = await _repository.ApproveAsync(id, ct);
        return ToDto(booking);
    }

    private static BookingDto ToDto(Booking b) => new(
        b.Id, b.BookingType, b.Dates, b.FormData, b.Status,
        b.SortDate, b.ModificationDate, b.IsNew, b.Note);
}
