using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Api.Domain.Entities;

namespace School.Api.Infrastructure.Configurations
{
  public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
  {
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
      builder.ToTable("enrollments");

      builder.HasKey(e => e.Id);

      builder.Property(e => e.FinalReport)
             .HasMaxLength(5000);

      builder.HasOne(e => e.Student)
             .WithMany()
             .HasForeignKey(e => e.StudentId)
             .OnDelete(DeleteBehavior.Restrict);

      builder.HasOne(e => e.Class)
             .WithMany(c => c.Enrollments)
             .HasForeignKey(e => e.ClassId)
             .OnDelete(DeleteBehavior.Restrict);

      builder.HasIndex(e => new { e.StudentId, e.ClassId, e.Status })
             .HasDatabaseName("ix_enrollments_student_class_status");
    }
  }
}
