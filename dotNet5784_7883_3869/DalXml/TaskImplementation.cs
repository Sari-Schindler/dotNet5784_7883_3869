
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

internal class TaskImplementation : ITask
{
    const string FILENAME = @"..\xml\tasks.xml";

    /// <summary>
    /// returns a task by some kind attribute.
    /// </summary>
    /// <param name="filter">The attributethat the search works by</param>
    /// <returns></returns>
    public int Create(Task item)
    {
       
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        int newID = Config.NextTaskId;
        Task newTask = item with { ID = newID };
        tasks?.Add(newTask);
        XMLTools.SaveListToXMLSerializer(tasks!, "tasks");
        return item.ID;
    }


    /// <summary>
    /// delete task entity
    /// </summary>
    /// <param name="id">wanted task to delete</param>
    /// <exception cref="Exception">there's no such task with the wanted ID</exception>
    public void Delete(int id)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task? task = tasks?.FirstOrDefault(task => task.ID == id);
        if (task is null)
            throw new DalDoesNotExistException($"Task with ID={id} already not exists\n");
        tasks?.Remove(task);
        XMLTools.SaveListToXMLSerializer(tasks!, "tasks");
    }

    /// <summary>
    /// returns a task by some kind attribute.
    /// </summary>
    /// <param name="filter">The attributethat the search works by</param>
    /// <returns></returns>
    public Task? Read(Func<Task, bool> filter)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        return tasks!.FirstOrDefault(filter);
    }


    /// <summary>
    /// display one task
    /// </summary>
    /// <param name="id">the wanted task</param>
    /// <returns>wanted task</returns>
    public Task? Read(int id)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        return (tasks?.Find(element => element!.ID == id));
    }


    /// <summary>
    /// return all the task's entities
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        if (filter == null)
            return tasks!.Select(item => item);
        else
            return tasks!.Where(filter);
    }

    /// <summary>
    /// update specific task entity
    /// </summary>
    /// <param name="item">wanted task's</param>
    /// <exception cref="Exception">there's no such task with the wanted ID</exception>
    public void Update(Task item)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task? task = tasks?.FirstOrDefault(task => task.ID == item.ID);
        if (task == null)
            throw new DalDoesNotExistException($"Task with ID={item.ID} already not exists\n");
        else
        {
            tasks?.Remove(task);
            tasks?.Add(item);
            XMLTools.SaveListToXMLSerializer(tasks!, "tasks");
        }
    }
}
