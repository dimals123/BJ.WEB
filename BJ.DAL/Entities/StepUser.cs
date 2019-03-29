using BJ.DataAccess.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.DataAccess.Entities
{
    public class StepUser:BaseEntity
    {
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        public int StepNumber { get; set; }
        public SuitType Suit { get; set; }
        public RankType Rank { get; set; }
            

    }
}
