using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneValidator.Application.Configurations;
using PhoneValidator.Application.Extensions;
using PhoneValidator.Infrastructure.Extensions;

namespace PhoneValidator.App.Extensions;

public static class RegisterProjectsServices
{
    public static IServiceCollection AddProjectsServices(this IServiceCollection serviceCollection, string dbString)
    {
        serviceCollection.AddApplicationServices();
        serviceCollection.AddInfrastructureServices(dbString);

        return serviceCollection;
    }
}