// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.DTOs;
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

/// <summary>
/// Use case for retrieving a paginated list of resources.
/// </summary>
public class ListResourcesUseCase
{
    private readonly IResourceRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListResourcesUseCase"/> class.
    /// </summary>
    /// <param name="repository">The resource repository used to retrieve data.</param>
    public ListResourcesUseCase(IResourceRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Retrieves a paginated list of resources from the repository.
    /// </summary>
    /// <param name="page">The page number (1-based).</param>
    /// <param name="perPage">The number of items per page (1-100).</param>
    /// <param name="ct">A cancellation token.</param>
    /// <returns>A read-only list of resource data transfer objects.</returns>
    public virtual async Task<IReadOnlyList<ResourceDto>> ExecuteAsync(
        int page = 1, int perPage = 20, CancellationToken ct = default)
    {
        var resources = await _repository.ListAsync(page, perPage, ct);
        return resources.Select(r => new ResourceDto(r.Id, r.Title, r.Cost, r.Visitors)).ToList();
    }
}
