using Microsoft.OpenApi.Models;
using PAR.Application.Abstractions;
using PAR.Domain.Entities;
using PAR.Infrastructure;
using PAR.Persistence;
using PAR.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddIdentityCore<ApplicationUser>(
        options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;
            options.SignIn.RequireConfirmedAccount = false;
        })
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ParDbContext>();

builder.Services.AddInfrastructure(configuration);

builder.Services.AddPersistence(configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var service = app.Services.GetRequiredService<IClock>();

app.MapGet("/", () => $"Hello World!\n{service.Current()}");

app.Run();