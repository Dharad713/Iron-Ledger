using IronLedger.AthleteService.Data;
using IronLedger.AthleteService.Mappings;
using IronLedger.AthleteService.Models;
using IronLedger.AthleteService.Validation;
using IronLedger.Contracts.Athletes;

namespace IronLedger.AthleteService.Services;

public class AthleteCommandHandler
{
    private readonly AthleteDbContext _dbContext;

    public AthleteCommandHandler(AthleteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateAthleteResponse> CreateAthlete(
        CreateAthleteRequest request,
        CancellationToken cancellationToken)
    {
        AthleteValidator.ValidateCreateRequest(request);

        Athlete athlete = AthleteMapper.ToEntity(request);

        _dbContext.Athletes.Add(athlete);

        await _dbContext.SaveChangesAsync(
            cancellationToken);

        return new CreateAthleteResponse
        {
            Athlete = AthleteMapper.ToMessage(athlete)
        };
    }

}


