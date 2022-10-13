using AioCore.Domain.Aggregates.IdentityAggregate;
using AioCore.Domain.Common;
using AioCore.Services;
using AioCore.Services.AutomationServices;
using AioCore.Services.CommonServices;
using AioCore.Services.Providers;
using AioCore.Shared.Extensions;
using JsonSubTypes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;

namespace AioCore.Application.Helpers;

public static class StartupHelper
{
    public static void AddAioController(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
            options.SerializerSettings.Converters.Add(new JsonSubtypes());
        });
        services.AddSwaggerGenNewtonsoftSupport();
        services.AddSwaggerGen();
    }

    public static void AddAioContext(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddDbContext<SettingsContext>(options =>
        {
            options.UseSqlServer(appSettings.ConnectionStrings.DefaultConnection, b =>
            {
                b.MigrationsHistoryTable("__EFMigrationsHistory", SettingsContext.Schema);
                b.MigrationsAssembly(appSettings.AssemblyName);
                b.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
            });
        }, ServiceLifetime.Transient);
        services.AddDbContext<IdentityContext>(options =>
        {
            options.UseSqlServer(appSettings.ConnectionStrings.DefaultConnection, b =>
            {
                b.MigrationsHistoryTable("__EFMigrationsHistory", IdentityContext.Schema);
                b.MigrationsAssembly(appSettings.AssemblyName);
                b.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
            });
        });
        services.AddDefaultIdentity<User>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<IdentityContext>();
    }

    public static void AddApiScoped(this IServiceCollection services, AppSettings appSettings)
    {
    }

    public static void AddWebScoped(this IServiceCollection services)
    {
        services.AddScoped<IAlertService, AlertService>();
        services.AddScoped<IClaimsTransformation, ClaimsTransformation>();
        services.AddScoped<AuthenticationStateProvider, StateProvider<User>>();
        services.AddAllScenarios();
    }

    public static void AddAllSingleton(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddSingleton<ICommandService, CommandService>();
        services.AddSingleton<IADBService, ADBService>();
        services.AddSingleton<IAppiumService, AppiumService>();
        services.AddSingleton<IDeviceService, DeviceService>();
        services.AddSingleton<IAvatarService, AvatarService>();
        services.AddSingleton<IClientService, ClientService>();
        services.AddSingleton<ITiktokService, TiktokService>();
    }

    public static void UseAioController(this WebApplication app)
    {
        app.MapControllers();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    public static void UseAioCoreDatabase(this WebApplication app, AppSettings appSettings)
    {
        app.MigrateDatabase<SettingsContext>((_, appServices) =>
        {
            var logger = appServices.GetService<ILogger<IdentityContextSeed>>();
            SettingsContextSeed.SeedAsync(appSettings, logger).Wait();
        });
        app.MigrateDatabase<IdentityContext>((_, appServices) =>
        {
            var logger = appServices.GetService<ILogger<IdentityContextSeed>>();
            var userManager = appServices.GetRequiredService<UserManager<User>>();
            var roleManager = appServices.GetRequiredService<RoleManager<Role>>();
            IdentityContextSeed.SeedAsync(appSettings, userManager, roleManager, logger).Wait();
        });
    }
}