# backend.api

Run this command to match the sql database in use

Scaffold-DbContext -Project backend.api 'Name=ConnectionStrings:fullstackdb' Microsoft.EntityFrameworkCore.SqlServer -ContextDir Data -OutputDir Models/Generated -DataAnnotation -ContextNamespace backend.api.Data -Namespace backend.api.Models.Generated -force -verbose

change the values as needed

todo:
auth
middlewares
attributes
ef