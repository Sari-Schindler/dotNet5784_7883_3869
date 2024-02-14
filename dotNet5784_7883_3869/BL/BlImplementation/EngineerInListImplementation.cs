using System;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using BlApi;
using BO;
using DO;


namespace BlImplementation;

internal class EngineerInListImplementation : IEngineerInList
{
    private DalApi.IDal _dal = DalApi.Factory.Get;


    /// <summary>
    /// Return all enginners in list
    /// </summary>
    /// <param name="filter">return just the engineers that fit the standarts</param>
    /// <returns></returns>
    /// <exception cref="BlDoesNotExistException"></exception>
    /// <exception cref="BO.BlDoesNotExistException"></exception>
    public IEnumerable<BO.EngineerInList> ReadAll(Func<BO.EngineerInList, bool>? filter = null)
    {
        try
        {
            var allEngineers = _dal.Engineer.ReadAll();
            if (!allEngineers.Any())
                throw new BlDoesNotExistException("no engineer exist");
            IEnumerable<BO.EngineerInList> newEngineers = from allEngineer in allEngineers
                                                    select new BO.EngineerInList
                                                    {
                                                        ID = allEngineer.ID,
                                                        Name = allEngineer.Name,
                                                        Level = (BO.EngineerExperience)allEngineer.Level
                                                    };
            return filter == null ? newEngineers : newEngineers.Where(filter);
        }
        catch (DO.DalDoesNotExistException exception)
        {
            throw new BO.BlDoesNotExistException($"no engineers found", exception);
        }
    }
}
