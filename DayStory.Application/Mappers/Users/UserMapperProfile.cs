using AutoMapper;
using DayStory.Common.DTOs;
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

        CreateMap<User, UserRegisterContract>()
            .ForMember(dest => dest.PasswordConfirmed, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.MapFrom(x => x.HashedPassword));
        CreateMap<User, UserRegisterContract>()
            .ForMember(dest => dest.PasswordConfirmed, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.MapFrom(x => x.HashedPassword))
            .ReverseMap();

        CreateMap<UserContract, UserUpdateContract>();
        CreateMap<UserContract, UserUpdateContract>().ReverseMap();

        CreateMap<User, UserGetContract>();
        CreateMap<User, UserGetContract>().ReverseMap();
    }
}
