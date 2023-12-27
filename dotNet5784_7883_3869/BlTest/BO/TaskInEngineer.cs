using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlTest.BO;

public class TaskInEngineer
{
    public required int ID { get; set; }
    public string? NickName { get; set; }
    public override string ToString() => this.ToStringProperty();

}
