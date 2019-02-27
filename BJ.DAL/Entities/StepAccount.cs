using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.DAL.Entities
{
    public class StepAccount:BaseEntity
    {
        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual User Account { get; set; }
        public Guid CardId { get; set; }
        [ForeignKey("CardId")]
        public virtual Card Card { get; set; }
        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        public int CountStep { get; set; }
        public Suit Suit { get; set; }
        public Value Value { get; set; }
            

    }
}
