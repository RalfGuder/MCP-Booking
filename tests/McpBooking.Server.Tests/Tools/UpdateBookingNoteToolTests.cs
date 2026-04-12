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
public class UpdateBookingNoteToolTests
{
    private static UpdateBookingNoteTool CreateToolWithMockUseCase(
        BookingDto? result = null, Exception? exception = null)
    {
        var useCaseMock = new Mock<UpdateBookingNoteUseCase>(Mock.Of<IBookingRepository>());
        if (exception != null)
        {
            useCaseMock.Setup(u => u.ExecuteAsync(
                    It.IsAny<int>(), It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);
        }
        else
        {
            useCaseMock.Setup(u => u.ExecuteAsync(
                    It.IsAny<int>(), It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(result!);
        }
        return new UpdateBookingNoteTool(useCaseMock.Object);
    }

    [TestMethod]
    public async Task ExecuteAsync_ValidParams_ReturnsJson()
    {
        var booking = new BookingDto(1, 2, ["2026-05-10"], null, "approved", null, null, null, "Neue Notiz");
        var tool = CreateToolWithMockUseCase(booking);

        var result = await tool.ExecuteAsync(1, "Neue Notiz");

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetProperty("note").GetString().ShouldBe("Neue Notiz");
    }

    [TestMethod]
    public async Task ExecuteAsync_InvalidId_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(0, "Notiz");

        result.ShouldContain("ID");
    }

    [TestMethod]
    public async Task ExecuteAsync_EmptyNote_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(1, "");

        result.ShouldContain("Notiz");
    }

    [TestMethod]
    public async Task ExecuteAsync_NotFound_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase(
            exception: new HttpRequestException("Not Found", null, HttpStatusCode.NotFound));

        var result = await tool.ExecuteAsync(999, "Notiz");

        result.ShouldContain("999");
    }
}
