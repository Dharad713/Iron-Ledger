using IronLedger.Api.Models.Entities;

namespace IronLedger.Api.Models.DataTransferObjects.MeetDtos;

public class MeetRegistrationDto
{
    public Guid MeetRegistrationId { get; set; }
    public Guid MeetId { get; set; }
    public Guid AthleteId { get; set; }

    public Guid MeetWeightClassId { get; set; }
    public required string WeightClassName { get; set; }

    public Guid MeetDivisionId { get; set; }
    public required string DivisionName { get; set; }

    public decimal BodyWeightKg { get; set; }
    public RegistrationStatus Status { get; set; }
}