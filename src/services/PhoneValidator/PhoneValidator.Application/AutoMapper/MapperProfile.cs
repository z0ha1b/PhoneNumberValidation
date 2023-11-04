using AutoMapper;
using PhoneValidator.Application.DTOs;
using PhoneValidator.Core.Model;

namespace PhoneValidator.Application.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<PhoneNumberDto, PhoneNumber>().ReverseMap();
    }
}