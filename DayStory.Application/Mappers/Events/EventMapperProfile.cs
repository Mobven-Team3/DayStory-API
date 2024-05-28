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

        CreateMap<Event, EventCreateContract>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.UserId));
        CreateMap<Event, EventCreateContract>().ReverseMap()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.UserId));

        CreateMap<Event, EventGetContract>();
        CreateMap<Event, EventGetContract>().ReverseMap();

        CreateMap<EventContract, EventUpdateContract>();
        CreateMap<EventContract, EventUpdateContract>().ReverseMap();
    }
}
