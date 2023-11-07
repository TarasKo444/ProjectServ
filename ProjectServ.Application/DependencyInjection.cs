using System.Reflection;
using Mapster;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectServ.Application.Services;

namespace ProjectServ.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddFluentValidation(new[] { typeof(DependencyInjection).Assembly });
        
        services.AddMapster();

        services.AddScoped<UserAuthService>();
        
        services.AddTransient<JwtService>();
        services.AddTransient<UserAccessorService>();
        services.AddTransient<IApiKeyValidation, ApiKeyValidation>();
        
        return services;
    } 
}
