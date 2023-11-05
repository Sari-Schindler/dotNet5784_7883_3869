

namespace DO;

/// <summary>
/// Task entity represent a task that we have to do
/// </summary>
/// <param name="ID"></param>
/// <param name="Description"></param>
/// <param name="Alias"></param>
/// <param name="Milestone"></param>
/// <param name="CreatedAdt"></param>
/// <param name="Start"></param>
/// <param name="ScheduledDate"></param>
/// <param name="ForecastDate"></param>
/// <param name="DeadLine"></param>
/// <param name="Complete"></param>
/// <param name="Deliverable"></param>
/// <param name="Remarks"></param>
/// <param name="Engineerld"></param>
/// <param name="ComplexityLevel"></param>
public record Task
    (
    string Description,
    bool? Milestone,
    DateTime Start,
    DateTime ScheduledDate,
    DateTime DeadLine,
    DateTime Complete,
    string Deliverable,
    int Engineerld,
    TaskLevel? ComplexityLevel,
    string? nickName=null,
    string? Comments = null,
    int ID=0
    )

{
    public DateTime CreatedAt => DateTime.Now; //get only
}