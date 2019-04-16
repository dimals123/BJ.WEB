using BJ.BusinessLogic.Extensions;
using BJ.BusinessLogic.Helpers.Interfaces;
using BJ.DataAccess.Entities.Enums;

namespace BJ.BusinessLogic.Helpers
{
    public class ScoreHelper : IScoreHelper
    {
        public int GetValueCard(RankType rankType, int currentPoints)
        {

            var result = rankType.GetDescription();
            var aceRank = RankType.Ace.GetDescription();
            result = result == aceRank & currentPoints > 10 ? 1 : result;
            return result;
        }
    }

}
