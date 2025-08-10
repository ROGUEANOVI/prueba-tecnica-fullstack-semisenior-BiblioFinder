using BiblioFinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BiblioFinder.Infrastructure.Persistence
{
    public class BiblioFinderDbContext : DbContext
    {
        public BiblioFinderDbContext(DbContextOptions<BiblioFinderDbContext> options) : base(options)
        {
        }

        public DbSet<SearchHistory> SearchHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchHistory>(entity =>
            {
                entity.ToTable("HistorialBusquedas");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Author).HasColumnName("Autor").IsRequired().HasMaxLength(255);
                entity.Property(e => e.Title).HasColumnName("Titulo").IsRequired().HasMaxLength(255);
                entity.Property(e => e.PublicationYear).HasColumnName("AnioPublicacion");
                entity.Property(e => e.Publisher).HasColumnName("Editorial").HasMaxLength(255);
                entity.Property(e => e.QueryDate).HasColumnName("FechaConsulta").HasDefaultValueSql("getutcdate()");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
