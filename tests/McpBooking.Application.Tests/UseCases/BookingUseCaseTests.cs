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
public class BookingUseCaseTests
{
    // ── ListAsync ──────────────────────────────────────────────────────────────

    [TestMethod]
    public async Task ListAsync_ReturnsBookings_MappedToDtos()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.ListAsync(1, 20,
                null, null, null, null, null, null, null, null,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Booking>
            {
                new() { Id = 1, BookingType = 2, Dates = [new() { BookingDateValue = "2026-05-01T00:00:00", Approved = 1 }], Status = "approved" },
                new() { Id = 2, BookingType = 3, Dates = [new() { BookingDateValue = "2026-06-01T00:00:00", Approved = 1 }, new() { BookingDateValue = "2026-06-02T00:00:00", Approved = 1 }], Status = "pending" }
            });
        var useCase = new BookingUseCase(mock.Object);

        var result = await useCase.ListAsync(1, 20);

        result.Count.ShouldBe(2);
        result[0].Id.ShouldBe(1);
        result[0].BookingType.ShouldBe(2);
        result[0].Status.ShouldBe("approved");
        result[1].Id.ShouldBe(2);
        result[1].Dates.Count.ShouldBe(2);
    }

    [TestMethod]
    public async Task ListAsync_EmptyList_ReturnsEmptyCollection()
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
        var useCase = new BookingUseCase(mock.Object);

        var result = await useCase.ListAsync(1, 20);

        result.ShouldBeEmpty();
    }

    [TestMethod]
    public async Task ListAsync_PassesAllFilterParameters()
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
        var useCase = new BookingUseCase(mock.Object);

        await useCase.ListAsync(
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

    // ── GetAsync ───────────────────────────────────────────────────────────────

    [TestMethod]
    public async Task GetAsync_BookingExists_ReturnsMappedDto()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.GetAsync(42, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking
            {
                Id = 42,
                BookingType = 3,
                Dates = [new() { BookingDateValue = "2026-05-01T00:00:00", Approved = 1 }, new() { BookingDateValue = "2026-05-02T00:00:00", Approved = 1 }],
                Status = "approved",
                SortDate = "2026-05-01",
                ModificationDate = "2026-04-12",
                IsNew = false,
                Note = "Test note"
            });
        var useCase = new BookingUseCase(mock.Object);

        var result = await useCase.GetAsync(42);

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
    public async Task GetAsync_BookingNotFound_ReturnsNull()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.GetAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Booking?)null);
        var useCase = new BookingUseCase(mock.Object);

        var result = await useCase.GetAsync(999);

        result.ShouldBeNull();
    }

    // ── CreateAsync ────────────────────────────────────────────────────────────

    [TestMethod]
    public async Task CreateAsync_ReturnsCreatedBookingAsDto()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.CreateAsync(2, "{}", "[]", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking
            {
                Id = 99,
                BookingType = 2,
                Dates = [new() { BookingDateValue = "2026-05-10T00:00:00", Approved = 1 }],
                Status = "pending",
                SortDate = "2026-05-10",
                ModificationDate = "2026-04-12",
                IsNew = true,
                Note = null
            });
        var useCase = new BookingUseCase(mock.Object);

        var result = await useCase.CreateAsync(2, "{}", "[]");

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
    public async Task CreateAsync_DelegatesCorrectParameters()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.CreateAsync(
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking { Id = 1, Status = "pending", Dates = [] });
        var useCase = new BookingUseCase(mock.Object);

        await useCase.CreateAsync(3, "{\"name\":\"Test\"}", "[\"2026-06-01\"]");

        mock.Verify(r => r.CreateAsync(
            3, "{\"name\":\"Test\"}", "[\"2026-06-01\"]",
            It.IsAny<CancellationToken>()), Times.Once);
    }

    // ── UpdateAsync ────────────────────────────────────────────────────────────

    [TestMethod]
    public async Task UpdateAsync_ReturnsUpdatedBookingAsDto()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.UpdateAsync(1, null, null, "approved", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking
            {
                Id = 1,
                BookingType = 2,
                Dates = [new() { BookingDateValue = "2026-05-10T00:00:00", Approved = 1 }],
                Status = "approved",
                SortDate = "2026-05-10",
                ModificationDate = "2026-04-12",
                IsNew = false,
                Note = null
            });
        var useCase = new BookingUseCase(mock.Object);

        var result = await useCase.UpdateAsync(1, null, null, "approved");

        result.ShouldNotBeNull();
        result.Id.ShouldBe(1);
        result.Status.ShouldBe("approved");
        result.BookingType.ShouldBe(2);
        result.IsNew.ShouldBe(false);
    }

    [TestMethod]
    public async Task UpdateAsync_DelegatesAllParameters()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.UpdateAsync(
                It.IsAny<int>(), It.IsAny<string?>(), It.IsAny<int?>(), It.IsAny<string?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking { Id = 5, Status = "pending", Dates = [] });
        var useCase = new BookingUseCase(mock.Object);

        await useCase.UpdateAsync(5, "{}", 3, "pending");

        mock.Verify(r => r.UpdateAsync(
            5, "{}", 3, "pending",
            It.IsAny<CancellationToken>()), Times.Once);
    }

    // ── DeleteAsync ────────────────────────────────────────────────────────────

    [TestMethod]
    public async Task DeleteAsync_DelegatesToRepository()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.DeleteAsync(5, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        var useCase = new BookingUseCase(mock.Object);

        await useCase.DeleteAsync(5);

        mock.Verify(r => r.DeleteAsync(5, It.IsAny<CancellationToken>()), Times.Once);
    }

    // ── ApproveAsync ───────────────────────────────────────────────────────────

    [TestMethod]
    public async Task ApproveAsync_ReturnsApprovedBookingAsDto()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.ApproveAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking
            {
                Id = 1,
                BookingType = 2,
                Dates = [new() { BookingDateValue = "2026-05-01T00:00:00", Approved = 1 }],
                Status = "approved",
                SortDate = "2026-05-01",
                ModificationDate = "2026-04-12",
                IsNew = false,
                Note = null
            });
        var useCase = new BookingUseCase(mock.Object);

        var result = await useCase.ApproveAsync(1);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(1);
        result.Status.ShouldBe("approved");
        result.BookingType.ShouldBe(2);
        result.Dates.Count.ShouldBe(1);
    }

    [TestMethod]
    public async Task ApproveAsync_DelegatesToRepository()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.ApproveAsync(5, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking { Id = 5, Status = "approved" });
        var useCase = new BookingUseCase(mock.Object);

        await useCase.ApproveAsync(5);

        mock.Verify(r => r.ApproveAsync(5, It.IsAny<CancellationToken>()), Times.Once);
    }

    // ── SetPendingAsync ────────────────────────────────────────────────────────

    [TestMethod]
    public async Task SetPendingAsync_ReturnsPendingBookingAsDto()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.SetPendingAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking
            {
                Id = 1,
                BookingType = 2,
                Dates = [new() { BookingDateValue = "2026-05-01T00:00:00", Approved = 1 }],
                Status = "pending",
                SortDate = "2026-05-01",
                ModificationDate = "2026-04-12",
                IsNew = false,
                Note = null
            });
        var useCase = new BookingUseCase(mock.Object);

        var result = await useCase.SetPendingAsync(1);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(1);
        result.Status.ShouldBe("pending");
        result.BookingType.ShouldBe(2);
        result.Dates.Count.ShouldBe(1);
    }

    [TestMethod]
    public async Task SetPendingAsync_DelegatesToRepository()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.SetPendingAsync(5, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking { Id = 5, Status = "pending" });
        var useCase = new BookingUseCase(mock.Object);

        await useCase.SetPendingAsync(5);

        mock.Verify(r => r.SetPendingAsync(5, It.IsAny<CancellationToken>()), Times.Once);
    }

    // ── UpdateNoteAsync ────────────────────────────────────────────────────────

    [TestMethod]
    public async Task UpdateNoteAsync_ReturnsUpdatedBookingAsDto()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.UpdateNoteAsync(1, "Neue Notiz", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking
            {
                Id = 1,
                BookingType = 2,
                Dates = [new() { BookingDateValue = "2026-05-10T00:00:00", Approved = 1 }],
                Status = "approved",
                SortDate = "2026-05-10",
                ModificationDate = "2026-04-12",
                IsNew = false,
                Note = "Neue Notiz"
            });
        var useCase = new BookingUseCase(mock.Object);

        var result = await useCase.UpdateNoteAsync(1, "Neue Notiz");

        result.ShouldNotBeNull();
        result.Id.ShouldBe(1);
        result.Note.ShouldBe("Neue Notiz");
    }

    [TestMethod]
    public async Task UpdateNoteAsync_DelegatesCorrectParameters()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.UpdateNoteAsync(
                It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Booking { Id = 5, Status = "approved", Dates = [], Note = "Test" });
        var useCase = new BookingUseCase(mock.Object);

        await useCase.UpdateNoteAsync(5, "Test");

        mock.Verify(r => r.UpdateNoteAsync(
            5, "Test",
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
