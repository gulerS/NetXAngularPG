using Microsoft.EntityFrameworkCore;
using NetXAngularPG.Domain.Entities;
using NetXAngularPG.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetXAngularPG.Persistance.Contexts
{
    public class NetXAngularPGDbContext : DbContext
    {
        public NetXAngularPGDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var records = ChangeTracker.Entries<BaseEntity>();

            foreach (var record in records)
            {
                _ = record.State switch
                {
                    EntityState.Added => record.Entity.CreatedDate = DateTime.Now,
                    EntityState.Modified => record.Entity.UpdateDate = DateTime.Now,
                    _ => DateTime.Now
                };
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
