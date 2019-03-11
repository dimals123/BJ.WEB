using BJ.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.GameViews
{
    public class StartGameResultView
    {
        public UserStartGameResultView User { get; set; }
        public List<BotStartGameResultViewItem> Bots { get; set; }
        public Guid GameId { get; set; }

        public StartGameResultView()
        {
            Bots = new List<BotStartGameResultViewItem>();
        }

    }

    public class UserStartGameResultView
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public List<StepUserStartGameResultViewItem> Cards { get; set; }

        public UserStartGameResultView()
        {
            Cards = new List<StepUserStartGameResultViewItem>();
        }
    }

    public class StepUserStartGameResultViewItem
    {
        public SuitType Suit { get; set; }
        public RankType Rank { get; set; }

    }

    public class BotStartGameResultViewItem
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public List<StepBotStartGameResultViewItem> Cards { get; set; }

        public BotStartGameResultViewItem()
        {
            Cards = new List<StepBotStartGameResultViewItem>();
        }

    }

    public class StepBotStartGameResultViewItem
    {
        public SuitType Suit { get; set; }
        public RankType Rank { get; set; }
    }

   

}
