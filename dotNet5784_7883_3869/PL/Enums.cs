using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL;

internal class EngineerExperienceCollection : IEnumerable
{
    public static readonly IEnumerable<BO.EngineerExperience> e_enums =
        (Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;

    public IEnumerator GetEnumerator() => e_enums.GetEnumerator();
}

internal class TaskLevelCollection : IEnumerable
{
    public static readonly IEnumerable<BO.TaskLevel> e_enums =
        (Enum.GetValues(typeof(BO.TaskLevel)) as IEnumerable<BO.TaskLevel>)!;

    public IEnumerator GetEnumerator() => e_enums.GetEnumerator();
}

internal class StatusCollection : IEnumerable
{
    public static readonly IEnumerable<BO.Status> e_enums =
        (Enum.GetValues(typeof(BO.Status)) as IEnumerable<BO.Status>)!;

    public IEnumerator GetEnumerator() => e_enums.GetEnumerator();
}