using Microsoft.EntityFrameworkCore;
using PhoneValidator.Core.Model;

namespace PhoneValidator.Infrastructure.DbContexts;

public class PhoneDbContext : DbContext
{
    public PhoneDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<PhoneNumber> PhoneNumbers { get; set; } = default!;
}