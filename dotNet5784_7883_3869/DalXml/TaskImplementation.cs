
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
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Task>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Task>? tasks = (List<Task>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        int newID = Config.NextTaskId;
        Task newTask = item with { ID = newID };
        tasks?.Add(newTask);
        StreamWriter writer = new StreamWriter(FILENAME);
        xmlSerializer.Serialize(writer, tasks);
        writer.Close();
        return newID;


    }

    public void Delete(int id)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Task>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Task>? tasks = (List<Task>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        Task? task = tasks?.FirstOrDefault(task => task.ID == id);
        if (task is null)
            throw new DalDoesNotExistException($"Task with ID={id} already not exists\n");
        tasks?.Remove(task);
        StreamWriter writer = new StreamWriter(FILENAME);
        xmlSerializer.Serialize(writer, task);
        writer.Close();
    }

    public Task? Read(Func<Task, bool> filter)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Task>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Task>? tasks = (List<Task>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        return tasks!.FirstOrDefault(filter);
    }

    public Task? Read(int id)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Task>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Task>? tasks = (List<Task>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        return (tasks!.Find(element => element!.ID == id));
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Task>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Task>? tasks = (List<Task>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        if (filter == null)
            return tasks!.Select(item => item);
        else
            return tasks!.Where(filter);
    }

    public void Update(Task item)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Task>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Task>? tasks = (List<Task>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        Task? task = tasks?.FirstOrDefault(task => task.ID == item.ID);
        if (task == null)
            throw new DalDoesNotExistException($"Task with ID={item.ID} already not exists\n");
        else
        {
            tasks?.Remove(task);
            tasks?.Add(item);
            StreamWriter writer = new StreamWriter(FILENAME);
            xmlSerializer.Serialize(writer, task);
            writer.Close();
        }
    }
}
