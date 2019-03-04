using BJ.DAL.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.DAL.Entities
{
    public class StepBot:BaseEntity
    {
        public Guid BotId { get; set; }
        [ForeignKey("BotId")]
        public virtual Bot Bot { get; set; }
        public Guid CardId { get; set; }
        [ForeignKey("CardId")]
        public virtual Card Card { get; set; }
        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        public int CountStep { get; set; }
        public SuitType Suit { get; set; }
        public RankType Value { get; set; }
    }
}
