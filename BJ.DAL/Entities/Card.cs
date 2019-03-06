using BJ.DAL.Entities.Enums;

namespace BJ.DAL.Entities
{
    public class Card:BaseEntity
    {
        public SuitType Suit { get; set; }
        public RankType Rank { get; set; }

    }
}
