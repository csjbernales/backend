# backend.api

Run this command to match the sql database in use

Scaffold-DbContext -Project backend.data 'Name=ConnectionStrings:fullstackdb' Microsoft.EntityFrameworkCore.SqlServer -ContextDir Data -OutputDir Models/Generated -DataAnnotation -ContextNamespace backend.data.Data -Namespace backend.data.Models.Generated -force -verbose 

change the values as needed