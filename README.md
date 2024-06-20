# backend.api

Run database scripts from db/ssms

Run this command to create a new EF models that matches sql database

delete first the Data/Generated and Models/Generated folders before running the cli command below

Scaffold-DbContext -Project backend.api 'Data Source=PREDATOHELIOS16;Database=FullstackDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False' Microsoft.EntityFrameworkCore.SqlServer -NoOnConfiguring -ContextDir Data/Generated -OutputDir Models/Generated -DataAnnotation -ContextNamespace backend.api.Data.Generated -Namespace backend.api.Models.Generated -verbose -UseDatabaseNames -force

-force is for force update only. use with caution


Change the values as needed

Add authorization to authentication
add after addauthentication() in Program.cs

//builder.Services.AddAuthorizationBuilder().AddPolicy("read:messages", policy =>
//    policy.Requirements.Add(new HasScopeRequirement("read:messages", builder.Configuration["Auth0:Domain"]!)));
____________________________________________________________________________________________


todo:
Call on Pocketbase

UI
auth policy