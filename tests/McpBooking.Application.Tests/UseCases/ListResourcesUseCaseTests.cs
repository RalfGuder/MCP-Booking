using McpBooking.Application.DTOs;
using McpBooking.Application.UseCases;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;
using Moq;
using Shouldly;
using Xunit;

namespace McpBooking.Application.Tests.UseCases;

public class ListResourcesUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ReturnsResources_MappedToDtos()
    {
        var mock = new Mock<IResourceRepository>();
        mock.Setup(r => r.ListAsync(1, 20, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Resource>
            {
                new() { Id = 1, Title = "Gemeindesaal", Cost = "50", Visitors = 30 },
                new() { Id = 2, Title = "Vereinsraum", Cost = null, Visitors = null }
            });
        var useCase = new ListResourcesUseCase(mock.Object);

        var result = await useCase.ExecuteAsync();

        result.Count.ShouldBe(2);
        result[0].ShouldBe(new ResourceDto(1, "Gemeindesaal", "50", 30));
        result[1].ShouldBe(new ResourceDto(2, "Vereinsraum", null, null));
    }

    [Fact]
    public async Task ExecuteAsync_EmptyList_ReturnsEmptyCollection()
    {
        var mock = new Mock<IResourceRepository>();
        mock.Setup(r => r.ListAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Resource>());
        var useCase = new ListResourcesUseCase(mock.Object);

        var result = await useCase.ExecuteAsync();

        result.ShouldBeEmpty();
    }

    [Fact]
    public async Task ExecuteAsync_PassesPaginationParameters()
    {
        var mock = new Mock<IResourceRepository>();
        mock.Setup(r => r.ListAsync(3, 50, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Resource>());
        var useCase = new ListResourcesUseCase(mock.Object);

        await useCase.ExecuteAsync(page: 3, perPage: 50);

        mock.Verify(r => r.ListAsync(3, 50, It.IsAny<CancellationToken>()), Times.Once);
    }
}
