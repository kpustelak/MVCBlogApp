# MVCBlogApp

A simple blog application built with ASP.NET Core MVC, featuring an admin panel, post publishing, categories, and image uploads.

## Features

- public post listing and details view
- post filtering by category, search, and sorting
- admin panel (login)
- create, edit, and delete posts
- category management
- image upload and management
- error logging (Serilog)

## Tech Stack

- .NET 8 (`ASP.NET Core MVC`)
- Entity Framework Core
- ASP.NET Core Identity
- AutoMapper
- Serilog
- SQLite (development) / PostgreSQL (production)

## Project Structure

- `MVCBlogApp/Controllers` - MVC controllers
- `MVCBlogApp/Service` - business logic
- `MVCBlogApp/Db` - database context and seeder
- `MVCBlogApp/Models` - entities, DTOs, and view models
- `MVCBlogApp/Views` - Razor views
- `MVCBlogApp/wwwroot` - static files

## Requirements

- .NET SDK 8.0+

## Local Run

1. Go to the project directory:

```powershell
cd MVCBlogApp
```

2. Restore packages and build:

```powershell
dotnet restore
dotnet build
```

3. Run the application:

```powershell
dotnet run
```

In development mode, the app uses a local `blog.db` SQLite database.

## Admin Login

Default credentials are configured in `appsettings.Development.json` (`AdminUser`):

- email: `admin@blog.pl`
- password: `BardzoSilneHaslo123!@#LongerThan20`

On startup, the seeder creates the admin account and synchronizes the password from configuration for an existing admin user.

## Environment Configuration

- development: SQLite
- production: PostgreSQL (connection string `DefaultConnection`)

Configuration files:

- `MVCBlogApp/appsettings.json`
- `MVCBlogApp/appsettings.Development.json`

## Useful Commands

```powershell
dotnet build MVCBlogApp.sln
dotnet run --project MVCBlogApp/MVCBlogApp.csproj
```

