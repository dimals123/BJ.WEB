using BJ.DAL.Entities.Enums;

namespace BJ.DAL.Entities
{
    public class Game:BaseEntity
    {
        public string WinnerName { get; set; }
        public PlayerType Player { get; set; }
        public int CountBots { get; set; }
        public bool IsEnd { get; set; }


    }
}
