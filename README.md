# Video Game Catalog API

This is the backend for the Video Game Catalog project. It's a clean, straightforward ASP.NET Core Web API built to manage a catalog of video games, along with their genres and platforms.

I built this using a layered architecture (Core, Infrastructure, Api) to keep the domain logic separate from the database stuff. The frontend is built with Angular and talks to this API.

## Tech Stack
* **Framework:** .NET 10 (ASP.NET Core Web API)
* **Database:** SQL Server Express / LocalDB
* **ORM:** Entity Framework Core (Code-First)
* **Testing:** xUnit + Moq for mocking dependencies
* **Logging:** NLog for writing global errors to disk

## Project Structure

```text
VideoGameCatalog/
├── VideoGameCatalog.Api/            – Controllers, Program.cs, Middleware
├── VideoGameCatalog.Core/           – Entities, DTOs, Interfaces, Exceptions
├── VideoGameCatalog.Infrastructure/ – EF Core DbContext, migrations, Repositories/Services
└── VideoGameCatalog.Tests/          – xUnit unit tests
```

* `VideoGameCatalog.Core`: The heart of the app. Contains the entities (`VideoGame`, `Genre`, `Platform`), DTOs, interfaces, and custom domain exceptions. Zero dependencies on EF Core or web frameworks here.
* `VideoGameCatalog.Infrastructure`: Where the database logic lives. It holds the `AppDbContext`, EF Core migrations, and the concrete implementation of the services.
* `VideoGameCatalog.Api`: The presentation layer. Sets up dependency injection, controllers, CORS, global exception middleware, and Scalar API UI.
* `VideoGameCatalog.Tests`: Unit test suite testing the controller logic by mocking the service layer.

## Getting Started

### Prerequisites
* .NET 10 SDK
* SQL Server Express or LocalDB (the default connection string uses `(localdb)\mssqllocaldb`)

### Running the API locally

1. Open the solution in Visual Studio or your preferred IDE.
2. In the `appsettings.json`, make sure the `DefaultConnection` string points to your local SQL Server instance.
3. Open the Package Manager Console or a terminal in the `VideoGameCatalog.Infrastructure` folder and run the migrations to create the database:
   ```bash
   dotnet ef database update -p ../VideoGameCatalog.Infrastructure -s ../VideoGameCatalog.Api
   ```
   *Note: This will also run the `SeedData` migration which populates the DB with some sample genres, platforms, and 10 popular games so you don't start with an empty screen.*
4. Hit F5 or run the API project:
   ```bash
   dotnet run --project VideoGameCatalog/VideoGameCatalog.Api
   ```
5. Head to `https://localhost:44371/scalar/v1` in your browser. Since .NET 10 removed Swashbuckle, I installed the new `Scalar.AspNetCore` package to generate a beautiful, interactive REST API dashboard where you can easily test all the GET/POST/PUT/DELETE endpoints!

## Key Features
* **Pagination & Filtering:** The `GET /api/games` endpoint supports server-side pagination, searching by title, and sorting by various columns.
* **Global Error Handling:** I didn't want to sprinkle `try/catch` blocks everywhere, so I set up the .NET `IExceptionHandler` middleware. If a controller throws a `NotFoundException` (like trying to update a game that doesn't exist), the middleware catches it and returns a clean 404 response. If it's a 500 server crash, it returns a generic safe message to the client but logs the full stack trace to the `logs/` folder using NLog.
* **Fluent API Configuration:** Instead of cluttering the models with data annotations, all the database constraints (like max lengths, foreign key behaviors, and composite indexes) are configured using the Fluent API in the DbContext.

## License
Do whatever you want with this codebase!
