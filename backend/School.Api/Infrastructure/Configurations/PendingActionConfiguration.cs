using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Api.Domain.Entities;

namespace School.Api.Infrastructure.Configurations
{
  public class PendingActionConfiguration : IEntityTypeConfiguration<PendingAction>
  {
    public void Configure(EntityTypeBuilder<PendingAction> builder)
    {
      builder.ToTable("pending_action");

      builder.HasKey(p => p.Id);

      builder.Property(p => p.Description)
             .IsRequired()
             .HasMaxLength(1000);

      builder.HasOne(p => p.Student)
             .WithMany()
             .HasForeignKey(p => p.StudentId)
             .OnDelete(DeleteBehavior.SetNull);

      builder.HasOne(p => p.Enrollment)
             .WithMany(e => e.PendingActions)
             .HasForeignKey(p => p.EnrollmentId)
             .OnDelete(DeleteBehavior.SetNull);

      builder.HasIndex(p => p.Status)
             .HasDatabaseName("ix_pending_action_status");

      builder.HasIndex(p => p.DueDate)
             .HasDatabaseName("ix_pending_action_due_date");
    }
  }
}
