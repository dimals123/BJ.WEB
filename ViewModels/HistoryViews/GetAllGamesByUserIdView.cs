using System;
using System.Collections.Generic;

namespace BJ.ViewModels.HistoryViews
{
    public class GetAllGamesByUserIdView
    {
        public List<GameGetAllGamesByUserIdViewItem> Games { get; set; }

        public GetAllGamesByUserIdView()
        {
            Games = new List<GameGetAllGamesByUserIdViewItem>();
        }
    }

    public class GameGetAllGamesByUserIdViewItem
    {
        public DateTime DateTime { get; set; }
        public int CountBots { get; set; }
        public string Winner { get; set; }

    }
}
