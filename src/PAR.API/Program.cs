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

var app = builder.Build();

var service = app.Services.GetRequiredService<IClock>();

app.MapGet("/", () => $"Hello World!\n{service.Current()}");

app.Run();