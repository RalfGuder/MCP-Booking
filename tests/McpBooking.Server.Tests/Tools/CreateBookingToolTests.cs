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
public class CreateBookingToolTests
{
    private static CreateBookingTool CreateToolWithMockUseCase(
        BookingDto? result = null, Exception? exception = null)
    {
        var useCaseMock = new Mock<CreateBookingUseCase>(Mock.Of<IBookingRepository>());
        if (exception != null)
        {
            useCaseMock.Setup(u => u.ExecuteAsync(
                    It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);
        }
        else
        {
            useCaseMock.Setup(u => u.ExecuteAsync(
                    It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(result!);
        }
        return new CreateBookingTool(useCaseMock.Object);
    }

    [TestMethod]
    public async Task ExecuteAsync_ValidParams_ReturnsJson()
    {
        var booking = new BookingDto(55, 2, ["2026-05-10"], null, "pending", null, null, null, null);
        var tool = CreateToolWithMockUseCase(booking);

        var result = await tool.ExecuteAsync(2, "{}", "[]");

        var doc = JsonDocument.Parse(result);
        doc.RootElement.GetProperty("id").GetInt32().ShouldBe(55);
        doc.RootElement.GetProperty("status").GetString().ShouldBe("pending");
    }

    [TestMethod]
    public async Task ExecuteAsync_InvalidBookingType_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(0, "{}", "[]");

        result.ShouldContain("Buchungstyp");
    }

    [TestMethod]
    public async Task ExecuteAsync_InvalidFormDataJson_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(1, "not-json", "[]");

        result.ShouldContain("formDataJson");
    }

    [TestMethod]
    public async Task ExecuteAsync_InvalidDatesJson_ReturnsError()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(1, "{}", "not-json");

        result.ShouldContain("datesJson");
    }

    [TestMethod]
    public async Task ExecuteAsync_AuthError_ReturnsReadableMessage()
    {
        var tool = CreateToolWithMockUseCase(
            exception: new HttpRequestException("Unauthorized", null, HttpStatusCode.Unauthorized));

        var result = await tool.ExecuteAsync(2, "{}", "[]");

        result.ShouldContain("Authentifizierung fehlgeschlagen");
    }
}
