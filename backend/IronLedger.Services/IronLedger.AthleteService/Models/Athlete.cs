using IronLedger.Contracts.Common;

namespace IronLedger.AthleteService.Models;

public class Athlete
{
    public Guid AthleteId { get; set; }
    public required string Name { get; set; }
    public required decimal BodyWeightKg { get; set; }
    public required Sex Sex { get; set; }
    public required DateOnly DateOfBirth { get; set; }
    public string? Team { get; set; }
    public bool IsArchived { get; set; }
}