using AutoMapper;
using DayStory.Application.Interfaces;
using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;

namespace DayStory.Application.Services;

public class ArtStyleService : BaseService<ArtStyle, ArtStyleContract>, IArtStyleService
{
    public ArtStyleService(IGenericRepository<ArtStyle, ArtStyleContract> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
