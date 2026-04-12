// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.Text.Json;
using McpBooking.Domain.Entities;

namespace McpBooking.Application.DTOs;

/// <summary>
/// Data transfer object representing a booking.
/// </summary>
/// <param name="Id">The unique identifier of the booking.</param>
/// <param name="BookingType">The resource/booking type ID.</param>
/// <param name="Dates">The list of booked dates.</param>
/// <param name="FormData">The submitted form data as a raw JSON element.</param>
/// <param name="Status">The booking status.</param>
/// <param name="SortDate">The sort date, or <see langword="null"/> if not set.</param>
/// <param name="ModificationDate">The modification date, or <see langword="null"/> if not set.</param>
/// <param name="IsNew">Whether the booking is new/unread, or <see langword="null"/> if not set.</param>
/// <param name="Note">The note, or <see langword="null"/> if not set.</param>
public record BookingDto(
    int Id, int BookingType, List<BookingDate> Dates,
    JsonElement? FormData, string Status,
    string? SortDate, string? ModificationDate,
    bool? IsNew, string? Note);
