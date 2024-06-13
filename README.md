# backend.api

Run this command to match the sql database in use

Scaffold-DbContext -Project backend.api 'Data Source=PREDATOHELIOS16;Database=FullstackDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False' Microsoft.EntityFrameworkCore.SqlServer -ContextDir Data -OutputDir Models/Generated -DataAnnotation -ContextNamespace backend.api.Data -Namespace backend.api.Models.Generated -force -verbose -UseDatabaseNames

change the values as needed

todo:
auth
middlewares
attributes
ef