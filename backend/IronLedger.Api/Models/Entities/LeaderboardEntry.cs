namespace IronLedger.Api.Models.Entities;

public class LeaderboardEntry
{
    public Guid LeaderboardEntryId { get; set; }
    public Guid AthleteId { get; set; }
    public string AthleteName { get; set; } = string.Empty;
    public Sex Sex { get; set; }
    public string? Nationality { get; set; }
    public Guid? MeetId { get; set; } // this is for getting meet winners
    
    public string? WeightClass { get; set; }
    public string? Division { get; set; }
    public string? Federation { get; set; }
    public EquipmentStatus? EquipmentStatus { get; set; }
    

    public decimal BestSquatKg { get; set; }
    public decimal BestBenchKg { get; set; }
    public decimal BestDeadliftKg { get; set; }

    public decimal DotsScore { get; set; } //Might want to add more rankings later on
    public decimal TotalKg { get; set; }

    public int Rank { get; set; }
}