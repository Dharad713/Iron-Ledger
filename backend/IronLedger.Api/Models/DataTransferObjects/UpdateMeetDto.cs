using IronLedger.Api.Models.Entities;

namespace IronLedger.Api.Models.DataTransferObjects;

public class UpdateMeetDto
{
    public string MeetName { get; set; }

    public DateTime Date { get; set; }

    public string Location { get; set; }

    public string Federation { get; set; }

    public EquipmentStatus EquipmentStatus { get; set; }

    public MeetStatus MeetStatus { get; set; }

    public DateTime RegistrationOpensAt { get; set; }

    public DateTime RegistrationClosesAt { get; set; }
}