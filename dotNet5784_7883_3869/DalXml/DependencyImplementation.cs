
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



    /// <summary>
    /// create a new dependency entity
    /// </summary>
    /// <param name="item">wanted dependency to add</param>
    /// <returns>wanted dependency</returns>
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

    /// <summary>
    /// delete dependency entity
    /// </summary>
    /// <param name="id">wanted dependency to delete</param>
    /// <exception cref="Exception">there's no such dependency with the wanted ID</exception>
    public void Delete(int id)
    {
        XElement? tempDependency = dependencyXml?.Elements("Dependency").Where(p => p.Element("ID")?.Value == id.ToString()).FirstOrDefault()
            ?? throw new DalDoesNotExistException($"Dependency with ID={id} doesn't exist"); ;
        tempDependency!.Remove();
        XMLTools.SaveListToXMLElement(dependencyXml, "dependencys");
    }


    /// <summary>
    /// display one dependency
    /// </summary>
    /// <param name="id">the wanted dependency</param>
    /// <returns>wanted dependency</returns>
    public Dependency? Read(int id)
    {
        XElement? tempDependency = dependencyXml?.Elements("Dependency").Where(p => p.Element("ID")?.Value == id.ToString()).FirstOrDefault();
        return XMLTools.parseDependency(tempDependency!);
    }

    /// <summary>
    /// returns a Dependency by some kind attribute.
    /// </summary>
    /// <param name="filter">The attributethat the search works by</param>
    /// <returns></returns>
    public Dependency? Read(Func<Dependency, bool> filter)
    {

        XElement? dependency = dependencyXml.Descendants("Dependency").FirstOrDefault(element => filter(element.parseDependency()!));
        if (dependency == null)
            return null;
        return XMLTools.parseDependency(dependency);
    }

    /// <summary>
    /// return all the dependency's entities
    /// </summary>
    /// <returns></returns>
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


    /// <summary>
    /// update specific dependency entity
    /// </summary>
    /// <param name="item">wanted dependency's ID</param>
    /// <exception cref="Exception">there's no such dependency with the wanted ID</exception>
    public void Update(Dependency item)
    {
        XElement? tempDependency = dependencyXml?.Elements("Dependency").Where(p => p.Element("ID")?.Value == item.ID.ToString()).FirstOrDefault();
        if (tempDependency is null)
            throw new DalDoesNotExistException($"Dependency with ID={item.ID} doesn't exist"); 
        tempDependency!.Remove();
        XElement dependence = new XElement("Dependency",
         new XElement("DependentTask", item.DependentTask),
         new XElement("previousIDTask", item.previousIDTask),
         new XElement("ID", item.ID)
     );
        dependencyXml?.Add(dependence);
        dependencyXml?.Save(FILENAME);

    }
}
