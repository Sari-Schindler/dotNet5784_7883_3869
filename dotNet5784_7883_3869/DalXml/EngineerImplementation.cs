
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Reflection;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;

internal class EngineerImplementation : IEngineer
{
    const string FILENAME = @"..\..\..\engineers.xml";
    public int Create(Engineer item)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Engineer>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Engineer>? engineers = (List<Engineer>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        Engineer? engineer = engineers?.FirstOrDefault(engineer => engineer.ID == item.ID);
        if ( engineer != null )
            throw new DalAlreadyExistsException( $"an Engineer with such an ID {engineer.ID} already exist");
        Engineer newEngineer = item with { };
        engineers?.Add(newEngineer);
        StreamWriter writer = new StreamWriter(FILENAME);
        xmlSerializer.Serialize(writer, engineer);
        writer.Close();
        return newEngineer.ID;
    }

    public void Delete(int id)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Engineer>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Engineer>? engineers = (List<Engineer>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        Engineer? engineer = engineers?.FirstOrDefault(engineer => engineer.ID == id);
        if (engineer is null)
            throw new DalDoesNotExistException($"engineer with ID={id} already not exists\n");
        engineers?.Remove(engineer);
        StreamWriter writer = new StreamWriter(FILENAME);
        xmlSerializer.Serialize(writer, engineer);
        writer.Close();
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Engineer>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Engineer>? engineers = (List<Engineer>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        return engineers!.FirstOrDefault(filter);
    }

    public Engineer? Read(int id)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Engineer>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Engineer>? engineers = (List<Engineer>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        return (engineers!.Find(element => element!.ID == id));
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Engineer>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Engineer>? engineers = (List<Engineer>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        if (filter == null)
            return engineers!.Select(item => item);
        else
            return engineers!.Where(filter);
    }

    public void Update(Engineer item)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Engineer>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Engineer>? engineers = (List<Engineer>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        Engineer? engineer = engineers?.FirstOrDefault(engineer => engineer.ID == item.ID);
        if (engineer == null)
            throw new DalDoesNotExistException($"engineer with ID={item.ID} already not exists\n");
        else
        {
            engineers?.Remove(engineer);
            engineers?.Add(item);
            StreamWriter writer = new StreamWriter(FILENAME);
            xmlSerializer.Serialize(writer, engineer);
            writer.Close();
        }
    }
}

