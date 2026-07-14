using IronLedger.Api.Models.Entities;

namespace IronLedger.Api.Models.DataTransferObjects;

public class UpdateAthleteDto
{
    public required string Name { get; set; }
    public required decimal BodyWeight { get; set; }
    public required Sex Sex { get; set; }
    public string? WeightClass { get; set; }
    public string? Division { get; set; }
    public string? Team { get; set; }
    public bool IsArchived { get; set; } = false;
}

