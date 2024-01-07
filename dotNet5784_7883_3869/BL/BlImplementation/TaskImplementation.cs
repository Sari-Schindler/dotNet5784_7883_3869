
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
    private void createTaskDependnce(List<TaskInList> tasks, int id)
    {
        if (tasks.Count == 0)
            return ;
        _dal.Dependency.Create(new DO.Dependency( tasks.First()!.ID,0,id));
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
            IEnumerable<BO.Task> allTasks = from task in _dal.Task.ReadAll()
                                            select convertToBo(task);
            if (!allTasks.Any())
                throw new NotImplementedException();
            return filter == null ? allTasks : allTasks.Where(filter);
        }
        catch (DO.DalDoesNotExistException exception)
        {
            throw new BO.BlDoesNotExistException($"no task found", exception);
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
            DependencysList = (List<TaskInList>)(from dependency in _dal!.Dependency!.ReadAll()!
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
                                           }), 

            TimeEstimatedLeft = task.TimeEstimatedLeft,
            DeadLine = task.DeadLine,
            CompleteDate = task.CompleteDate,
            productDescription = task.productDescription,
            ComplexityLevel = (BO.TaskLevel?)task.ComplexityLevel,
            nickName = task.nickName,
            Comments = task.Comments,
            ID = task.ID,
            CurrentEngineer = FindCurrentEngineer(task.ID)
        };
    }

    private MilestoneInTask FindMilestoneForTask(int id)
    {
        DO.Dependency? milstone_dep = _dal.Dependency.ReadAll(dep => dep.DependentTask == id).First(dep => _dal.Task?.Read(task => task.ID == dep!.previousIDTask)!.Milestone == true);
        DO.Task? milestone = _dal.Task.Read(task => task.ID == milstone_dep!.previousIDTask);
        return new MilestoneInTask
        {
            ID = milestone!.ID,
            NickName = milestone.nickName
        };

    }
    private EngineerInTask FindCurrentEngineer(int Id)
    {
        try
        {
            if(_dal.Engineer.Read(Id) == null)
            { return null!; }
            return new BO.EngineerInTask
            {
                ID = Id!,
                Name = _dal.Engineer?.Read(Id)!.Name
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
                throw new DalDeletionImpossible($"can't delete task with Id {taskId}");
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

