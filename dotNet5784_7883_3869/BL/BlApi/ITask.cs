using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

/// <summary>
/// Include all methods of the task
/// </summary>
public interface ITask
{
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null);
    public BO.Task Read(int ID);
    public void Create(BO.Task newTask);
    public void Delete(int taskId);
    public void Update(BO.Task task);

}
