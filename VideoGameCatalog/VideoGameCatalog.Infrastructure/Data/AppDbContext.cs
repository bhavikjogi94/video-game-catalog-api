using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.Core.Models;

namespace VideoGameCatalog.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<VideoGame> VideoGames => Set<VideoGame>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Platform> Platforms => Set<Platform>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ── Genre ────────────────────────────────────────────────────────────
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Name).IsRequired().HasMaxLength(100);

            // Unique index — no two genres can share the same name
            entity.HasIndex(g => g.Name).IsUnique();
        });

        // ── Platform ─────────────────────────────────────────────────────────
        modelBuilder.Entity<Platform>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Manufacturer).IsRequired().HasMaxLength(100);

            // Unique index — no two platforms can share the same name
            entity.HasIndex(p => p.Name).IsUnique();
        });

        // ── VideoGame ─────────────────────────────────────────────────────────
        modelBuilder.Entity<VideoGame>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Title).IsRequired().HasMaxLength(200);
            entity.Property(g => g.Developer).IsRequired().HasMaxLength(150);
            entity.Property(g => g.Description).HasMaxLength(1000);

            // Index on Title to support fast search/filter queries
            entity.HasIndex(g => g.Title);

            // Composite index on (GenreId, ReleaseYear) — speeds up
            // browse-by-genre + sort-by-year queries
            entity.HasIndex(g => new { g.GenreId, g.ReleaseYear });

            // Foreign key: VideoGame → Genre (cascade: deleting a genre removes its games)
            entity.HasOne(g => g.Genre)
                  .WithMany(genre => genre.VideoGames)
                  .HasForeignKey(g => g.GenreId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Foreign key: VideoGame → Platform (restrict: can't delete a platform that has games)
            entity.HasOne(g => g.Platform)
                  .WithMany(p => p.VideoGames)
                  .HasForeignKey(g => g.PlatformId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
