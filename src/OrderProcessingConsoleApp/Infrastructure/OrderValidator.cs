using OrderProcessingConsoleApp.Interfaces;

namespace OrderProcessingConsoleApp.Infrastructure;

/// <summary>
/// Order validator that checks order ID is greater than zero.
/// </summary>
public class OrderValidator : IOrderValidator
{
    public bool IsValid(int orderId)
    {
        // Check orderId - must be greater than 0
        return (orderId > 0);
    }
}
