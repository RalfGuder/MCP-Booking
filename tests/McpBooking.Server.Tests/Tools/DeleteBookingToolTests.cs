// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using System.Net;
using McpBooking.Application.UseCases;
using McpBooking.Domain.Interfaces;
using McpBooking.Server.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace McpBooking.Server.Tests.Tools;

[TestClass]
public class DeleteBookingToolTests
{
    private static DeleteBookingTool CreateToolWithMockUseCase(Exception? exception = null)
    {
        var useCaseMock = new Mock<DeleteBookingUseCase>(Mock.Of<IBookingRepository>());
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
                .Returns(Task.CompletedTask);
        }
        return new DeleteBookingTool(useCaseMock.Object);
    }

    [TestMethod]
    public async Task ExecuteAsync_ValidId_ReturnsSuccessMessage()
    {
        var tool = CreateToolWithMockUseCase();

        var result = await tool.ExecuteAsync(5);

        result.ShouldContain("5");
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

    [TestMethod]
    public async Task ExecuteAsync_AuthError_ReturnsReadableMessage()
    {
        var tool = CreateToolWithMockUseCase(
            exception: new HttpRequestException("Unauthorized", null, HttpStatusCode.Unauthorized));

        var result = await tool.ExecuteAsync(1);

        result.ShouldContain("Authentifizierung fehlgeschlagen");
    }
}
