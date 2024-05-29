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

        CreateMap<User, LoginUserContract>();
        CreateMap<User, LoginUserContract>().ReverseMap();

        CreateMap<User, RegisterUserContract>()
            .ForMember(dest => dest.PasswordConfirmed, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.MapFrom(x => x.HashedPassword));
        CreateMap<User, RegisterUserContract>()
            .ForMember(dest => dest.PasswordConfirmed, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.MapFrom(x => x.HashedPassword))
            .ReverseMap();

        CreateMap<UserContract, UpdateUserContract>();
        CreateMap<UserContract, UpdateUserContract>().ReverseMap();

        CreateMap<User, GetUserContract>();
        CreateMap<User, GetUserContract>().ReverseMap();
    }
}
