﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace backend.api.OpenApi
{
    public static class Swagger
    {
        public static Action<SwaggerGenOptions> SwaggerOptions(WebApplicationBuilder builder)
        {
            return c =>
            {
                c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Backend API - V1",
                    Version = "v1",
                    Description = "A simple API for fullstack .net app",
                    TermsOfService = new Uri(builder.Configuration.GetSection("SwaggerDoc")["TosUrl"]!),
                    Contact = new OpenApiContact
                    {
                        Name = "Jhon B",
                        Email = "email"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Apache 2.0",
                        Url = new Uri(builder.Configuration.GetSection("SwaggerDoc")["LicenseUrl"]!)
                    }
                });

                if (!builder.Environment.IsDevelopment())
                {
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
                }
            };
        }
    }
}