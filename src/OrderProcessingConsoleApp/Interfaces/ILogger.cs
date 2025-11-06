namespace OrderProcessingConsoleApp.Interfaces;

/// <summary>
/// Contract for logging application events and errors.
/// </summary>
public interface ILogger
{
    void LogInfo(string message);
    void LogError(string message, Exception ex);
}