using System.Net;
using System.Text.Json;
using McpBooking.Domain.Entities;
using McpBooking.Infrastructure.Http;
using McpBooking.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Shouldly;

namespace McpBooking.Infrastructure.Tests.Repositories;

[TestClass]
public class ResourceRepositoryTests
{
    private static (ResourceRepository repo, Mock<HttpMessageHandler> handlerMock) CreateRepoWithMockHttp(
        HttpStatusCode statusCode, string responseBody)
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(responseBody, System.Text.Encoding.UTF8, "application/json")
            });

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("https://example.com/wp-json/wpbc/v1")
        };
        var apiClient = new BookingApiClient(httpClient);
        var repo = new ResourceRepository(apiClient);
        return (repo, handlerMock);
    }

    [TestMethod]
    public async Task ListAsync_ReturnsDeserializedResources()
    {
        var json = JsonSerializer.Serialize(new[]
        {
            new { id = 1, title = "Gemeindesaal", cost = (string?)"50", visitors = (int?)30 },
            new { id = 2, title = "Vereinsraum", cost = (string?)null, visitors = (int?)null }
        });
        var (repo, _) = CreateRepoWithMockHttp(HttpStatusCode.OK, json);

        var result = await repo.ListAsync();

        result.Count.ShouldBe(2);
        result[0].Id.ShouldBe(1);
        result[0].Title.ShouldBe("Gemeindesaal");
        result[0].Cost.ShouldBe("50");
        result[0].Visitors.ShouldBe(30);
        result[1].Id.ShouldBe(2);
        result[1].Title.ShouldBe("Vereinsraum");
    }

    [TestMethod]
    public async Task ListAsync_EmptyArray_ReturnsEmptyList()
    {
        var (repo, _) = CreateRepoWithMockHttp(HttpStatusCode.OK, "[]");

        var result = await repo.ListAsync();

        result.ShouldBeEmpty();
    }

    [TestMethod]
    public async Task ListAsync_SendsCorrectQueryParameters()
    {
        var (repo, handlerMock) = CreateRepoWithMockHttp(HttpStatusCode.OK, "[]");

        await repo.ListAsync(page: 3, perPage: 50);

        handlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.RequestUri!.PathAndQuery.Contains("page=3") &&
                req.RequestUri.PathAndQuery.Contains("per_page=50")),
            ItExpr.IsAny<CancellationToken>());
    }
}
