using IronLedger.Api.Models.Entities;

namespace IronLedger.Api.Models.DataTransferObjects;

public class UpdateAttemptDto
{
    public required LiftType LiftType { get; set; }
    public required int AttemptNum { get; set; }
    public required int Weight { get; set; }
    public required Result Result { get; set; }
}