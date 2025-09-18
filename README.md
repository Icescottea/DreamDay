DreamDay - Wedding Planning & Management System

https://img.shields.io/badge/ASP.NET_Core-6.0%252B-512BD4?style=for-the-badge&logo=dotnet
https://img.shields.io/badge/SQL_Server-2017%252B-CC2927?style=for-the-badge&logo=microsoft-sql-server
https://img.shields.io/badge/Bootstrap-5.3-7952B3?style=for-the-badge&logo=bootstrap
📖 Abstract

DreamDay is a comprehensive, all-in-one wedding planning and coordination platform designed to simplify the wedding planning experience for couples and professional planners. The system centralizes key elements of wedding planning—such as vendor booking, guest management, budget tracking, task scheduling, and real-time communication—into a single, user-friendly web application.

Built using modern web technologies with a strong emphasis on Object-Oriented Programming (OOP) principles, DreamDay offers role-based dashboards for Couples, Planners, and Administrators. It streamlines traditionally manual and stressful processes, reduces miscommunication, and provides powerful tools for efficient wedding management, ensuring a seamless journey from planning to the big day.
✨ Key Features

    Multi-Role User System: Secure registration and authentication for Couples, Planners, and Administrators with tailored dashboards.

    Vendor Catalog: Browse and filter a comprehensive directory of wedding vendors by category (Photography, Catering, Venues, etc.).

    Guest Management: Full CRUD operations for guest lists, including RSVP tracking, meal preference logging, and intelligent table assignment with capacity checks.

    Budget Tracker: Create, update, and monitor wedding expenses with real-time calculations of estimated vs. actual costs.

    Interactive Checklist: Create and track planning tasks, assign vendors, set due dates, and mark items as complete.

    Wedding Day Timeline: Schedule and manage the events of the big day, including vendor arrival times.

    Real-Time Messaging: An integrated messaging system allows seamless communication between couples and their assigned planner.

    Reporting & Analytics (Planner & Admin): View insights on popular venues, average budgets, and vendor performance.

    Admin Dashboard: Oversight of the entire platform, including user management, vendor management, and system-wide reporting.

🛠️ Technology Stack
Backend

    Framework: ASP.NET Core MVC

    Authentication & Authorization: ASP.NET Core Identity with Role-Based Access Control (RBAC)

    ORM: Entity Framework Core

    Language: C#

Frontend

    UI Framework: Bootstrap 5.3

    Templating: Razor Pages (.cshtml)

    Styling: CSS3

    Scripting: JavaScript (ES6+)

Database

    DBMS: Microsoft SQL Server

    Design: Normalized relational database with foreign key constraints.

Development & Tools

    IDE: Microsoft Visual Studio 2022

    Version Control: Git & GitHub

    Diagramming: Draw.io (for UML & ER Diagrams)

    Database Management: SQL Server Management Studio (SSMS)

🚀 Installation & Setup
Prerequisites

Ensure you have the following installed on your development machine:

    .NET 6.0 SDK or later

    SQL Server (2017 or later)

    Visual Studio 2022 (with ASP.NET and web development workload)

Steps

    Clone the Repository
    bash

git clone <your-repository-url>
cd DreamDay

Restore Dependencies
bash

dotnet restore

Database Setup

    Create a new database in SQL Server named DreamDayDB.

    Update the connection string in appsettings.json with your server credentials.

json

"ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=DreamDayDB;Trusted_Connection=True;TrustServerCertificate=True;"
}

Run Database Migrations
This will create the necessary tables, including the ASP.NET Identity tables.
bash

dotnet ef database update

Seed the Database (Optional)

    The project includes a RoleInitializer service. Run the application once to ensure default roles (Admin, Planner, Couple) are created.

    You may need to register the first admin user manually through the interface and then assign the role in the database.

Run the Application
bash

dotnet run

    The application will be available at https://localhost:7000 or http://localhost:5000.

📋 System Architecture & Design

DreamDay follows a classic 3-Tier Architecture:

    Presentation Layer (Client): Razor Views, Bootstrap, JavaScript.

    Application Layer (Server): ASP.NET Core MVC Controllers, Services, ViewModels.

    Data Layer: SQL Server Database, Entity Framework Core.

Key Design Principles:

    MVC Pattern: Separation of concerns for maintainability.

    OOP Principles: Heavy use of Encapsulation, Inheritance, Abstraction, and Polymorphism.

    Role-Based Authorization: Secure access control via ASP.NET Identity.

    Repository Pattern: EF Core acts as the data access layer.

👥 Default User Roles & Access

    Couple:

        Can create one wedding profile.

        Manage guests, budget, checklist, and timeline.

        Message their assigned planner.

        Browse the vendor catalog.

    Planner:

        View a dashboard of all weddings assigned to them.

        Manage the checklist, timeline, and guest list for assigned weddings.

        Communicate with couples.

        Access analytical reports on their performance.

    Administrator:

        Manage all user accounts (activate, deactivate, delete).

        Manage the vendor catalog (add, edit, remove vendors).

        View system-wide reports and analytics.

        Oversee platform health and usage.

🔐 Security Features

    Passwords are hashed using ASP.NET Identity's built-in hasher.

    Anti-forgery tokens validate all form submissions.

    Controller actions are decorated with [Authorize(Roles = "RoleName")] to enforce role-based access.

    SQL parameters are used exclusively via EF Core to prevent injection attacks.

    Sensitive data is not logged, and error messages are user-friendly to avoid information leakage.

📄 License

This project is licensed under the CC0-1.0 license - see the [LICENSE.txt] file for details.
