using BJ.BLL.Configurations;
using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.GameViews;
using static BJ.DAL.Entities.Game;

namespace BJ.BLL.Services
{
    [Authorize]
    public class GameService
    {
        //private static readonly List<Value> _values;
        //private static readonly List<Suit> _suits;

        //static GameService()

        //{
        //    _values = GetValues();
        //    _suits = GetSuits();
        //}



        //private static List<Value> GetValues()
        //{
        //    var ranks = Enum.GetValues(typeof(Value)).Cast<Value>().ToList();     
        //    return ranks;
        //}

        //private static List<Suit> GetSuits()
        //{
        //    var suit = Enum.GetValues(typeof(Suit)).Cast<Suit>().ToList();
        //    return suit;
        //}

        //private readonly IUnitOfWork _unitOfWork;

        //public GameService(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

       
        //public async Task Start(Guid AccountId, StartGameView startGameView)
        //{
        //    var game = new Game();
        //    await _unitOfWork.Games.CreateAsync(game);

        //    var bots = HostBots.Init(_unitOfWork, startGameView.CountBots);
        //    var deck = HostDeck.Init(_unitOfWork);

        //    await GetCard(AccountId, game.Id, deck, 0, PlayerType.User);
        //}

        //public async Task GetCard(Guid Id, Guid gameId, List<Card> cards, int stepCount, PlayerType playerType)
        //{
        //    if(playerType == PlayerType.User)
        //    {
        //        var stepAccount = new StepAccount { AccountId = Id, CardId = cards.FirstOrDefault().Id, GameId = gameId, Suit = cards.FirstOrDefault().Suit, Value = cards.FirstOrDefault().Value, CountStep = stepCount };
        //        await _unitOfWork.StepsAccounts.CreateAsync(stepAccount);
        //    }
        //    else
        //    {
        //        var stepBot = new StepBot { BotId = Id, CardId = cards.FirstOrDefault().Id, GameId = gameId, Suit = cards.FirstOrDefault().Suit, Value = cards.FirstOrDefault().Value, CountStep = stepCount };
        //        await _unitOfWork.StepsBots.CreateAsync(stepBot);
        //    }
            
        //}

       
    }
}
