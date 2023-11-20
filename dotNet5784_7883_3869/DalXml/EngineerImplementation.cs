
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
using System.Threading.Tasks;

internal class EngineerImplementation : IEngineer
{
    const string FILENAME = @"..\xml\engineers.xml";
    public int Create(Engineer item)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = engineers?.FirstOrDefault(engineer => engineer.ID == item.ID);
        if (engineer != null)
            throw new DalAlreadyExistsException("An engineer with this ID number already exists");
        Engineer new_engineer = item with { };
        engineers?.Add(new_engineer);
        XMLTools.SaveListToXMLSerializer(engineers!, "engineers");
        return new_engineer.ID;
    }

    public void Delete(int id)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = engineers?.FirstOrDefault(engineer => engineer.ID == id);
        if (engineer is null)
            throw new DalDoesNotExistException($"engineer with ID={id} already not exists\n");
        engineers?.Remove(engineer);
        XMLTools.SaveListToXMLSerializer(engineers!, "engineers");
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return engineers!.FirstOrDefault(filter);
    }

    public Engineer? Read(int id)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return (engineers!.Find(element => element!.ID == id));
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        if (filter == null)
            return engineers!.Select(item => item);
        else
            return engineers!.Where(filter);
    }

    public void Update(Engineer item)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = engineers?.FirstOrDefault(engineer => engineer.ID == item.ID);
        if (engineer == null)
            throw new DalDoesNotExistException($"engineer with ID={item.ID} already not exists\n");
        else
        {
            engineers?.Remove(engineer);
            engineers?.Add(item);
            XMLTools.SaveListToXMLSerializer(engineers!, "engineers");
        }
    }
}

