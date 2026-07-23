using Grpc.Core;
using IronLedger.AthleteService.Data;
using IronLedger.AthleteService.Models;
using IronLedger.AthleteService.Validation;
using IronLedger.Contracts.Athletes;
using Microsoft.EntityFrameworkCore;

namespace IronLedger.AthleteService.Services;

public class AthleteQueryHandler
{
    private readonly AthleteDbContext _dbContext;

    public AthleteQueryHandler(AthleteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetAthleteResponse> GetAthlete(
        GetAthleteRequest request,
        CancellationToken cancellationToken)
    {
        Guid athleteId = AthleteValidator.ValidateAndParseAthleteId(request.AthleteId);

        Athlete? athlete = await _dbContext.Athletes
            .SingleOrDefaultAsync(
                a => a.AthleteId == athleteId,
                cancellationToken);

        if (athlete is null)
        {
            throw new RpcException(
                new Status(
                    StatusCode.NotFound,
                    "Athlete not found."));
        }

        return new GetAthleteResponse
        {
            Athlete = AthleteMapper.ToMessage(athlete)
        };
    }
    

}