

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
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
        Dependency? tempDependency = (DataSource.Dependencys.Find(element => element!.ID == id));
        if (tempDependency is null)
            throw new Exception("An object of type Dependency with such an ID does not exist");
        DataSource.Dependencys.Remove(tempDependency);
    }

    /// <summary>
    /// display one dependency
    /// </summary>
    /// <param name="id">the wanted dependency</param>
    /// <returns>wanted dependency</returns>
    public Dependency? Read(int id)
    {
        return (DataSource.Dependencys.Find(element => element!.ID == id));
    }


    /// <summary>
    /// return all the dependency's entities
    /// </summary>
    /// <returns></returns>
    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencys);
    }


    /// <summary>
    /// update specific dependency entity
    /// </summary>
    /// <param name="item">wanted dependency's ID</param>
    /// <exception cref="Exception">there's no such dependency with the wanted ID</exception>
    public void Update(Dependency item)
    {
        Dependency ?tempDependency = (DataSource.Dependencys.Find(element => element!.ID == item.ID));
        if (tempDependency is null)
            throw new Exception("An object of type Dependency with such an ID does not exist");
        else
        {
            DataSource.Dependencys.Remove(tempDependency);
            DataSource.Dependencys.Add(item);
        }
    }
}
