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
            Team = addAthleteDto.Team
        };

        _context.Athletes.Add(athleteEntity); // this does not sync anything up, just creates the action
        _context.SaveChanges();

        return Ok(athleteEntity); // Ok returns  200 as the return type, but 201 might be better
    }

    [HttpPut]
    [Route("{id:guid}")]
    public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
    {
        var athlete = _context.Athletes.Find(id);
        if (athlete is null)
        {
            return NotFound();
        }

        athlete.Name = updateEmployeeDto.Name;
        athlete.BodyWeight = updateEmployeeDto.BodyWeight;
        athlete.Sex = updateEmployeeDto.Sex;
        athlete.WeightClass = updateEmployeeDto.WeightClass;
        athlete.Division = updateEmployeeDto.Division;
        athlete.Team = updateEmployeeDto.Team;

        _context.SaveChanges();
        return Ok(athlete);

    }

    [HttpDelete]
    [Route("{id:guid}")]
    public IActionResult DeleteAthlete(Guid id)
    {
        var athlete = _context.Athletes.Find(id);
        if (athlete is null)
        {
            return NotFound();
        }

        _context.Athletes.Remove(athlete);
        _context.SaveChanges();
        return Ok();

    }

}