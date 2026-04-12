// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.DTOs;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

/// <summary>
/// Use case for updating the note of an existing booking.
/// </summary>
public class UpdateBookingNoteUseCase
{
    private readonly IBookingRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBookingNoteUseCase"/> class.
    /// </summary>
    /// <param name="repository">The booking repository used to update booking notes.</param>
    public UpdateBookingNoteUseCase(IBookingRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Updates the note of a booking via the repository and returns the updated booking as a DTO.
    /// </summary>
    /// <param name="id">The identifier of the booking to update.</param>
    /// <param name="note">The new note text for the booking.</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>The updated booking as a <see cref="BookingDto"/>.</returns>
    public virtual async Task<BookingDto> ExecuteAsync(int id, string note, CancellationToken ct = default)
    {
        var booking = await _repository.UpdateNoteAsync(id, note, ct);
        return ToDto(booking);
    }

    private static BookingDto ToDto(Booking b) => new(
        b.Id, b.BookingType, b.Dates, b.FormData, b.Status,
        b.SortDate, b.ModificationDate, b.IsNew, b.Note);
}
