// Copyright (c) 2026 RalfGuder. Licensed under the MIT License.
using McpBooking.Application.UseCases;
using McpBooking.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace McpBooking.Application.Tests.UseCases;

[TestClass]
public class DeleteBookingUseCaseTests
{
    [TestMethod]
    public async Task ExecuteAsync_DelegatesToRepository()
    {
        var mock = new Mock<IBookingRepository>();
        mock.Setup(r => r.DeleteAsync(5, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        var useCase = new DeleteBookingUseCase(mock.Object);

        await useCase.ExecuteAsync(5);

        mock.Verify(r => r.DeleteAsync(5, It.IsAny<CancellationToken>()), Times.Once);
    }
}
