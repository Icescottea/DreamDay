# DreamDay – Wedding Planning & Management System  

![ASP.NET](https://img.shields.io/badge/ASP.NET_Core-6.0%252B-512BD4?style=for-the-badge&logo=dotnet)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-7952B3?style=for-the-badge&logo=bootstrap)
![SQL Server](https://img.shields.io/badge/SQL_Server-2017%252B-CC2927?style=for-the-badge&logo=microsoft-sql-server)

## 📖 Abstract  

DreamDay is a comprehensive wedding planning and management system designed to simplify and streamline the wedding experience for both couples and professional planners.  
It consolidates vendor booking, guest management, budgeting, task scheduling, and real-time communication into a single, user-friendly platform.  

By providing role-based dashboards, real-time messaging, and powerful planning tools, DreamDay eliminates manual processes, reduces miscommunication, and ensures a seamless journey from initial planning through to the big day.  

## ✨ Key Features  

*   **Multi-Role User System:** Secure registration and authentication for Couples, Planners, and Administrators with tailored dashboards.  
*   **Vendor Catalog:** Browse and filter a directory of wedding vendors by category (Photography, Catering, Venues, etc.).  
*   **Guest Management:** Full CRUD operations for guest lists, including RSVP tracking, meal preferences, and table assignment with capacity checks.  
*   **Budget Tracker:** Create, update, and monitor wedding expenses with real-time calculations of estimated vs. actual costs.  
*   **Interactive Checklist:** Create and track planning tasks, assign vendors, set due dates, and mark items as complete.  
*   **Wedding Day Timeline:** Schedule and manage the events of the big day, including vendor arrival times.  
*   **Real-Time Messaging:** Integrated messaging system between couples and planners.  
*   **Reporting & Analytics:** View insights on venues, budgets, and vendor performance (for Planners & Admins).  
*   **Admin Dashboard:** Oversight of the entire platform including user, vendor, and system-wide management.  

## 🛠️ Technology Stack  

### Backend  
*   **Framework:** ASP.NET Core MVC  
*   **Authentication:** ASP.NET Core Identity with Role-Based Access Control (RBAC)  
*   **ORM:** Entity Framework Core  
*   **Language:** C#  

### Frontend  
*   **Templating:** Razor Pages (.cshtml)  
*   **Styling:** Bootstrap 5.3, CSS3  
*   **Interactivity:** JavaScript (ES6+)  

### Database  
*   **DBMS:** Microsoft SQL Server  
*   **Design:** Normalized relational database with foreign key constraints  

### Development & Operations Tools  
*   **IDE:** Microsoft Visual Studio 2022  
*   **Version Control:** Git & GitHub  
*   **Diagramming:** Draw.io (for UML & ER diagrams)  
*   **Database Management:** SQL Server Management Studio (SSMS)  

## 🚀 Installation & Setup  

Follow these steps to set up the project locally for development and testing.  

1.  **Clone the Repository**  
    ```bash
    git clone https://github.com/[your-username]/dreamday.git
    cd dreamday
    ```

2.  **Restore Dependencies**  
    ```bash
    dotnet restore
    ```

3.  **Database Setup**  
    * Create a new database in SQL Server named `DreamDayDB`.  
    * Update the connection string in `appsettings.json`:  
      ```json
      "ConnectionStrings": {
        "DefaultConnection": "Server=YOUR_SERVER;Database=DreamDayDB;Trusted_Connection=True;TrustServerCertificate=True;"
      }
      ```

4.  **Run Database Migrations**  
    ```bash
    dotnet ef database update
    ```

5.  **Seed the Database (Optional)**  
    * Run the application once to ensure default roles (Admin, Planner, Couple) are created.  
    * Register the first admin user manually and assign the role in the database.  

6.  **Run The Application**  
    ```bash
    dotnet run
    ```
    The application will be available at:  
    - https://localhost:7000  
    - http://localhost:5000  

## 📋 System Workflow  

1.  **User Registration/Login:** Couples, planners, and admins register and receive role-based dashboards.  
2.  **Wedding Setup:** Couples create a wedding profile, manage guests, budget, and tasks.  
3.  **Vendor & Planner Coordination:** Browse vendors, assign planners, and schedule services.  
4.  **Task & Timeline Management:** Track milestones, vendor arrival times, and wedding-day events.  
5.  **Messaging & Notifications:** Real-time communication between couples and planners.  
6.  **Oversight & Analytics:** Administrators oversee the entire system, manage users/vendors, and view reports.  

## 🔐 Security Features  

*   Passwords are hashed using ASP.NET Identity’s built-in hasher.  
*   Anti-forgery tokens protect all form submissions.  
*   Controller actions are secured with `[Authorize(Roles = "RoleName")]`.  
*   SQL parameters via EF Core prevent injection attacks.  
*   Sensitive data is never logged; error messages are user-friendly to prevent information leakage.  

## 📊 Data Visualization  

Planners and Administrators can view real-time analytics on popular venues, average budgets, and vendor performance, enabling data-driven decision-making.  

## 👥 Default User Roles  

1.  **Couple:** Can create a wedding profile, manage guests/budget/checklists/timelines, and message their assigned planner.  
2.  **Planner:** View and manage all weddings assigned to them, communicate with couples, and access performance analytics.  
3.  **Administrator:** Manage all user accounts, vendor catalog, and system-wide reports.  

## 📝 License  

This project is licensed under the **CC0-1.0 license** – see the [LICENSE.txt](LICENSE.txt) file for details.  
