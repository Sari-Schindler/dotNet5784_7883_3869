using System;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using BlApi;
using BO;
using DO;

namespace BlImplementation;


internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    private void CheckValidation(BO.Engineer engineer)
    {
        string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])""|" + @"([-a-z0-9!#$%&'+/=?^_`{|}~]|(?<!\.)\.))(?<!\.)" + @"@[a-z0-9][\w\.-][a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        var regex = new Regex(pattern, RegexOptions.IgnoreCase);
        if(!regex.IsMatch(engineer.Email!))
        {
            throw new Exception("Email is not valid");
        }
        if (engineer.ID<=0)
            throw new Exception("ID is incorrect here");
        if (engineer.Name == "")
            throw new Exception("Name can't be null");
        if (engineer.Cost <= 0)
            throw new Exception("Cost must be larger than 0");
    }
    public int Create(BO.Engineer newEngineer)
    {
        try
        {
            CheckValidation(newEngineer);
            var engineerID = _dal.Engineer.Create(new DO.Engineer(newEngineer.ID, newEngineer.Name, (DO.EngineerExperience)newEngineer.Level, newEngineer.Cost, newEngineer.Email));
            return engineerID;
        }
        catch (DO.DalAlreadyExistsException exception)
        {
            throw new BO.BlAlreadyExistsException($"engineer with ID {newEngineer.ID} already exists", exception);
        }
    }


    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        try
        {
            var allEngineers = _dal.Engineer.ReadAll();
            if (!allEngineers.Any())
                throw new NotImplementedException();
            IEnumerable<BO.Engineer> newEngineers = from allEngineer in allEngineers
                                                   select new BO.Engineer
                                                   {
                                                       ID = allEngineer.ID,
                                                       Name = allEngineer.Name,
                                                       Level = (BO.EngineerExperience)allEngineer.Level,
                                                       Cost = allEngineer.Cost,
                                                       Email = allEngineer.Email,
                                                       CurrentTask = getCurrentTask(allEngineer.ID)
                                                   };
            return filter==null ? newEngineers : newEngineers.Where(filter);
        }
        catch (DO.DalDoesNotExistException exception)
        {
            throw new BO.BlDoesNotExistException($"no engineers found", exception);
        }
    }
    private BO.TaskInEngineer? getCurrentTask(int engineer_id)
    {
        DO.Task? currentTask = (from task in _dal.Task.ReadAll((DO.Task tempTask) => engineer_id == tempTask.EngineerId) select task)
            .FirstOrDefault(task => task!.StartTime != DateTime.MinValue && task.CompleteDate == DateTime.MinValue, null);
        return currentTask == null ? null : new BO.TaskInEngineer { ID = currentTask.ID, NickName = currentTask.nickName };
    }

    public void Delete(int engineerID)
    {
        try
        {
            var isExistEngineer = _dal.Engineer?.Read(engineerID);
            IEnumerable<DO.Task> allTasks = _dal.Task.ReadAll()!;
            IEnumerable<int> isExistInTask = from task in allTasks
                                             where task.EngineerId == engineerID
                                             select task.ID;
            if (isExistInTask.Any())
                //check validation of exception!!
                throw new BO.DalDeletionImpossible($"can't delete engineer with ID {engineerID}");
            _dal.Engineer!.Delete(engineerID);
        }
        catch (DO.DalDoesNotExistException exception)
        {
            throw new BO.BlDoesNotExistException($"engineer with ID {engineerID} already not exists", exception);
        }
    }

    public BO.Engineer Read(int ID)
    {
        var engineer=_dal.Engineer.Read(ID);
        if(engineer is null)
            throw new BO.BlDoesNotExistException($"engineer with ID {ID} already not exists");
        return new BO.Engineer {
            ID = engineer.ID,
            Name = engineer.Name,
            Level = (BO.EngineerExperience)engineer.Level,
            Cost = engineer.Cost,
            Email = engineer.Email,
            CurrentTask = getCurrentTask(ID)
        };
    }

    public void Update(BO.Engineer engineer)
    {
        try
        {
            var isExistEngineer = _dal.Engineer?.Read(engineer.ID);
            CheckValidation(engineer);
            _dal.Engineer!.Update(new DO.Engineer(engineer.ID, engineer.Name,(DO.EngineerExperience)engineer.Level, engineer.Cost, engineer.Email));
        }
        catch (DO.DalDoesNotExistException exception)
        {
            throw new BO.BlDoesNotExistException($"engineer with ID {engineer.ID} already not exists", exception);
        }
    }
}
