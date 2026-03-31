using DataEntrySystem.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataEntrySystem.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Revenue> Revenues { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Additional configurations if needed
            modelBuilder.Entity<Revenue>()
                .Property(r => r.ContractPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Revenue>()
                .Property(r => r.OfferPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Revenue>()
                .Property(r => r.PaidAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Expense>()
                .Property(e => e.Amount)
                .HasPrecision(18, 2);
        }
    }
}
