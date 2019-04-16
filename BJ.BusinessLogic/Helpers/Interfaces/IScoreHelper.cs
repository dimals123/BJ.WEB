using BJ.DataAccess.Entities.Enums;

namespace BJ.BusinessLogic.Helpers.Interfaces
{
    public interface IScoreHelper
    {
        int GetValueCard(RankType rank, int currentPoints);
    }
}
