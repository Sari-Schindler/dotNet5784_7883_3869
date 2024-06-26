﻿

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
   
    /// <summary>
    /// create new task
    /// </summary>
    /// <param name="item">wanted task to add</param>
    /// <returns></returns>
    public int Create(Task item)
    {
        int newID = DataSource.Config.NextTaskId;
        Task newTask = item with { ID = newID };
        DataSource.Tasks.Add(newTask);  
        return newID;
    }


    /// <summary>
    /// delete task entity
    /// </summary>
    /// <param name="id">wanted task to delete</param>
    /// <exception cref="Exception">there's no such task with the wanted ID</exception>
    public void Delete(int id)
    {
        var tempTask = DataSource.Tasks.FirstOrDefault(element => element!.ID == id,null);
        if (tempTask is null)
            throw new DalDoesNotExistException($"engineer with ID={id} already not exists\n");
        DataSource.Tasks.Remove(tempTask);
    }

    /// <summary>
    /// returns a task by some kind attribute.
    /// </summary>
    /// <param name="filter">The attributethat the search works by</param>
    /// <returns></returns>
    public Task? Read(Func<Task, bool> filter)
    {
        return DataSource.Tasks.FirstOrDefault(filter);
    }


    /// <summary>
    /// display one task
    /// </summary>
    /// <param name="id">the wanted task</param>
    /// <returns>wanted task</returns>
    public Task? Read(int id)
    {
        return (DataSource.Tasks.FirstOrDefault(element => element!.ID == id, null));
    }



    /// <summary>
    /// return all the task's entities
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Tasks.Select(item => item);
        else
            return DataSource.Tasks.Where(filter);
    }


    /// <summary>
    /// update specific task entity
    /// </summary>
    /// <param name="item">wanted task's</param>
    /// <exception cref="Exception">there's no such task with the wanted ID</exception>
    public void Update(Task item)
    {
        var tempTask = DataSource.Tasks.FirstOrDefault(element => element!.ID == item.ID, null);
        if (tempTask is null)
            throw new DalDoesNotExistException($"task with ID={item.ID} already not exists\n");
        else
        {
            DataSource.Tasks.Remove(tempTask);
            DataSource.Tasks.Add(item);
        }
    }


}
