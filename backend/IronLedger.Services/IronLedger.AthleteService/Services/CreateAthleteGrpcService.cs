using Grpc.Core;
using IronLedger.AthleteService.Data;
using IronLedger.AthleteService.Models;
using IronLedger.Contracts.Athletes;

namespace IronLedger.AthleteService.Services;


public class CreateAthleteGrpcService : Contracts.Athletes.AthleteService.AthleteServiceBase
{
    private readonly AthleteDbContext _dbContext;
    
    public CreateAthleteGrpcService(AthleteDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public override async Task<CreateAthleteResponse> CreateAthlete(
        CreateAthleteRequest request,
        ServerCallContext context)
    {
        // Take in create athlete request -> Validate and then turn into Athlete object 
        // -> Add athlete to db -> make response -> send response
        
        ValidateCreateRequest(request);

        Athlete athlete = MapToEntity(request);

        _dbContext.Athletes.Add(athlete);
        await _dbContext.SaveChangesAsync(context.CancellationToken);
        
        return MapToResponse(athlete);
    }
    
    private static Athlete MapToEntity(CreateAthleteRequest request)
    {
        return new Athlete
        {
            AthleteId = Guid.NewGuid(),
            Name = request.Name.Trim(),
            BodyWeightKg = (decimal)request.BodyWeightKg,
            Sex = request.Sex,
            DateOfBirth = request.DateOfBirth,
            Team = StringToNullable(request.Team),
            IsArchived = false //you don't want to be able to make a new athlete start as archived
        };
    }
    
    private static CreateAthleteResponse MapToResponse(Athlete athlete)
    {
        var message = new AthleteMessage
        {
            AthleteId = athlete.AthleteId.ToString(),
            Name = athlete.Name,
            BodyWeightKg = (double)athlete.BodyWeightKg,
            DateOfBirth = athlete.DateOfBirth,
            Sex = athlete.Sex,
            Team = athlete.Team ?? string.Empty,
            IsArchived = athlete.IsArchived
        };

        return new CreateAthleteResponse
        {
            Athlete = message
        };
    }
    /*------------------ Validation functions ------------------*/
    private static void ValidateCreateRequest(CreateAthleteRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, "Name is required."));
        }
        
        if (request.BodyWeightKg <= 0)
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument, "Bodyweight must be greater than zero."));
        }
        
        if (request.DateOfBirth is null)
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument,
                    "Date of birth is required."));
        }
        
        try
        {
            _ = new DateOnly(
                request.DateOfBirth.Year,
                request.DateOfBirth.Month,
                request.DateOfBirth.Day);
        }
        catch (ArgumentOutOfRangeException)
        {
            throw new RpcException(
                new Status(StatusCode.InvalidArgument,
                    "Date of birth is invalid."));
        }
        
    }
    
    /*------------------ Helper functions ------------------*/
    private static string? StringToNullable(string value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
    
    
}