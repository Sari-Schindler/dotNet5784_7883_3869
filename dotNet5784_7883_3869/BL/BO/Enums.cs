namespace BO;

/// <summary>
/// 2 enums that the one describes the engineer's experience and the other describes the task level
/// </summary>
public enum EngineerExperience
{
    EIT,
    Junior,
    Senior,
    All // initial value for PL
}

public enum TaskLevel
{
    easy,
    medium,
    hard,
    export
}

public enum Status
{
    Unscheduled, Scheduled, OnTrack, InJeopardy
}