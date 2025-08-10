# Settlement Manager

# Overview
The Settlement Manager is a Blazor application designed to manage settlements with CRUD operations. <br>
It supports paginated views of settlements, modal-based add, edit, and delete functionality, and integrates with an ASP.NET Core Web API for backend operations. <br>
This application uses clean architecture principles with domain driven design and the use of Entity Framework Core, MediatR for CQRS, and asynchronous operations.

![image alt](https://github.com/AlanEmersic/SettlementManager/blob/02d08ff70fd104ad8a316ed181a23155b6c39199/images/image.png)

# Project Structure
```bash
SettlementManager
├── src
│   ├── SettlementManager.Web                # Blazor Web project
│   ├── SettlementManager.Application        # Application logic, CQRS handlers
│   ├── SettlementManager.Domain             # Domain models and interfaces
│   ├── SettlementManager.Infrastructure     # Infrastructure for database and repositories
│   └── SettlementManager.Api                # ASP.NET Core Web API
└── README.md
```

# Setup Instructions:

## 1. Open project
open SettlementManager.sln file in editor

## 2. Build the Project
```bash
dotnet build
```

## 3. Database Setup
- Ensure SQL Server is running.
- [Optional] Update the connection string in <b>appsettings.Development.json</b> in the <b>SettlementManager.Api</b> project.

## 4. Run the Application
- To start both the API and Blazor application, set both projects (SettlementManager.Web and SettlementManager.Api) as startup projects in Rider or Visual Studio.
- Alternatively, you can start them using the following commands:
```bash
# In one terminal
cd src/SettlementManager.Api
dotnet run
# open https://localhost:7288/swagger/index.html

# In another terminal
cd src/SettlementManager.Web
dotnet run
# open https://localhost:7254
```

## 5. Migrations
Migrations run on SettlementManager.Api startup (DatabaseInitializer.cs)

## 6. Seed data
Run:
1. Countries.sql (194 rows)
2. Settlements.sql (1000 rows)

# Features
- Add, edit, and delete settlements using modal dialogs
- View settlements with paginated tables
- Search settlements by name

# Project Components
## SettlementManager.Web
    Blazor WebAssembly/Server project for managing settlements through a UI.
    Uses Blazor components for displaying tables, modals, and pagination.

## SettlementManager.Application
    Contains application logic including commands, queries, and services.
    Implements the CQRS pattern with MediatR.

## SettlementManager.Domain
    Contains domain models and core interfaces.
    Represents the core business logic and rules.

## SettlementManager.Infrastructure
    Provides database context, repositories, and persistence logic.
    Uses Entity Framework Core.

## SettlementManager.Api
    ASP.NET Core Web API project that provides RESTful endpoints for settlements.
    Handles requests from the Blazor frontend.