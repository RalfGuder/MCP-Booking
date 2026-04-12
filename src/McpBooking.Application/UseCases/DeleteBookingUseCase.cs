// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

/// <summary>
/// Use case for deleting a booking.
/// </summary>
public class DeleteBookingUseCase
{
    private readonly IBookingRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteBookingUseCase"/> class.
    /// </summary>
    /// <param name="repository">The booking repository used to delete bookings.</param>
    public DeleteBookingUseCase(IBookingRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Deletes a booking via the repository.
    /// </summary>
    public virtual async Task ExecuteAsync(int id, CancellationToken ct = default)
    {
        await _repository.DeleteAsync(id, ct);
    }
}
