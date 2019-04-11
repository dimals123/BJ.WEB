using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.DataAccess.Entities
{
    public class BotInGame:BaseEntity
    {
        public int CountPoint { get; set; }


        public Guid BotId { get; set; }
        [ForeignKey("BotId")]
        [Write(false)]
        public virtual Bot Bot { get; set; }


        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        [Write(false)]
        public virtual Game Game { get; set; }
    }
}
