

namespace DO;


/// <summary>
/// Task entity represent a task that we have to do
/// </summary>
/// <param name="Description">task's description- what should we do in the task</param>
/// <param name="Milestone">returns a bolian value</param>
/// <param name="CreatedDateTask">Task creation date</param>
/// <param name="StartTime">The actual task start date</param>
/// <param name="TimeEstimatedLeft">Scheduled date for task completion</param>
/// <param name="DeadLine">Last date for completing the task</param>
/// <param name="CompleteDate">Task completion date</param>
/// <param name="productDescription">Checks if the task is deliverable</param>
/// <param name="EngineerId">Engineer stage</param>
/// <param name="ComplexityLevel">Level of complexity of the task</param>
/// <param name="nickName">Another nickname for the task</param>
/// <param name="Comments">additional comments</param>
/// <param name="ID">ID's number of task(unique number)</param>
public record Task
    (
    string Description,
    bool? Milestone,
    DateTime CreatedDateTask,
    DateTime StartTime,
    DateTime TimeEstimatedLeft,
    DateTime DeadLine,
    DateTime CompleteDate,
    string productDescription,
    int? EngineerId,
    TaskLevel? ComplexityLevel,
    string? nickName = null,
    string? Comments = null,
    int ID = 0
    )
    
{
    public Task() : this("", false,DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.Now, DateTime.MinValue,"", 0,null,null) { }

}