using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using FintecDemo.API.Configuration.Database;
using FintecDemo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FintecDemo.API.Database
{
    public class FintecDbContext : DbContext
    {
        public FintecDbContext(DbContextOptions<FintecDbContext> options)
            : base(options)
        {
        }

        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<Exchange>()
                .ConfigureExchange(modelBuilder)
                .Entity<Stock>()
                .ConfigureStock(modelBuilder)
                .Entity<CompanyProfile>()
                .ConfigureCompanyProfile(modelBuilder)
                .Entity<Person>()
                .ConfigurePerson(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();
            var now = DateTime.UtcNow;
            ChangeTracker.Entries()
                .Where(item => item.State == EntityState.Added && item.Entity is IModificationTracker)
                .ToList()
                .ForEach(item =>
                {
                    item.Entity.GetType().GetField("_created", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item.Entity, now);
                    item.Entity.GetType().GetField("_lastModified", BindingFlags.NonPublic | BindingFlags.Instance)
                        .SetValue(item.Entity, now);
                });
            ChangeTracker.Entries()
                .Where(item => item.State == EntityState.Modified && item.Entity is IModificationTracker)
                .ToList()
                .ForEach(item => item.Entity.GetType().GetField("_lastModified", BindingFlags.NonPublic | BindingFlags.Instance)
                    .SetValue(item.Entity, now));

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
