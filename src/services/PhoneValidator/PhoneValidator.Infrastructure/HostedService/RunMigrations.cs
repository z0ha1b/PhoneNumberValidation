using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Hosting;
using PhoneValidator.Infrastructure.DbContexts;

namespace PhoneValidator.Infrastructure.HostedService;

public class RunMigrations : IHostedService
{
    private readonly IDbContextFactory<PhoneDbContext> _dbContextFactory;

    public RunMigrations(IDbContextFactory<PhoneDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var dbContext = _dbContextFactory.CreateDbContext();
        await dbContext.Database.MigrateAsync(cancellationToken);
        await dbContext.DisposeAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}