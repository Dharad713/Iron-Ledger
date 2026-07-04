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
    Draft,
    Active,
    Completed,
    Cancelled
}

public enum Result // add Failure light options??
{
    Pending,
    GoodLift,
    NoLift
}