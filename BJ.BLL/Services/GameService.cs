using BJ.BLL.Configurations;
using BJ.BLL.Interfaces;
using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.BLL.Services
{
    [Authorize]
    public class GameService:IGameService
    {
        private readonly IUnitOfWork _unitOfWork;


        public GameService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }
      
        public async Task StartGame(string userId, int countBots)
        {

            var game = new Game { CountBots = countBots};
            await _unitOfWork.Games.Create(game);

            var bots = await DbInitialize.InitBots(_unitOfWork, countBots);
            var deck  = await DbInitialize.InitCards(_unitOfWork);
        
            var stepUsers = new List<StepUser>();
            var stepBots = new List<StepBot>();
            var pointsUser = new List<PointUser>();
            var pointsBots = new List<PointBot>();


            var cardsRemove = new List<Card>();

            for (int i = 0; i < 2; i++) 
            {
                var stepUser = new StepUser { GameId = game.Id, UserId = userId, Suit = deck[0].Suit, Rank = deck[0].Rank};
                stepUsers.Add(stepUser);

                var countPointUser = await _unitOfWork.PointsAccounts.GetUserIdMax(userId, game.Id);
                var pointUser = new PointUser { GameId = game.Id, UserId = userId, CountPoint = countPointUser.CountPoint };
                pointsUser.Add(pointUser);

                cardsRemove.Add(deck[0]);
                deck.Remove(deck[0]);

                for (int j = 0; j < countBots; j++)
                {
                    var stepBot = new StepBot { GameId = game.Id, BotId = bots[j].Id, Suit = deck[0].Suit, Rank = deck[0].Rank, };
                    stepBots.Add(stepBot);

                    var countPointMax = await _unitOfWork.PointsBots.GetBotIdMax(bots[j].Id, game.Id);
                    var PointBot = new PointBot { GameId = game.Id, BotId = bots[j].Id, CountPoint = countPointMax.CountPoint };
                    pointsBots.Add(PointBot);

                    cardsRemove.Add(deck[0]);
                    deck.Remove(deck[0]);
                }
            }         
            await _unitOfWork.StepsAccounts.CreateRange(stepUsers);
            await _unitOfWork.StepsBots.CreateRange(stepBots);
            _unitOfWork.Cards.DeleteRange(cardsRemove);
            await _unitOfWork.PointsAccounts.CreateRange(pointsUser);
            await _unitOfWork.PointsBots.CreateRange(pointsBots);
            _unitOfWork.Save();            
        }

        public async Task GetCard(Guid gameId, string userId, int countBots)
        {
            var ListRemoveCards = new List<Card>();
            var deck = await _unitOfWork.Cards.GetAll();
            var bots = await _unitOfWork.Bots.GetAll();


            var stepUser = new StepUser { GameId = gameId, UserId = userId, Rank = deck[0].Rank, Suit = deck[0].Suit };
            await _unitOfWork.StepsAccounts.Create(stepUser);

            var countPointUser = await _unitOfWork.PointsAccounts.GetUserIdMax(userId, gameId);
            var pointUser = new PointUser { GameId = gameId, UserId = userId, CountPoint = countPointUser.CountPoint };
            await _unitOfWork.PointsAccounts.Create(pointUser);

            ListRemoveCards.Add(deck[0]);
            deck.Remove(deck[0]);


            for (int i = 0; i < countBots; i++)
            {
                var pointBot = new PointBot();
                pointBot = await _unitOfWork.PointsBots.GetBotIdMax(bots[i].Id, gameId);
                if (_unitOfWork.Bots.IsCard(pointBot))
                {
                    var stepBot = new StepBot { BotId = bots[i].Id, GameId = gameId, Rank = deck[0].Rank, Suit = deck[0].Suit };

                }
            }
        }


    }
}
