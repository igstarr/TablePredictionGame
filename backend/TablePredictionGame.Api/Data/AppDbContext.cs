using Microsoft.EntityFrameworkCore;
using TablePredictionGame.Api.Models;

namespace TablePredictionGame.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<League> Leagues => Set<League>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Prediction> Predictions => Set<Prediction>();
    public DbSet<PredictionEntry> PredictionEntries => Set<PredictionEntry>();
    public DbSet<ActualStanding> ActualStandings => Set<ActualStanding>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>()
            .HasOne(t => t.ActualStanding)
            .WithOne(a => a.Team)
            .HasForeignKey<ActualStanding>(a => a.TeamId);

        modelBuilder.Entity<PredictionEntry>()
            .HasIndex(e => new { e.PredictionId, e.TeamId })
            .IsUnique();
    }
}
