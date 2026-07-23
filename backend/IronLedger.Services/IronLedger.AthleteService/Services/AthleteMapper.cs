using IronLedger.AthleteService.Models;
using IronLedger.Contracts.Athletes;
using ContractDate = IronLedger.Contracts.Common.Date;

namespace IronLedger.AthleteService.Services;

public static class AthleteMapper
{
    public static Athlete ToEntity(CreateAthleteRequest request)
    {
        return new Athlete
        {
            AthleteId = Guid.NewGuid(),
            Name = request.Name.Trim(),
            BodyWeightKg = (decimal)request.BodyWeightKg,
            Sex = request.Sex,
            DateOfBirth = new DateOnly(
                request.DateOfBirth.Year,
                request.DateOfBirth.Month,
                request.DateOfBirth.Day),
            Team = StringToNullable(request.Team),
            IsArchived = false
        };
    }

    public static AthleteMessage ToMessage(Athlete athlete)
    {
        return new AthleteMessage
        {
            AthleteId = athlete.AthleteId.ToString(),
            Name = athlete.Name,
            BodyWeightKg = (double)athlete.BodyWeightKg,
            Sex = athlete.Sex,
            DateOfBirth = new ContractDate
            {
                Year = athlete.DateOfBirth.Year,
                Month = athlete.DateOfBirth.Month,
                Day = athlete.DateOfBirth.Day
            },
            Team = athlete.Team ?? string.Empty,
            IsArchived = athlete.IsArchived
        };
    }

    /*------------------ Helper functions ------------------*/
    private static string? StringToNullable(string value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}