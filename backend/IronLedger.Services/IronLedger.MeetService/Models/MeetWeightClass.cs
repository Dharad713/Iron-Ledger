using IronLedger.Contracts.Common;

namespace IronLedger.MeetService.Models;

public class MeetWeightClass
{
    public Guid MeetWeightClassId { get; set; }
    public Guid MeetId { get; set; }
    public required string WeightClassName { get; set; }
    public required Sex Sex { get; set; }

    public decimal MinimumWeightKg { get; set; }
    public decimal MaximumWeightKg { get; set; }
}