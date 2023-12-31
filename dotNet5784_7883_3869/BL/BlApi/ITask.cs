using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface ITask
{
    public IEnumerable<BO.Task> ReadAll(Func<Task, bool>? filter = null);
    public BO.Task Read(int ID);
    public void Create(BO.Task newTask);
    public void Delete(BO.Task task);
    public void Update(BO.Task task);

}
