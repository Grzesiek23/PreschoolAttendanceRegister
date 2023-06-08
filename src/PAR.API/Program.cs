using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PAR.API.Endpoints;
using PAR.API.Middlewares;
using PAR.Application;
using PAR.Domain.Entities;
using PAR.Infrastructure;
using PAR.Persistence;
using PAR.Persistence.Context;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
builder.Host.UseSerilog((context, config) => { config.ReadFrom.Configuration(context.Configuration); });

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
builder.Services.AddApplication();
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

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
loggerFactory.AddSerilog();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapApiEndpoints();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();