

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class DependencyImplementation : IDependency
{
    

    /// <summary>
    /// create a new dependency entity
    /// </summary>
    /// <param name="item">wanted dependency to add</param>
    /// <returns>wanted dependency</returns>
    public int Create(Dependency item)
    {
        int IDReplace = DataSource.Config.NextDependencyId;
        Dependency newDependency= item with { ID=IDReplace };   
        DataSource.Dependencys.Add(newDependency);  
        return IDReplace;
    }

    /// <summary>
    /// delete dependency entity
    /// </summary>
    /// <param name="id">wanted dependency to delete</param>
    /// <exception cref="Exception">there's no such dependency with the wanted ID</exception>
    public void Delete(int id)
    {
        var tempDependency = DataSource.Dependencys.FirstOrDefault(element => element!.ID == id,null);
        if (tempDependency is null)
            throw new DalDoesNotExistException($"dependency with ID={id} already not exists\n");
        DataSource.Dependencys.Remove(tempDependency);
    }

    /// <summary>
    /// display one dependency
    /// </summary>
    /// <param name="id">the wanted dependency</param>
    /// <returns>wanted dependency</returns>
    public Dependency? Read(int id)
    {
        return (DataSource.Dependencys.FirstOrDefault(element => element!.ID == id, null));
    }

    /// <summary>
    /// returns a Dependency by some kind attribute.
    /// </summary>
    /// <param name="filter">The attributethat the search works by</param>
    /// <returns></returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return DataSource.Dependencys.FirstOrDefault(filter);
    }

    /// <summary>
    /// return all the dependency's entities
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null) //stage 2
    {
        if (filter != null)
        {
            return from item in DataSource.Dependencys
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependencys
               select item;
    }

    /// <summary>
    /// update specific dependency entity
    /// </summary>
    /// <param name="item">wanted dependency's ID</param>
    /// <exception cref="Exception">there's no such dependency with the wanted ID</exception>
    public void Update(Dependency item)
    {
        var tempDependency = DataSource.Dependencys.FirstOrDefault(element => element!.ID == item.ID, null);
        if (tempDependency is null)
            throw new DalAlreadyExistsException($"engineer with ID={item.ID} already not exists\n");
        {
            DataSource.Dependencys.Remove(tempDependency);
            DataSource.Dependencys.Add(item);
        }
    }

}
