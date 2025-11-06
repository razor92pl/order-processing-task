namespace OrderProcessingConsoleApp.Interfaces;

/// <summary>
/// Contract for validating orders before processing.
/// </summary>
public interface IOrderValidator
{
    bool IsValid(int orderId);
}