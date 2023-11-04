using PhoneValidator.Core.Model;

namespace PhoneValidator.Core.IRepository.Interfaces;

public interface IPhoneRepository
{
    Task<bool> AddPhoneNumber(PhoneNumber phoneNumber, CancellationToken cancellationToken = default);
    Task<bool> UpdatePhoneNumber(PhoneNumber phoneNumber, CancellationToken cancellationToken = default);
    Task<List<PhoneNumber>> GetPhoneNumbers(CancellationToken cancellationToken = default);
    Task AddPhoneNumbers(List<PhoneNumber> pNumbers, CancellationToken cancellationToken = default);
}