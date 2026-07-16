namespace IronLedger.Api.Models.DataTransferObjects.MeetDtos;

public class RegisterAthleteDto
{
    public required Guid AthleteId { get; set; }

    public required Guid MeetWeightClassId { get; set; }
    public required Guid MeetDivisionId { get; set; }

    public required decimal BodyWeightKg { get; set; }
}