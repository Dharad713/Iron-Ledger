using IronLedger.AthleteService.Models;
using Microsoft.EntityFrameworkCore;

namespace IronLedger.AthleteService.Data;

public class AthleteDbContext : DbContext
{
    public AthleteDbContext(
        DbContextOptions<AthleteDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Athlete> Athletes { get; set; } = null!;
}