namespace BO;

/// <summary>
/// 3 enums that the one describes the engineer's experience and the other describes the task level and status
/// </summary>
public enum EngineerExperience
{
    EIT,
    Junior,
    Senior,
    None // initial value for PL
}

public enum TaskLevel
{
    easy,
    medium,
    hard,
    export,
    None // initial value for PL
}

public enum Status
{
    Unscheduled,
    Scheduled,
    OnTrack,
    InJeopardy,
    None // initial value for PL

}