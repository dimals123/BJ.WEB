using System;
using System.Collections.Generic;

namespace BJ.ViewModels.HistoryViews
{
    public class GetUserGamesHistoryView
    {
        public List<GameGetUserGamesHistoryViewItem> Games { get; set; }

        public GetUserGamesHistoryView()
        {
            Games = new List<GameGetUserGamesHistoryViewItem>();
        }
    }

    public class GameGetUserGamesHistoryViewItem
    {
        public Guid GameId { get; set; }
        public DateTime DateTime { get; set; }
        public int CountBots { get; set; }
        public string Winner { get; set; }
        public bool IsWinner { get; set; }

    }
}
