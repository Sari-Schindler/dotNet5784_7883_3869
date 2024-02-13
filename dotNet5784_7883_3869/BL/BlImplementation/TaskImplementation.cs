
using BlApi;
using BO;
using DO;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;

namespace BlImplementation;


internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    private void CheckValidation(BO.Task task)
    {
        if(task.Description=="")
            throw new BlInvalidValueException("Description can't be null");
        if (task.Milestone is null)
            throw new BlInvalidValueException("Milestone can't be null");
        if (task.requierdTime == TimeSpan.MinValue)
            throw new BlInvalidValueException("requierdTime can't be null");
        if (task.CreatedDateTask == DateTime.MinValue)
            throw new BlInvalidValueException("CreatedDateTask can't be null");
        if (task.EstimatedStartTime == DateTime.MinValue)
            throw new BlInvalidValueException("EstimatedStartTime can't be null");
        if (task.StartTime == DateTime.MinValue)
            throw new BlInvalidValueException("StartTime can't be null");
        if (task.TaskStatus == 0)
            throw new BlInvalidValueException("Status can't be null");
        if (task.TimeEstimatedLeft == DateTime.MinValue)
            throw new BlInvalidValueException("TimeEstimatedLeft can't be null");
        if (task.DeadLine == DateTime.MinValue)
            throw new BlInvalidValueException("DeadLine can't be null");
        if (task.CompleteDate == DateTime.MinValue)
            throw new BlInvalidValueException("CompleteDate can't be null");
        if (task.productDescription =="")
            throw new BlInvalidValueException("productDescription can't be null");
        if (task.nickName == "")
            throw new BlInvalidValueException("Name can't be null");
        if (task.ID < 0)
            throw new BlInvalidValueException("ID is incorrect here"); 
    }
    private void createTaskDependnce(List<TaskInList> tasks, int id)
    {
        if (tasks == null || tasks.Count == 0)
            return ;
        _dal.Dependency.Create(new DO.Dependency(id, tasks.First()!.ID, 0));
        tasks.RemoveAt(0);
         createTaskDependnce(tasks, id);
     
    }
    public void Create(BO.Task newTask)
    {
        try
        {
            CheckValidation(newTask);
            _dal.Task.Create(new DO.Task(newTask.Description,false,newTask.requierdTime, newTask.CreatedDateTask,newTask.EstimatedStartTime,
                newTask.StartTime, newTask.TimeEstimatedLeft, newTask.DeadLine, newTask.CompleteDate,
                newTask.productDescription, newTask.CurrentEngineer?.ID, (DO.TaskLevel?)newTask?.ComplexityLevel,newTask!.nickName,newTask?.Comments, newTask!.ID));
                createTaskDependnce(newTask.DependencysList!, newTask.ID);
        }
        catch (DO.DalAlreadyExistsException exception)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID {newTask.ID} already exists", exception);
        }
    }



    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        try
        {

            var allTasks = _dal.Task.ReadAll();
            if (!allTasks.Any())
                throw new BlDoesNotExistException("no task exist");
            IEnumerable<BO.Task> newTask = from allTask in allTasks
                                           select convertToBo(allTask);
            return filter == null ? newTask : newTask.Where(filter);
        }
        catch (DO.DalDoesNotExistException exception)
        {
            throw new BO.BlDoesNotExistException($"no tasks found", exception);
        }
    }




    private BO.Task convertToBo(DO.Task task)
    {
        return new BO.Task
        {
            Description = task.Description,
            Milestone = FindMilestoneForTask(task.ID),
            requierdTime = task.requiredTime,
            CreatedDateTask = task.CreatedDateTask,
            EstimatedStartTime = task.estimatedTimeStart,
            StartTime = task.StartTime,
            TaskStatus = (BO.Status)(task!.CreatedDateTask == DateTime.MinValue ? 0
                            : task!.StartTime == DateTime.MinValue ? 1
                            : task.CompleteDate == DateTime.MinValue ? 2
                            : 3),
            DependencysList = (List<TaskInList>)(from dependency in _dal.Dependency.ReadAll()
                               where dependency.previousIDTask == task.ID
                               let tempTask = _dal.Task.Read(dependency.previousIDTask)
                               select new TaskInList
                               {
                                   ID = task.ID,
                                   NickName = task.nickName,
                                   Description = task.Description,
                                   TaskInListStatus = (BO.Status)(task!.CreatedDateTask == DateTime.MinValue ? 0
                                                                  : task!.StartTime == DateTime.MinValue ? 1
                                                                  : task.CompleteDate == DateTime.MinValue ? 2
                                                                  : 3),
                               }).ToList(),
            TimeEstimatedLeft = task.TimeEstimatedLeft,
            DeadLine = task.DeadLine,
            CompleteDate = task.CompleteDate,
            productDescription = task.productDescription,
            ComplexityLevel = (BO.TaskLevel?)task.ComplexityLevel!,
            nickName = task.nickName,
            Comments = task.Comments,
            ID = task.ID,
            CurrentEngineer = FindCurrentEngineer((int)task.EngineerId!)
        };
}

    private MilestoneInTask FindMilestoneForTask(int id)
    {
          MilestoneInTask milestoneInTask = (from d in _dal.Dependency.ReadAll()
                                           let IdPreviousTask = d.previousIDTask
                                           where IdPreviousTask == id && (_dal.Task.Read(d.DependentTask)!.Milestone == true)
                                           select new MilestoneInTask { ID = d.DependentTask, NickName = _dal.Task.Read(d.DependentTask)!.nickName }).FirstOrDefault()!;
        return milestoneInTask;

    }



    private EngineerInTask FindCurrentEngineer(int EngineerId)
    {
        try
        {
            var engineer = _dal.Engineer.Read(EngineerId);
            if(engineer == null)
            { return null!; }
            return new BO.EngineerInTask
            {
                ID = engineer.ID,
                Name = engineer.Name
            };
        }
        catch { return null!; }
    }


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
                throw new BlCannotBeDeletedException($"can't delete task with Id {taskId}");
            _dal.Engineer!.Delete(taskId);
        }
        catch (DO.DalDoesNotExistException exception)
        {
            throw new BO.BlDoesNotExistException($"engineer with ID {taskId} already not exists", exception);
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
        catch (DO.DalDoesNotExistException exception)
        {
            throw new BO.BlDoesNotExistException($"engineer with ID {ID} already not exists", exception);
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
        catch (DO.DalDoesNotExistException exception)
        {
            throw new BO.BlDoesNotExistException($"engineer with ID {task.ID} already not exists", exception);
        }
    }
}

