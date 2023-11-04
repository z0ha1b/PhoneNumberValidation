using PhoneValidator.App.Services.Interfaces;
using PhoneValidator.Application.ApiServices.Interfaces;
using PhoneValidator.Application.DTOs;
using PhoneValidator.Application.Services.Interfaces;

namespace PhoneValidator.App.Services.Implementations;

public class JobService : IJobService
{
    private readonly IPhoneVerificationService _phoneVerification;
    private readonly IPhoneNumberService _phoneNumberService;

    public JobService(IPhoneNumberService phoneNumberService, IPhoneVerificationService phoneVerification)
    {
        _phoneNumberService = phoneNumberService;
        _phoneVerification = phoneVerification;
    }

    public async Task<List<PhoneNumberDto>> Proceed(List<PhoneNumberDto>? phoneNumbers = null)
    {
        if (phoneNumbers != null)
        {
            await _phoneNumberService.AddPhoneNumbers(phoneNumbers);
        }

        var pNumbers = await _phoneNumberService.GetPhoneList();
        foreach (var phoneNumber in pNumbers)
        {
            await _phoneVerification.Verify(phoneNumber);

            await _phoneNumberService.AddPhoneNumber(phoneNumber);
        }

        return pNumbers;
    }
}