using Microsoft.EntityFrameworkCore;
using Grpc.Core;
using IronLedger.Contracts.Meets;
using IronLedger.MeetService.Data;
using IronLedger.MeetService.Models;
using IronLedger.MeetService.Services.Validation;

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
        // MeetValidator.ValidateCreateRequest(request);
        // Map request to entity
        Meet meet = MeetMapper.ToEntity(request);
        //Add to dbcontext and save changes
        _dbContext.Meets.Add(meet);
        // return meet message
        await _dbContext.SaveChangesAsync(
            cancellationToken);

        return new CreateMeetResponse()
        {
            Meet = MeetMapper.ToMessage(meet)
        };
    }
}