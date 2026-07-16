# Clinic API - Setup Guide

Welcome to the **Clinic API**, an ASP.NET Core Web API built with **.NET 8**, **Entity Framework Core**, **SQLite**, and **JWT Authentication**.

This guide will help you set up and run the project on your local machine.

---

# Prerequisites

Before running the project, make sure you have the following installed:

* Visual Studio 2022 (Community or later)
* .NET 8 SDK
* Git (optional, for cloning the repository)
* SQLite (optional, the database will be created automatically)

---

# Clone the Repository

```bash
git clone https://github.com/yourusername/ClinicAPI.git
```

or download the ZIP file and extract it.

---

# Open the Solution

Open the solution file:

```
ClinicAPI.sln
```

using **Visual Studio 2022**.

---

# Restore NuGet Packages

Visual Studio usually restores packages automatically.

If not, open the Package Manager Console or terminal and run:

```bash
dotnet restore
```

---

# Configure the Database

The project uses **SQLite**.

Open:

```
appsettings.json
```

Verify the connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=hospital.db"
  }
}
```

---

# Apply Database Migrations

Open a terminal inside the project folder and execute:

```bash
dotnet ef database update
```

This will create:

* clinic.db
* All required tables

---

# (Optional) Create New Migrations

If you changed the models:

```bash
dotnet ef migrations add MigrationName
```

Then update the database:

```bash
dotnet ef database update
```

---

# Configure JWT

Open:

```
appsettings.json
```

Example configuration:

```json
"Jwt": {
  "Key": "THIS_IS_A_SUPER_SECRET_KEY_CHANGE_ME",
  "Issuer": "ClinicAPI",
  "Audience": "CPMRS-Web",
  "ExpiryMinutes": 60
}
```

Replace the secret key before deploying to production.

---

# Create an Admin User

The API requires a registered user before logging in.

If the project includes a seed method, run the application once and it will create the default administrator account.

Otherwise, create a user through the Register endpoint.

Example:

```
Username: admin
Password: admin123
```

---

# Run the Project

Press

```
F5
```

or

```
Ctrl + F5
```

or run:

```bash
dotnet run
```

The API will start on something similar to:

```
https://localhost:7078
```

or

```
http://localhost:5207
```

The port may vary depending on your local environment.

---

# Swagger Documentation

Once the application is running, open:

```
https://localhost:7078/swagger
```

or

```
http://localhost:5207/swagger
```

Swagger provides interactive documentation where you can test every endpoint.

---

# Authentication

1. Register a user (if needed)
2. Login
3. Copy the generated JWT token
4. Click **Authorize** in Swagger
5. Enter:

```
Bearer YOUR_TOKEN_HERE
```

6. Click **Authorize**

You can now access protected endpoints.

---

# API Endpoints

## Authentication

| Method | Endpoint           |
| ------ | ------------------ |
| POST   | /api/auth/login    |

---

## Patients

| Method | Endpoint           |
| ------ | ------------------ |
| GET    | /api/patient     |
| GET    | /api/patient/{id} |
| POST   | /api/patient      |
| PUT    | /api/patient/{id} |
| DELETE | /api/patient/{id} |

---

# Project Structure

```
ClinicApi
│
├── Controllers
│   ├── AuthController.cs
│   └── PatientsController.cs
│
├── Data
│   └── AppDbContext.cs
│
├── Models
│   ├── Patient.cs
│   └── User.cs
│
├── Services
│   └── JwtService.cs
│
├── Migrations
│
├── appsettings.json
│
├── Program.cs
│
└── clinic.db
```

---

# Technologies Used

* ASP.NET Core 8 Web API
* Entity Framework Core
* SQLite
* JWT Authentication
* Swagger (OpenAPI)

---

# Troubleshooting

### Database not created

Run:

```bash
dotnet ef database update
```

---

### Missing packages

Restore NuGet packages:

```bash
dotnet restore
```

---

### Port already in use

Close the application using the port or change the launch profile in:

```
Properties/launchSettings.json
```

---

### Invalid JWT Token

* Make sure the token is not expired.
* Ensure the JWT Key in `appsettings.json` matches the one used to generate the token.
* Include the `Bearer` prefix when authorizing.

---

# Development Notes

* This project is intended for learning purposes and demonstrates a simple Hospital Management API.
* Passwords are securely hashed before being stored.
* JWT authentication protects secured endpoints.
* SQLite is used for lightweight local development.

---

Happy coding!
