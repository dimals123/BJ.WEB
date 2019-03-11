using BJ.BLL.Configurations;
using BJ.BLL.Interfaces;
using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using System;
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
      
        public async Task<StartGameResultView> StartGame(StartGameView startGameView)
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
            var userInGame = new UserInGame
            {
                GameId = game.Id,
                UserId = startGameView.UserId,
                CountPoint = 0
            };
            var botsInGame = new List<BotInGame>();
            foreach (var bot in bots)
            {
                botsInGame.Add(new BotInGame
                {
                    BotId = bot.Id,
                    GameId = game.Id,
                    CountPoint = 0
                });
            }
          


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

                var pointUser = new UserInGame
                {
                    GameId = game.Id,
                    UserId = startGameView.UserId,
                    CountPoint =/* pointMaxUser.CountPoint +*/ (int)currentCard.Rank
                };

                userInGame.CountPoint += (int)currentCard.Rank;
                           
              
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

                    foreach(var botInGame in botsInGame)
                    {
                        if(bots[j].Id == botInGame.Id)
                        {
                            botInGame.CountPoint += (int)currentCard.Rank;
                            break;
                        }
                    }

                    
                    cardsRemove.Add(currentCard);
                    deck.Remove(currentCard);
                }
            }

            await _unitOfWork.StepsAccounts.CreateRange(stepUsers);
            await _unitOfWork.StepsBots.CreateRange(stepBots);
            _unitOfWork.Cards.DeleteRange(cardsRemove);
            await _unitOfWork.UserInGames.Create(userInGame);
            await _unitOfWork.BotInGames.CreateRange(botsInGame);
            await _unitOfWork.Save();

            var result = await CreateStartGameResultView(game.Id, startGameView.UserId);
            return result;
        }

        public async Task<StartGameResultView> GetCards(GetCardsGameView getCardsGameView)
        {
            var removeCards = new List<Card>();
            var deck = await _unitOfWork.Cards.GetAll();
            var bots = await _unitOfWork.Bots.GetAll();


            var pointsUser = await _unitOfWork.UserInGames.GetFirst();
            var pointsBot = await _unitOfWork.BotInGames.GetAll();

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

            pointsUser.CountPoint += (int)currentCard.Rank;

            _unitOfWork.UserInGames.Update(pointsUser);
            await _unitOfWork.Save();

            removeCards.Add(currentCard);
            deck.Remove(currentCard);

            var stepsBotAdd = new List<StepBot>();
            var pointsBotAdd = new List<BotInGame>();

            for (int i = 0; i < game.CountBots; i++)
            {
                currentCard = deck.FirstOrDefault();

                var pointMaxBot = new BotInGame();
                pointMaxBot = _unitOfWork.BotInGames.GetMaxCountPoints(bots[i].Id, getCardsGameView.GameId, pointsBot);

                if (_unitOfWork.Bots.IsCard(pointMaxBot))
                {
                    var stepBot = new StepBot
                    {
                        BotId = bots[i].Id,
                        GameId = getCardsGameView.GameId,
                        Rank = currentCard.Rank,
                        Suit = currentCard.Suit };
                    stepsBotAdd.Add(stepBot);
                    
                    var pointBot = new BotInGame
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
            await _unitOfWork.BotInGames.CreateRange(pointsBotAdd);
            
            _unitOfWork.Cards.DeleteRange(removeCards);
            await _unitOfWork.Save();
            //if (pointUser.CountPoint >= 21)
            //    await Stop(getCardsGameView);


            var result = await CreateStartGameResultView(getCardsGameView.GameId, getCardsGameView.UserId);
            return result;
            

        }

        public async Task Stop(GetCardsGameView getCardsGameView)
        {
            var deck = await _unitOfWork.Cards.GetAll();
            var bots = await _unitOfWork.Bots.GetAll();

            var pointsBot = await _unitOfWork.BotInGames.GetAll();

            var removeCards = new List<Card>();         
            var stepsBotAdd = new List<StepBot>();
            var pointsBotAdd = new List<BotInGame>();

            var countStopBots = 0;

            var game = await _unitOfWork.Games.Get(getCardsGameView.GameId);
            while (countStopBots != game.CountBots)
            {
                countStopBots = 0;
                for (int i = 0; i < game.CountBots; i++)
                {

                    var pointMaxBot = new BotInGame();
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

                        var pointBot = new BotInGame
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


            await _unitOfWork.BotInGames.CreateRange(pointsBotAdd);
            await _unitOfWork.StepsBots.CreateRange(stepsBotAdd);
            await _unitOfWork.Save();
            await EndGame(getCardsGameView);

        }

        public async Task EndGame(GetCardsGameView getCardsGameView)
        {
            var pointsMaxBots = new List<BotInGame>();
            var game = await _unitOfWork.Games.Get(getCardsGameView.GameId);
            var bots = await _unitOfWork.Bots.GetAll();
            var deck = await _unitOfWork.Cards.GetAll();

            var pointsUser = await _unitOfWork.UserInGames.GetAll();
            var pointsBot = await _unitOfWork.BotInGames.GetAll();


            for (int i = 0; i < game.CountBots; i++)
            {
                pointsMaxBots.Add();
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
            _unitOfWork.Games.Update(game);
            await _unitOfWork.Save();
        }
            
        private async Task<StartGameResultView> CreateStartGameResultView(Guid gameId, string userId)
        {
            var game = await _unitOfWork.Games.Get(gameId);

            var startGameResultView = new StartGameResultView();

            var userStartGameResultView = new UserStartGameResultView();
            var botStartGameResultViewItem = new List<BotStartGameResultViewItem>();

            userStartGameResultView.Name = (await _unitOfWork.Users.Get(userId)).UserName;
            userStartGameResultView.Points = (await _unitOfWork.UserInGames.Get(gameId, userId)).CountPoint;

            var cardsUser = await _unitOfWork.StepsAccounts.GetOfUser(userId, gameId);

            foreach (var card in cardsUser)
            {
                userStartGameResultView.Cards.Add(new StepUserStartGameResultViewItem
                {
                    Rank = card.Rank,
                    Suit = card.Suit
                });
            }

            var botsInGames = await _unitOfWork.BotInGames.GetAllBots(game.Id);
            

            for (int i = 0; i < game.CountBots; i++) 
            {
                var cardsBot = await _unitOfWork.StepsBots.GetOfBot(botsInGames[i].BotId, game.Id);

                var stepBotsStartGame = new List<StepBotStartGameResultViewItem>();
                foreach (var card in cardsBot)
                {
                    var stepBotStartGameResultViewItem = new StepBotStartGameResultViewItem
                    {
                        Rank = card.Rank,
                        Suit = card.Suit
                    };
                    stepBotsStartGame.Add(stepBotStartGameResultViewItem);
                }



                botStartGameResultViewItem.Add(new BotStartGameResultViewItem
                {
                    Name = botsInGames[i].Bot.Name,
                    Points = botsInGames[i].CountPoint,
                    Cards = stepBotsStartGame
                });

                
                
               
            }

            startGameResultView.Bots = botStartGameResultViewItem;
            startGameResultView.User = userStartGameResultView;
            startGameResultView.GameId = game.Id;

            return startGameResultView;
        }

    }
}
