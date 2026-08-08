using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using IronLedger.Contracts.Meets;
using IronLedger.MeetService.Models;
using ContractDate = IronLedger.Contracts.Common.Date;

namespace IronLedger.MeetService.Services;

public class MeetMapper
{
    public static Meet ToEntity(CreateMeetRequest request)
    {
        Guid meetId = Guid.NewGuid();
        return new Meet
        {
            MeetId = meetId,
            MeetName = request.MeetName.Trim(),
            Date = new DateTime(request.Date.Year, request.Date.Month, request.Date.Day),
            Federation = StringToNullable(request.Federation),
            Location = StringToNullable(request.Location),
            RegistrationOpensAt = request.RegistrationOpensAt.ToDateTime(),
            RegistrationClosesAt = request.RegistrationClosesAt.ToDateTime(),

            EquipmentStatus = MapEquipmentStatus(request.EquipmentStatus),
            MeetStatus = MapMeetStatus(request.MeetStatus),
            WeightClasses = MapWeightClasses(request.WeightClasses.ToList(), meetId),
            Divisions = MapDivisions(request.Divisions.ToList(), meetId),

        };
    }

    public static MeetMessage ToMessage(Meet meet)
    {
        return new MeetMessage
        {
            MeetId = meet.MeetId.ToString(),
            MeetName = meet.MeetName,
            Date = new ContractDate
            {
                Year = meet.Date.Year,
                Month = meet.Date.Month,
                Day = meet.Date.Day
            },
            Federation = StringToNullable(meet.Federation),
            Location = StringToNullable(meet.Location),
            RegistrationOpensAt = Timestamp.FromDateTime(meet.RegistrationOpensAt.ToUniversalTime()),
            RegistrationClosesAt = Timestamp.FromDateTime(meet.RegistrationClosesAt.ToUniversalTime()),
            EquipmentStatus = (Contracts.Meets.EquipmentStatus)meet.EquipmentStatus,
            MeetStatus = (Contracts.Meets.MeetStatus)meet.MeetStatus,
            WeightClasses =
            {
                ToMessage(meet.WeightClasses.ToList())
            },
            Divisions =
            {
                ToMessage(meet.Divisions.ToList())
            },

        };
    }


    private static List<MeetWeightClassMessage> ToMessage(List<MeetWeightClass> weightClasses)
    {
        return weightClasses.Select(weightClass => new MeetWeightClassMessage()
        {
            MeetWeightClassId = weightClass.MeetWeightClassId.ToString(),
            MeetId = weightClass.MeetId.ToString(),
            WeightClassName = weightClass.WeightClassName,
            Sex = weightClass.Sex,
            MaximumWeightKg = (double)weightClass.MaximumWeightKg,
            MinimumWeightKg = (double)weightClass.MinimumWeightKg
        }).ToList();
    }

    private static List<MeetDivisionMessage> ToMessage(List<MeetDivision> divisions)
    {
        return divisions.Select(division => new MeetDivisionMessage()
        {
            MeetDivisionId = division.MeetDivisionId.ToString(),
            MeetId = division.MeetId.ToString(),
            MeetDivisionName = division.MeetDivisionName,
            Sex = division.Sex,
            MaximumAge = division.MaximumAge,
            MinimumAge = division.MinimumAge
        }).ToList();
    }

    private static Models.MeetStatus MapMeetStatus(Contracts.Meets.MeetStatus status)
    {
        return status switch
        {
            Contracts.Meets.MeetStatus.Draft =>
                Models.MeetStatus.Draft,
            Contracts.Meets.MeetStatus.Unspecified =>
                Models.MeetStatus.Unspecified,
            Contracts.Meets.MeetStatus.RegistrationOpen =>
                Models.MeetStatus.RegistrationOpen,
            Contracts.Meets.MeetStatus.RegistrationClosed =>
                Models.MeetStatus.RegistrationClosed,
            Contracts.Meets.MeetStatus.Archived =>
                Models.MeetStatus.Archived,
            Contracts.Meets.MeetStatus.Completed =>
                Models.MeetStatus.Completed,
            Contracts.Meets.MeetStatus.Cancelled =>
                Models.MeetStatus.Cancelled,
            Contracts.Meets.MeetStatus.Active =>
                Models.MeetStatus.Active,
            _ => throw new RpcException(
                new Status(
                    StatusCode.InvalidArgument,
                    "Meet status is invalid."))
        };
    }

    private static Models.EquipmentStatus MapEquipmentStatus(Contracts.Meets.EquipmentStatus status)
    {
        return status switch
        {
            Contracts.Meets.EquipmentStatus.Raw =>
                Models.EquipmentStatus.Raw,
            Contracts.Meets.EquipmentStatus.Wraps =>
                Models.EquipmentStatus.Wraps,
            Contracts.Meets.EquipmentStatus.Equipped =>
                Models.EquipmentStatus.Equipped,
            Contracts.Meets.EquipmentStatus.Unspecified =>
                Models.EquipmentStatus.Unspecified,

            _ => throw new RpcException(
                new Status(
                    StatusCode.InvalidArgument,
                    "Equipment status is invalid."))
        };
    }

    private static List<MeetWeightClass> MapWeightClasses(List<CreateMeetWeightClassMessage> messages, Guid meetId)
    {
        return messages.Select(message => new MeetWeightClass
        {
            MeetWeightClassId = Guid.NewGuid(),
            MeetId = meetId,
            WeightClassName = message.WeightClassName.Trim(),
            Sex = message.Sex,
            MaximumWeightKg = (decimal)message.MaximumWeightKg,
            MinimumWeightKg = (decimal)message.MinimumWeightKg
        }).ToList();
    }

    private static List<MeetDivision> MapDivisions(List<CreateMeetDivisionMessage> messages, Guid meetId)
    {
        return messages.Select(message => new MeetDivision
        {
            MeetId = meetId,
            MeetDivisionId = Guid.NewGuid(),
            MeetDivisionName = message.MeetDivisionName,
            MaximumAge = message.MaximumAge,
            MinimumAge = message.MinimumAge,
            Sex = message.Sex
        }).ToList();
    }

    /*------------------ Helper functions ------------------*/
    private static Guid ValidateAndParseGuid(string meetId)
    {
        if (!Guid.TryParse(meetId, out Guid parsedMeetId))
        {
            throw new RpcException(
                new Status(
                    StatusCode.InvalidArgument,
                    "Meet ID must be a valid GUID."));
        }

        return parsedMeetId;
    }
    private static string? StringToNullable(string value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}