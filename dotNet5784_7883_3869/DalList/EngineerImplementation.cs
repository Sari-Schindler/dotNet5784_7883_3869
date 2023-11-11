

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class EngineerImplementation : IEngineer
{

    /// <summary>
    /// create a new engineer entity
    /// </summary>
    /// <param name="item">wanted engineer to add</param>
    /// <returns>new entity</returns>
    /// <exception cref="Exception">there's no such engineer with the wanted ID</exception>
    public int Create(Engineer item)
    {

        if (DataSource.Engineers.FirstOrDefault(element => element!.ID == item.ID,null) is not null)
            throw new Exception("An object of type Engineer with such an ID already exists");
        DataSource.Engineers.Add(item with { });
        return item.ID;
    }


    /// <summary>
    /// delete engineer entity
    /// </summary>
    /// <param name="id">wanted engineer to delete</param>
    /// <exception cref="Exception">there's no such engineer with the wanted ID</exception>
    public void Delete(int id)
    {
        var tempEngineer = DataSource.Engineers.FirstOrDefault(element => element!.ID == id, null);
        if (tempEngineer is null)
            throw new Exception("An object of type Engineer with such an ID does not exist");
        DataSource.Engineers.Remove(tempEngineer);
    }


    /// <summary>
    /// display one engineer
    /// </summary>
    /// <param name="id">the wanted engineer</param>
    /// <returns></returns>
    public Engineer? Read(int id)
    {
        return (DataSource.Engineers.Find(element => element!.ID == id));
    }


    /// <summary>
    /// return all the engineer's entities
    /// </summary>
    /// <returns></returns>

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Engineers.Select(item => item);
        else
            return DataSource.Engineers.Where(filter);
    }

    /// <summary>
    /// update specific engineer entity
    /// </summary>
    /// <param name="item">wanted engineer's</param>
    /// <exception cref="Exception">there's no such engineer with the wanted ID</exception>
    public void Update(Engineer item)
    {
        var tempEngineer = DataSource.Engineers.FirstOrDefault(element => element!.ID == item.ID, null);
        if (tempEngineer is null)
            throw new Exception("An object of type Engineer with such an ID does not exist");
        else
        {
            DataSource.Engineers.Remove(tempEngineer);  
            DataSource.Engineers.Add(item); 
        }
    }
}
