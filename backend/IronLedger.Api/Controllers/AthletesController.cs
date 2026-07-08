using IronLedger.Api.Data;
using IronLedger.Api.Models;
using IronLedger.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IronLedger.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // localhost:xxxx/api/athletes
public class AthletesController : ControllerBase
{
    private readonly IronLedgerDbContext dbContext;
    public AthletesController(IronLedgerDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    // [HttpGet]
    // public IActionResult GetAllAthletes()
    // {
    //     dbContext.Athletes
    //
    // }
}