using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Task
{
    public required string Description { get; set; }
    public required MilestoneInTask Milestone { get; set; }
    public required DateTime CreatedDateTask { get; set; }
    public required DateTime EstimatedStartTime { get; set; }
    public required DateTime StartTime { get; set; }
    public required Status TaskStatus { get; set; }
    public List<TaskInList>? DependencysList { get; set; }
    public required DateTime TimeEstimatedLeft { get; set; }
    public required DateTime DeadLine {  get; set; }
    public required DateTime CompleteDate { get; set; }
    public required string productDescription { get; set; }
    public TaskLevel? ComplexityLevel {  get; set; }
    public required string nickName {  get; set; }  
    public string? Comments { get; set; }
    public int ID { get; init; }
    public EngineerInTask? CurrentEngineer{ get; set; }
    public override string ToString() => this.ToStringProperty();

}