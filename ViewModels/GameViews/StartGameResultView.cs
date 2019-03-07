using BJ.DAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.GameViews
{
    public class StartGameResultView
    {
        public UserStartGameResultView User { get; set; }
        public List<BotStartGameViewItem> Bots { get; set; }

    }

    public class UserStartGameResultView
    {
        public string Name { get; set; }
        public List<StepUserStartGameResultViewItem> Cards { get; set; }
        public PointUserStartGameView Points { get; set; }
    }

    public class StepUserStartGameResultViewItem
    {
        public SuitType Suit { get; set; }
        public RankType Rank { get; set; }

    }

    public class PointUserStartGameView
    {
        public int CountPoint { get; set; }
    }

    public class BotStartGameViewItem
    {
        public string Name { get; set; }
        public List<StepBotStartGameViewItem> Cards { get; set; }
        public PointBotStartGameView Points { get; set; }
    }

    public class StepBotStartGameViewItem
    {
        public SuitType Suit { get; set; }
        public RankType Rank { get; set; }
    }

    public class PointBotStartGameView
    {
        public int CountPoint { get; set; }
    }

}
