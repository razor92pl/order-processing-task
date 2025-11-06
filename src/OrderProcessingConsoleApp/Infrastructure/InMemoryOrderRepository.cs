using System.Collections.Concurrent;
using OrderProcessingConsoleApp.Interfaces;
using OrderProcessingConsoleApp.Models;

namespace OrderProcessingConsoleApp.Infrastructure;

/// <summary>
/// In-memory implementation of the order repository using ConcurrentDictionary.
/// </summary>
public class InMemoryOrderRepository : IOrderRepository
{
    // ConcurrentDictionary private variable to ensure thread safety
    private readonly ConcurrentDictionary<int, Order> _orders = new();

    // Add samples Orders to in-memory repository in constructor
    public InMemoryOrderRepository()
    {
        _orders.TryAdd(1, new Order { Id = 1, Description = "Laptop" });
        _orders.TryAdd(2, new Order { Id = 2, Description = "Phone" });
        _orders.TryAdd(3, new Order { Id = 3, Description = "TV" });
    }

    // GetOrderAsync method from in-memory repository based on order id - simulate delay for async programming
    public async Task<string> GetOrderAsync(int orderId)
    {
        if (orderId <= 0)
            throw new ArgumentException("Order Id must be greater than 0!");

        //Delay simulation
        await Task.Delay(100);

        if (!_orders.TryGetValue(orderId, out var order))
            throw new KeyNotFoundException($"Order with Id {orderId} was not found!");

        return order.Description;
    }

    // AddOrder method with reject duplicates
    public void AddOrder(Order order)
    {
        if (order is null)
            throw new ArgumentNullException(nameof(order), "Order cannot be null.");

        if (order.Id <= 0)
            throw new ArgumentException("Order Id must be greater than 0!", nameof(order));

        if (string.IsNullOrWhiteSpace(order.Description))
            throw new ArgumentException("Order Description cannot be empty.", nameof(order));

        var added = _orders.TryAdd(order.Id, order);

        // Reject duplicate
        if (!added)
            throw new InvalidOperationException($"Order with Id {order.Id} already exist!");
    }
}
