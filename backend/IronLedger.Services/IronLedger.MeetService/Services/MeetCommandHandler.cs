using Microsoft.EntityFrameworkCore;
using Grpc.Core;
using IronLedger.Contracts.Meets;
using IronLedger.MeetService.Data;

namespace IronLedger.MeetService.Services;

public class MeetCommandHandler
{
    private readonly MeetDbContext _dbContext;
    
    public MeetCommandHandler(MeetDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateMeetResponse> CreateMeet(
        CreateMeetRequest request,
        CancellationToken cancellationToken)
    {
        // Validate Create Request
        
        // Map request to entity
        
        //Add to dbcontext and save changes
        
        // return meet message
    }
}