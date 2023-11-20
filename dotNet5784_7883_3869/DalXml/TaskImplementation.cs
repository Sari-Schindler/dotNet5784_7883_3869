
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

internal class TaskImplementation : ITask
{
    const string FILENAME = @"..\xml\tasks.xml";
    public int Create(Task item)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task newEngineer = item with {};
        tasks?.Add(newEngineer);
        XMLTools.SaveListToXMLSerializer(tasks!, "tasks");
        return newEngineer.ID;
    }

    public void Delete(int id)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        Task? task = tasks?.FirstOrDefault(task => task.ID == id);
        if (task is null)
            throw new DalDoesNotExistException($"Task with ID={id} already not exists\n");
        tasks?.Remove(task);
        XMLTools.SaveListToXMLSerializer(tasks!, "tasks");
    }

    public Task? Read(Func<Task, bool> filter)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        return tasks!.FirstOrDefault(filter);
    }

    public Task? Read(int id)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        return (tasks!.Find(element => element!.ID == id));
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        List<Task>? tasks = XMLTools.LoadListFromXMLSerializer<Task>("tasks");
        if (filter == null)
            return tasks!.Select(item => item);
        else
            return tasks!.Where(filter);
    }

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
