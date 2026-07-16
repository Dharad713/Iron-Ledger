namespace IronLedger.Api.Models.Entities.MeetObjects;

public class MeetRegistration
{
    public Guid MeetRegistrationId { get; set; }

    public Guid MeetId { get; set; }
    public Guid AthleteId { get; set; }

    public Guid MeetWeightClassId { get; set; }
    public MeetWeightClass MeetWeightClass { get; set; } = null!;

    public Guid MeetDivisionId { get; set; }
    public MeetDivision MeetDivision { get; set; } = null!;

    public decimal BodyWeightKg { get; set; }

    public RegistrationStatus Status { get; set; }
}