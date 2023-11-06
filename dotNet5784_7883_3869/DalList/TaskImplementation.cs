

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int newID = DataSource.Config.NextTaskId;
        Task newTask = item with { ID = newID };
        DataSource.Tasks.Add(newTask);  
        return newID;
    }

    public void Delete(int id)
    {
        Task? tempTask = (DataSource.Tasks.Find(element => element!.ID == id));
        if (tempTask is null)
            throw new Exception("An object of type Task with such an ID does not exist");
         DataSource.Tasks.Remove(tempTask);
    }

    public Task? Read(int id)
    {
        return (DataSource.Tasks.Find(element => element!.ID == id));
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

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
