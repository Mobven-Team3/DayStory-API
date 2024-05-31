using AutoMapper;
using DayStory.Common.DTOs;
using DayStory.Domain.Entities;

namespace DayStory.Application.Mappers;

public class DaySummaryMapperProfile : Profile
{
    public DaySummaryMapperProfile()
    {
        CreateMap<DaySummary, DaySummaryContract>();
        CreateMap<DaySummary, DaySummaryContract>().ReverseMap();

        CreateMap<DaySummary, GetDaySummaryContract>();
        CreateMap<DaySummary, GetDaySummaryContract>().ReverseMap();
    }
}
