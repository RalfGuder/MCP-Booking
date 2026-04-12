// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.DTOs;
using McpBooking.Application.UseCases;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Application.Tests.UseCases;

[TestClass]
public class ListBookingsUseCaseTests
{
    [TestMethod]
    public async Task ExecuteAsync_ReturnsBookings_MappedToDtos()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.ListAsync(1, 20,
                null, null, null, null, null, null, null, null,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Booking>
            {
                new() { Id = 1, BookingType = 2, Dates = ["2026-05-01"], Status = "approved" },
                new() { Id = 2, BookingType = 3, Dates = ["2026-06-01", "2026-06-02"], Status = "pending" }
            });
        var useCase = new ListBookingsUseCase(mock.Object);

        var result = await useCase.ExecuteAsync(1, 20);

        result.Count.ShouldBe(2);
        result[0].Id.ShouldBe(1);
        result[0].BookingType.ShouldBe(2);
        result[0].Status.ShouldBe("approved");
        result[1].Id.ShouldBe(2);
        result[1].Dates.Count.ShouldBe(2);
    }

    [TestMethod]
    public async Task ExecuteAsync_EmptyList_ReturnsEmptyCollection()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.ListAsync(
                It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<int?>(), It.IsAny<string?>(),
                It.IsAny<string?>(), It.IsAny<string?>(),
                It.IsAny<bool?>(), It.IsAny<string?>(),
                It.IsAny<string?>(), It.IsAny<string?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Booking>());
        var useCase = new ListBookingsUseCase(mock.Object);

        var result = await useCase.ExecuteAsync(1, 20);

        result.ShouldBeEmpty();
    }

    [TestMethod]
    public async Task ExecuteAsync_PassesAllFilterParameters()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.ListAsync(
                It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<int?>(), It.IsAny<string?>(),
                It.IsAny<string?>(), It.IsAny<string?>(),
                It.IsAny<bool?>(), It.IsAny<string?>(),
                It.IsAny<string?>(), It.IsAny<string?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Booking>());
        var useCase = new ListBookingsUseCase(mock.Object);

        await useCase.ExecuteAsync(
            page: 2, perPage: 50,
            resourceId: 5, status: "pending",
            dateFrom: "2026-01-01", dateTo: "2026-12-31",
            isNew: true, search: "test",
            orderBy: "sort_date", order: "DESC");

        mock.Verify(r => r.ListAsync(
            2, 50, 5, "pending",
            "2026-01-01", "2026-12-31",
            true, "test",
            "sort_date", "DESC",
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
