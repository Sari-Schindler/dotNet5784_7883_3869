
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

internal class DependencyImplementation : IDependency
{
    const string FILENAME = @"..\xml\dependencys.xml";
    XElement dependencyXml = XMLTools.LoadListFromXMLElement("dependencys");


    static DO.Dependency? getDependency(XElement? s) =>
      s!.ToIntNullable("ID") is null ? null : new DO.Dependency()
      {
          ID = (int)s.Element("ID")!,
          DependentTask = (int)s.Element("DependentTask")!,
          previousIDTask = (int)s.Element("previousIDTask")!,
      };

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
        XElement? tempDependency = dependencyXml!.Elements("Dependency").Where(p => p.Element("ID")?.Value == id.ToString()).FirstOrDefault()
            ?? throw new DalDoesNotExistException($"Dependency with ID={id} doesn't exist"); ;
        tempDependency!.Remove();
        XMLTools.SaveListToXMLElement(dependencyXml, "dependencys");
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
     
        XElement? dependency = dependencyXml.Descendants("Dependence").FirstOrDefault(element => filter(element.parseDependency()!));
        if (dependency == null)
            return null;
        return XMLTools.parseDependency(dependency);
    }

    public Dependency? Read(int id)
    {
        XElement? tempDependency = dependencyXml!.Elements("Dependency").Where(p => p.Element("ID")?.Value == id.ToString()).FirstOrDefault();
        return XMLTools.parseDependency(tempDependency!);
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
        XElement? tempDependency = dependencyXml!.Elements("Dependency").Where(p => p.Element("ID")?.Value == item.ID.ToString()).FirstOrDefault();
        if (tempDependency is null)
            throw new DalDoesNotExistException($"Dependency with ID={item.ID} doesn't exist"); 
        tempDependency!.Remove();
        XElement dependence = new XElement("Dependency",
         new XElement("DependentTask", item.DependentTask),
         new XElement("previousIDTask", item.previousIDTask),
         new XElement("ID", item.ID)
     );
        dependencyXml.Add(dependence);
        dependencyXml.Save(FILENAME);

    }
}
