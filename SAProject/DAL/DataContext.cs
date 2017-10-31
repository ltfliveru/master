using SAProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SAProject.DAL
{
    public class DataContext : DbContext
    {
        public DataContext() : base("University")
        {
            
        }
        public DbSet<Student> Students { get; set; }
        
      
        public override int SaveChanges()
        {
            /*
             Time tracking of Added / Modified Entity
             */
            var addedAuditedEntities = ChangeTracker.Entries<IAuditedEntity>()
              .Where(p => p.State == EntityState.Added)
              .Select(p => p.Entity);

            var modifiedAuditedEntities = ChangeTracker.Entries<IAuditedEntity>()
              .Where(p => p.State == EntityState.Modified)
              .Select(p => p.Entity);

            var now = DateTime.UtcNow;

            foreach (var added in addedAuditedEntities)
            {
                added.CreatedAt = now; 
            }            

            foreach (var modified in modifiedAuditedEntities)
            {
                modified.UpdatedAt = now;
            }

            return base.SaveChanges();
        }
    }
}