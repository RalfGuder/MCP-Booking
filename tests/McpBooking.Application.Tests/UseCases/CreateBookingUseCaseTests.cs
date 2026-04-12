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
public class CreateBookingUseCaseTests
{
    [TestMethod]
    public async Task ExecuteAsync_ReturnsCreatedBookingAsDto()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.CreateAsync(2, "{}", "[]", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking
            {
                Id = 99,
                BookingType = 2,
                Dates = ["2026-05-10"],
                Status = "pending",
                SortDate = "2026-05-10",
                ModificationDate = "2026-04-12",
                IsNew = true,
                Note = null
            });
        var useCase = new CreateBookingUseCase(mock.Object);

        var result = await useCase.ExecuteAsync(2, "{}", "[]");

        result.ShouldNotBeNull();
        result.Id.ShouldBe(99);
        result.BookingType.ShouldBe(2);
        result.Dates.Count.ShouldBe(1);
        result.Status.ShouldBe("pending");
        result.SortDate.ShouldBe("2026-05-10");
        result.ModificationDate.ShouldBe("2026-04-12");
        result.IsNew.ShouldBe(true);
        result.Note.ShouldBeNull();
    }

    [TestMethod]
    public async Task ExecuteAsync_DelegatesCorrectParameters()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.CreateAsync(
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking { Id = 1, Status = "pending", Dates = [] });
        var useCase = new CreateBookingUseCase(mock.Object);

        await useCase.ExecuteAsync(3, "{\"name\":\"Test\"}", "[\"2026-06-01\"]");

        mock.Verify(r => r.CreateAsync(
            3, "{\"name\":\"Test\"}", "[\"2026-06-01\"]",
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
