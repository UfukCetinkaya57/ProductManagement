# Product Management API

This project is a Product Management API built using ASP.NET Core and follows the principles of Clean Architecture, CQRS, and SOLID. The API includes features such as CRUD operations for products, JWT-based authentication, role management, and logging with Serilog. It is designed to run in Docker containers using Docker Compose.

## Technologies and Libraries Used

- **ASP.NET Core**: The web framework used for building the API.
- **Entity Framework Core**: For data access and Code-First database modeling.
- **MediatR**: To implement the CQRS pattern.
- **AutoMapper**: For mapping between DTOs and domain models.
- **FluentValidation**: For input validation.
- **JWT**: For authentication and authorization.
- **Serilog**: For structured logging to various sinks (console, file, and SQL Server).
- **Autofac**: As the Dependency Injection container.
- **Docker & Docker Compose**: For containerization and orchestration.

## Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker](https://www.docker.com/get-started)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/yourusername/product-management-api.git
cd product-management-api
