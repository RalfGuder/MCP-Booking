// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.DTOs;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

/// <summary>
/// Use case for updating an existing booking.
/// </summary>
public class UpdateBookingUseCase
{
    private readonly IBookingRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBookingUseCase"/> class.
    /// </summary>
    /// <param name="repository">The booking repository used to update bookings.</param>
    public UpdateBookingUseCase(IBookingRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Updates an existing booking via the repository and returns the updated booking as a DTO.
    /// </summary>
    /// <param name="id">The identifier of the booking to update.</param>
    /// <param name="formDataJson">Optional updated form data as a JSON string.</param>
    /// <param name="bookingType">Optional updated booking type ID.</param>
    /// <param name="status">Optional updated booking status.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The updated booking as a <see cref="BookingDto"/>.</returns>
    public virtual async Task<BookingDto> ExecuteAsync(
        int id, string? formDataJson = null, int? bookingType = null,
        string? status = null, CancellationToken ct = default)
    {
        var booking = await _repository.UpdateAsync(id, formDataJson, bookingType, status, ct);
        return ToDto(booking);
    }

    private static BookingDto ToDto(Booking b) => new(
        b.Id, b.BookingType, b.Dates, b.FormData, b.Status,
        b.SortDate, b.ModificationDate, b.IsNew, b.Note);
}
