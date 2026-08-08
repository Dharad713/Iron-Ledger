using IronLedger.MeetService.Models;
using Microsoft.EntityFrameworkCore;

namespace IronLedger.MeetService.Data;

public class MeetDbContext : DbContext
{
    public MeetDbContext(
        DbContextOptions<MeetDbContext> options)
        : base(options)
    {
    }

    public DbSet<Meet> Meets { get; set; } = null!;
}