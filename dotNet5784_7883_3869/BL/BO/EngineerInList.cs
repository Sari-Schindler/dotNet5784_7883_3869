using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class EngineerInList
{
    public required int ID { get; set; }
    public required string Name { get; set; }
    public EngineerExperience Level { get; set; }
    public override string ToString() => this.ToStringProperty();
}
