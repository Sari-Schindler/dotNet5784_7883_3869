
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

internal class DependencyImplementation : IDependency
{
    const string FILENAME = @"..\..\..\dependencys.xml";

    public int Create(Dependency item)
    {
        XDocument xDoc = XDocument.Load(FILENAME);
        int IDReplace = Config.NextTaskId;
        XElement newDependency = new("Dependency",
                 new XElement("ID", IDReplace),
                 new XElement("DependentTask", item.DependentTask),
                  new XElement("previousIDTask", item.previousIDTask));
        return IDReplace;
    }

    public void Delete(int id)
    {
        XElement dependencys = XDocument.LoadListFromXMLElement();
        var tempDependency = DataSource.Dependencys.FirstOrDefault(element => element!.ID == id, null);
        if (tempDependency is null)
            throw new DalDoesNotExistException($"dependency with ID={id} already not exists\n");
        DataSource.Dependencys.Remove(tempDependency);
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}


XDocument xDoc = XDocument.Load(FILENAME);
string? color = xDoc.Root?.Element("Colors")?.Element("Console")?.Value;
string? color2=xDoc.Root?.Descendants("Console").First().Value;
// שימו לב שאפשר לכתוב שאילתות לינק וביטויי למבדא בדיוק כמו באוספים
ConsoleColor x;
Enum.TryParse<ConsoleColor>(color, out x); 
  Console.BackgroundColor =x;

  // הוספה ידנית לקובץ xml
  XDocument xDocProducts = XDocument.Load(FILENAME2);

XElement el = new("Product",
               new XElement("Name", "לבן"),
               new XAttribute("category", "מוצרי חלב"));

List<Kalah> list = new()
  {   new Kalah() {  Name="Toybi" },
      new Kalah() {  Name="Dvora" },
      new Kalah() {  Name="Roysi" },
      new Kalah() {  Name="sari" }};
const string FILEKALA = @"..\..\files\kalot.xml";
const string FILEKALAJson = @"..\..\files\kalot.Json";