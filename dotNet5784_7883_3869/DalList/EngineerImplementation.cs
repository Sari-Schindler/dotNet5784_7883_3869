﻿

namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        if (DataSource.Engineers.Find(element => element!.ID == item.ID) is not null)
            throw new Exception("An object of type Engineer with such an ID already exists");
        DataSource.Engineers.Add(item with { });
        return item.ID;
    }

    public void Delete(int id)
    {
        Engineer? tempEngineer = (DataSource.Engineers.Find(element => element!.ID == id));
        if (tempEngineer is null)
            throw new Exception("An object of type Engineer with such an ID does not exist");
        DataSource.Engineers.Remove(tempEngineer);
    }

    public Engineer? Read(int id)
    {
        return (DataSource.Engineers.Find(element => element!.ID == id));
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        Engineer ?tempEngineer = (DataSource.Engineers.Find(element => element!.ID == item.ID));
        if (tempEngineer is null)
            throw new Exception("An object of type Engineer with such an ID does not exist");
        else
        {
            DataSource.Engineers.Remove(tempEngineer);  
            DataSource.Engineers.Add(item); 
        }
    }
}