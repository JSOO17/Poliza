using Microsoft.EntityFrameworkCore;

namespace Poliza.DataAccess.Models
{
    public partial class PolizaContext : DbContext
    {
        public PolizaContext()
        {
        }

        public PolizaContext(DbContextOptions<PolizaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Policy> Policy { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Policy>(entity =>
            {
                entity.Property(e => e.DateEnd).HasColumnType("datetime");

                entity.Property(e => e.DateExpired).HasColumnType("datetime");

                entity.Property(e => e.DateInit).HasColumnType("datetime");

                entity.Property(e => e.Placa)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Policy)
                    .HasForeignKey(d => d.CityId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
