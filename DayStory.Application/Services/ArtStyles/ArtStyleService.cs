using AutoMapper;
using DayStory.Application.DTOs;
using DayStory.Application.Interfaces;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;

namespace DayStory.Application.Services;

public class ArtStyleService : BaseService<ArtStyle, ArtStyleContract>, IArtStyleService
{
    public ArtStyleService(IGenericRepository<ArtStyle> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
