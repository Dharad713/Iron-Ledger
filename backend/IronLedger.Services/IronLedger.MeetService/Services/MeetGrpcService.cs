using Grpc.Core;
using IronLedger.Contracts.Meets;

namespace IronLedger.MeetService.Services;

public class MeetGrpcService: Contracts.Meets.MeetService.MeetServiceBase
{
    private readonly MeetCommandHandler _commandHandler;
    private readonly MeetQueryHandler _queryHandler;
    
    public MeetGrpcService(
        MeetCommandHandler commandHandler,
        MeetQueryHandler queryHandler)
    {
        _commandHandler = commandHandler;
        _queryHandler = queryHandler;
    }
    
    public override Task<CreateMeetResponse> CreateMeet(
        CreateMeetRequest request,
        ServerCallContext context)
    {
        return _commandHandler.CreateMeet(
            request,
            context.CancellationToken);
    }
    
}