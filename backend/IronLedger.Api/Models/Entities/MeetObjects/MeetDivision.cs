namespace IronLedger.Api.Models.Entities.MeetObjects;

public class MeetDivision
{
    public Guid MeetDivisionId { get; set; }
    public Guid MeetId { get; set; }
    public required string MeetDivisionName { get; set; }
    public required Sex Sex { get; set; }

    public int? MinimumAge { get; set; }
    public int? MaximumAge { get; set; }
}