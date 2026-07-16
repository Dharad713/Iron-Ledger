using IronLedger.Api.Models.Entities.MeetObjects;

namespace IronLedger.Api.Models.Entities;

public class Meet
{
    public Guid MeetId { get; set; }
    public required string MeetName { get; set; }
    public required DateTime Date { get; set; }
    public string? Location { get; set; }

    public string? Federation { get; set; } = string.Empty;
    public required EquipmentStatus EquipmentStatus { get; set; }

    public required MeetStatus MeetStatus { get; set; }

    public DateTime RegistrationOpensAt { get; set; }
    public DateTime RegistrationClosesAt { get; set; }

    public ICollection<MeetWeightClass> WeightClasses { get; set; } = [];
    public ICollection<MeetDivision> Divisions { get; set; } = [];
    public ICollection<MeetRegistration> Registrations { get; set; } = [];
}

