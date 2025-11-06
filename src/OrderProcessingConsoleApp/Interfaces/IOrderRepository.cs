using OrderProcessingConsoleApp.Models;

namespace OrderProcessingConsoleApp.Interfaces;

/// <summary>
/// Contract for accessing and adding orders.
/// </summary>
public interface IOrderRepository
{
    Task<string> GetOrderAsync(int orderId);
    void AddOrder(Order order);
}