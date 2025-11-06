namespace OrderProcessingConsoleApp.Interfaces;

/// <summary>
/// Contract for processing orders asynchronously.
/// </summary>
public interface IOrderService
{
    Task ProcessOrderAsync(int orderId);
}