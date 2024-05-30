using DayStory.Domain.Entities;
using DayStory.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DayStory.Infrastructure.Data.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.Property(e => e.Id).IsRequired(true).ValueGeneratedOnAdd();
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title).IsRequired(true).HasMaxLength(250);
        builder.Property(e => e.Description).IsRequired(false).HasMaxLength(350);
        builder.Property(e => e.Date).HasDefaultValueSql("TO_CHAR(NOW(), 'DD-MM-YYYY')").IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.Time).IsRequired(false).HasMaxLength(50);
        builder.Property(e => e.Priority).IsRequired(true).HasDefaultValue(Priority.NoPriority);
        builder.Property(e => e.CreatedOn).HasDefaultValueSql("CURRENT_TIMESTAMP").HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc)).IsRequired();
        builder.Property(e => e.UpdatedOn).HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null).IsRequired(false);
    }
}
