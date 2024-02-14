using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

/// <summary>
/// Create observe collection for each entity
/// </summary>
internal class Bl : IBl
{
    public ITask Task =>  new TaskImplementation();

    public IEngineer Engineer =>  new EngineerImplementation();

    public IMilestone Milestone => new MilestoneImplementation();

    public IEngineerInList EngineerInList => new EngineerInListImplementation();

    public ITaskInList TaskInList => new TaskInListImplementation();
 
}
