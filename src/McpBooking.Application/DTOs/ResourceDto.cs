// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
namespace McpBooking.Application.DTOs;

/// <summary>
/// Data transfer object representing a resource.
/// </summary>
/// <param name="Id">The unique identifier of the resource.</param>
/// <param name="Title">The display name of the resource.</param>
/// <param name="Cost">The cost of the resource, or <see langword="null"/> if not set.</param>
/// <param name="Visitors">The maximum number of visitors, or <see langword="null"/> if not set.</param>
public record ResourceDto(int Id, string Title, string? Cost, int? Visitors);
