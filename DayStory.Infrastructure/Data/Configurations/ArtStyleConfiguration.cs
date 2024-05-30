using DayStory.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DayStory.Infrastructure.Data.Configurations;

public class ArtStyleConfiguration : IEntityTypeConfiguration<ArtStyle>
{
    public void Configure(EntityTypeBuilder<ArtStyle> builder)
    {
        builder.Property(e => e.Id).IsRequired(true).ValueGeneratedOnAdd();
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).IsRequired(true).HasMaxLength(150);

        builder.Property(e => e.IsDeleted).HasDefaultValue(false);
        builder.Property(e => e.CreatedOn).HasDefaultValueSql("CURRENT_TIMESTAMP").HasConversion(v => v,v => DateTime.SpecifyKind(v, DateTimeKind.Utc)).IsRequired();
        builder.Property(e => e.UpdatedOn).HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null).IsRequired(false);
        builder.Property(e => e.DeletedOn).HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null).IsRequired(false);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
