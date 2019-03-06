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

                var pointMaxUser = await _unitOfWork.PointsAccounts.GetMax(userId, game.Id);
                var pointUser = new PointUser { GameId = game.Id, UserId = userId, CountPoint = pointMaxUser.CountPoint + (int)deck[0].Rank };
                pointsUser.Add(pointUser);

                cardsRemove.Add(deck[0]);
                deck.Remove(deck[0]);

                for (int j = 0; j < countBots; j++)
                {
                    var stepBot = new StepBot { GameId = game.Id, BotId = bots[j].Id, Suit = deck[0].Suit, Rank = deck[0].Rank, };
                    stepBots.Add(stepBot);

                    var pointMaxBot = await _unitOfWork.PointsBots.GetMax(bots[j].Id, game.Id);
                    var pointBot = new PointBot { GameId = game.Id, BotId = bots[j].Id, CountPoint = pointMaxBot.CountPoint + (int)deck[0].Rank };
                    pointsBots.Add(pointBot);

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

        public async Task GetCards(Guid gameId, string userId)
        {
            var ListRemoveCards = new List<Card>();
            var deck = await _unitOfWork.Cards.GetAll();
            var bots = await _unitOfWork.Bots.GetAll();

            var game = await _unitOfWork.Games.Get(gameId);

            var stepUser = new StepUser { GameId = gameId, UserId = userId, Rank = deck[0].Rank, Suit = deck[0].Suit };
            await _unitOfWork.StepsAccounts.Create(stepUser);

            var pointMaxUser = await _unitOfWork.PointsAccounts.GetMax(userId, gameId);
            var pointUser = new PointUser { GameId = gameId, UserId = userId, CountPoint = pointMaxUser.CountPoint };
            await _unitOfWork.PointsAccounts.Create(pointUser);

            ListRemoveCards.Add(deck[0]);
            deck.Remove(deck[0]);

            var ListStepBots = new List<StepBot>();
            var ListPointBots = new List<PointBot>();

            for (int i = 0; i < game.CountBots; i++)
            {
                var pointMaxBot = new PointBot();
                pointMaxBot = await _unitOfWork.PointsBots.GetMax(bots[i].Id, gameId);
                if (_unitOfWork.Bots.IsCard(pointMaxBot))
                {
                    var stepBot = new StepBot { BotId = bots[i].Id, GameId = gameId, Rank = deck[0].Rank, Suit = deck[0].Suit };
                    ListStepBots.Add(stepBot);
                    
                    var pointBot = new PointBot { GameId = gameId, BotId = bots[i].Id, CountPoint = pointMaxBot.CountPoint + (int)deck[0].Rank };
                    ListPointBots.Add(pointBot);

                    ListRemoveCards.Add(deck[0]);
                    deck.Remove(deck[0]);
                }
            }

            await _unitOfWork.StepsBots.CreateRange(ListStepBots);
            await _unitOfWork.PointsBots.CreateRange(ListPointBots);
            _unitOfWork.Cards.DeleteRange(ListRemoveCards);
            _unitOfWork.Save();
        }

        public async Task StopCard(Guid gameId, string userId)
        {
            var deck = await _unitOfWork.Cards.GetAll();
            var bots = await _unitOfWork.Bots.GetAll();

            var ListRemoveCards = new List<Card>();         
            var ListStepBots = new List<StepBot>();
            var ListPointBots = new List<PointBot>();

            var countStopBots = 0;

            var game = await _unitOfWork.Games.Get(gameId);
            while (countStopBots != game.CountBots)
            {
                for (int i = 0; i < game.CountBots; i++)
                {

                    var pointMaxBot = new PointBot();
                    pointMaxBot = await _unitOfWork.PointsBots.GetMax(bots[i].Id, gameId);
                    if (_unitOfWork.Bots.IsCard(pointMaxBot))
                    {
                        var stepBot = new StepBot { BotId = bots[i].Id, GameId = gameId, Rank = deck[0].Rank, Suit = deck[0].Suit };
                        ListStepBots.Add(stepBot);

                        var pointBot = new PointBot { GameId = gameId, BotId = bots[i].Id, CountPoint = pointMaxBot.CountPoint + (int)deck[0].Rank };
                        ListPointBots.Add(pointBot);

                        ListRemoveCards.Add(deck[0]);
                        deck.Remove(deck[0]);
                    }
                    else
                    {
                        countStopBots++;
                    }
                }
            }
            await EndGame(gameId, userId);

        }

        public async Task EndGame(Guid gameId, string userId)
        {
            var listPointMaxBots = new List<PointBot>();
            var game = await _unitOfWork.Games.Get(gameId);
            var bots = await _unitOfWork.Bots.GetAll();

            var pointMaxUser = await _unitOfWork.PointsAccounts.GetMax(userId, gameId);

            for (int i = 0; i < game.CountBots; i++)
            {
                var pointMaxBot = await _unitOfWork.PointsBots.GetMax(bots[i].Id, gameId);
                listPointMaxBots.Add(pointMaxBot);
            }
            var Winner = (await _unitOfWork.Users.Get(userId)).UserName;

            for (int i = 0; i < game.CountBots; i++)
            {
                if (pointMaxUser.CountPoint < listPointMaxBots[0].CountPoint)
                {
                    Winner = bots[i].Name;
                }
            }
            game.WinnerId = Winner;
        }
            


    }
}
