using IronLedger.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IronLedger.Api.Data;

public class IronLedgerDbContext : DbContext
{
    public IronLedgerDbContext(
        DbContextOptions<IronLedgerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Athlete> Athletes { get; set; } = null!;
    public DbSet<Meet> Meets { get; set; } = null!;
    public DbSet<Attempt> Attempts { get; set; } = null!;
    public DbSet<LeaderboardEntry> Leaderboard { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) // athlete id should be the primary key for leaderboards 
    {
        modelBuilder.Entity<LeaderboardEntry>()
            .HasKey(entry => entry.AthleteId);
    }
}