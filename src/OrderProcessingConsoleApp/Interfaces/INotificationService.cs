namespace OrderProcessingConsoleApp.Interfaces;

/// <summary>
/// Contract for sending notifications.
/// </summary>
public interface INotificationService
{
    void Send(string message);
}