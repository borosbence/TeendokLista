using JWTSecurity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTSecurity.Extensions
{
    public static class WebAppBuilder
    {
        public static WebApplicationBuilder RegisterJWTService(this WebApplicationBuilder builder, bool swaggerEnabled = false)
        {
            ConfigurationManager configuration = builder.Configuration;

            // DI szolgáltatások
            builder.Services.AddSingleton<JwtManagerService>();

            // JWT hitelesítés
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // on production make it true
                    ValidateAudience = true, // on production make it true
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                };
            });
            if (swaggerEnabled)
            {
                // Swagger beállítása
                builder.Services.AddSwaggerGen(c =>
                {
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        BearerFormat = "JWT", // Optional
                        Scheme = "bearer",
                        Description = "JWT Authorization header using the Bearer scheme. Insert the plain token only.",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });
                });
            }

            return builder;
        }
    }
}
