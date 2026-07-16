using IronLedger.Api.Models.Entities;

namespace IronLedger.Api.Models.DataTransferObjects.MeetDtos;

public class AddMeetWeightClassDto
{
    public required string WeightClassName { get; set; }
    public required Sex Sex { get; set; }

    public decimal? MinimumWeightKg { get; set; }
    public decimal? MaximumWeightKg { get; set; }
}