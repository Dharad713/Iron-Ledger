using IronLedger.Api.Data;
using IronLedger.Api.Models.DataTransferObjects;
using IronLedger.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IronLedger.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // localhost:xxxx/api/athletes
public class AthletesController : ControllerBase
{
    private readonly IronLedgerDbContext _context;
    public AthletesController(IronLedgerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllAthletes() // turn these into async calls
    {
        var allAthletes = _context.Athletes.ToList();
        return Ok(allAthletes);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetAthleteById(Guid id)
    {
        var athlete = _context.Athletes.Find(id);

        if (athlete is null)
        {
            return NotFound("Athlete was not found in database!");
        }

        return Ok(athlete);
    }

    [HttpPost]
    public IActionResult AddAthlete(AddAthleteDto addAthleteDto)
    {
        var athleteEntity = new Athlete()
        {
            Name = addAthleteDto.Name,
            BodyWeight = addAthleteDto.BodyWeight,
            Sex = addAthleteDto.Sex,
            WeightClass = addAthleteDto.WeightClass,
            Division = addAthleteDto.Division,
            Team = addAthleteDto.Team,
            IsArchived = addAthleteDto.IsArchived,
        };

        _context.Athletes.Add(athleteEntity); // this does not sync anything up, just creates the action
        _context.SaveChanges();

        return Ok(athleteEntity); // Ok returns  200 as the return type, but 201 might be better
    }

    [HttpPut]
    [Route("{id:guid}")]
    public IActionResult UpdateAthlete(Guid id, UpdateAthleteDto updateAthleteDto)
    {
        var athlete = _context.Athletes.Find(id);
        if (athlete is null)
        {
            return NotFound();
        }

        athlete.Name = updateAthleteDto.Name;
        athlete.BodyWeight = updateAthleteDto.BodyWeight;
        athlete.Sex = updateAthleteDto.Sex;
        athlete.WeightClass = updateAthleteDto.WeightClass;
        athlete.Division = updateAthleteDto.Division;
        athlete.Team = updateAthleteDto.Team;
        athlete.IsArchived = updateAthleteDto.IsArchived;

        _context.SaveChanges();
        return Ok(athlete);

    }
    
    // TODO: turn this into archive rather than delete
    
    // [HttpDelete]
    // [Route("{id:guid}")]
    // public IActionResult DeleteAthlete(Guid id)
    // {
    //     var athlete = _context.Athletes.Find(id);
    //     if (athlete is null)
    //     {
    //         return NotFound();
    //     }
    //
    //     _context.Athletes.Remove(athlete);
    //     _context.SaveChanges();
    //     return Ok();
    //
    // }

}