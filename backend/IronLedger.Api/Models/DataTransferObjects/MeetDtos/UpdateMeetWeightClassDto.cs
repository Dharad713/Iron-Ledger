using IronLedger.Api.Models.Entities;

namespace IronLedger.Api.Models.DataTransferObjects.MeetDtos;

public class UpdateMeetWeightClassDto
{
    public string? WeightClassName { get; set; }
    public Sex? Sex { get; set; }

    public decimal? MinimumWeightKg { get; set; }
    public decimal? MaximumWeightKg { get; set; }
}