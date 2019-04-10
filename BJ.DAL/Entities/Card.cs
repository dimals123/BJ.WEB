using BJ.DataAccess.Entities.Enums;
using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.DataAccess.Entities
{
    public class Card:BaseEntity
    {
        public SuitType Suit { get; set; }
        public RankType Rank { get; set; }

        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        [Write(false)]
        public virtual Game Game { get; set; }

    }
}
