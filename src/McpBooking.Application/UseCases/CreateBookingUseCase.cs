// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.DTOs;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

/// <summary>
/// Use case for creating a new booking.
/// </summary>
public class CreateBookingUseCase
{
    private readonly IBookingRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBookingUseCase"/> class.
    /// </summary>
    /// <param name="repository">The booking repository used to create bookings.</param>
    public CreateBookingUseCase(IBookingRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Creates a new booking via the repository.
    /// </summary>
    public virtual async Task<BookingDto> ExecuteAsync(
        int bookingType, string formDataJson, string datesJson,
        CancellationToken ct = default)
    {
        var booking = await _repository.CreateAsync(bookingType, formDataJson, datesJson, ct);
        return ToDto(booking);
    }

    private static BookingDto ToDto(Booking b) => new(
        b.Id, b.BookingType, b.Dates, b.FormData, b.Status,
        b.SortDate, b.ModificationDate, b.IsNew, b.Note);
}
