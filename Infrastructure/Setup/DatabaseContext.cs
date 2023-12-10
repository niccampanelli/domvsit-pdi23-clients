using Domain.Entities.Attendant;
using Domain.Entities.Client;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Setup
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<AttendantEntity> Attendants { get; set; }
        public virtual DbSet<AttendantTokenEntity> AttendantTokens { get; set; }
        public virtual DbSet<ClientEntity> Clients { get; set; }

        public DatabaseContext(
            DbContextOptions<DatabaseContext> options
        ) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AttendantEntity>()
                .HasOne(a => a.Client)
                .WithMany();
        }
    }
}
