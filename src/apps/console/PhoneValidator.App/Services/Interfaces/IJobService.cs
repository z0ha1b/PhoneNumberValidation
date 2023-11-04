using PhoneValidator.Application.DTOs;

namespace PhoneValidator.App.Services.Interfaces;

public interface IJobService
{
    Task<List<PhoneNumberDto>> Proceed(List<PhoneNumberDto>? phoneNumbers = null);
}