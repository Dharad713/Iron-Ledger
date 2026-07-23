using IronLedger.AthleteService.Data;
using IronLedger.AthleteService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AthleteDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AthleteDatabase")));

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddScoped<AthleteCommandHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<AthleteGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
