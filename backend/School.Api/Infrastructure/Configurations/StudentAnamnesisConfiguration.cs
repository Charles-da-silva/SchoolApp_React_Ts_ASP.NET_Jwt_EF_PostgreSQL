using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace School.Api.Infrastructure.Configurations
{
  public class StudentAnamnesisConfiguration : IEntityTypeConfiguration<StudentAnamnesis>
  {
    public void Configure(EntityTypeBuilder<StudentAnamnesis> builder)
    {
      builder.ToTable("student_anamneses");

      builder.HasKey(a => a.Id);

      builder.Property(a => a.Content)
             .HasMaxLength(5000);

      builder.HasOne(a => a.Student)
             .WithOne(s => s.Anamnesis)
             .HasForeignKey<StudentAnamnesis>(a => a.StudentId)
             .OnDelete(DeleteBehavior.Cascade);
      /* DeleteBehavior.Cascade deleta (hard) a anamnese 
      se o student for deletado */

      /* garantindo que o banco não permitirá duas anamneses para
      o mesmo aluno. */
      builder.HasIndex(a => a.StudentId)
             .IsUnique();
    }
  }
}
