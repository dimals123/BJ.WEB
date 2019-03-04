using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BJ.DAL.Entities
{
    public class PointUser:BaseEntity
    {
        public int CountPoint { get; set; }


        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User Account { get; set; }


        public Guid GameId { get; set; }
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
    }
}
