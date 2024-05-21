using DayStory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DayStory.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(e => e.Id).IsRequired(true).ValueGeneratedOnAdd();
        builder.HasKey(e => e.Id);

        builder.Property(e => e.FirstName).IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.LastName).IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.Username).IsRequired(true).HasMaxLength(50);
        builder.HasIndex(e => e.Username).IsUnique(true);
        builder.Property(e => e.Email).IsRequired(true).HasMaxLength(50);
        builder.HasIndex(e => e.Email).IsUnique(true);
        builder.Property(e => e.HashedPassword).IsRequired(true).HasMaxLength(250);
        builder.Property(e => e.BirthDate).IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.ProfilePicturePath).IsRequired(false).HasMaxLength(250);
        builder.Property(e => e.Gender).IsRequired(true).HasMaxLength(50);
        builder.Property(e => e.Role).IsRequired(true).HasMaxLength(50).HasDefaultValue("User");
        builder.Property(e => e.IsDeleted).HasDefaultValue(false);
        builder.Property(e => e.CreatedOn).HasDefaultValueSql("getdate()").HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc)).IsRequired();
        builder.Property(e => e.UpdatedOn).HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null);
        builder.Property(e => e.DeletedOn).HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null);
        builder.Property(e => e.LastLogin).HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}
