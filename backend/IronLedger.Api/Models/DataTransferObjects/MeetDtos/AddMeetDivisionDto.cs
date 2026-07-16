using IronLedger.Api.Models.Entities;

namespace IronLedger.Api.Models.DataTransferObjects.MeetDtos;

public class AddMeetDivisionDto
{
    public required string MeetDivisionName { get; set; }
    public required Sex Sex { get; set; }

    public int? MinimumAge { get; set; }
    public int? MaximumAge { get; set; }
}