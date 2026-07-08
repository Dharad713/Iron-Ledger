using IronLedger.Api.Models.Entities;

namespace IronLedger.Api.Models.DataTransferObjects;

public class AddAttemptDto
{
    public required Guid MeetId { get; set; }
    public required Guid AthleteId { get; set; }
    public required LiftType LiftType { get; set; }
    public required int AttemptNum { get; set; }
    public required int Weight { get; set; }
    public required Result Result { get; set; }
}