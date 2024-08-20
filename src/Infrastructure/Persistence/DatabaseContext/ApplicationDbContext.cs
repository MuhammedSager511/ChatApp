using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            foreach(var entity in ChangeTracker.Entries<BaseEntity>())
            {
                if (entity.State ==EntityState.Modified)
                {
                    entity.Entity.ModifiedData = DateOnly.FromDateTime(DateTime.UtcNow);
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        public DbSet<Message> Messages { get; set; }
    }
}
