﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

/// <summary>
/// class for milestone
/// </summary>
public class Milestone
{
    public required int ID { get; set; }
    public string? NickName { get; set; }
    public required string Description { get; set; }
    public required DateTime CreatedDate { get; set; } 
    public required Status MilestoneStatus { get; set; }
    public required DateTime startDate { get; set; }
    public DateTime? finishTimeEstimated {  get; set; }
    public required DateTime DeadLine { get; set; }
    public required DateTime EndedDate {  get; set; }
    public double? ProgressPercentage { get; set; }    
    public string? Comments {  get; set; }
    public List<TaskInList>? DependencysList { get; set; }
   public override string ToString() => this.ToStringProperty();

}
