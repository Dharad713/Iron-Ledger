using IronLedger.Api.Data;
using IronLedger.Api.Models;
using IronLedger.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IronLedger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttemptsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllAttempts()
    {
        return Ok("Attempts API works");
    }
}