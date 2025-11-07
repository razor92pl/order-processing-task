# Order Processing Console App


## Project Overview
Order Processing Console App is a small, modular, thread-safe order-processing console application in C# (.NET 8)
The solution demonstrates:
- Dependency Injection
- logging
- error handling
- thread safety


## ⚙️ Requirements
- .NET 8.0 or later


## 🚀 Usage
1️⃣ Clone the repository
```bash
    git clone https://github.com/razor92pl/order-processing-task
    cd order-processing-task
```
2️⃣ Restore dotnet dependencies
```bash
    dotnet restore
```
3️⃣ Build project
```bash
    dotnet build
```
4️⃣ Run the application
```bash
    dotnet run --project src/OrderProcessingConsoleApp
```
5️⃣ Run the unit tests
```bash
    dotnet test
```
👨‍💻 Of course, you can also open the solution using Visual Studio.


## Architecture Diagram (text)
```text
src/
 └── OrderProcessingConsoleApp/
      ├── Interfaces/
      │    ├── IOrderService.cs
      │    ├── IOrderRepository.cs
      │    ├── ILogger.cs
      │    ├── IOrderValidator.cs
      │    └── INotificationService.cs
      │
      ├── Models/
      │    └── Order.cs
      │
      ├── Infrastructure/
      │    ├── ConsoleLogger.cs
      │    ├── InMemoryOrderRepository.cs
      │    ├── NotificationService.cs
      │    └── OrderValidator.cs
      │
      ├── Services/
      │    └── OrderService.cs
      │
      ├── DIContainer.cs
      └── Program.cs

tests/
 └── OrderProcessingConsoleApp.Tests/
      └── OrderProcessingTests.cs
```

## Completed Bonus Tasks
```text
✅ Asynchronous Processing
✅ Add Order (CRUD)
✅ IOrderValidator
✅ Unit Tests
✅ Notification Service (was commented out in the online IDE to keep console results clean and easy to read)
```