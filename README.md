# 👥 Company Management System

An internal HR web application for managing employees and departments, built with **ASP.NET Core MVC** and **N-Tier Architecture**, featuring role-based access control and a clean admin interface.

---

## ✨ Features

- 👤 **Employee Management** — full CRUD operations for employee records
- 🏢 **Department Management** — manage departments and assignments
- 🔐 **Role-Based Access Control** — different permissions for Admin and HR roles using ASP.NET Core Identity
- 🗺️ **AutoMapper** — clean mapping between domain entities and view models
- 📐 **N-Tier Architecture** — clear separation between Presentation, Business Logic, and Data Access layers
- 🔁 **Repository Pattern + Unit of Work** — consistent and maintainable data access

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Language | C# / .NET |
| Framework | ASP.NET Core MVC |
| ORM | Entity Framework Core |
| Database | SQL Server |
| Auth | ASP.NET Core Identity |
| Mapping | AutoMapper |
| Architecture | N-Tier |
| Patterns | Repository · Unit of Work |

---

## 🏛️ Architecture Overview

```
├── Presentation Layer (ASP.NET Core MVC)
│   ├── Controllers
│   ├── Views
│   └── ViewModels
├── Business Logic Layer
│   ├── Services
│   └── DTOs
├── Data Access Layer
│   ├── Repositories (Generic + Specific)
│   ├── Unit of Work
│   └── EF Core DbContext
└── Domain
    └── Entities (Employee, Department, ApplicationUser)
```

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server

### Setup

```bash
# 1. Clone the repository
git clone https://github.com/karimsalahabdelghany/Company-Management-MVC.git
cd Company-Management-MVC

# 2. Update appsettings.json with your SQL Server connection string

# 3. Apply EF Core migrations
dotnet ef database update

# 4. Run the application
dotnet run
```

Navigate to `https://localhost:{port}` in your browser.

---

## 🔐 Default Roles

| Role | Access |
|---|---|
| Admin | Full access — manage employees, departments, and users |
| HR | Manage employees only |

---

## 📸 Key Pages

- `/employees` — list, search, and manage employees
- `/departments` — manage departments
- `/account/login` — login page
- `/admin` — admin dashboard (Admin role only)

---

## 👤 Author

**Karim Salah** — Junior .NET Backend Developer
- 📧 karimabdelghany753@gmail.com
- 💼 [LinkedIn](https://linkedin.com/in/karim-salah22)
- 🐙 [GitHub](https://github.com/karimsalahabdelghany)
