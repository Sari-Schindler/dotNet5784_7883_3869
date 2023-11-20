
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

internal class DependencyImplementation : IDependency
{
    const string FILENAME = @"..\xml\dependencys.xml";
    XElement dependencyXml = XMLTools.LoadListFromXMLElement("dependencys");

    public int Create(Dependency item)
    {
        int IDReplace = Config.NextDependencyId;
        XElement dependence = new XElement("Dependency",
            new XElement("DependentTask", item.DependentTask),
            new XElement("previousIDTask", item.previousIDTask),
            new XElement("ID", IDReplace)
        );
        dependencyXml.Add(dependence);
        dependencyXml.Save(FILENAME);
        return IDReplace;

    }
    public void Delete(int id)
    {
        XElement? tempDependency = dependencyXml.Elements("ArrayOfDependency").First(element => XMLTools.ToIntNullable(element, "ID") == id)
            ?? throw new DalDoesNotExistException($"Dependency with ID={id} doesn't exist"); ;
        tempDependency!.Remove();
        XMLTools.SaveListToXMLElement(dependencyXml, "dependencys");
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        XElement? tempDep = dependencyXml.Elements("ArrayOfDependency").First(element => filter(XMLTools.parseDependency(element)!));
        return XMLTools.parseDependency(tempDep);
    }

    public Dependency? Read(int id)
    {
        XElement ?tempDependency = dependencyXml!.Elements("Dependency").FirstOrDefault(element => (int)element.Element("ID") == id);
        //if (tempDependency == null)
        //   return null;
        return tempDependency.ToEntity<Dependency>();
          //return XMLTools.parseDependency(tempDependency!);
        //XElement? tempDependency = dependencyRoot.Elements("ArrayOfDependency").First(x => XMLTools.(x, "ID") == id);
        //return XMLTools.parseDependency(tempDependency);
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        return (filter is null) ?
            dependencyXml
                .Elements("Dependency")
           .Select(el => XMLTools.parseDependency(el))
           : dependencyXml
                .Elements("Dependency")
                .Where(el => filter(XMLTools.parseDependency(el)!))
           .Select(el => XMLTools.parseDependency(el));
    }

    public void Update(Dependency item)
    {
        XElement? tempDependency = dependencyXml.Elements("ArrayOfDependency").First(elementx => XMLTools.ToIntNullable(element "Id") == item.ID);
        if (tempDependency is null)
            throw new DalDoesNotExistException($"Dependency with ID={item.ID} doesn't exist"); ;
        {
            tempDependency!.Remove();
            dependencyXml.Add(item);
        }
    }
}
