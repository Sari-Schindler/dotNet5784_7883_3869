using BlApi;
using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

internal class TaskInListImplementation: ITaskInList
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// Return all tasks in list
    /// </summary>
    /// <param name="filter">If there's filter return just the items that fit the standards</param>
    /// <returns></returns>
    /// <exception cref="BlDoesNotExistException"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public IEnumerable<BO.TaskInList> ReadAll(Func<BO.TaskInList, bool>? filter = null)
    {
        try
        {

            var allTasks = _dal.Task.ReadAll();
            if (!allTasks.Any())
                throw new BlDoesNotExistException("no task exist");
            IEnumerable<BO.TaskInList> newTask = from allTask in allTasks
                                           select convertToBo(allTask);
            return filter == null ? newTask : newTask.Where(filter);
        }
        catch (DO.DalDoesNotExistException exception)
        {
            throw new BO.BlDoesNotExistException($"no tasks found", exception);
        }
    }




    private BO.TaskInList convertToBo(DO.Task task)
    {
        return new BO.TaskInList
        {
            ID = task.ID,
            Description = task.Description,
            NickName = task.nickName,
            TaskInListStatus = (BO.Status)(task!.CreatedDateTask == DateTime.MinValue ? 0
                            : task!.StartTime == DateTime.MinValue ? 1
                            : task.CompleteDate == DateTime.MinValue ? 2
                            : 3)
        };
    }

}
