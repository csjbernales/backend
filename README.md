# backend.api

Run database scripts from db/ssms

Run this command to create a new EF models that matches sql database

Scaffold-DbContext -Project backend.api 'Data Source=PREDATOHELIOS16;Database=FullstackDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False' Microsoft.EntityFrameworkCore.SqlServer -ContextDir Data/Generated -OutputDir Models/Generated -DataAnnotation -ContextNamespace backend.api.Data.Generated -Namespace backend.api.Models.Generated -force -verbose -UseDatabaseNames

Change the values as needed

Run this command to update and match the current changes made from sql database

Update-Database

___________________________________________________________________________________________________


todo:
auth

Authentication - do latest
Authorization

Meaningful comments on methods 

Call on Pocketbase

UI

move partials from business to data
create a repository dependent from data layer

swagger documentation

ip:
Unit tests 