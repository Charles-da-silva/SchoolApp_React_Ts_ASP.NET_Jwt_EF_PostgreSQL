using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Api.Domain.Entities;

namespace School.Api.Infrastructure.Configurations
{
    public class ResponsibleConfiguration : IEntityTypeConfiguration<Responsible>
    {
        public void Configure(EntityTypeBuilder<Responsible> builder)
        {
            builder.ToTable("responsibles");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName).IsRequired().HasMaxLength(250).HasColumnName("full_name");
            builder.Property(x => x.Cpf).HasMaxLength(30).HasColumnName("cpf");
            builder.Property(x => x.Email).HasMaxLength(200).HasColumnName("email");
            builder.Property(x => x.Phone).HasMaxLength(50).HasColumnName("phone");
            builder.Property(x => x.Profession).HasMaxLength(150).HasColumnName("profession");
            builder.Property(x => x.Address).HasMaxLength(500).HasColumnName("address");
            builder.Property(x => x.IsActive).HasColumnName("is_active");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at");
            builder.Property(x => x.DeactivatedAt).HasColumnName("deactivated_at");

            builder.HasMany(r => r.StudentResponsibles)
                   .WithOne(sr => sr.Responsible)
                   .HasForeignKey(sr => sr.ResponsibleId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}