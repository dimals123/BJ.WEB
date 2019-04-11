using BJ.ViewModels.EnumsViews;
using System.Collections.Generic;

namespace BJ.ViewModels.GameViews
{
    public class GetUnfinishedGameResponseView
    {

        public UserGetUnfinishedGameResponseView User { get; set; }
        public List<BotGetUnfinishedGameResponseViewItem> Bots { get; set; }
        public string Winner { get; set; }

        public GetUnfinishedGameResponseView()
        {
            Bots = new List<BotGetUnfinishedGameResponseViewItem>();
        }

    }

    public class UserGetUnfinishedGameResponseView
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public List<StepUserGetUnfinishedGameResponseViewItem> Cards { get; set; }

        public UserGetUnfinishedGameResponseView()
        {
            Cards = new List<StepUserGetUnfinishedGameResponseViewItem>();
        }
    }

    public class StepUserGetUnfinishedGameResponseViewItem
    {
        public SuitTypeView Suit { get; set; }
        public RankTypeView Rank { get; set; }

    }

    public class BotGetUnfinishedGameResponseViewItem
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public List<StepBotGetUnfinishedGameResponseViewItem> Cards { get; set; }

        public BotGetUnfinishedGameResponseViewItem()
        {
            Cards = new List<StepBotGetUnfinishedGameResponseViewItem>();
        }

    }

    public class StepBotGetUnfinishedGameResponseViewItem
    {
        public SuitTypeView Suit { get; set; }
        public RankTypeView Rank { get; set; }
    }


    
}
