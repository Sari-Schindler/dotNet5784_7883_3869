using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class MilestoneInTask
{
    public string? Description { get; set; }
    public string? NickName { get; set; }
    public required DateTime CreatedDate {  get; set; } 
    public Status MilestoneInTaskStatus { get; set; }
    public double? ProgressPercentage { get; set; }
    public override string ToString() => this.ToStringProperty();

}
