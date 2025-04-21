using Dg.Pays.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dg.Pays.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .OwnsOne(s => s.CardNumber, cb =>
                {
                    cb.Property(cn => cn.Value)
                      .HasColumnName("CardNumber")
                      .IsRequired();
                });

            modelBuilder.Entity<Transaction>()
                .OwnsOne(s => s.CardScheme, cs =>
                {
                    cs.Property(cst => cst.Name)
                    .HasColumnName("CardScheme")
                    .IsRequired();
                });

            modelBuilder.Entity<Transaction>()
                .OwnsOne(s => s.ExpiryDate, cb =>
                {
                    cb.Property(cn => cn.ExpiryDate)
                    .HasColumnName("ExpiryDate")
                    .IsRequired();
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
