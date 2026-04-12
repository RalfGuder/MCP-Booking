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
public class GetBookingUseCaseTests
{
    [TestMethod]
    public async Task ExecuteAsync_BookingExists_ReturnsMappedDto()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.GetAsync(42, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking
            {
                Id = 42,
                BookingType = 3,
                Dates = ["2026-05-01", "2026-05-02"],
                Status = "approved",
                SortDate = "2026-05-01",
                ModificationDate = "2026-04-12",
                IsNew = false,
                Note = "Test note"
            });
        var useCase = new GetBookingUseCase(mock.Object);

        var result = await useCase.ExecuteAsync(42);

        result.ShouldNotBeNull();
        result!.Id.ShouldBe(42);
        result.BookingType.ShouldBe(3);
        result.Dates.Count.ShouldBe(2);
        result.Status.ShouldBe("approved");
        result.SortDate.ShouldBe("2026-05-01");
        result.ModificationDate.ShouldBe("2026-04-12");
        result.IsNew.ShouldBe(false);
        result.Note.ShouldBe("Test note");
    }

    [TestMethod]
    public async Task ExecuteAsync_BookingNotFound_ReturnsNull()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.GetAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Booking?)null);
        var useCase = new GetBookingUseCase(mock.Object);

        var result = await useCase.ExecuteAsync(999);

        result.ShouldBeNull();
    }
}
