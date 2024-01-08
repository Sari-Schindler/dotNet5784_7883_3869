

using BlApi;
using System.Xml.Linq;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    //יצירת לו"ז
    //private BO.Milestone convertToBo(DO.Task task)
    //{
    //    return new BO.Milestone
    //    {
    //        ID = task.ID,
    //        NickName = task.nickName,
    //        Description = task.Description,
    //        CreatedDate = task.CreatedDateTask,
    //        MilestoneStatus = task.,
    //        startDate = task.StartTime,
    //        finishTimeEstimated = task.,
    //        DeadLine = task.DeadLine,
    //        EndedDate = task.E,
    //        ProgressPercentage == 
    //        Comments=task.Comments,
    //        DependencysList= 
             

    //    };
    //}
   
    //public BO.Milestone Read(int ID)
    //{
    //    try
    //    {
    //        //return convertToBo(_dal.Task.Read((DO.Task milestone) => ID == milestone.ID));
    //    }
    //    catch (DO.DalDoesNotExistException exception)
    //    {
    //        throw new BO.BlDoesNotExistException(exception.Message);
    //    }
    //}

    public BO.Milestone Update(int ID)
    {
        throw new NotImplementedException();
    }
}
