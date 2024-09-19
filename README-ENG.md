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

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/yourusername/product-management-api.git
cd product-management-api

2. Build and Run the Application with Docker
Build and run the application using Docker Compose:

docker-compose up --build

```
## This command will:

Build the API and create a Docker image.
Start containers for the API and SQL Server.
Automatically apply database migrations.
### 3. Access the API
Once the application is running, you can access the API at:

Swagger UI: http://localhost:5000/swagger
API Base URL: http://localhost:5000/api

## How to Use the API
Authentication
Register or log in to obtain a JWT token.
Include the token in the Authorization header of your requests:
Authorization: Bearer your_jwt_token
## Endpoints
Products
GET /api/products: Retrieve all products.
GET /api/products/{id}: Retrieve a specific product.
POST /api/products: Create a new product.
PUT /api/products/{id}: Update an existing product.
DELETE /api/products/{id}: Delete a product (Admin only).
## Project Structure
ProductManagement.API: Contains the API controllers and configuration.
ProductManagement.Application: Contains application logic, services, and CQRS handlers.
ProductManagement.Core: Contains the core domain entities and interfaces.
ProductManagement.Infrastructure: Contains implementations of services and repositories.
ProductManagement.Persistence: Contains database context and migrations.
Logging
The API uses Serilog for structured logging. Logs are stored in the following sinks:

Console: Logs are displayed in the console.
File: Logs are written to logs/log.txt.
SQL Server: Logs are stored in the Logs table of the database.
Additional Notes
Ensure that the SQL Server is accessible and running.
Customize the Docker Compose file (docker-compose.yml) as needed for your environment.
