using BJ.ViewModels.EnumsViews;
using System;
using System.Collections.Generic;

namespace BJ.ViewModels.HistoryViews
{
    public class GetDetailsGameHistoryView
    {
        public GetDetailsGameHistoryView()
        {
            Bots = new List<BotGetDetailsGameHistoryViewItem>();
        }

        public DateTime DateTime { get; set; }
        public string Winner { get; set; }
        public int CountBots { get; set; }

        public UserGetDetailsGameHistoryView User { get; set; }
        public List<BotGetDetailsGameHistoryViewItem> Bots { get; set; }

    }

    public class UserGetDetailsGameHistoryView
    {
        public UserGetDetailsGameHistoryView()
        {
            Cards = new List<StepUserGetDetailsGameHistoryViewItem>();
        }

        public string Name { get; set; }
        public List<StepUserGetDetailsGameHistoryViewItem> Cards { get; set; }

    }

    public class BotGetDetailsGameHistoryViewItem
    {
        public BotGetDetailsGameHistoryViewItem()
        {
            Cards = new List<StepBotGetDetailsGameHistoryViewItem>();
        }

        public string Name { get; set; }
        public List<StepBotGetDetailsGameHistoryViewItem> Cards { get; set; }
      
    }

    public class StepUserGetDetailsGameHistoryViewItem
    {
        public SuitTypeView Suit { get; set; }
        public RankTypeView Rank { get; set; }
    }
    public class StepBotGetDetailsGameHistoryViewItem
    {
        public SuitTypeView Suit { get; set; }
        public RankTypeView Rank { get; set; }
    }
}
