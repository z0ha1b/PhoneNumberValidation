using PhoneValidator.Application.DTOs;

namespace PhoneValidator.Application.Services.Interfaces;

public interface IPhoneNumberService
{
    Task<bool> AddPhoneNumber(PhoneNumberDto? phoneNumberDto);
    Task<bool> UpdatePhoneNumber(PhoneNumberDto? phoneNumberDto);
    Task<List<PhoneNumberDto>> GetPhoneList();

    Task<bool> AddPhoneNumbers(List<PhoneNumberDto> phoneNumbers);
}