﻿using DayStory.Common.DTOs;
using DayStory.Domain.Entities;

namespace DayStory.Domain.Repositories;

public interface IArtStyleRepository : IGenericRepository<ArtStyle, ArtStyleContract>
{
    Task<ArtStyle> GetArtStyleByIdAsync(int id);
}
