

namespace DO;


/// <summary>
/// Task entity represent a task that we have to do
/// </summary>
/// <param name="Description">task's description- what should we do in the task</param>
/// <param name="Milestone">returns a bolian value</param>
/// <param name="createdAdt">Task creation date</param>
/// <param name="Start">The actual task start date</param>
/// <param name="ScheduledDate">Scheduled date for task completion</param>
/// <param name="DeadLine">Last date for completing the task</param>
/// <param name="Complete">Task completion date</param>
/// <param name="Deliverable">Checks if the task is deliverable</param>
/// <param name="Engineerld">Engineer stage</param>
/// <param name="ComplexityLevel">Level of complexity of the task</param>
/// <param name="nickName">Another nickname for the task</param>
/// <param name="Comments">additional comments</param>
/// <param name="ID">ID's number of task(unique number)</param>
public record Task
    (
    string Description,
    bool? Milestone,
    DateTime createdAdt,
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
   
}