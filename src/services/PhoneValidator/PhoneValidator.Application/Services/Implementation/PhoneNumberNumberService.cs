using PhoneValidator.Application.DTOs;
using PhoneValidator.Application.Services.Interfaces;
using PhoneValidator.Core.IRepository.Interfaces;
using AutoMapper;
using PhoneValidator.Core.Model;

namespace PhoneValidator.Application.Services.Implementation;

public class PhoneNumberService : IPhoneNumberService
{
    private readonly IPhoneRepository _phoneRepository;
    private readonly IMapper _mapper;

    public PhoneNumberService(IPhoneRepository phoneRepository, IMapper mapper)
    {
        _phoneRepository = phoneRepository;
        _mapper = mapper;
    }

    public async Task<bool> AddPhoneNumber(PhoneNumberDto? phoneNumberDto)
    {
        if (phoneNumberDto == null)
        {
            return false;
        }

        var addNumber = _mapper.Map<PhoneNumber>(phoneNumberDto);
        await _phoneRepository.AddPhoneNumber(addNumber);
        return true;
    }

    public async Task<bool> UpdatePhoneNumber(PhoneNumberDto? phoneNumberDto)
    {
        if (phoneNumberDto == null)
        {
            return false;
        }

        var updateNumber = _mapper.Map<PhoneNumber>(phoneNumberDto);
        await _phoneRepository.UpdatePhoneNumber(updateNumber);

        return true;
    }

    public async Task<List<PhoneNumberDto>> GetPhoneList()
    {
        var phoneNumbers = await _phoneRepository.GetPhoneNumbers();
        var phoneNumberList = _mapper.Map<List<PhoneNumberDto>>(phoneNumbers);

        return phoneNumberList;
    }

    public async Task<bool> AddPhoneNumbers(List<PhoneNumberDto> phoneNumbers)
    {
        var pNumbers = _mapper.Map<List<PhoneNumber>>(phoneNumbers);

        await _phoneRepository.AddPhoneNumbers(pNumbers);
        return true;
    }
}