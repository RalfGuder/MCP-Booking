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
public class ListBookingsToolTests
{
    private static ListBookingsTool CreateToolWithMockUseCase(
        IReadOnlyList<BookingDto>? result = null, Exception? exception = null)
    {
        var useCaseMock = new Mock<ListBookingsUseCase>(Mock.Of<IBookingRepository>());
        if (exception != null)
        {
            useCaseMock.Setup(u => u.ExecuteAsync(
                    It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<int?>(), It.IsAny<string?>(),
                    It.IsAny<string?>(), It.IsAny<string?>(),
                    It.IsAny<bool?>(), It.IsAny<string?>(),
                    It.IsAny<string?>(), It.IsAny<string?>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);
        }
        else
        {
            useCaseMock.Setup(u => u.ExecuteAsync(
                    It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<int?>(), It.IsAny<string?>(),
                    It.IsAny<string?>(), It.IsAny<string?>(),
                    It.IsAny<bool?>(), It.IsAny<string?>(),
                    It.IsAny<string?>(), It.IsAny<string?>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(result ?? new List<BookingDto>());
        }
        return new ListBookingsTool(useCaseMock.Object);
    }

    [TestMethod]
    public async Task ExecuteAsync_ValidParams_ReturnsJson()
    {
        var bookings = new List<BookingDto>
        {
            new(1, 2, ["2026-05-01"], null, "approved", null, null, null, null)
        };
        var tool = CreateToolWithMockUseCase(bookings);

        var result = await tool.ExecuteAsync();

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetArrayLength().ShouldBe(1);
        doc.RootElement[0].GetProperty("id").GetInt32().ShouldBe(1);
        doc.RootElement[0].GetProperty("status").GetString().ShouldBe("approved");
    }

    [TestMethod]
    public async Task ExecuteAsync_InvalidPage_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(page: 0);

        result.ShouldContain("page muss >= 1 sein");
    }

    [TestMethod]
    public async Task ExecuteAsync_InvalidPerPage_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(perPage: 101);

        result.ShouldContain("per_page muss zwischen 1 und 100 liegen");
    }

    [TestMethod]
    public async Task ExecuteAsync_AuthError_ReturnsReadableMessage()
    {
        var tool = CreateToolWithMockUseCase(
            exception: new HttpRequestException("Unauthorized", null, HttpStatusCode.Unauthorized));

        var result = await tool.ExecuteAsync();

        result.ShouldContain("Authentifizierung fehlgeschlagen");
    }
}
