using BJ.DAL.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.DAL.Entities
{
    public class Card:BaseEntity
    {
        public SuitType Suit { get; set; }
        public RankType Rank { get; set; }
        
        //public Guid GameId { get; set; }
        //[ForeignKey("GameId")]
        //public virtual Game Game { get; set; }

    }
}
