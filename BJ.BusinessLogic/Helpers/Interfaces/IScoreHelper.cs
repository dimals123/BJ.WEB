using BJ.DataAccess.Entities.Enums;

namespace BJ.BusinessLogic.Helpers.Interfaces
{
    public interface IScoreHelper
    {
        int ValueCard(RankType rank, int currentPoints);
    }
}
