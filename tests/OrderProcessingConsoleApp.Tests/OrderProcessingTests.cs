using Moq;
using OrderProcessingConsoleApp.Interfaces;
using OrderProcessingConsoleApp.Services;

namespace OrderProcessingConsoleApp.Tests;

public class OrderProcessingTests
{
    // Happy Path - should correct processing order
    [Fact]
    public async Task ProcessOrderAsync_Should_ProcessSuccessfully()
    {
        var mockRepo = new Mock<IOrderRepository>();
        var mockLogger = new Mock<ILogger>();
        var mockValidator = new Mock<IOrderValidator>();
        var mockNotification = new Mock<INotificationService>();

        int validOrderId = 1;

        mockValidator.Setup(v => v.IsValid(validOrderId)).Returns(true);
        mockRepo.Setup(r => r.GetOrderAsync(validOrderId)).ReturnsAsync("Laptop");

        var service = new OrderService(
            mockRepo.Object,
            mockLogger.Object,
            mockValidator.Object,
            mockNotification.Object
        );

        // Act
        await service.ProcessOrderAsync(validOrderId);

        // Verification
        mockRepo.Verify(r => r.GetOrderAsync(validOrderId), Times.Once);
        mockLogger.Verify(l => l.LogInfo(It.Is<string>(s => s.Contains("Successfully processed"))), Times.Once);
        mockNotification.Verify(n => n.Send(It.Is<string>(msg => msg.Contains("Laptop"))), Times.Once);
    }

    // Invalid Id Path
    [Fact]
    public async Task ProcessOrderAsync_ShouldLogValidationError()
    {
        var mockRepo = new Mock<IOrderRepository>();
        var mockLogger = new Mock<ILogger>();
        var mockValidator = new Mock<IOrderValidator>();
        var mockNotification = new Mock<INotificationService>();

        int invalidOrderId = -1;

        mockValidator.Setup(v => v.IsValid(invalidOrderId)).Returns(false);

        var service = new OrderService(
            mockRepo.Object,
            mockLogger.Object,
            mockValidator.Object,
            mockNotification.Object
        );

        // Act
        await service.ProcessOrderAsync(invalidOrderId);

        // Verification
        mockLogger.Verify(
            l => l.LogError(It.Is<string>(s => s.Contains("failed validation")), It.IsAny<ArgumentException>()),
            Times.Once
        );

        // check repo and notification - should be never call
        mockRepo.Verify(r => r.GetOrderAsync(It.IsAny<int>()), Times.Never);
        mockNotification.Verify(n => n.Send(It.IsAny<string>()), Times.Never);
    }

    // Not found test
    [Fact]
    public async Task ProcessOrderAsync_ShouldLogErrorWhenOrderNotFound()
    {
        var mockRepo = new Mock<IOrderRepository>();
        var mockLogger = new Mock<ILogger>();
        var mockValidator = new Mock<IOrderValidator>();
        var mockNotification = new Mock<INotificationService>();

        int notExistingOrderId = 100;

        mockValidator.Setup(v => v.IsValid(notExistingOrderId)).Returns(true);
        mockRepo.Setup(r => r.GetOrderAsync(notExistingOrderId))
                .ThrowsAsync(new KeyNotFoundException());

        var service = new OrderService(
            mockRepo.Object,
            mockLogger.Object,
            mockValidator.Object,
            mockNotification.Object
        );

        // Act
        await service.ProcessOrderAsync(notExistingOrderId);

        // Verification
        mockLogger.Verify(
            l => l.LogError(It.Is<string>(s => s.Contains("Failed to process")), It.IsAny<KeyNotFoundException>()),
            Times.Once
        );
        mockNotification.Verify(n => n.Send(It.IsAny<string>()), Times.Never);
    }

    // Already exist
    [Fact]
    public async Task ProcessOrderAsync_ShouldLogError_WhenOrderAlreadyExists()
    {
        var mockRepo = new Mock<IOrderRepository>();
        var mockLogger = new Mock<ILogger>();
        var mockValidator = new Mock<IOrderValidator>();
        var mockNotification = new Mock<INotificationService>();

        int duplicateOrderId = 1;

        mockValidator.Setup(v => v.IsValid(duplicateOrderId)).Returns(true);
        mockRepo.Setup(r => r.GetOrderAsync(duplicateOrderId))
                .ThrowsAsync(new InvalidOperationException("Order already exists"));

        var service = new OrderService(
            mockRepo.Object,
            mockLogger.Object,
            mockValidator.Object,
            mockNotification.Object
        );

        // Act
        await service.ProcessOrderAsync(duplicateOrderId);

        // Verification
        mockLogger.Verify(
            l => l.LogError(It.Is<string>(s => s.Contains("Failed to process")), It.IsAny<InvalidOperationException>()),
            Times.Once
        );
        mockNotification.Verify(n => n.Send(It.IsAny<string>()), Times.Never);
    }

}