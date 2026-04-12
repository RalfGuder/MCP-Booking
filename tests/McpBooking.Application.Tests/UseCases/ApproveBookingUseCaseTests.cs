// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.UseCases;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Application.Tests.UseCases;

[TestClass]
public class ApproveBookingUseCaseTests
{
    [TestMethod]
    public async Task ExecuteAsync_ReturnsApprovedBookingAsDto()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.ApproveAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking
            {
                Id = 1,
                BookingType = 2,
                Dates = ["2026-05-01"],
                Status = "approved",
                SortDate = "2026-05-01",
                ModificationDate = "2026-04-12",
                IsNew = false,
                Note = null
            });
        var useCase = new ApproveBookingUseCase(mock.Object);

        var result = await useCase.ExecuteAsync(1);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(1);
        result.Status.ShouldBe("approved");
        result.BookingType.ShouldBe(2);
        result.Dates.Count.ShouldBe(1);
    }

    [TestMethod]
    public async Task ExecuteAsync_DelegatesToRepository()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.ApproveAsync(5, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking { Id = 5, Status = "approved" });
        var useCase = new ApproveBookingUseCase(mock.Object);

        await useCase.ExecuteAsync(5);

        mock.Verify(r => r.ApproveAsync(5, It.IsAny<CancellationToken>()), Times.Once);
    }
}
