using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WorkerMan.CrossCutting.Entities.Identity;
using WorkerMan.CrossCutting.Entities.Interfaces;
using WorkerMan.CrossCutting.Entities.Models;

namespace WorkerMan.CrossCutting.Contexts
{
    public class WorkerManContext : IdentityDbContext<WorkerManUser, WorkerManRole, string>
    {
        public WorkerManContext(DbContextOptions<WorkerManContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(WorkerManContext).Assembly);

            base.OnModelCreating(builder);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            UpdateEntriesBeforeSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateEntriesBeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        private void UpdateEntriesBeforeSave()
        {
            IEntity entity = null;
            foreach (var entry in ChangeTracker.Entries())
            {
                entity = entry.Entity as IEntity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.Now;
                    entity.UpdatedAt = DateTime.Now;
                    entity.IsActive = true;
                }

                else if (entry.State == EntityState.Modified)
                    entity.UpdatedAt = DateTime.Now;

                else if (entry.State == EntityState.Deleted)
                    entity.IsActive = false;
            }
        }

    }
}
