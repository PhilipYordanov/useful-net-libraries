using MediatRFluentDemo.Models;
using MediatRFluentDemo.Models.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRFluentDemo.Context
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
        }

        public DbSet<Milestone> Milestones { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        //  current user user info 
                        entry.Entity.LastModifiedBy = "system";
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "system"; 
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        break;
                    default:
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
