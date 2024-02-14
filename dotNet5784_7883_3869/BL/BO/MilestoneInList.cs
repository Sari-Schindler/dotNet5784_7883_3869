using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
/// <summary>
/// class for milestone in list
/// </summary>
public class MilestoneInList
{
    public required int ID {  get; set; }   
    public string? Description { get; set; }
    public string? NickName { get; set; }
    public required Status MilestoneInListStatus {  get; set; } 
    public double? ProgressPercentage {  get; set; }
   public override string ToString() => this.ToStringProperty();
}
