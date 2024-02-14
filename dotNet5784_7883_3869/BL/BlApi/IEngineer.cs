using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

/// <summary>
/// Include all methods of the engineer
/// </summary>
public interface IEngineer
{
    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null);
    public BO.Engineer Read(int ID);
    public int Create(BO.Engineer newEngineer);
    public void Delete(int engineerID);
    public void Update(BO.Engineer engineer);

}
