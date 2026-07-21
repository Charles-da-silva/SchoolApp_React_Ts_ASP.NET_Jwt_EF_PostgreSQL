using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Api.Domain.Entities;

namespace School.Api.Infrastructure.Configurations
{
    public class StudentResponsibleConfiguration : IEntityTypeConfiguration<StudentResponsible>
    {
        public void Configure(EntityTypeBuilder<StudentResponsible> builder)
        {
            builder.ToTable("student_responsibles");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.StudentId).HasColumnName("student_id");
            builder.Property(x => x.ResponsibleId).HasColumnName("responsible_id");

            builder.Property(x => x.RelationshipType)
                   .HasConversion<string>()
                   .HasMaxLength(50)
                   .HasColumnName("relationship_type");

            builder.Property(x => x.CanPickUpChild).HasColumnName("can_pick_up_child");
            builder.Property(x => x.IsFinanceContact).HasColumnName("is_finance_contact");
            builder.Property(x => x.IsLegalResponsable).HasColumnName("is_legal_responsable");
            builder.Property(x => x.Observation).HasMaxLength(1000).HasColumnName("observation");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at");

            builder.HasIndex(x => new { x.StudentId, x.ResponsibleId }).IsUnique();

            builder.HasOne(sr => sr.Student)
                   .WithMany()
                   .HasForeignKey(sr => sr.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sr => sr.Responsible)
                   .WithMany(r => r.StudentResponsibles)
                   .HasForeignKey(sr => sr.ResponsibleId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}