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

        modelBuilder.Entity<ArtStyle>().HasData(
            new ArtStyle { Id = 1, Name = "Realist" },
            new ArtStyle { Id = 2, Name = "Surrealist" },
            new ArtStyle { Id = 3, Name = "Minimalist" },
            new ArtStyle { Id = 4, Name = "Retro/Vintage" },
            new ArtStyle { Id = 5, Name = "Steampunk" },
            new ArtStyle { Id = 6, Name = "Cyberpunk" },
            new ArtStyle { Id = 7, Name = "Futuristic" },
            new ArtStyle { Id = 8, Name = "Baroque" },
            new ArtStyle { Id = 9, Name = "Gothic" },
            new ArtStyle { Id = 10, Name = "Pop Art" },
            new ArtStyle { Id = 11, Name = "Abstract" },
            new ArtStyle { Id = 12, Name = "Art Deco" },
            new ArtStyle { Id = 13, Name = "Fantasy" },
            new ArtStyle { Id = 14, Name = "Digital Art" },
            new ArtStyle { Id = 15, Name = "Cinematic" },
            new ArtStyle { Id = 16, Name = "Anime" },
            new ArtStyle { Id = 17, Name = "Cartoonist" },
            new ArtStyle { Id = 18, Name = "Comic" },
            new ArtStyle { Id = 19, Name = "Retro Pop" },
            new ArtStyle { Id = 20, Name = "Logo" },
            new ArtStyle { Id = 21, Name = "Whimsical" },
            new ArtStyle { Id = 22, Name = "Horror" },
            new ArtStyle { Id = 23, Name = "Monster" },
            new ArtStyle { Id = 24, Name = "Figure" },
            new ArtStyle { Id = 25, Name = "Retro Sci-Fi" },
            new ArtStyle { Id = 26, Name = "Dark Fantasy" },
            new ArtStyle { Id = 27, Name = "Dreamwave" },
            new ArtStyle { Id = 28, Name = "Mystical" },
            new ArtStyle { Id = 29, Name = "Expressionism" },
            new ArtStyle { Id = 30, Name = "Flora" },
            new ArtStyle { Id = 31, Name = "Daydream" },
            new ArtStyle { Id = 32, Name = "Radioactive" },
            new ArtStyle { Id = 33, Name = "Starry" }
        );

        //modelBuilder.Entity<User>().HasData(
        //    new User { Id = 1, FirstName = "admin", LastName = "admin", Username = "admin", Email = "admin", HashedPassword = "AQAAAAIAAYagAAAAEMAjbf9FW8x1DZZ+AyQXeu64godJj2gAwx6EPpTyTL1El6DlT1fXmkdw/+a81zac3g==", BirthDate = "24-03-1999", Role = "Admin", Gender = 0, CreatedOn = DateTime.UtcNow },
        //    new User { Id = 2, FirstName = "Nisa", LastName = "Turhan", Username = "nisatur", Email = "nisa@gmail.com", HashedPassword = "AQAAAAIAAYagAAAAEEbO/nyiGYmwOTIe8lSZOx5V0fA3uJCJoajax2zFl9ZSSMbM10M27yrBbwzUPCc6gg==", BirthDate = "24-03-1999", Role = "User", Gender = 1, CreatedOn = DateTime.UtcNow }
        //);
    }
}
