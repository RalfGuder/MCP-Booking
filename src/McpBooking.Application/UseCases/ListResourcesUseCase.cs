using McpBooking.Application.DTOs;
using McpBooking.Domain.Interfaces;

namespace McpBooking.Application.UseCases;

public class ListResourcesUseCase
{
    private readonly IResourceRepository _repository;

    public ListResourcesUseCase(IResourceRepository repository)
    {
        _repository = repository;
    }

    public virtual async Task<IReadOnlyList<ResourceDto>> ExecuteAsync(
        int page = 1, int perPage = 20, CancellationToken ct = default)
    {
        var resources = await _repository.ListAsync(page, perPage, ct);
        return resources.Select(r => new ResourceDto(r.Id, r.Title, r.Cost, r.Visitors)).ToList();
    }
}
