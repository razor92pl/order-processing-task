using OrderProcessingConsoleApp.Models;

Console.WriteLine("Order Processing System");

// Initialize DI container, services, and repository
var (service, repository, logger) = DIContainer.CreateServices();

// Demonstration AddOrder function (one example with duplicate)
var addingTasks = new List<Task>
{
    Task.Run(() =>
    {
        try
        {
            repository.AddOrder(new Order { Id = 4, Description = "Microphone" });
            logger.LogInfo("AddOrder: Order 4 added successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError("AddOrder failed for Id=4", ex);
        }
    }),
    Task.Run(() =>
    {
        try
        {
            repository.AddOrder(new Order { Id = 3, Description = "Test" }); // duplikat
            logger.LogInfo("AddOrder: Order 3 added successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError("AddOrder failed for Id=3", ex);
        }
    })
};

await Task.WhenAll(addingTasks);

// Demonstration multi-threaded order processing
Task[] processingTasks = new Task[4];
processingTasks[0] = service.ProcessOrderAsync(1);
processingTasks[1] = service.ProcessOrderAsync(2);
processingTasks[2] = service.ProcessOrderAsync(-1);
processingTasks[3] = service.ProcessOrderAsync(4); // processing added Order

await Task.WhenAll(processingTasks);

Console.WriteLine("Processing complete.");
