using IronLedger.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace IronLedger.Api.Data;

public class IronLedgerDbContext : DbContext
{
    public IronLedgerDbContext(DbContextOptions<IronLedgerDbContext> options) : base(options)
    {
    }

    public DbSet<Athlete> Athletes => Set<Athlete>();

    public DbSet<Meet> Meets => Set<Meet>();

    public DbSet<Attempt> Attempts => Set<Attempt>();

}