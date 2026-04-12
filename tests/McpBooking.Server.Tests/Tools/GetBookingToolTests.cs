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
public class GetBookingToolTests
{
    private static GetBookingTool CreateToolWithMockUseCase(
        BookingDto? result = null, Exception? exception = null)
    {
        var useCaseMock = new Mock<GetBookingUseCase>(Mock.Of<IBookingRepository>());
        if (exception != null)
        {
            useCaseMock.Setup(u => u.ExecuteAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);
        }
        else
        {
            useCaseMock.Setup(u => u.ExecuteAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);
        }
        return new GetBookingTool(useCaseMock.Object);
    }

    [TestMethod]
    public async Task ExecuteAsync_ValidId_ReturnsJson()
    {
        var booking = new BookingDto(42, 2, ["2026-05-01"], null, "approved", null, null, null, null);
        var tool = CreateToolWithMockUseCase(booking);

        var result = await tool.ExecuteAsync(42);

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetProperty("id").GetInt32().ShouldBe(42);
        doc.RootElement.GetProperty("status").GetString().ShouldBe("approved");
    }

    [TestMethod]
    public async Task ExecuteAsync_InvalidId_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(0);

        result.ShouldContain("ID");
    }

    [TestMethod]
    public async Task ExecuteAsync_NotFound_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase(result: null);

        var result = await tool.ExecuteAsync(999);

        result.ShouldContain("999");
    }

    [TestMethod]
    public async Task ExecuteAsync_AuthError_ReturnsReadableMessage()
    {
        var tool = CreateToolWithMockUseCase(
            exception: new HttpRequestException("Unauthorized", null, HttpStatusCode.Unauthorized));

        var result = await tool.ExecuteAsync(1);

        result.ShouldContain("Authentifizierung fehlgeschlagen");
    }
}
