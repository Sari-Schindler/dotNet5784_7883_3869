
using BlApi;
namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private void CheckValidation(BO.Task task)
    {
        if (task.ID <= 0)
            throw new Exception("ID is incorrect here");
        if (task.nickName == "")
            throw new Exception("Name can't be null");
    }
    public void Create(BO.Task newTask)
    {
        try
        {
            CheckValidation(newTask);
            _dal.Task.Create(new DO.Task(newTask.Description,false, newTask.CreatedDateTask,
                newTask.StartTime, newTask.TimeEstimatedLeft, newTask.DeadLine, newTask.CompleteDate,
                newTask.productDescription,0,(DO.TaskLevel?)newTask?.ComplexityLevel,newTask!.nickName,null, newTask.ID));
        }
        catch
        {
            throw new NotImplementedException();
        }
    }

    public IEnumerable<BO.Task> ReadAll(Func<Task, bool>? filter = null)
    {
        IEnumerable<BO.Task> allTasks = from task in _dal.Task.ReadAll()
                                                    select convertToBo(task);
        return filter == null ? allTasks : allTasks.Where(filter);
    }

    private BO.Task convertToBo(DO.Task task)
    {
        return new BO.Task
        {
            Description= task.Description,
            Milestone= (bool)task.Milestone,
            CreatedDateTask= task.CreatedDateTask,
            EstimatedStartTime = task.EstimatedStartTime,
            StartTime = task.StartTime,
            TaskStatus= (BO.Status)(task!.CreatedDateTask == DateTime.MinValue ? 0
                            : task!.StartTime == DateTime.MinValue ? 1
                            : task.CompleteDate == DateTime.MinValue ? 2
                            : 3),
            DependencysList =task.DependencysList,
            TimeEstimatedLeft=task.TimeEstimatedLeft,
            DeadLine=task.DeadLine,
            CompleteDate =task.CompleteDate,
            productDescription=task.productDescription,
            ComplexityLevel=task.ComplexityLevel,
            nickName=task.nickName,
            Comments=task.Comments,
            ID=task.ID,
            CurrentEngineer=task.CurrentEngineer
        };
    }
    public void Delete(BO.Task task)
    {
        throw new NotImplementedException();
    }

    public BO.Task Read(int ID)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Task task)
    {
        try
        {
            CheckValidation(task);
            _dal.Task.Update(new DO.Task(task.Description, false, task.CreatedDateTask,
                task.StartTime, task.TimeEstimatedLeft, task.DeadLine, task.CompleteDate,
                task.productDescription, 0, (DO.TaskLevel?)task?.ComplexityLevel, task!.nickName, null, task.ID));
        }
        catch (NotImplementedException ex)
        {
            throw new NotImplementedException();
        }
    }
}
