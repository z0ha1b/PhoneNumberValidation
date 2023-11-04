using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhoneValidator.Core.IRepository.Interfaces;
using PhoneValidator.Infrastructure.DbContexts;
using PhoneValidator.Infrastructure.DbContexts.Factories;
using PhoneValidator.Infrastructure.HostedService;
using PhoneValidator.Infrastructure.Repository.Implementation;

namespace PhoneValidator.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
    {
        var migrationsAssemblyName = typeof(SqlitePhoneNumberDbContextFactory).Assembly.GetName().Name;

        return services
            .AddPooledDbContextFactory<PhoneDbContext>(x => x.UseSqlite(connectionString, db => db.MigrationsAssembly(migrationsAssemblyName)))
            .AddSingleton<IPhoneRepository, PhoneRepository>()
            .AddHostedService<RunMigrations>();
    }
}