// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.UseCases;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Application.Tests.UseCases;

[TestClass]
public class SetBookingPendingUseCaseTests
{
    [TestMethod]
    public async Task ExecuteAsync_ReturnsPendingBookingAsDto()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.SetPendingAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking
            {
                Id = 1,
                BookingType = 2,
                Dates = ["2026-05-01"],
                Status = "pending",
                SortDate = "2026-05-01",
                ModificationDate = "2026-04-12",
                IsNew = false,
                Note = null
            });
        var useCase = new SetBookingPendingUseCase(mock.Object);

        var result = await useCase.ExecuteAsync(1);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(1);
        result.Status.ShouldBe("pending");
        result.BookingType.ShouldBe(2);
        result.Dates.Count.ShouldBe(1);
    }

    [TestMethod]
    public async Task ExecuteAsync_DelegatesToRepository()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.SetPendingAsync(5, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking { Id = 5, Status = "pending" });
        var useCase = new SetBookingPendingUseCase(mock.Object);

        await useCase.ExecuteAsync(5);

        mock.Verify(r => r.SetPendingAsync(5, It.IsAny<CancellationToken>()), Times.Once);
    }
}
