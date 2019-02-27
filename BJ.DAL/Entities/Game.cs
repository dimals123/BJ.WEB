using BJ.DAL.Entities.Enums;

namespace BJ.DAL.Entities
{
    public class Game:BaseEntity
    {
        public string WinnerId { get; set; }
        public PlayerType Player { get; set; }


    }
}
