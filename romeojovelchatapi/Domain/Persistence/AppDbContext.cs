using Microsoft.EntityFrameworkCore;
using romeojovelchatapi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.Domain.Persistence
{
    public class AppDbContext : DbContext
    {

        public DbSet<TbUser> TbUser { get; set; }
        public DbSet<TbChatMessage> TbChatMessage { get; set; }
        public DbSet<TbChatRoom> TbChatRoom { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public async Task<int> SaveChangesAsync()
        {
            AddAuitInfo();
            return await base.SaveChangesAsync();
        }

        private void AddAuitInfo()
        {

            var entries = ChangeTracker.Entries()
               .Where(x =>
               (x.Entity is AuditedEntity)
               && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((AuditedEntity)entry.Entity).DtCreated = DateTime.UtcNow;
                }
                //((AuditedEntity)entry.Entity).DtAuditModified = DateTime.UtcNow;
            }
        }
    }
}
