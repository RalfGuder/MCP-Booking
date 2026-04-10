using McpBooking.Domain.Entities;

namespace McpBooking.Domain.Interfaces;

public interface IResourceRepository
{
    Task<IReadOnlyList<Resource>> ListAsync(int page = 1, int perPage = 20, CancellationToken ct = default);
}
