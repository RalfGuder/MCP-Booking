// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.Net;
using System.Text.Json;
using McpBooking.Application.DTOs;
using McpBooking.Application.UseCases;
using McpBooking.Domain.Interfaces;
using McpBooking.Server.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Server.Tests.Tools;

[TestClass]
public class BookingToolTests
{
    private static BookingTool CreateTool(
        Action<Mock<BookingUseCase>>? setup = null)
    {
        var mock = new Mock<BookingUseCase>(Mock.Of<IBookingRepository>());
        setup?.Invoke(mock);
        return new BookingTool(mock.Object);
    }

    // ── ListBookingsAsync ──────────────────────────────────────────────────────

    [TestMethod]
    public async Task ListBookingsAsync_ValidParams_ReturnsJson()
    {
        var bookings = new List<BookingDto>
        {
            new(1, 2, ["2026-05-01"], null, "approved", null, null, null, null)
        };
        var tool = CreateTool(mock => mock
            .Setup(u => u.ListAsync(
                It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<int?>(), It.IsAny<string?>(),
                It.IsAny<string?>(), It.IsAny<string?>(),
                It.IsAny<bool?>(), It.IsAny<string?>(),
                It.IsAny<string?>(), It.IsAny<string?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(bookings));

        var result = await tool.ListBookingsAsync();

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetArrayLength().ShouldBe(1);
        doc.RootElement[0].GetProperty("id").GetInt32().ShouldBe(1);
        doc.RootElement[0].GetProperty("status").GetString().ShouldBe("approved");
    }

    [TestMethod]
    public async Task ListBookingsAsync_InvalidPage_ReturnsError()
    {
        var tool = CreateTool();

        var result = await tool.ListBookingsAsync(page: 0);

        result.ShouldContain("page muss >= 1 sein");
    }

    [TestMethod]
    public async Task ListBookingsAsync_InvalidPerPage_ReturnsError()
    {
        var tool = CreateTool();

        var result = await tool.ListBookingsAsync(perPage: 101);

        result.ShouldContain("per_page muss zwischen 1 und 100 liegen");
    }

    [TestMethod]
    public async Task ListBookingsAsync_AuthError_ReturnsReadableMessage()
    {
        var tool = CreateTool(mock => mock
            .Setup(u => u.ListAsync(
                It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<int?>(), It.IsAny<string?>(),
                It.IsAny<string?>(), It.IsAny<string?>(),
                It.IsAny<bool?>(), It.IsAny<string?>(),
                It.IsAny<string?>(), It.IsAny<string?>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException("Unauthorized", null, HttpStatusCode.Unauthorized)));

        var result = await tool.ListBookingsAsync();

        result.ShouldContain("Authentifizierung fehlgeschlagen");
    }

    // ── GetBookingAsync ────────────────────────────────────────────────────────

    [TestMethod]
    public async Task GetBookingAsync_ValidId_ReturnsJson()
    {
        var booking = new BookingDto(42, 2, ["2026-05-01"], null, "approved", null, null, null, null);
        var tool = CreateTool(mock => mock
            .Setup(u => u.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(booking));

        var result = await tool.GetBookingAsync(42);

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetProperty("id").GetInt32().ShouldBe(42);
        doc.RootElement.GetProperty("status").GetString().ShouldBe("approved");
    }

    [TestMethod]
    public async Task GetBookingAsync_InvalidId_ReturnsError()
    {
        var tool = CreateTool();

        var result = await tool.GetBookingAsync(0);

        result.ShouldContain("ID");
    }

    [TestMethod]
    public async Task GetBookingAsync_NotFound_ReturnsError()
    {
        var tool = CreateTool(mock => mock
            .Setup(u => u.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((BookingDto?)null));

        var result = await tool.GetBookingAsync(999);

        result.ShouldContain("999");
    }

    [TestMethod]
    public async Task GetBookingAsync_AuthError_ReturnsReadableMessage()
    {
        var tool = CreateTool(mock => mock
            .Setup(u => u.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException("Unauthorized", null, HttpStatusCode.Unauthorized)));

        var result = await tool.GetBookingAsync(1);

        result.ShouldContain("Authentifizierung fehlgeschlagen");
    }

    // ── CreateBookingAsync ─────────────────────────────────────────────────────

    [TestMethod]
    public async Task CreateBookingAsync_ValidParams_ReturnsJson()
    {
        var booking = new BookingDto(55, 2, ["2026-05-10"], null, "pending", null, null, null, null);
        var tool = CreateTool(mock => mock
            .Setup(u => u.CreateAsync(
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(booking));

        var result = await tool.CreateBookingAsync(2, "{}", "[]");

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetProperty("id").GetInt32().ShouldBe(55);
        doc.RootElement.GetProperty("status").GetString().ShouldBe("pending");
    }

    [TestMethod]
    public async Task CreateBookingAsync_InvalidBookingType_ReturnsError()
    {
        var tool = CreateTool();

        var result = await tool.CreateBookingAsync(0, "{}", "[]");

        result.ShouldContain("Buchungstyp");
    }

    [TestMethod]
    public async Task CreateBookingAsync_InvalidFormDataJson_ReturnsError()
    {
        var tool = CreateTool();

        var result = await tool.CreateBookingAsync(1, "not-json", "[]");

        result.ShouldContain("formDataJson");
    }

    [TestMethod]
    public async Task CreateBookingAsync_InvalidDatesJson_ReturnsError()
    {
        var tool = CreateTool();

        var result = await tool.CreateBookingAsync(1, "{}", "not-json");

        result.ShouldContain("datesJson");
    }

    [TestMethod]
    public async Task CreateBookingAsync_AuthError_ReturnsReadableMessage()
    {
        var tool = CreateTool(mock => mock
            .Setup(u => u.CreateAsync(
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException("Unauthorized", null, HttpStatusCode.Unauthorized)));

        var result = await tool.CreateBookingAsync(2, "{}", "[]");

        result.ShouldContain("Authentifizierung fehlgeschlagen");
    }

    // ── UpdateBookingAsync ─────────────────────────────────────────────────────

    [TestMethod]
    public async Task UpdateBookingAsync_ValidParams_ReturnsJson()
    {
        var booking = new BookingDto(42, 2, ["2026-05-10"], null, "approved", null, null, null, null);
        var tool = CreateTool(mock => mock
            .Setup(u => u.UpdateAsync(
                It.IsAny<int>(), It.IsAny<string?>(), It.IsAny<int?>(), It.IsAny<string?>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(booking));

        var result = await tool.UpdateBookingAsync(42, status: "approved");

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetProperty("id").GetInt32().ShouldBe(42);
        doc.RootElement.GetProperty("status").GetString().ShouldBe("approved");
    }

    [TestMethod]
    public async Task UpdateBookingAsync_InvalidId_ReturnsError()
    {
        var tool = CreateTool();

        var result = await tool.UpdateBookingAsync(0);

        result.ShouldContain("ID");
    }

    [TestMethod]
    public async Task UpdateBookingAsync_InvalidFormDataJson_ReturnsError()
    {
        var tool = CreateTool();

        var result = await tool.UpdateBookingAsync(1, formDataJson: "not-json");

        result.ShouldContain("formDataJson");
    }

    [TestMethod]
    public async Task UpdateBookingAsync_NotFound_ReturnsError()
    {
        var tool = CreateTool(mock => mock
            .Setup(u => u.UpdateAsync(
                It.IsAny<int>(), It.IsAny<string?>(), It.IsAny<int?>(), It.IsAny<string?>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException("Not Found", null, HttpStatusCode.NotFound)));

        var result = await tool.UpdateBookingAsync(999);

        result.ShouldContain("999");
    }

    // ── DeleteBookingAsync ─────────────────────────────────────────────────────

    [TestMethod]
    public async Task DeleteBookingAsync_ValidId_ReturnsSuccessMessage()
    {
        var tool = CreateTool(mock => mock
            .Setup(u => u.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask));

        var result = await tool.DeleteBookingAsync(5);

        result.ShouldContain("5");
    }

    [TestMethod]
    public async Task DeleteBookingAsync_InvalidId_ReturnsError()
    {
        var tool = CreateTool();

        var result = await tool.DeleteBookingAsync(0);

        result.ShouldContain("ID");
    }

    [TestMethod]
    public async Task DeleteBookingAsync_NotFound_ReturnsError()
    {
        var tool = CreateTool(mock => mock
            .Setup(u => u.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException("Not Found", null, HttpStatusCode.NotFound)));

        var result = await tool.DeleteBookingAsync(999);

        result.ShouldContain("999");
    }

    [TestMethod]
    public async Task DeleteBookingAsync_AuthError_ReturnsReadableMessage()
    {
        var tool = CreateTool(mock => mock
            .Setup(u => u.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException("Unauthorized", null, HttpStatusCode.Unauthorized)));

        var result = await tool.DeleteBookingAsync(1);

        result.ShouldContain("Authentifizierung fehlgeschlagen");
    }

    // ── ApproveBookingAsync ────────────────────────────────────────────────────

    [TestMethod]
    public async Task ApproveBookingAsync_ValidId_ReturnsJson()
    {
        var booking = new BookingDto(1, 2, ["2026-05-01"], null, "approved", null, null, null, null);
        var tool = CreateTool(mock => mock
            .Setup(u => u.ApproveAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(booking));

        var result = await tool.ApproveBookingAsync(1);

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetProperty("id").GetInt32().ShouldBe(1);
        doc.RootElement.GetProperty("status").GetString().ShouldBe("approved");
    }

    [TestMethod]
    public async Task ApproveBookingAsync_InvalidId_ReturnsError()
    {
        var tool = CreateTool();

        var result = await tool.ApproveBookingAsync(0);

        result.ShouldContain("ID");
    }

    [TestMethod]
    public async Task ApproveBookingAsync_NotFound_ReturnsError()
    {
        var tool = CreateTool(mock => mock
            .Setup(u => u.ApproveAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException("Not Found", null, HttpStatusCode.NotFound)));

        var result = await tool.ApproveBookingAsync(999);

        result.ShouldContain("999");
    }

    // ── SetBookingPendingAsync ─────────────────────────────────────────────────

    [TestMethod]
    public async Task SetBookingPendingAsync_ValidId_ReturnsJson()
    {
        var booking = new BookingDto(1, 2, ["2026-05-01"], null, "pending", null, null, null, null);
        var tool = CreateTool(mock => mock
            .Setup(u => u.SetPendingAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(booking));

        var result = await tool.SetBookingPendingAsync(1);

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetProperty("id").GetInt32().ShouldBe(1);
        doc.RootElement.GetProperty("status").GetString().ShouldBe("pending");
    }

    [TestMethod]
    public async Task SetBookingPendingAsync_InvalidId_ReturnsError()
    {
        var tool = CreateTool();

        var result = await tool.SetBookingPendingAsync(0);

        result.ShouldContain("ID");
    }

    [TestMethod]
    public async Task SetBookingPendingAsync_NotFound_ReturnsError()
    {
        var tool = CreateTool(mock => mock
            .Setup(u => u.SetPendingAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException("Not Found", null, HttpStatusCode.NotFound)));

        var result = await tool.SetBookingPendingAsync(999);

        result.ShouldContain("999");
    }

    // ── UpdateBookingNoteAsync ─────────────────────────────────────────────────

    [TestMethod]
    public async Task UpdateBookingNoteAsync_ValidParams_ReturnsJson()
    {
        var booking = new BookingDto(1, 2, ["2026-05-10"], null, "approved", null, null, null, "Neue Notiz");
        var tool = CreateTool(mock => mock
            .Setup(u => u.UpdateNoteAsync(
                It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(booking));

        var result = await tool.UpdateBookingNoteAsync(1, "Neue Notiz");

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetProperty("note").GetString().ShouldBe("Neue Notiz");
    }

    [TestMethod]
    public async Task UpdateBookingNoteAsync_InvalidId_ReturnsError()
    {
        var tool = CreateTool();

        var result = await tool.UpdateBookingNoteAsync(0, "Notiz");

        result.ShouldContain("ID");
    }

    [TestMethod]
    public async Task UpdateBookingNoteAsync_EmptyNote_ReturnsError()
    {
        var tool = CreateTool();

        var result = await tool.UpdateBookingNoteAsync(1, "");

        result.ShouldContain("Notiz");
    }

    [TestMethod]
    public async Task UpdateBookingNoteAsync_NotFound_ReturnsError()
    {
        var tool = CreateTool(mock => mock
            .Setup(u => u.UpdateNoteAsync(
                It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new HttpRequestException("Not Found", null, HttpStatusCode.NotFound)));

        var result = await tool.UpdateBookingNoteAsync(999, "Notiz");

        result.ShouldContain("999");
    }
}
