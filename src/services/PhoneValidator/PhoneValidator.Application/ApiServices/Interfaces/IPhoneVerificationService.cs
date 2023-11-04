using PhoneValidator.Application.DTOs;

namespace PhoneValidator.Application.ApiServices.Interfaces;

public interface IPhoneVerificationService
{
    Task Verify(PhoneNumberDto phoneNumber);
}