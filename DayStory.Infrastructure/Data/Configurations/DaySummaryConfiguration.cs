using DayStory.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DayStory.Infrastructure.Data.Configurations;

public class DaySummaryConfiguration : IEntityTypeConfiguration<DaySummary>
{
    public void Configure(EntityTypeBuilder<DaySummary> builder)
    {
        builder.Property(e => e.Id).IsRequired(true).ValueGeneratedOnAdd();
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Date).IsRequired(true).HasDefaultValueSql("FORMAT(GETDATE(), 'dd-MM-yyyy')").IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.ImagePath).IsRequired(false).HasMaxLength(250);
        builder.Property(e => e.Summary).IsRequired(true).HasMaxLength(500);
        
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);
        builder.Property(e => e.CreatedOn).HasDefaultValueSql("getdate()").HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc)).IsRequired();
        builder.Property(e => e.UpdatedOn).HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null);
        builder.Property(e => e.DeletedOn).HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
