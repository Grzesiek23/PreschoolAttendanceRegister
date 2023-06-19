using Microsoft.AspNetCore.Identity;
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
using ILogger = Serilog.ILogger;

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

builder.Services.AddCors();
builder.Services.AddAuthorization();

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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        await SeedContext.SeedDefaultData(userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger>();
        logger.Error(ex, "An error occurred seeding the DB");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(corsPolicyBuilder => corsPolicyBuilder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithExposedHeaders("Location"));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapApiEndpoints();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();