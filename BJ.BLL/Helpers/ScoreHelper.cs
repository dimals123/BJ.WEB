using BJ.BusinessLogic.Helpers.Interfaces;
using BJ.DataAccess.Entities.Enums;

namespace BJ.BusinessLogic.Helpers
{
    public class ScoreHelper:IScoreHelper
    {
        public int ValueCard(RankType rank, int points)
        {
            var result = 0;
            switch (rank)
            {
                case RankType.Six:
                    result = 6;
                    break;
                case RankType.Seven:
                    result = 7;
                    break;
                case RankType.Eight:
                    result = 8;
                    break;
                case RankType.Nine:
                    result = 9;
                    break;
                case RankType.Ten:
                    result = 10;
                    break;
                case RankType.Jack:
                    result = 2;
                    break;
                case RankType.Lady:
                    result = 3;
                    break;
                case RankType.King:
                    result = 4;
                    break;
                case RankType.Ace:
                    result = (points <= 10) ? 11 : 1;
                    break;
            }
            return result;
        }
    }
}
