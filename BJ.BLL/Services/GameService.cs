using BJ.BLL.Configurations;
using BJ.BLL.Interfaces;
using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.GameViews;

namespace BJ.BLL.Services
{

    public class GameService:IGameService
    {
        private readonly IUnitOfWork _unitOfWork;


        public GameService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }
      
        public async Task StartGame(StartGameView startGameView)
        {

            var game = new Game { CountBots = startGameView.CountBots};
            await _unitOfWork.Games.Create(game);

            var bots = await DbInitialize.InitBots(_unitOfWork, startGameView.CountBots);
            var deck  = await DbInitialize.InitCards(_unitOfWork);
        
            var stepUsers = new List<StepUser>();
            var stepBots = new List<StepBot>();
            var pointsUser = new List<PointUser>();
            var pointsBots = new List<PointBot>();


            var cardsRemove = new List<Card>();

            for (int i = 0; i < 2; i++) 
            {
                var stepUser = new StepUser { GameId = game.Id, UserId = startGameView.UserId, Suit = deck[0].Suit, Rank = deck[0].Rank};
                stepUsers.Add(stepUser);

                var pointMaxUser = await _unitOfWork.PointsAccounts.GetMax(startGameView.UserId, game.Id);
                var pointUser = new PointUser { GameId = game.Id, UserId = startGameView.UserId, CountPoint = pointMaxUser.CountPoint + (int)deck[0].Rank };
                pointsUser.Add(pointUser);

                cardsRemove.Add(deck[0]);
                deck.Remove(deck[0]);

                for (int j = 0; j < startGameView.CountBots; j++)
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

        public async Task GetCards(GetCardsGameView getCardsGameView)
        {
            var listRemoveCards = new List<Card>();
            var deck = await _unitOfWork.Cards.GetAll();
            var bots = await _unitOfWork.Bots.GetAll();

            var game = await _unitOfWork.Games.Get(getCardsGameView.GameId);

            var stepUser = new StepUser { GameId = getCardsGameView.GameId, UserId = getCardsGameView.UserId, Rank = deck[0].Rank, Suit = deck[0].Suit };
            await _unitOfWork.StepsAccounts.Create(stepUser);

            var pointMaxUser = await _unitOfWork.PointsAccounts.GetMax(getCardsGameView.UserId, getCardsGameView.GameId);
            var pointUser = new PointUser { GameId = getCardsGameView.GameId, UserId = getCardsGameView.UserId, CountPoint = pointMaxUser.CountPoint };
            await _unitOfWork.PointsAccounts.Create(pointUser);

            listRemoveCards.Add(deck[0]);
            deck.Remove(deck[0]);

            var listStepBots = new List<StepBot>();
            var listPointBots = new List<PointBot>();

            for (int i = 0; i < game.CountBots; i++)
            {
                var pointMaxBot = new PointBot();
                pointMaxBot = await _unitOfWork.PointsBots.GetMax(bots[i].Id, getCardsGameView.GameId);
                if (_unitOfWork.Bots.IsCard(pointMaxBot))
                {
                    var stepBot = new StepBot { BotId = bots[i].Id, GameId = getCardsGameView.GameId, Rank = deck[0].Rank, Suit = deck[0].Suit };
                    listStepBots.Add(stepBot);
                    
                    var pointBot = new PointBot { GameId = getCardsGameView.GameId, BotId = bots[i].Id, CountPoint = pointMaxBot.CountPoint + (int)deck[0].Rank };
                    listPointBots.Add(pointBot);

                    listRemoveCards.Add(deck[0]);
                    deck.Remove(deck[0]);
                }
            }

            await _unitOfWork.StepsBots.CreateRange(listStepBots);
            await _unitOfWork.PointsBots.CreateRange(listPointBots);
            _unitOfWork.Cards.DeleteRange(listRemoveCards);
            _unitOfWork.Save();
        }

        public async Task Stop(GetCardsGameView getCardsGameView)
        {
            var deck = await _unitOfWork.Cards.GetAll();
            var bots = await _unitOfWork.Bots.GetAll();

            var ListRemoveCards = new List<Card>();         
            var ListStepBots = new List<StepBot>();
            var ListPointBots = new List<PointBot>();

            var countStopBots = 0;

            var game = await _unitOfWork.Games.Get(getCardsGameView.GameId);
            while (countStopBots != game.CountBots)
            {
                for (int i = 0; i < game.CountBots; i++)
                {

                    var pointMaxBot = new PointBot();
                    pointMaxBot = await _unitOfWork.PointsBots.GetMax(bots[i].Id, getCardsGameView.GameId);
                    if (_unitOfWork.Bots.IsCard(pointMaxBot))
                    {
                        var currentCard = deck.FirstOrDefault();
                        var stepBot = new StepBot
                        {
                            BotId = bots[i].Id,
                            GameId = getCardsGameView.GameId,
                            Rank = currentCard.Rank,
                            Suit = currentCard.Suit };
                        ListStepBots.Add(stepBot);

                        var pointBot = new PointBot { GameId = getCardsGameView.GameId, BotId = bots[i].Id, CountPoint = pointMaxBot.CountPoint + (int)deck[0].Rank };
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
            await EndGame(getCardsGameView);

        }

        public async Task EndGame(GetCardsGameView getCardsGameView)
        {
            var listPointMaxBots = new List<PointBot>();
            var game = await _unitOfWork.Games.Get(getCardsGameView.GameId);
            var bots = await _unitOfWork.Bots.GetAll();

            var pointMaxUser = await _unitOfWork.PointsAccounts.GetMax(getCardsGameView.UserId, getCardsGameView.GameId);

            for (int i = 0; i < game.CountBots; i++)
            {
                var pointMaxBot = await _unitOfWork.PointsBots.GetMax(bots[i].Id, getCardsGameView.GameId);
                listPointMaxBots.Add(pointMaxBot);
            }
            var Winner = (await _unitOfWork.Users.Get(getCardsGameView.UserId)).UserName;

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
