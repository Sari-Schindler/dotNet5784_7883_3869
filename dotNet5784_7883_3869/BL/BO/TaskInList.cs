using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class TaskInList
{
    public required int ID { get; set; }
    public string? Description { get; set; }
    public string? NickName { get; set; }
    public Status TaskInListStatus {  get; set; }
    //public override string ToString() => this.ToStringProperty();

}
