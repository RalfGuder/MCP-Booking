// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.UseCases;
using McpBooking.Domain.Entities;
using McpBooking.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Application.Tests.UseCases;

[TestClass]
public class UpdateBookingNoteUseCaseTests
{
    [TestMethod]
    public async Task ExecuteAsync_ReturnsUpdatedBookingAsDto()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.UpdateNoteAsync(1, "Neue Notiz", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking
            {
                Id = 1,
                BookingType = 2,
                Dates = ["2026-05-10"],
                Status = "approved",
                SortDate = "2026-05-10",
                ModificationDate = "2026-04-12",
                IsNew = false,
                Note = "Neue Notiz"
            });
        var useCase = new UpdateBookingNoteUseCase(mock.Object);

        var result = await useCase.ExecuteAsync(1, "Neue Notiz");

        result.ShouldNotBeNull();
        result.Id.ShouldBe(1);
        result.Note.ShouldBe("Neue Notiz");
    }

    [TestMethod]
    public async Task ExecuteAsync_DelegatesCorrectParameters()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.UpdateNoteAsync(
                It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking { Id = 5, Status = "approved", Dates = [], Note = "Test" });
        var useCase = new UpdateBookingNoteUseCase(mock.Object);

        await useCase.ExecuteAsync(5, "Test");

        mock.Verify(r => r.UpdateNoteAsync(
            5, "Test",
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
