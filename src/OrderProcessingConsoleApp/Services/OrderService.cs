using OrderProcessingConsoleApp.Interfaces;

namespace OrderProcessingConsoleApp.Services;

/// <summary>
/// Business service responsible for orchestrating order processing.
/// </summary>
public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly ILogger _logger;
    private readonly IOrderValidator _validator;
    private readonly INotificationService _notificationService;

    // Constructor with interfaces injection
    public OrderService(IOrderRepository repository, ILogger logger, IOrderValidator validator, INotificationService notificationService)
    {
        _repository = repository;
        _logger = logger;
        _validator = validator;
        _notificationService = notificationService;
    }
    // Async method for processing orders
    public async Task ProcessOrderAsync(int orderId)
    {
        // OrderId validation before repository call (must be greater than 0)
        if (!_validator.IsValid(orderId))
        {
            _logger.LogError($"Order {orderId} failed validation.", new ArgumentException());
            return;
        }
        // Starting processing order log with OrderId
        _logger.LogInfo($"Start processing order {orderId}");

        try
        {
            var description = await _repository.GetOrderAsync(orderId);
            _logger.LogInfo($"Successfully processed order {orderId}: {description}");
            // End successed process notification 
            _notificationService.Send($"Order {orderId} processed: {description}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to process order {orderId}", ex);
        }
    }
}