using IronLedger.Api.Models.Entities;

namespace IronLedger.Api.Models.DataTransferObjects;

public class UpdateMeetDto
{
    public required string MeetName { get; set; }
    public required DateTime Date { get; set; }
    public required string Federation { get; set; }
    public required MeetStatus MeetStatus { get; set; }
    public required EquipmentStatus EquipmentStatus { get; set; }
}