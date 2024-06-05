using AutoMapper;
using DayStory.Application.Interfaces;
using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;

namespace DayStory.Application.Services;

public class ArtStyleService : BaseService<ArtStyle, ArtStyleContract>, IArtStyleService
{
    private readonly IArtStyleRepository _artStyleRepository;
    private readonly Random _random;
    private readonly IMapper _mapper;
    public ArtStyleService(IGenericRepository<ArtStyle, ArtStyleContract> repository, IMapper mapper, IArtStyleRepository artStyleRepository) : base(repository, mapper)
    {
        _artStyleRepository = artStyleRepository;
        _random = new Random();
        _mapper = mapper;
    }

    public async Task<ArtStyleContract> GetRandomArtStyleIdAsync()
    {
        var artStyles = await GetAllAsync();
        if (artStyles == null || !artStyles.Any())
        {
            throw new InvalidOperationException("No art styles available.");
        }

        int index = _random.Next(artStyles.Count);
        var response = await _artStyleRepository.GetArtStyleByIdAsync(index);
        return _mapper.Map<ArtStyleContract>(response);
    }
}
