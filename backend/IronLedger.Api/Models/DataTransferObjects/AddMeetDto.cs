using IronLedger.Api.Models.DataTransferObjects.MeetDtos;
using IronLedger.Api.Models.Entities;

namespace IronLedger.Api.Models.DataTransferObjects;

public class AddMeetDto
{
    public required string MeetName { get; set; }
    public required DateTime Date { get; set; }

    public string? Location { get; set; }
    public string? Federation { get; set; }

    public required EquipmentStatus EquipmentStatus { get; set; }
    public required MeetStatus MeetStatus { get; set; }

    public DateTime RegistrationOpensAt { get; set; }
    public DateTime RegistrationClosesAt { get; set; }

    public ICollection<AddMeetWeightClassDto> WeightClasses { get; set; } = [];
    public ICollection<AddMeetDivisionDto> Divisions { get; set; } = [];
}