using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;
using DayStory.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DayStory.Infrastructure.Repositories;

internal class ArtStyleRepository : GenericRepository<ArtStyle, ArtStyleContract>, IArtStyleRepository
{
    private readonly DbSet<ArtStyle> _dbSet;

    public ArtStyleRepository(DayStoryAPIDbContext context) : base(context)
    {
        _dbSet = context.Set<ArtStyle>();
    }

    public async Task<ArtStyle> GetArtStyleByIdAsync(int id)
    {
        var result = await _dbSet.Include(x => x.DaySummaries).FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
            throw new ArgumentNullException("Art style not found.");
        return result;
    }
}
