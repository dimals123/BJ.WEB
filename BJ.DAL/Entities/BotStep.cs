using BJ.DataAccess.Entities.Enums;
using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.DataAccess.Entities
{
    public class BotStep:BaseEntity
    {
        public Guid BotId { get; set; }
        [ForeignKey("BotId")]
        [Write(false)]
        public virtual Bot Bot { get; set; }

        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        [Write(false)]
        public virtual Game Game { get; set; }

        public int StepNumber { get; set; }
        public SuitType Suit { get; set; }
        public RankType Rank { get; set; }
    }
}
