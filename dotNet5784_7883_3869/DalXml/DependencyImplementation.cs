
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

internal class DependencyImplementation : IDependency
{
    const string FILENAME = @"..\..\..\dependencys.xml";
    XElement dependencyRoot = XMLTools.LoadListFromXMLElement("dependencys");

    public int Create(Dependency item)
    {
        XDocument xDoc = XDocument.Load(FILENAME);
        int IDReplace = Config.NextTaskId;
        XElement newDependency = new("Dependency",
                 new XElement("ID", IDReplace),
                 new XElement("DependentTask", item.DependentTask),
                  new XElement("previousIDTask", item.previousIDTask));
        XMLTools.SaveListToXMLElement(dependencyRoot, "dependencys");
        return IDReplace;
    }

    public void Delete(int id)
    {
        XElement? tempDependency = dependencyRoot.Elements("ArrayOfDependency").First(x => XMLTools.ToIntNullable(x, "ID") == id);
            ?? throw new DalDoesNotExistException($"Dependency with ID={id} doesn't exist"); ;
        tempDependency!.Remove();
        XMLTools.SaveListToXMLElement(dependencyRoot, "dependencys");
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        XElement? tempDep = dependencyRoot.Elements("ArrayOfDependency").First(x => filter(XMLTools.parseDependency(x)!));
        return XMLTools.parseDependency(tempDep);
    }

    public Dependency? Read(int id)
    {
        XElement? tempDependency = dependencyRoot.Elements("ArrayOfDependency").First(x => XMLTools.ToIntNullable(x, "ID") == id);
        return XMLTools.parseDependency(tempDependency);
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        return (filter is null) ?
            dependencyRoot
                .Elements("Dependency")
           .Select(el => XMLTools.parseDependency(el))
           : dependencyRoot
                .Elements("Dependency")
                .Where(el => filter(XMLTools.parseDependency(el)!))
           .Select(el => XMLTools.parseDependency(el));
    }

    public void Update(Dependency item)
    {
        XElement? tempDependency = dependencyRoot.Elements("ArrayOfDependency").First(x => XMLTools.ToIntNullable(x, "Id") == item.ID);
        if (tempDependency is null)
            throw new DalDoesNotExistException($"Dependency with ID={item.ID} doesn't exist"); ;
        {
            tempDependency!.Remove();
            dependencyRoot.Add(item);
        }
    }
}
