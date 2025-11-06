using OrderProcessingConsoleApp.Interfaces;

namespace OrderProcessingConsoleApp.Infrastructure;

/// <summary>
/// Console-based logger implementation for logging events
/// </summary>
public class ConsoleLogger : ILogger
{
    // lock for ensure that logs will be thread-safe
    private readonly object _lock = new object();

    public void LogInfo(string message)
    {
        lock (_lock)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [INFO] {message}");
        }
    }

    // LogError method with exception message
    public void LogError(string message, Exception ex)
    {
        lock (_lock)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [ERROR] {message} | Exception: {ex.Message}");
        }
    }
}