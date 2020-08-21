using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkerMan.CrossCutting.Entities.Identity;
namespace WorkerMan.CrossCutting.Configurations
{
    public class WorkerManUserConfiguration : IEntityTypeConfiguration<WorkerManUser>
    {
        public void Configure(EntityTypeBuilder<WorkerManUser> builder)
        {
            builder.HasKey(key => key.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(x => x.AffiliatedTo)
                .WithMany(x => x.StaffMembers)
                .HasForeignKey(x => x.AffiliatedToId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.WorksDone)
                .WithOne(x => x.WorkerManUser)
                .HasForeignKey(x => x.WorkerManUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
