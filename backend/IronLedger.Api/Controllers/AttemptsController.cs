using IronLedger.Api.Data;
using IronLedger.Api.Models.DataTransferObjects;
using IronLedger.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IronLedger.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // localhost:xxxx/api/attempts
public class AttemptsController : ControllerBase
{
    private readonly IronLedgerDbContext _context;

    public AttemptsController(IronLedgerDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public IActionResult GetAllAttempts() // turn these into async calls later
    {
        var allAttempts = _context.Attempts.ToList();

        return Ok(allAttempts);
    }


    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetAttemptById(Guid id)
    {
        var attempt = _context.Attempts.Find(id);

        if (attempt is null)
        {
            return NotFound("Attempt was not found in database!");
        }

        return Ok(attempt);
    }


    [HttpPost]
    public IActionResult AddAttempt(AddAttemptDto addAttemptDto)
    {
        var attemptEntity = new Attempt()
        {
            MeetId = addAttemptDto.MeetId,
            AthleteId = addAttemptDto.AthleteId,
            LiftType = addAttemptDto.LiftType,
            AttemptNum = addAttemptDto.AttemptNum,
            Weight = addAttemptDto.Weight,
            Result = addAttemptDto.Result
        };

        _context.Attempts.Add(attemptEntity);
        _context.SaveChanges();

        return Ok(attemptEntity);
    }


    [HttpPut]
    [Route("{id:guid}")]
    public IActionResult UpdateAttempt(
        Guid id,
        UpdateAttemptDto updateAttemptDto)
    {
        var attempt = _context.Attempts.Find(id);

        if (attempt is null)
        {
            return NotFound();
        }

        attempt.LiftType = updateAttemptDto.LiftType;
        attempt.AttemptNum = updateAttemptDto.AttemptNum;
        attempt.Weight = updateAttemptDto.Weight;
        attempt.Result = updateAttemptDto.Result;

        _context.SaveChanges();

        return Ok(attempt);
    }

    // TODO: turn this into archive rather than delete
    // [HttpDelete]
    // [Route("{id:guid}")]
    // public IActionResult DeleteAttempt(Guid id)
    // {
    //     var attempt = _context.Attempts.Find(id);
    //
    //     if (attempt is null)
    //     {
    //         return NotFound();
    //     }
    //
    //     _context.Attempts.Remove(attempt);
    //     _context.SaveChanges();
    //
    //     return Ok();
    // }
}