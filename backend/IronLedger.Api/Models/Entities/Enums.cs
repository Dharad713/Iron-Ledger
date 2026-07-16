namespace IronLedger.Api.Models.Entities;


public enum LiftType
{
    Squat,
    Bench,
    Deadlift
}

public enum AttemptResult
{
    Pending,
    GoodLift,
    NoLift
}

public enum MeetStatus
{
    Unspecified,
    Draft,
    RegistrationOpen,
    RegistrationClosed,
    Archive,
    Completed,
    Cancelled,
    Active,
}

public enum Result // add Failure light options??
{
    Pending,
    GoodLift,
    NoLift
}

public enum Sex
{
    Unspecified,
    Male,
    Female
}

public enum EquipmentStatus
{
    Raw,
    Wraps,
    Equipped,
    Unspecified
}

public enum RegistrationStatus
{
    Unspecified,
    Registered,
    Withdrawn,
}

