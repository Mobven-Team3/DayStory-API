using AutoMapper;
using DayStory.Common.DTOs;
using DayStory.Domain.Entities;

namespace DayStory.Application.Mappers;

public class EventMapperProfile : Profile
{
    public EventMapperProfile()
    {
        CreateMap<Event, EventContract>();
        CreateMap<Event, EventContract>().ReverseMap();

        CreateMap<Event, CreateEventContract>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.UserId));
        CreateMap<Event, CreateEventContract>().ReverseMap()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.UserId));

        CreateMap<Event, GetEventContract>();
        CreateMap<Event, GetEventContract>().ReverseMap();

        CreateMap<EventContract, UpdateEventContract>();
        CreateMap<EventContract, UpdateEventContract>().ReverseMap();
    }
}
