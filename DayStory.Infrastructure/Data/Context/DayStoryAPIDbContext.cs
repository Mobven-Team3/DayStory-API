using DayStory.Domain.Entities;
using DayStory.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DayStory.Infrastructure.Data.Context;

/// Migrations = add-migration name
/// Db update = update-database
public class DayStoryAPIDbContext : DbContext
{
    public DayStoryAPIDbContext() { }
    public DayStoryAPIDbContext(DbContextOptions options) : base(options) { }

    DbSet<User> Users { get; set; }
    DbSet<Event> Events { get; set; }
    DbSet<DaySummary> DaySummaries { get; set; }
    DbSet<ArtStyle> ArtStyles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new DaySummaryConfiguration());
        modelBuilder.ApplyConfiguration(new ArtStyleConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
