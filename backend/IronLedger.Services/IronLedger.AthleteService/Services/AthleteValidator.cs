using Grpc.Core;
using IronLedger.Contracts.Athletes;
using IronLedger.Contracts.Common;

namespace IronLedger.AthleteService.Validation;

public class AthleteValidator
{
    public static void ValidateCreateRequest(CreateAthleteRequest request)
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

    public static Guid ValidateAndParseAthleteId(string athleteId)
    {
        if (!Guid.TryParse(athleteId, out Guid parsedAthleteId))
        {
            throw new RpcException(
                new Status(
                    StatusCode.InvalidArgument,
                    "Athlete ID must be a valid GUID."));
        }

        return parsedAthleteId;
    }
}