# backend.api

## Overview

The `backend.api` project serves as the backend for your application. It interacts with a SQL database and provides essential functionality for your application's frontend.

## Getting Started

1. **Database Setup:**
   - Execute the necessary database scripts using SSMS (SQL Server Management Studio).
   - Ensure that your database connection string is correctly configured in your application.

2. **Entity Framework (EF) Models:**
   - To create EF models that match your SQL database, run the following command:
     ```
     Scaffold-DbContext -Project backend.data 'Data Source=PREDATOHELIOS16;Database=FullstackDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False' Microsoft.EntityFrameworkCore.SqlServer -NoOnConfiguring -ContextDir Data/Generated -OutputDir Models/Generated -DataAnnotation -ContextNamespace backend.data.Data.Generated -Namespace backend.data.Models.Generated -verbose -UseDatabaseNames -force
     ```
     Replace the values as necessary.

3. **Authentication and Authorization:**
   - To add authorization to your authentication flow:
     - In `Program.cs`, after adding authentication, include the following code snippet:
       ```csharp
       // builder.Services.AddAuthorizationBuilder().AddPolicy("read:messages", policy =>
       //     policy.Requirements.Add(new HasScopeRequirement("read:messages", builder.Configuration["Auth0:Domain"]!)));
       ```
     - Customize the policy name and scope as needed.

## To-Do List

- **Integrate with Pocketbase:** Call the necessary APIs or services from Pocketbase.
- **UI Development:** Create a user-friendly frontend for your application.
- **Mapper** Map objects


**.Net 9**
- openapi https://www.youtube.com/watch?v=8xEkVmqlr4I
- quartz demo 