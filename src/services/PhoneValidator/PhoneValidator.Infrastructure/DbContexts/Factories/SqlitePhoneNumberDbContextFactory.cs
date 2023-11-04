using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PhoneValidator.Infrastructure.DbContexts.Factories;

public class SqlitePhoneNumberDbContextFactory : IDesignTimeDbContextFactory<PhoneDbContext>
{
    public PhoneDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<PhoneDbContext>();
        var connectionString = "Data Source=phone.db;Cache=Shared";

        builder.UseSqlite(connectionString);

        return new PhoneDbContext(builder.Options);
    }
}