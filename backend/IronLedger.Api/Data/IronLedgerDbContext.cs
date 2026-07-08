using IronLedger.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IronLedger.Api.Data;

public class IronLedgerDbContext : DbContext
{
    public IronLedgerDbContext(DbContextOptions<IronLedgerDbContext> options) : base(options)
    {
    }

	public DbSet<Athlete> Athletes { get; set; } = null!;
	public DbSet<Meet> Meets { get; set; } = null!;
	public DbSet<Attempt> Attempts { get; set; } = null!;

}