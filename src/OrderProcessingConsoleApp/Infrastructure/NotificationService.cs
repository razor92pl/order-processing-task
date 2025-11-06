using OrderProcessingConsoleApp.Interfaces;

namespace OrderProcessingConsoleApp.Infrastructure;

/// <summary>
/// Notification service.
/// </summary>
public class NotificationService : INotificationService
{
    public void Send(string message)
    {

        Console.WriteLine($"NOTIFICATION: {message}");
    }
}