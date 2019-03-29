using BJ.DataAccess.Entities.Enums;

namespace BJ.DataAccess.Entities
{
    public class Game:BaseEntity
    {
        public string WinnerName { get; set; }
        public int CountBots { get; set; }
        public bool IsFinished { get; set; }


    }
}
