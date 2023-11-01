

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int IDReplace = DataSource.Config.NextDependencyId;
        Dependency newDependency= item with { ID=IDReplace };   
        DataSource.Dependencys.Add(newDependency);  
        return IDReplace;
    }

    public void Delete(int id)
    {
        Dependency? tempDependency = (DataSource.Dependencys.Find(element => element!.ID == id));
        if (tempDependency is null)
            throw new Exception("An object of type Dependency with such an ID does not exist");
        DataSource.Dependencys.Remove(tempDependency);
    }

    public Dependency? Read(int id)
    {
        return (DataSource.Dependencys.Find(element => element!.ID == id));
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencys);
    }

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
