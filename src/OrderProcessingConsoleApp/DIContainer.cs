using OrderProcessingConsoleApp.Infrastructure;
using OrderProcessingConsoleApp.Interfaces;
using OrderProcessingConsoleApp.Services;

/// <summary>
/// Simple manual dependency injection container via static class
/// </summary>
public static class DIContainer
{
    public static (IOrderService, IOrderRepository, ILogger) CreateServices()
    {
        IOrderRepository repository = new InMemoryOrderRepository();
        ILogger logger = new ConsoleLogger();
        IOrderValidator validator = new OrderValidator();
        INotificationService notificationService = new NotificationService();
        IOrderService service = new OrderService(repository, logger, validator, notificationService);

        return (service, repository, logger); // we need three values, included repository, for AddOrder method
    }
}