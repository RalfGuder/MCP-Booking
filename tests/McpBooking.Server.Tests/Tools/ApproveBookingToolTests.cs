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
public class ApproveBookingToolTests
{
    private static ApproveBookingTool CreateToolWithMockUseCase(
        BookingDto? result = null, Exception? exception = null)
    {
        var useCaseMock = new Mock<ApproveBookingUseCase>(Mock.Of<IBookingRepository>());
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
                .ReturnsAsync(result!);
        }
        return new ApproveBookingTool(useCaseMock.Object);
    }

    [TestMethod]
    public async Task ExecuteAsync_ValidId_ReturnsJson()
    {
        var booking = new BookingDto(1, 2, ["2026-05-01"], null, "approved", null, null, null, null);
        var tool = CreateToolWithMockUseCase(booking);

        var result = await tool.ExecuteAsync(1);

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetProperty("id").GetInt32().ShouldBe(1);
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
        var tool = CreateToolWithMockUseCase(
            exception: new HttpRequestException("Not Found", null, HttpStatusCode.NotFound));

        var result = await tool.ExecuteAsync(999);

        result.ShouldContain("999");
    }
}
