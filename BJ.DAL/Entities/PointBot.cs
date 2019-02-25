using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BJ.DAL.Entities
{
    public class PointBot:BaseEntity
    {
        public int CountPoint { get; set; }


        public Guid BotId { get; set; }
        [ForeignKey("BotId")]
        public Bot Bot { get; set; }


        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
    }
}
