# MoleculesApi
Api for the molecules project, this API provides the following functionalities:

- Manage Calculation Orders
- Get Molecular Reports ( To be completed )


# Logging

Logging is developed using the [Serilog](https://serilog.net/) library.
We use [Serilog.Extensions.Logging](https://github.com/serilog/serilog-extensions-logging) library


# Database Migrations

We use entity framework core migrations to manage the database schema.

## common commands

- dotnet ef migrations add <migration_name>  --startup-project ../MoleculesWebApp/MoleculesWebApp
- dotnet ef migrations list --startup-project ../MoleculesWebApp/MoleculesWebApp
- dotnet ef migrations script --startup-project ../MoleculesWebApp/MoleculesWebApp --idempotent --output molecules.sql

for a reference of all commands : [Entity Framework Core tools reference - .NET Core CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)


# References
- **How to write markdown** :
	- [Medium Article] (https://rasha-abdulrazzak.medium.com/how-to-write-a-readme-md-file-for-your-project-82ffd02c4d9b)
	- [Github Article] (https://docs.github.com/en/github/writing-on-github/basic-writing-and-formatting-syntax)
	- [Guide] (https://guides.github.com/features/mastering-markdown/)

- **Logging in ASP.NET Code** :
	- [Medium Article] (https://medium.com/@mehmetozkaya/logging-in-asp-net-core-2-0-6c2d6a19a76d)
	- [Microsoft Article] (https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-5.0)


https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
