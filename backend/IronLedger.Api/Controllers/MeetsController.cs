using IronLedger.Api.Data;
using IronLedger.Api.Models.DataTransferObjects;
using IronLedger.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IronLedger.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // localhost:xxxx/api/meets
public class MeetsController : ControllerBase
{
    private readonly IronLedgerDbContext _context;

    public MeetsController(IronLedgerDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public IActionResult GetAllMeets() // turn these into async calls later
    {
        var allMeets = _context.Meets.ToList();

        return Ok(allMeets);
    }


    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetMeetById(Guid id)
    {
        var meet = _context.Meets.Find(id);

        if (meet is null)
        {
            return NotFound("Meet was not found in database!");
        }

        return Ok(meet);
    }


    [HttpPost]
    public IActionResult AddMeet(AddMeetDto addMeetDto)
    {
        var meetEntity = new Meet()
        {
            MeetName = addMeetDto.MeetName,
            Date = addMeetDto.Date,
            Federation = addMeetDto.Federation,
            MeetStatus = addMeetDto.MeetStatus,
            EquipmentStatus = addMeetDto.EquipmentStatus
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
        meet.Federation = updateMeetDto.Federation;
        meet.MeetStatus = updateMeetDto.MeetStatus;
        meet.EquipmentStatus = updateMeetDto.EquipmentStatus;
        _context.SaveChanges();

        return Ok(meet);
    }

    // TODO: turn this into archive rather than delete
    // [HttpDelete]
    // [Route("{id:guid}")]
    // public IActionResult DeleteMeet(Guid id)
    // {
    //     var meet = _context.Meets.Find(id);
    //
    //     if (meet is null)
    //     {
    //         return NotFound();
    //     }
    //
    //     _context.Meets.Remove(meet);
    //     _context.SaveChanges();
    //
    //     return Ok();
    // }
}