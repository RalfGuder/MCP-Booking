using System.Net;
using System.Text.Json;
using McpBooking.Application.DTOs;
using McpBooking.Application.UseCases;
using McpBooking.Domain.Interfaces;
using McpBooking.Server.Tools;
using Moq;
using Shouldly;
using Xunit;

namespace McpBooking.Server.Tests.Tools;

public class ListResourcesToolTests
{
    private static ListResourcesTool CreateToolWithMockUseCase(
        IReadOnlyList<ResourceDto>? result = null, Exception? exception = null)
    {
        var useCaseMock = new Mock<ListResourcesUseCase>(Mock.Of<IResourceRepository>());
        if (exception != null)
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);
        }
        else
        {
            useCaseMock.Setup(u => u.ExecuteAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result ?? new List<ResourceDto>());
        }
        return new ListResourcesTool(useCaseMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ValidParams_ReturnsJson()
    {
        var resources = new List<ResourceDto>
        {
            new(1, "Gemeindesaal", "50", 30)
        };
        var tool = CreateToolWithMockUseCase(resources);

        var result = await tool.ExecuteAsync();

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetArrayLength().ShouldBe(1);
        doc.RootElement[0].GetProperty("id").GetInt32().ShouldBe(1);
        doc.RootElement[0].GetProperty("title").GetString().ShouldBe("Gemeindesaal");
    }

    [Fact]
    public async Task ExecuteAsync_InvalidPage_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(page: 0);

        result.ShouldContain("page muss >= 1 sein");
    }

    [Fact]
    public async Task ExecuteAsync_InvalidPerPage_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(perPage: 101);

        result.ShouldContain("per_page muss zwischen 1 und 100 liegen");
    }

    [Fact]
    public async Task ExecuteAsync_AuthError_ReturnsReadableMessage()
    {
        var tool = CreateToolWithMockUseCase(
            exception: new HttpRequestException("Unauthorized", null, HttpStatusCode.Unauthorized));

        var result = await tool.ExecuteAsync();

        result.ShouldContain("Authentifizierung fehlgeschlagen");
    }
}
