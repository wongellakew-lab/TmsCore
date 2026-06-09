# TMS Core: Training Management System (Module 1)

This repository contains the backend core logic for the Training Management System (TMS). The project focus on fixing critical floating point errors, null reference crashes, and thread starvation using modern C# 14 and .NET 10 architectural patterns. It also focuses on transitioning from a bug prone legacy system to a production grade, and asynchronous architecture.

## 🌿 Branching Strategy
The code for each session is organized into separate branches. Switch branches to see the implementation for each milestone:
- **`main`**: Initial project setup and documentation.
- **`module-1-session-1`**: Data modeling and precision.
- **`module-1-session-2`**: Logic, classification, and LINQ.
- **`module-1-session-3`**: Async, Custom Exception, and final integration.

---

## 📖 Session Summaries

### Session 1: The Data Model
*   **Null safety**: Implementation of `string?`, `?.`, `??`, and `??=` operators to eliminate `NullReferenceExceptions`.
*   **Financial precision**: Using `decimal` to solve floating point "drift" in currency calculations.
*   **Immutable data**: Using **record types** to ensure data integrity across the pipeline.
*   **C# 14 field keyword**: Using the new `field` keyword for concise, validated properties.
*   **Domain models**: Implementation of `Student`, `Course`, `EnrollmentRecord`, and the `IGradable` interface.

### Session 2: Query and Classification
*   **Guard clauses**: Refactoring nested logic (the "Pyramid of Doom") into flat, readable preconditions.
*   **Switch expressions**: Using modern pattern matching for academic standing and business logic.
*   **LINQ**: Declarative data transformations including filtering, sorting, grouping, and aggregation.
*   **Modern collection expressions**: Utilizing C# 12+ bracket syntax `[]` and the spread operator `..`.

### Session 3: Async and Resilience
*   **Async/Await**: Transitioning from blocking `.Result` calls to non-blocking tasks to prevent thread starvation.
*   **Parallel loading**: Using `Task.WhenAll` to fetch data concurrently, reducing execution time significantly.
*   **Custom domain exceptions**: Implementation of `CapacityReachedException` and `TmsDatabaseException` for better error context.
*   **Safe fire and forget**: Using try/catch patterns for background notifications.
*   **Full enrollment summary**: A comprehensive integration report summarizing the entire processing run.
*   **Decoupled Events (Optional)**: Using **Delegates** and **Lambdas** to handle side effects (like SMS/Logging).


---

## 📂 Project Structure
```text
├──TmsCore/
│    ├── Program.cs              # Main entry point + demos
│    ├── Models.cs               # All domain models
│    ├── Exception.cs            # Custome exceptions
│    ├── EnrollmentService.cs    # Business logic
│    ├── TmsCore.csproj          # Project configuration
│    └── README.md               # Documentation
│
└──TmsCore.Tests/          # Test Suite (Created in Activity 1)
    ├── Program.cs
    └── TmsCore.Tests.csproj
```
## 🛠️ Requirements
- .NET 10 SDK (Verify via dotnet --version: must show 10.x)
- Nullable Reference Types enabled in the project file (<Nullable>enable</Nullable>)
- Recommended: Visual Studio Code + C# Dev Kit extension

## 🚀 Getting Started
1. Clone the repository
```text
git clone https://github.com/YOUR_USERNAME/TmsCore.git
cd TmsCore
```
2. Switch to a Session branch
This repository uses branches to track progress. To view the completed code for a specific session (e.g., Session 3):
```text 
git checkout module-1-session-3
```
3. Run the project
```text
dotnet run
```

