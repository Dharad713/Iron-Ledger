using Grpc.Core;
using IronLedger.Contracts.Athletes;

namespace IronLedger.AthleteService.Services;


public class AthleteGrpcService : Contracts.Athletes.AthleteService.AthleteServiceBase
{
    private readonly AthleteCommandHandler _commandHandler;

    public AthleteGrpcService(
        AthleteCommandHandler commandHandler)
    {
        _commandHandler = commandHandler;
    }

    public override Task<CreateAthleteResponse> CreateAthlete(
        CreateAthleteRequest request,
        ServerCallContext context)
    {
        return _commandHandler.CreateAthlete(
            request,
            context.CancellationToken);
    }

}