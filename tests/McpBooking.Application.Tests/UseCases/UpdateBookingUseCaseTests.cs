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
public class UpdateBookingUseCaseTests
{
    [TestMethod]
    public async Task ExecuteAsync_ReturnsUpdatedBookingAsDto()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.UpdateAsync(1, null, null, "approved", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking
            {
                Id = 1,
                BookingType = 2,
                Dates = ["2026-05-10"],
                Status = "approved",
                SortDate = "2026-05-10",
                ModificationDate = "2026-04-12",
                IsNew = false,
                Note = null
            });
        var useCase = new UpdateBookingUseCase(mock.Object);

        var result = await useCase.ExecuteAsync(1, null, null, "approved");

        result.ShouldNotBeNull();
        result.Id.ShouldBe(1);
        result.Status.ShouldBe("approved");
        result.BookingType.ShouldBe(2);
        result.IsNew.ShouldBe(false);
    }

    [TestMethod]
    public async Task ExecuteAsync_DelegatesAllParameters()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.UpdateAsync(
                It.IsAny<int>(), It.IsAny<string?>(), It.IsAny<int?>(), It.IsAny<string?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking { Id = 5, Status = "pending", Dates = [] });
        var useCase = new UpdateBookingUseCase(mock.Object);

        await useCase.ExecuteAsync(5, "{}", 3, "pending");

        mock.Verify(r => r.UpdateAsync(
            5, "{}", 3, "pending",
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
