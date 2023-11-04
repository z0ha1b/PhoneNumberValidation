using Microsoft.EntityFrameworkCore;
using PhoneValidator.Core.IRepository.Interfaces;
using PhoneValidator.Core.Model;
using PhoneValidator.Infrastructure.DbContexts;

namespace PhoneValidator.Infrastructure.Repository.Implementation;

public class PhoneRepository : IPhoneRepository
{
    private readonly IDbContextFactory<PhoneDbContext> _dbContextFactory;

    public PhoneRepository(IDbContextFactory<PhoneDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<bool> AddPhoneNumber(PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var existingPhoneNumber = await dbContext.PhoneNumbers.FirstOrDefaultAsync(x => x.Id == phoneNumber.Id, cancellationToken: cancellationToken);
        if (existingPhoneNumber == null)
        {
            await dbContext.PhoneNumbers.AddAsync(phoneNumber, cancellationToken);
            return true;
        }

        dbContext.Entry(existingPhoneNumber).CurrentValues.SetValues(phoneNumber);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return true;
    }

    public async Task<bool> UpdatePhoneNumber(PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        if (phoneNumber != null)
        {
            var checkPhoneNumber = await dbContext.PhoneNumbers.FirstOrDefaultAsync(x => x.Number == phoneNumber.Number, cancellationToken: cancellationToken);
            if (checkPhoneNumber == null)
            {
                return false;
            }

            dbContext.PhoneNumbers.Update(phoneNumber);
            
            await dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        return false;
    }

    public async Task<List<PhoneNumber>> GetPhoneNumbers(CancellationToken cancellationToken = default)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var phoneNumbersList = await dbContext.PhoneNumbers.Where(x => x.IsProcessed == false).ToListAsync(cancellationToken: cancellationToken);
        return phoneNumbersList;
    }

    public async Task AddPhoneNumbers(List<PhoneNumber> pNumbers, CancellationToken cancellationToken = default)
    {
        var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        await dbContext.PhoneNumbers.AddRangeAsync(pNumbers, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}