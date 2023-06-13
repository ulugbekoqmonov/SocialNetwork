using Application.Interfaces;
using Application.Mappings;
using Application.Services.Authentication;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {            
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddAutoMapper(typeof(MapProfiles));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR((config) =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}