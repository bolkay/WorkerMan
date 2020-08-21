using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerMan.CrossCutting.Entities.Models;

namespace WorkerMan.CrossCutting.Configurations
{
    public class WorkerCompanyConfiguration : IEntityTypeConfiguration<WorkerCompany>
    {
        public void Configure(EntityTypeBuilder<WorkerCompany> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.EstimatedStaffStrength)
                .HasColumnName("Estimated Staff Strength")
                .IsRequired(true);

            builder.Property(x => x.OfficalLogo)
                .HasColumnName("Official Logo");

            builder.Property(x => x.OfficalOfficeAddress)
                .HasColumnName("Office Address")
                .IsRequired(true);

        }
    }
}
