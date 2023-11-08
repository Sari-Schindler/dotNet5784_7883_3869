

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
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
        Task? tempTask = (DataSource.Tasks.Find(element => element!.ID == id));
        if (tempTask is null)
            throw new Exception("An object of type Task with such an ID does not exist");
         DataSource.Tasks.Remove(tempTask);
    }

    /// <summary>
    /// display one task
    /// </summary>
    /// <param name="id">the wanted task</param>
    /// <returns>wanted task</returns>
    public Task? Read(int id)
    {
        return (DataSource.Tasks.Find(element => element!.ID == id));
    }



    /// <summary>
    /// return all the task's entities
    /// </summary>
    /// <returns></returns>
    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }


    /// <summary>
    /// update specific task entity
    /// </summary>
    /// <param name="item">wanted task's</param>
    /// <exception cref="Exception">there's no such task with the wanted ID</exception>
    public void Update(Task item)
    {
        Task? tempTask = (DataSource.Tasks.Find(element => element!.ID == item.ID));
        if (tempTask is null)
            throw new Exception("An object of type Task with such an ID does not exist");
        else
        {
            DataSource.Tasks.Remove(tempTask);
            DataSource.Tasks.Add(item);
        }
    }


}
