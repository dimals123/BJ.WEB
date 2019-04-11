using System.Collections.Generic;
using BJ.ViewModels.EnumsViews;

namespace BJ.ViewModels.GameViews
{
    public class GetDetailsGameResponseView
    {
        public UserGetDetailsGameResponseView User { get; set; }
        public List<BotGetDetailsGameResponseViewItem> Bots { get; set; }
        public string Winner { get; set; }

        public GetDetailsGameResponseView()
        {
            Bots = new List<BotGetDetailsGameResponseViewItem>();
        }

    }

    public class UserGetDetailsGameResponseView
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public List<StepUserGetDetailsGameResponseViewItem> Cards { get; set; }

        public UserGetDetailsGameResponseView()
        {
            Cards = new List<StepUserGetDetailsGameResponseViewItem>();
        }
    }

    public class StepUserGetDetailsGameResponseViewItem
    {
        public SuitTypeView Suit { get; set; }
        public RankTypeView Rank { get; set; }

    }

    public class BotGetDetailsGameResponseViewItem
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public List<StepBotGetDetailsGameResponseViewItem> Cards { get; set; }

        public BotGetDetailsGameResponseViewItem()
        {
            Cards = new List<StepBotGetDetailsGameResponseViewItem>();
        }

    }

    public class StepBotGetDetailsGameResponseViewItem
    {
        public SuitTypeView Suit { get; set; }
        public RankTypeView Rank { get; set; }
    }



}
