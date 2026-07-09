namespace IronLedger.Api.Models.Entities;

public class Athlete
{
    public Guid AthleteId { get; set; }
    public required string Name { get; set; }
    public required decimal BodyWeight { get; set; }
    public required Sex Sex { get; set; }
    public string? WeightClass { get; set; }
    public string? Division { get; set; }
    public string? Team { get; set; }
}