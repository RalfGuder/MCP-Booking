// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Domain.Entities;

namespace McpBooking.Domain.Interfaces;

/// <summary>
/// Defines the contract for retrieving resources from the data source.
/// </summary>
public interface IResourceRepository
{
    /// <summary>
    /// Retrieves a paginated list of resources from the data source.
    /// </summary>
    /// <param name="page">The page number (1-based).</param>
    /// <param name="perPage">The number of items per page (1-100).</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A read-only list of resources.</returns>
    Task<IReadOnlyList<Resource>> ListAsync(int page = 1, int perPage = 20, CancellationToken ct = default);
}
