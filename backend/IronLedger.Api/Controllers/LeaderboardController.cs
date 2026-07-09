using IronLedger.Api.Data;
using IronLedger.Api.Models.DataTransferObjects;
using IronLedger.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IronLedger.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // localhost:xxxx/api/leaderboard
public class LeaderboardController : ControllerBase
{
    private readonly IronLedgerDbContext _context;

    public LeaderboardController(IronLedgerDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult GetLeaderBoard()
    {
        var allLeaderboardEntries = _context.Leaderboard.ToList();
        return Ok(allLeaderboardEntries);
    }
}