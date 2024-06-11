using AutoMapper;
using DayStory.Application.Helper;
using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace DayStory.Application.Mappers;

public class DaySummaryMapperProfile : Profile
{
    public DaySummaryMapperProfile(IConfiguration configuration)
    {
        var baseUrl = configuration.GetSection("DayStory").GetValue<string>("BaseUrl");

        CreateMap<DaySummary, DaySummaryContract>();
        CreateMap<DaySummary, DaySummaryContract>().ReverseMap();

        CreateMap<DaySummary, GetDaySummaryContract>()
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(x => $"{baseUrl}{ImagePathHelper.RemovePathPrefix(x.ImagePath)}"))
            .ReverseMap();

        CreateMap<DaySummaryContract, CreateDaySummaryContract>();
        CreateMap<DaySummaryContract, CreateDaySummaryContract>().ReverseMap();
    }
}
