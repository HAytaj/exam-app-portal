# 🎓 Exam Management System

A full-stack exam management application built with **ASP.NET Core Web API** and **Angular**. This app allows admins to manage students, subjects, teachers, classes, and exams efficiently.

## ✨ Features

- 🔐 Student and subject relationship with validation
- 🧠 Clean architecture with Generic Repository + UnitOfWork pattern
- 📊 Exam CRUD operations with subject/class filtering
- 💡 Angular frontend with validation
- 📦 Lazy/Eager loading with EF Core
- 🧪 Custom validators and error handling

## 🧱 Technologies Used

- Backend: ASP.NET Core Web API (.NET 8)
- ORM: Entity Framework Core + SQL Server
- Frontend: Angular 20.1.0, TypeScript, Bootstrap 5
- Architecture: Clean layered + SOLID


## 🧰 Setup Instructions

### Backend (API)
```bash
cd api
dotnet restore
dotnet run



Frontend (Angular)
cd client
npm install
ng serve


For .NET
api/bin/
api/obj/
*.user
*.suo
*.db


For Angular
client/node_modules/
client/dist/
.env



Database Design and Dummy Data


The project includes a SQL database design that shows the main entities and their relationships: Classes, Teachers, Subjects, Students, and Exams.

The SQL scripts are located in the db/ folder under the file database_scripts_with_dummy_data.sql.

These scripts create the tables and establish foreign key relationships between them, ensuring data integrity.

Dummy (sample) data is also included so the application can be tested immediately.

How to use the database scripts
Open SQL Server Management Studio (or your preferred SQL tool).

Execute the scripts in the db/ folder in the following order:

First, run the table creation scripts.

Then, run the scripts to insert dummy data.

You can run queries on the tables to verify that the database and application are working correctly.

You can also find screenshots of the project in the project_images folder to get a preview of the application.


Author

This project was developed and presented by Aytaj Hesenova on July 19, 2025.



