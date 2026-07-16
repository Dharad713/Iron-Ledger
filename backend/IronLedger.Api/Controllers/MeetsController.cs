using IronLedger.Api.Data;
using IronLedger.Api.Models.DataTransferObjects;
using IronLedger.Api.Models.Entities;
using IronLedger.Api.Models.Entities.MeetObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IronLedger.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MeetsController : ControllerBase
{
    private readonly IronLedgerDbContext _context;

    public MeetsController(IronLedgerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllMeets()
    {
        var allMeets = _context.Meets
            .Include(meet => meet.WeightClasses)
            .Include(meet => meet.Divisions)
            .ToList();

        return Ok(allMeets);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetMeetById(Guid id)
    {
        var meet = _context.Meets
            .Include(meet => meet.WeightClasses)
            .Include(meet => meet.Divisions)
            .FirstOrDefault(meet => meet.MeetId == id);

        if (meet is null)
        {
            return NotFound("Meet was not found in database!");
        }

        return Ok(meet);
    }

    [HttpPost]
    public IActionResult AddMeet(AddMeetDto addMeetDto)
    {
        var meetId = Guid.NewGuid();

        var meetEntity = new Meet
        {
            MeetId = meetId,
            MeetName = addMeetDto.MeetName,
            Date = addMeetDto.Date,
            Location = addMeetDto.Location,
            Federation = addMeetDto.Federation,
            MeetStatus = addMeetDto.MeetStatus,
            EquipmentStatus = addMeetDto.EquipmentStatus,
            RegistrationOpensAt = addMeetDto.RegistrationOpensAt,
            RegistrationClosesAt = addMeetDto.RegistrationClosesAt,

            WeightClasses = addMeetDto.WeightClasses
                .Select(weightClass => new MeetWeightClass
                {
                    MeetWeightClassId = Guid.NewGuid(),
                    MeetId = meetId,
                    WeightClassName = weightClass.WeightClassName,
                    Sex = weightClass.Sex,
                    MinimumWeightKg = weightClass.MinimumWeightKg,
                    MaximumWeightKg = weightClass.MaximumWeightKg
                })
                .ToList(),

            Divisions = addMeetDto.Divisions
                .Select(division => new MeetDivision
                {
                    MeetDivisionId = Guid.NewGuid(),
                    MeetId = meetId,
                    MeetDivisionName = division.MeetDivisionName,
                    Sex = division.Sex,
                    MinimumAge = division.MinimumAge,
                    MaximumAge = division.MaximumAge
                })
                .ToList()
        };

        _context.Meets.Add(meetEntity);
        _context.SaveChanges();

        return Ok(meetEntity);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public IActionResult UpdateMeet(
        Guid id,
        UpdateMeetDto updateMeetDto)
    {
        var meet = _context.Meets.Find(id);

        if (meet is null)
        {
            return NotFound();
        }

        meet.MeetName = updateMeetDto.MeetName;
        meet.Date = updateMeetDto.Date;
        meet.Location = updateMeetDto.Location;
        meet.Federation = updateMeetDto.Federation;
        meet.MeetStatus = updateMeetDto.MeetStatus;
        meet.EquipmentStatus = updateMeetDto.EquipmentStatus;
        meet.RegistrationOpensAt = updateMeetDto.RegistrationOpensAt;
        meet.RegistrationClosesAt = updateMeetDto.RegistrationClosesAt;

        _context.SaveChanges();

        return Ok(meet);
    }
}
