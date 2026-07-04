using IronLedger.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<IronLedgerDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("IronLedgerDb")
    )
);

var app = builder.Build();

app.MapControllers();

app.Run();