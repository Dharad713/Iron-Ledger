namespace IronLedger.Api.Models.Entities;

public class Attempt // do I want to have equipstatus in this class? currently Its a lookup from meet
{
    public Guid AttemptId { get; set; }
    public Guid MeetId { get; set; }
    public Guid AthleteId { get; set; }
    public required LiftType LiftType { get; set; }
    public required int AttemptNum { get; set; }
    public required int Weight { get; set; }
    public required Result Result { get; set; }
}