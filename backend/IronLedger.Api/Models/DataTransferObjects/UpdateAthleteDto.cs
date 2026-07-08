namespace IronLedger.Api.Models.DataTransferObjects;

public class UpdateEmployeeDto
{
    public required string Name { get; set; }
    public required double BodyWeight { get; set; }
    public string? WeightClass { get; set; }
    public string? Division { get; set; }
    public string? Team { get; set; }
}

