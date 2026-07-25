using Grpc.Core;
using IronLedger.Contracts.Athletes;
using IronLedger.Contracts.Common;

namespace IronLedger.AthleteService.Services.Validation;

public class AthleteValidator
{
    public static void ValidateCreateRequest(CreateAthleteRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        ValidateName(request.Name);
        ValidateBodyWeight(request.BodyWeightKg);
        ValidateDateOfBirth(request.DateOfBirth);
        ValidateSex(request.Sex);
    }

    public static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new RpcException(
                new Status(
                    StatusCode.InvalidArgument,
                    "Name is required."));
        }
    }

    public static void ValidateBodyWeight(double bodyWeightKg)
    {
        if (bodyWeightKg <= 0)
        {
            throw new RpcException(
                new Status(
                    StatusCode.InvalidArgument,
                    "Bodyweight must be greater than zero."));
        }
    }

    public static void ValidateDateOfBirth(
        Date? dateOfBirth)
    {
        if (dateOfBirth is null)
        {
            throw new RpcException(
                new Status(
                    StatusCode.InvalidArgument,
                    "Date of birth is required."));
        }

        try
        {
            _ = new DateOnly(
                dateOfBirth.Year,
                dateOfBirth.Month,
                dateOfBirth.Day);
        }
        catch (ArgumentOutOfRangeException)
        {
            throw new RpcException(
                new Status(
                    StatusCode.InvalidArgument,
                    "Date of birth is invalid."));
        }
    }

    public static void ValidateSex(Sex sex)
    {
        if (sex == Sex.Unspecified)
        {
            throw new RpcException(
                new Status(
                    StatusCode.InvalidArgument,
                    "Sex is required."));
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