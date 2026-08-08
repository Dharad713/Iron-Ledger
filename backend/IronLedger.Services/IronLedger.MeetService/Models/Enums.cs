namespace IronLedger.MeetService.Models;


public enum MeetStatus
{
    Unspecified,
    Draft,
    RegistrationOpen,
    RegistrationClosed,
    Archived,
    Completed,
    Cancelled,
    Active,
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
