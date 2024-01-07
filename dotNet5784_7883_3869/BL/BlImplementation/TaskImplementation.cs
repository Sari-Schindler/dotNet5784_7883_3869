
using BlApi;
using BO;
using System.Runtime.Intrinsics.Arm;
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
            _dal.Task.Create(new DO.Task(newTask.Description,false,newTask.requierdTime, newTask.CreatedDateTask,newTask.EstimatedStartTime,
                newTask.StartTime, newTask.TimeEstimatedLeft, newTask.DeadLine, newTask.CompleteDate,
                newTask.productDescription, newTask.CurrentEngineer?.ID, (DO.TaskLevel?)newTask?.ComplexityLevel,newTask!.nickName,newTask?.Comments, newTask!.ID));
            //הוספת משימות קודמות מתוך רשימת המשימות הקיימת
        }
        catch
        {
            throw new NotImplementedException();
        }
    }

           
    
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        try
        {
            IEnumerable<BO.Task> allTasks = from task in _dal.Task.ReadAll()
                                            select convertToBo(task);
            if (!allTasks.Any())
                throw new NotImplementedException();
            return filter == null ? allTasks : allTasks.Where(filter);
        }
        catch (NotImplementedException ex)
        {
            throw new NotImplementedException();
        }
    }

            
      
    private BO.Task convertToBo(DO.Task task)
    {
        return new BO.Task
        {
            Description = task.Description,
            Milestone = (bool)task.Milestone,
            CreatedDateTask = task.CreatedDateTask,
            EstimatedStartTime = task.,
            StartTime = task.StartTime,
            TaskStatus = (BO.Status)(task!.CreatedDateTask == DateTime.MinValue ? 0
                            : task!.StartTime == DateTime.MinValue ? 1
                            : task.CompleteDate == DateTime.MinValue ? 2
                            : 3),
            DependencysList = task.DependencysList,
            TimeEstimatedLeft = task.TimeEstimatedLeft,
            DeadLine = task.DeadLine,
            CompleteDate = task.CompleteDate,
            productDescription = task.productDescription,
            ComplexityLevel = (BO.TaskLevel?)task.ComplexityLevel,
            nickName = task.nickName,
            Comments = task.Comments,
            ID = task.ID,
            CurrentEngineer /*convertToEngineerInTask(task!.EngineerId);*/
        };
    }

    //private BO.EngineerInTask convertToEngineerInTask(int engineerId)
    //{
    //    return new BO.EngineerInTask {engineerId,"KCFV" };
    //}

    public void Delete(int taskId)
    {
        try
        {
            var isExistTask = _dal.Task?.Read(taskId);
            IEnumerable<DO.Dependency> allDependencys = _dal.Dependency.ReadAll()!;
            IEnumerable<int> isExistInDependency = from dependency in allDependencys
                                                   where dependency.DependentTask == taskId
                                                   select isExistTask!.ID;
            if (isExistInDependency.Any())
                throw new NotImplementedException();
            _dal.Engineer!.Delete(taskId);
        }
        catch (NotImplementedException ex)
        {
            throw new NotImplementedException();
        }
    }


    public BO.Task Read(int ID)
    {
        try
        {
            var doTask = _dal.Task.Read(ID);
            BO.Task boTask = convertToBo(doTask!);
            return boTask;          
        }
        catch (NotImplementedException ex)
        {
            throw new NotImplementedException();
        }
    }
  
    public void Update(BO.Task task)
    {
        try
        {
            var isExistTask = _dal.Task?.Read(task.ID);
            CheckValidation(task);
            _dal.Task!.Update(new DO.Task(task.Description, false, task.requierdTime, task.CreatedDateTask, task.EstimatedStartTime,
                task.StartTime, task.TimeEstimatedLeft, task.DeadLine, task.CompleteDate,
                task.productDescription, task.CurrentEngineer?.ID, (DO.TaskLevel?)task?.ComplexityLevel, task!.nickName, task?.Comments, task!.ID));
        }
        catch (NotImplementedException ex)
        {
            throw new NotImplementedException();
        }
    }
}

