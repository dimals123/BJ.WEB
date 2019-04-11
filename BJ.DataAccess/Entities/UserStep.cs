using BJ.DataAccess.Entities.Enums;
using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.DataAccess.Entities
{
    public class UserStep:BaseEntity
    {
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [Write(false)]
        public virtual User User { get; set; }
        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        [Write(false)]
        public virtual Game Game { get; set; }

        public int StepNumber { get; set; }
        public SuitType Suit { get; set; }
        public RankType Rank { get; set; }
            

    }
}
