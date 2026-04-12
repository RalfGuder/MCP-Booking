// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
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
public class BookingRepositoryTests
{
    private static (BookingRepository repo, Mock<HttpMessageHandler> handlerMock) CreateRepoWithMockHttp(
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
            BaseAddress = new Uri("https://example.com/wp-json/wpbc/v1/")
        };
        var apiClient = new BookingApiClient(httpClient);
        var repo = new BookingRepository(apiClient);
        return (repo, handlerMock);
    }

    [TestMethod]
    public async Task ListAsync_ReturnsDeserializedBookings()
    {
        var json = JsonSerializer.Serialize(new[]
        {
            new { id = 1, booking_type = 2, dates = new[] { "2026-05-01" }, status = "approved" },
            new { id = 2, booking_type = 3, dates = new[] { "2026-06-01", "2026-06-02" }, status = "pending" }
        });
        var (repo, _) = CreateRepoWithMockHttp(HttpStatusCode.OK, json);

        var result = await repo.ListAsync(1, 20);

        result.Count.ShouldBe(2);
        result[0].Id.ShouldBe(1);
        result[0].BookingType.ShouldBe(2);
        result[0].Status.ShouldBe("approved");
        result[1].Id.ShouldBe(2);
        result[1].Dates.Count.ShouldBe(2);
    }

    [TestMethod]
    public async Task ListAsync_SendsCorrectQueryWithFilters()
    {
        var (repo, handlerMock) = CreateRepoWithMockHttp(HttpStatusCode.OK, "[]");

        await repo.ListAsync(page: 2, perPage: 50,
            resourceId: 5, status: "pending",
            dateFrom: "2026-01-01", dateTo: "2026-12-31",
            isNew: true, search: "test",
            orderBy: "sort_date", order: "DESC");

        handlerMock.Protected().Verify(
            "SendAsync",
            Times.Once(),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.RequestUri!.PathAndQuery.Contains("page=2") &&
                req.RequestUri.PathAndQuery.Contains("per_page=50") &&
                req.RequestUri.PathAndQuery.Contains("resource_id=5") &&
                req.RequestUri.PathAndQuery.Contains("status=pending") &&
                req.RequestUri.PathAndQuery.Contains("date_from=2026-01-01") &&
                req.RequestUri.PathAndQuery.Contains("date_to=2026-12-31") &&
                req.RequestUri.PathAndQuery.Contains("is_new=true") &&
                req.RequestUri.PathAndQuery.Contains("search=test") &&
                req.RequestUri.PathAndQuery.Contains("orderby=sort_date") &&
                req.RequestUri.PathAndQuery.Contains("order=DESC")),
            ItExpr.IsAny<CancellationToken>());
    }

    [TestMethod]
    public async Task ListAsync_EmptyArray_ReturnsEmptyList()
    {
        var (repo, _) = CreateRepoWithMockHttp(HttpStatusCode.OK, "[]");

        var result = await repo.ListAsync(1, 20);

        result.ShouldBeEmpty();
    }
}
