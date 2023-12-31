

using BlApi;

namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public BO.Milestone getMilestoneDetials(int ID)
    {
        throw new NotImplementedException();
    }

    public BO.Milestone updateMilestone(int ID)
    {
        throw new NotImplementedException();
    }
}
