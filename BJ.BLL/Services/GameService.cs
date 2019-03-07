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

            var game = new Game
            {
                CountBots = startGameView.CountBots
            };
            await _unitOfWork.Games.Create(game);

            var bots = await DbInitialize.InitBots(_unitOfWork, startGameView.CountBots);
            var deck  = await DbInitialize.InitCards(_unitOfWork);

            

            var stepUsers = new List<StepUser>();
            var stepBots = new List<StepBot>();
            var pointsUser = new List<PointUser>();
            var pointsBot = new List<PointBot>();


            var cardsRemove = new List<Card>();

            for (int i = 0; i < 2; i++) 
            {
                var currentCard = deck.FirstOrDefault();
                var stepUser = new StepUser
                {
                    GameId = game.Id,
                    UserId = startGameView.UserId,
                    Suit = currentCard.Suit,
                    Rank = currentCard.Rank
                };
                stepUsers.Add(stepUser);

                var pointMaxUser = _unitOfWork.PointsAccounts.GetMax(startGameView.UserId, game.Id, pointsUser);
                if (pointMaxUser == null)
                {
                    var pointUser = new PointUser
                    {
                        GameId = game.Id,
                        UserId = startGameView.UserId,
                        CountPoint =/* pointMaxUser.CountPoint +*/ (int)currentCard.Rank
                    };
                    pointsUser.Add(pointUser);
                }
                else
                {
                    var pointUser = new PointUser
                    {
                        GameId = game.Id,
                        UserId = startGameView.UserId,
                        CountPoint = pointMaxUser.CountPoint + (int)currentCard.Rank
                    };
                    pointsUser.Add(pointUser);
                }
              
                cardsRemove.Add(currentCard);
                deck.Remove(currentCard);

                for (int j = 0; j < startGameView.CountBots; j++)
                {
                    currentCard = deck.FirstOrDefault();
                    var stepBot = new StepBot
                    {
                        GameId = game.Id,
                        BotId = bots[j].Id,
                        Suit = currentCard.Suit,
                        Rank = currentCard.Rank,
                    };
                    stepBots.Add(stepBot);

                    var pointMaxBot = _unitOfWork.PointsBots.GetMax(bots[j].Id, game.Id, pointsBot);
                    if (pointMaxBot == null)
                    {
                        var pointBot = new PointBot
                        {
                            GameId = game.Id,
                            BotId = bots[j].Id,
                            CountPoint = (int)currentCard.Rank
                        };
                        pointsBot.Add(pointBot);
                    }
                    else
                    {
                        var pointBot = new PointBot
                        {
                            GameId = game.Id,
                            BotId = bots[j].Id,
                            CountPoint = pointMaxBot.CountPoint + (int)currentCard.Rank
                        };
                        pointsBot.Add(pointBot);
                    }
                    cardsRemove.Add(currentCard);
                    deck.Remove(currentCard);
                }
            }
            var botsView = new List<BotStartGameViewItem>();
            for (int i = 0; i < game.CountBots; i++) 
            {
                var botStartGameView = new BotStartGameViewItem
                {
                    Name = bots[i].Name,
                     Cards = new List<StepBotStartGameViewItem> { new StepBotStartGameViewItem {  Rank = stepBots} }
                };
            }

            var reult = new StartGameResultView
            {
                User = new UserStartGameResultView
                {
                    Name = (await _unitOfWork.Users.Get(startGameView.UserId)).UserName,
                    Cards = new List<StepUserStartGameResultViewItem>
                    {
                        new StepUserStartGameResultViewItem
                        {
                            Suit = stepUsers.FirstOrDefault().Suit,
                            Rank = stepUsers.FirstOrDefault().Rank
                        },
                        new StepUserStartGameResultViewItem
                        {
                            Suit = stepUsers.LastOrDefault().Suit,
                            Rank = stepUsers.LastOrDefault().Rank
                        }
                    },
                    Points = new PointUserStartGameView { CountPoint = pointsUser.Select(x => x.CountPoint).Max() }      
                   
                },
                 Bots = new List<BotStartGameViewItem>
                 {
                     
                 }
            };
            await _unitOfWork.StepsAccounts.CreateRange(stepUsers);
            await _unitOfWork.StepsBots.CreateRange(stepBots);
            _unitOfWork.Cards.DeleteRange(cardsRemove);
            await _unitOfWork.PointsAccounts.CreateRange(pointsUser);
            await _unitOfWork.PointsBots.CreateRange(pointsBot);
            await _unitOfWork.Save();            
        }

        public async Task GetCards(GetCardsGameView getCardsGameView)
        {
            var removeCards = new List<Card>();
            var deck = await _unitOfWork.Cards.GetAll();
            var bots = await _unitOfWork.Bots.GetAll();


            var pointsUser = await _unitOfWork.PointsAccounts.GetAll();
            var pointsBot = await _unitOfWork.PointsBots.GetAll();

            var currentCard = deck.FirstOrDefault();

            var game = await _unitOfWork.Games.Get(getCardsGameView.GameId);

            var stepUser = new StepUser
            {
                GameId = getCardsGameView.GameId,
                UserId = getCardsGameView.UserId,
                Rank = currentCard.Rank,
                Suit = currentCard.Suit
            };
            await _unitOfWork.StepsAccounts.Create(stepUser);
            await _unitOfWork.Save();

            var pointMaxUser = _unitOfWork.PointsAccounts.GetMax(getCardsGameView.UserId, getCardsGameView.GameId, pointsUser);
            var pointUser = new PointUser
            {
                GameId = getCardsGameView.GameId,
                UserId = getCardsGameView.UserId,
                CountPoint = pointMaxUser.CountPoint + (int)currentCard.Rank
            };
            await _unitOfWork.PointsAccounts.Create(pointUser);
            await _unitOfWork.Save();

            removeCards.Add(currentCard);
            deck.Remove(currentCard);

            var stepsBotAdd = new List<StepBot>();
            var pointsBotAdd = new List<PointBot>();

            for (int i = 0; i < game.CountBots; i++)
            {
                currentCard = deck.FirstOrDefault();

                var pointMaxBot = new PointBot();
                pointMaxBot = _unitOfWork.PointsBots.GetMax(bots[i].Id, getCardsGameView.GameId, pointsBot);

                if (_unitOfWork.Bots.IsCard(pointMaxBot))
                {
                    var stepBot = new StepBot
                    {
                        BotId = bots[i].Id,
                        GameId = getCardsGameView.GameId,
                        Rank = currentCard.Rank,
                        Suit = currentCard.Suit };
                    stepsBotAdd.Add(stepBot);
                    
                    var pointBot = new PointBot
                    {
                        GameId = getCardsGameView.GameId,
                        BotId = bots[i].Id,
                        CountPoint = pointMaxBot.CountPoint + (int)deck[0].Rank
                    };
                    pointsBotAdd.Add(pointBot);

                    removeCards.Add(deck[0]);
                    deck.Remove(deck[0]);
                }
            }

            await _unitOfWork.StepsBots.CreateRange(stepsBotAdd);
            await _unitOfWork.PointsBots.CreateRange(pointsBotAdd);
            
            _unitOfWork.Cards.DeleteRange(removeCards);
            await _unitOfWork.Save();
            if (pointUser.CountPoint >= 21)
                await Stop(getCardsGameView);

        }

        public async Task Stop(GetCardsGameView getCardsGameView)
        {
            var deck = await _unitOfWork.Cards.GetAll();
            var bots = await _unitOfWork.Bots.GetAll();

            var pointsBot = await _unitOfWork.PointsBots.GetAll();

            var removeCards = new List<Card>();         
            var stepsBotAdd = new List<StepBot>();
            var pointsBotAdd = new List<PointBot>();

            var countStopBots = 0;

            var game = await _unitOfWork.Games.Get(getCardsGameView.GameId);
            while (countStopBots != game.CountBots)
            {
                countStopBots = 0;
                for (int i = 0; i < game.CountBots; i++)
                {

                    var pointMaxBot = new PointBot();
                    pointMaxBot = _unitOfWork.PointsBots.GetMax(bots[i].Id, getCardsGameView.GameId, pointsBot);
                    if (_unitOfWork.Bots.IsCard(pointMaxBot))
                    {
                        var currentCard = deck.FirstOrDefault();
                        var stepBot = new StepBot
                        {
                            BotId = bots[i].Id,
                            GameId = getCardsGameView.GameId,
                            Rank = currentCard.Rank,
                            Suit = currentCard.Suit
                        };
                        stepsBotAdd.Add(stepBot);

                        var pointBot = new PointBot
                        {
                            GameId = getCardsGameView.GameId,
                            BotId = bots[i].Id,
                            CountPoint = pointMaxBot.CountPoint + (int)currentCard.Rank
                        };
                        pointsBotAdd.Add(pointBot);
                        pointsBot.Add(pointBot);
                        removeCards.Add(currentCard);
                        deck.Remove(currentCard);
                    }
                    else
                    {
                        countStopBots++;
                    }
                }
            }
            await _unitOfWork.PointsBots.CreateRange(pointsBotAdd);
            await _unitOfWork.StepsBots.CreateRange(stepsBotAdd);
            await _unitOfWork.Save();
            await EndGame(getCardsGameView);

        }

        public async Task EndGame(GetCardsGameView getCardsGameView)
        {
            var pointsMaxBots = new List<PointBot>();
            var game = await _unitOfWork.Games.Get(getCardsGameView.GameId);
            var bots = await _unitOfWork.Bots.GetAll();
            var deck = await _unitOfWork.Cards.GetAll();

            var pointsUser = await _unitOfWork.PointsAccounts.GetAll();
            var pointsBot = await _unitOfWork.PointsBots.GetAll();

            var pointMaxUser = _unitOfWork.PointsAccounts.GetMax(getCardsGameView.UserId, getCardsGameView.GameId, pointsUser);

            for (int i = 0; i < game.CountBots; i++)
            {
                var pointMaxBot = _unitOfWork.PointsBots.GetMax(bots[i].Id, getCardsGameView.GameId, pointsBot);
                pointsMaxBots.Add(pointMaxBot);
            }
            var Winner = (await _unitOfWork.Users.Get(getCardsGameView.UserId)).UserName;

            for (int i = 0; i < game.CountBots; i++)
            {
                if (pointMaxUser.CountPoint < pointsMaxBots[i].CountPoint && pointsMaxBots[i].CountPoint <=21)
                {
                    Winner = (await _unitOfWork.Bots.Get(pointsMaxBots[i].BotId)).Name;
                }
            }
            game.WinnerId = Winner;

            _unitOfWork.Cards.DeleteRange(deck);
            await _unitOfWork.Games.Update(game);
            await _unitOfWork.Save();
        }
            


    }
}
