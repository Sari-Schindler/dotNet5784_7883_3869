﻿using System;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using BlApi;
using BO;
using DO;

namespace BlImplementation;


internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// Check validation of enginner's details
    /// </summary>
    /// <param name="engineer"></param>
    /// <exception cref="BlInvalidValueException"></exception>
    /// 
    private void CheckValidation(BO.Engineer engineer)
    {
        string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
        var regex = new Regex(pattern, RegexOptions.IgnoreCase);
        if(!regex.IsMatch(engineer.Email!))
            throw new BlInvalidValueException("Email is not valid");
        if (engineer.ID<=0 || engineer.ID>1000000000)
            throw new BlInvalidValueException("ID is incorrect here");
        if (engineer.Name == "")
            throw new BlInvalidValueException("Name can't be null");
        if (engineer.Cost <= 0)
            throw new BlInvalidValueException("Cost must be larger than 0");
        if (engineer.Level == BO.EngineerExperience.None)
            throw new BlInvalidValueException("Level can't be none");
    }

    /// <summary>
    /// Crearte new engineer
    /// </summary>
    /// <param name="newEngineer"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlAlreadyExistsException"></exception>
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

    /// <summary>
    /// Return list of all engineers
    /// </summary>
    /// <param name="filter">if there's a filter return just the fited engineers</param>
    /// <returns></returns>
    /// <exception cref="BlDoesNotExistException"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        try
        {
            var allEngineers = _dal.Engineer.ReadAll();
            if (!allEngineers.Any())
                throw new BlDoesNotExistException("no engineer exist");
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

    /// <summary>
    /// return the current task for specific engineer
    /// </summary>
    /// <param name="engineer_id"></param>
    /// <returns></returns>
    private BO.TaskInEngineer? getCurrentTask(int engineer_id)
    {
        List<DO.Task?> tasks = _dal.Task.ReadAll().ToList();
        TaskInEngineer? currentTask = (from task in tasks
                                       let engineerId = task.EngineerId
                                          where engineerId == engineer_id && task.CompleteDate> DateTime.Now
                                          select new TaskInEngineer { ID = task.ID, NickName = task.nickName }).FirstOrDefault();
        return currentTask;
    }

    /// <summary>
    /// Delete an enginner by wanted ID
    /// </summary>
    /// <param name="engineerID">Enginner wanted ID fir deltet</param>
    /// <exception cref="BO.BlCannotBeDeletedException"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
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
                throw new BO.BlCannotBeDeletedException($"can't delete engineer with ID {engineerID}");
            _dal.Engineer!.Delete(engineerID);
        }
        catch (DO.DalDoesNotExistException exception)
        {
            throw new BO.BlDoesNotExistException($"engineer with ID {engineerID} already not exists", exception);
        }
    }

    /// <summary>
    /// Return one engineer by wanted Id
    /// </summary>
    /// <param name="ID">engineer's Id wanted</param>
    /// <returns></returns>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
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

    /// <summary>
    /// update an enginner
    /// </summary>
    /// <param name="engineer">speific engineer for upate</param>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
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
