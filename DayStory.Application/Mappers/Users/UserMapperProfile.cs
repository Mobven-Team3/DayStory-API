using AutoMapper;
using DayStory.Application.DTOs;
using DayStory.Domain.Entities;

namespace DayStory.Application.Mappers;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<User, UserContract>();
        CreateMap<User, UserContract>().ReverseMap();

        CreateMap<User, UserLoginContract>();
        CreateMap<User, UserLoginContract>().ReverseMap();

        CreateMap<User, UserRegisterContract>();
        CreateMap<User, UserRegisterContract>().ReverseMap();
    }
}
