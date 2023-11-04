using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PhoneValidator.Application.ApiServices.Implementations;
using PhoneValidator.Application.ApiServices.Interfaces;
using PhoneValidator.Application.Configurations;
using PhoneValidator.Application.Services.Implementation;
using PhoneValidator.Application.Services.Interfaces;

namespace PhoneValidator.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services
            .AddScoped<IPhoneNumberService, PhoneNumberService>()
            .AddScoped<IPhoneVerificationService, PhoneVerificationService>();
    }
}