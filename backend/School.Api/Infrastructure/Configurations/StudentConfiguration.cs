using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Api.Domain.Entities;

/* Essa classe é responsável por configurar a entidade Student para o banco via Entity Framework Core, 
usando a Fluent API., registrado lá no SchoolDbContext.cs*/

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.FullName)
               .IsRequired()
               .HasMaxLength(200);
        
        builder.Property(s => s.DateOfBirth)
               .IsRequired()
               .HasMaxLength(8);

        builder.Property(s => s.Cpf)
               .HasMaxLength(11);

        builder.Property(s => s.BirthCertificateNumber)
               .HasMaxLength(32);
        
        builder.Property(s => s.Email)
               .HasMaxLength(100);

        // índice único para CPF
        builder.HasIndex(s => s.Cpf)
               .IsUnique();

        // índice único para certidão
        builder.HasIndex(s => s.BirthCertificateNumber)
               .IsUnique();
    }
}