
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


    /// <summary>
    /// create a new engineer entity
    /// </summary>
    /// <param name="item">wanted engineer to add</param>
    /// <returns>new entity</returns>
    /// <exception cref="Exception">there's no such engineer with the wanted ID</exception>
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

    /// <summary>
    /// delete engineer entity
    /// </summary>
    /// <param name="id">wanted engineer to delete</param>
    /// <exception cref="Exception">there's no such engineer with the wanted ID</exception>
    public void Delete(int id)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = engineers?.FirstOrDefault(engineer => engineer.ID == id);
        if (engineer is null)
            throw new DalDoesNotExistException($"engineer with ID={id} already not exists\n");
        engineers?.Remove(engineer);
        XMLTools.SaveListToXMLSerializer(engineers!, "engineers");
    }


    /// <summary>
    /// returns a Engineer by some kind attribute.
    /// </summary>
    /// <param name="filter">The attributethat the search works by</param>
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return engineers!.FirstOrDefault(filter);
    }

    /// <summary>
    /// display one engineer
    /// </summary>
    /// <param name="id">the wanted engineer</param>
    /// <returns></returns>
    public Engineer? Read(int id)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        return (engineers!.Find(element => element!.ID == id));
    }

    /// <summary>
    /// return all the engineer's entities
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        if (filter == null)
            return engineers!.Select(item => item);
        else
            return engineers!.Where(filter);
    }

    /// <summary>
    /// update specific engineer entity
    /// </summary>
    /// <param name="item">wanted engineer's</param>
    /// <exception cref="Exception">there's no such engineer with the wanted ID</exception>
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

