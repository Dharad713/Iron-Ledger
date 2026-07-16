using IronLedger.Api.Models.Entities;
using IronLedger.Api.Models.Entities.MeetObjects;
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

    public DbSet<MeetWeightClass> MeetWeightClasses { get; set; }

    public DbSet<MeetDivision> MeetDivisions { get; set; }

    public DbSet<MeetRegistration> MeetRegistrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<LeaderboardEntry>() // athlete id should be the primary key for leaderboards 
            .HasKey(entry => entry.AthleteId);

        modelBuilder.Entity<MeetRegistration>()
            .HasOne<Meet>()
            .WithMany(meet => meet.Registrations)
            .HasForeignKey(registration => registration.MeetId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MeetRegistration>()
            .HasOne(registration => registration.MeetWeightClass)
            .WithMany()
            .HasForeignKey(registration => registration.MeetWeightClassId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<MeetRegistration>()
            .HasOne(registration => registration.MeetDivision)
            .WithMany()
            .HasForeignKey(registration => registration.MeetDivisionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}