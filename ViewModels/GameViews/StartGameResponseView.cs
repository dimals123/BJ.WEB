using BJ.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using ViewModels.Enums;

namespace ViewModels.GameViews
{
    public class StartGameResponseView
    {
        public UserStartGameResponseView User { get; set; }
        public List<BotStartGameResponseViewItem> Bots { get; set; }
        public Guid GameId { get; set; }
        public string Winner { get; set; }

        public StartGameResponseView()
        {
            Bots = new List<BotStartGameResponseViewItem>();
        }

    }

    public class UserStartGameResponseView
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public List<StepUserStartGameResponseViewItem> Cards { get; set; }

        public UserStartGameResponseView()
        {
            Cards = new List<StepUserStartGameResponseViewItem>();
        }
    }

    public class StepUserStartGameResponseViewItem
    {
        public SuitTypeView Suit { get; set; }
        public RankTypeView Rank { get; set; }

    }

    public class BotStartGameResponseViewItem
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public List<StepBotStartGameResponseViewItem> Cards { get; set; }

        public BotStartGameResponseViewItem()
        {
            Cards = new List<StepBotStartGameResponseViewItem>();
        }

    }

    public class StepBotStartGameResponseViewItem
    {
        public SuitTypeView Suit { get; set; }
        public RankTypeView Rank { get; set; }
    }

   

}
