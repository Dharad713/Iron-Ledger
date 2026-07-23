using Grpc.Core;
using IronLedger.AthleteService.Data;
using IronLedger.AthleteService.Models;
using IronLedger.AthleteService.Validation;
using IronLedger.Contracts.Athletes;
using Microsoft.EntityFrameworkCore;


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

        public async Task<ArchiveAthleteResponse> ArchiveAthlete(
        ArchiveAthleteRequest request,
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

        if (athlete.IsArchived)
        {
            return new ArchiveAthleteResponse
            {
                Success = true,
                AlreadyArchived = true
            };
        }

        athlete.IsArchived = true;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ArchiveAthleteResponse
        {
            Success = true,
            AlreadyArchived = false
        };
    }
    
    public async Task<RestoreAthleteResponse> RestoreAthlete(
        RestoreAthleteRequest request,
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

        if (!athlete.IsArchived)
        {
            return new RestoreAthleteResponse()
            {
                Success = true,
                AlreadyActive = true
            };
        }

        athlete.IsArchived = false;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new RestoreAthleteResponse()
        {
            Success = true,
            AlreadyActive = false
        };
    }
}


