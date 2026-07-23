using Grpc.Core;
using IronLedger.Contracts.Athletes;

namespace IronLedger.AthleteService.Services;


public class AthleteGrpcService : Contracts.Athletes.AthleteService.AthleteServiceBase
{
    private readonly AthleteCommandHandler _commandHandler;
    private readonly AthleteQueryHandler _queryHandler;


    public AthleteGrpcService(
        AthleteCommandHandler commandHandler,
        AthleteQueryHandler queryHandler)
    {
        _commandHandler = commandHandler;
        _queryHandler = queryHandler;
    }

    public override Task<CreateAthleteResponse> CreateAthlete(
        CreateAthleteRequest request,
        ServerCallContext context)
    {
        return _commandHandler.CreateAthlete(
            request,
            context.CancellationToken);
    }

    public override Task<GetAthleteResponse> GetAthlete(
        GetAthleteRequest request,
        ServerCallContext context)
    {
        return _queryHandler.GetAthlete(
            request,
            context.CancellationToken);
    }
    
    public override Task<ArchiveAthleteResponse> ArchiveAthlete(
        ArchiveAthleteRequest request,
        ServerCallContext context)
    {
        return _commandHandler.ArchiveAthlete(
            request,
            context.CancellationToken);
    }
    
    public override Task<RestoreAthleteResponse> RestoreAthlete(
        RestoreAthleteRequest request,
        ServerCallContext context)
    {
        return _commandHandler.RestoreAthlete(
            request,
            context.CancellationToken);
    }
}