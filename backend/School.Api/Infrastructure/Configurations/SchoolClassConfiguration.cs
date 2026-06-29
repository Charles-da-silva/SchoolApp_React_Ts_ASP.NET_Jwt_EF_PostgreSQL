using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Api.Domain.Entities;

namespace School.Api.Infrastructure.Configurations
{
  public class SchoolClassConfiguration : IEntityTypeConfiguration<SchoolClass>
  {
    public void Configure(EntityTypeBuilder<SchoolClass> builder)
    {
      builder.ToTable("class");

      builder.HasKey(c => c.Id);

      builder.Property(c => c.Name)
             .IsRequired()
             .HasMaxLength(120);

      builder.Property(c => c.Room)
             .IsRequired()
             .HasMaxLength(80);

      builder.Property(c => c.Observations)
             .HasMaxLength(1000);

      builder.Property(c => c.IsActive)
             .HasDefaultValue(true);

      builder.HasIndex(c => new { c.Name, c.Year })
             .HasDatabaseName("ix_class_name_year");
    }
  }
}
