using BJ.BLL.Configurations;
using BJ.BLL.Services.Interfaces;
using BJ.DAL.Entities;
using BJ.DAL.Entities.Enums;
using BJ.DAL.Repositories.Interfaces;
using BJ.ViewModels.Enums;
using BJ.ViewModels.GameViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BJ.BLL.Services
{

    public class GameService:IGameService
    {
        private readonly IUnitOfWork _unitOfWork;


        public GameService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        private int ValueCard(RankType rank)
        {
            var result = 0;
            switch (rank)
            {
                case RankType.Six:
                    result = 6;
                    break;
                case RankType.Seven:
                    result = 7;
                    break;
                case RankType.Eight:
                    result = 8;
                    break;
                case RankType.Nine:
                    result = 9;
                    break;
                case RankType.Ten:
                    result = 10;
                    break;
                case RankType.Jack:
                    result = 2;
                    break;
                case RankType.Lady:
                    result = 3;
                    break;
                case RankType.King:
                    result = 4;
                    break;
                case RankType.Ace:
                    result = 11 | 1;
                    break;
            }
            return result;
        }
      

        private Game IsEndGame(List<Game> games)
        {
            var result = games.FirstOrDefault(x => x.IsEnd == false);
            return result;
            
        }

        public async Task<StartGameResponseView> Start(int countBots, string userId)
        {
       

            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {
                    
                var userInGames = await _unitOfWork.UserInGames.GetAllByUserId(userId);
                var games = userInGames.Select(x => x.Game).ToList();
                
                var game = IsEndGame(games);
                                
                

                if (game == null)
                {

                    game = new Game
                    {
                        CountBots = countBots
                    };

                    await _unitOfWork.Games.Create(game);

                    var bots = await DbInitialize.InitBots(_unitOfWork, countBots, game.Id);
                    var deck = await DbInitialize.InitCards(_unitOfWork, game.Id);

                    var stepUsers = new List<StepUser>();
                    var stepBots = new List<StepBot>();

                    var userInGame = new UserInGame
                    {
                        GameId = game.Id,
                        UserId = userId,
                        CountPoint = 0
                    };

                    var botsInGame = bots.Select(x => new BotInGame
                    {
                        BotId = x.Id,
                        GameId = game.Id,
                        CountPoint = 0
                    }).ToList();

                    var cardsRemove = new List<Card>();
                    var StartCards = 2;


                    for (int i = 0; i < StartCards; i++)
                    {
                        var currentCard = deck.FirstOrDefault();
                        var stepUser = new StepUser
                        {
                            GameId = game.Id,
                            UserId = userId,
                            Suit = currentCard.Suit,
                            Rank = currentCard.Rank
                        };
                        stepUsers.Add(stepUser);

                        userInGame.CountPoint += ValueCard(currentCard.Rank);


                        cardsRemove.Add(currentCard);
                        deck.Remove(currentCard);

                        foreach(var bot in bots)
                        {
                            currentCard = deck.FirstOrDefault();
                            var stepBot = new StepBot
                            {
                                GameId = game.Id,
                                BotId = bot.Id,
                                Suit = currentCard.Suit,
                                Rank = currentCard.Rank,
                            };
                            stepBots.Add(stepBot);

                            foreach (var botInGame in botsInGame)
                            {
                                if (bot.Id == botInGame.BotId)
                                {
                                    botInGame.CountPoint += ValueCard(currentCard.Rank);
                                    break;
                                }
                            }
                            cardsRemove.Add(currentCard);
                            deck.Remove(currentCard);
                        }
                    
                       
                    }

                   

                    await _unitOfWork.StepsAccounts.CreateRange(stepUsers);
                    await _unitOfWork.StepsBots.CreateRange(stepBots);
                    await _unitOfWork.Cards.DeleteRange(cardsRemove);
                    await _unitOfWork.UserInGames.Create(userInGame);
                    await _unitOfWork.BotInGames.CreateRange(botsInGame);
                }
                var result = new StartGameResponseView();
                result.GameId = game.Id;
                transactionScope.Complete();
                
                return result;
            }
          
        }

        public async Task GetCards(Guid gameId, string userId)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {

                var game = await _unitOfWork.Games.GetById(gameId);

                var removeCards = new List<Card>();

                var pointsUser = await _unitOfWork.UserInGames.GetByUserIdAndGameId(userId, gameId);
                var pointsBot = await _unitOfWork.BotInGames.GetAllByGameId(game.Id);

                //var deck = await _unitOfWork.Cards.GetAll();
                var deck = await _unitOfWork.Cards.GetByGameId(game.Id);
                var bots = pointsBot.Select(x => x.Bot).ToList();

                var currentCard = deck.FirstOrDefault();

                var stepUser = new StepUser
                {
                    GameId = gameId,
                    UserId = userId,
                    Rank = currentCard.Rank,
                    Suit = currentCard.Suit
                };
                await _unitOfWork.StepsAccounts.Create(stepUser);

                pointsUser.CountPoint += ValueCard(currentCard.Rank);

                await _unitOfWork.UserInGames.Update(pointsUser);

                removeCards.Add(currentCard);
                deck.Remove(currentCard);

                var stepsBotAdd = new List<StepBot>();

                foreach (var bot in bots)
                {
                    currentCard = deck.FirstOrDefault();

                    var pointBot = pointsBot.FirstOrDefault(x => x.BotId == bot.Id);

                    if (IsCard(pointBot))
                    {
                        var stepBot = new StepBot
                        {
                            BotId = bot.Id,
                            GameId = gameId,
                            Rank = currentCard.Rank,
                            Suit = currentCard.Suit
                        };
                        stepsBotAdd.Add(stepBot);

                        pointBot.CountPoint += ValueCard(currentCard.Rank);

                        removeCards.Add(currentCard);
                        deck.Remove(currentCard);
                    }
                }

                await _unitOfWork.StepsBots.CreateRange(stepsBotAdd);
                await _unitOfWork.BotInGames.UpdateRange(pointsBot);

                await _unitOfWork.Cards.DeleteRange(removeCards);
                //if (pointUser.CountPoint >= 21)
                //    await Stop(getCardsGameView);
                transactionScope.Complete();
                

            }

        }

        public async Task Stop(Guid gameId, string userId)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {

                var game = await _unitOfWork.Games.GetById(gameId);

                var pointsBot = await _unitOfWork.BotInGames.GetAllByGameId(game.Id);

                //var deck = await _unitOfWork.Cards.GetAll();
                var deck = await _unitOfWork.Cards.GetByGameId(game.Id);
                var bots = pointsBot.Select(x => x.Bot).ToList();

                var removeCards = new List<Card>();
                var stepsBotAdd = new List<StepBot>();



                foreach(var points in pointsBot)
                {
                    for(int i = 0; ; i++)
                    {
                        if (!IsCard(points))
                        {
                            break;
                        }
                        var currentCard = deck.FirstOrDefault();
                        var stepBot = new StepBot
                        {
                            BotId = bots.FirstOrDefault(x => x.Id == points.BotId).Id,
                            GameId = gameId,
                            Rank = currentCard.Rank,
                            Suit = currentCard.Suit
                        };
                        stepsBotAdd.Add(stepBot);

                        points.CountPoint += ValueCard(currentCard.Rank);

                        removeCards.Add(currentCard);
                        deck.Remove(currentCard);
                    }

                }
                
                await EndGame(gameId, userId);
                await _unitOfWork.BotInGames.UpdateRange(pointsBot);
                await _unitOfWork.StepsBots.CreateRange(stepsBotAdd);
                
                transactionScope.Complete();

            }
           
        }

        public async Task EndGame(Guid gameId, string userId)
        {
            var game = await _unitOfWork.Games.GetById(gameId);

            var pointsBots = await _unitOfWork.BotInGames.GetAllByGameId(game.Id);
            var pointsUser = await _unitOfWork.UserInGames.GetByUserIdAndGameId(userId, gameId);

            var bots = pointsBots.Select(x => x.Bot).ToList();
            //var deck = await _unitOfWork.Cards.GetAll();
            var deck = await _unitOfWork.Cards.GetByGameId(game.Id);


            var winnerName = string.Empty;
            int winnerPoints, newPoints;
            if (pointsUser.CountPoint <= 21)
            {
                winnerPoints = pointsUser.CountPoint;
                var winner = await _unitOfWork.Users.GetById(userId);
                winnerName = winner.UserName;
            }
            else
            {
                winnerPoints = 0;
            }

            foreach(var points in pointsBots)
            {
                newPoints = points.CountPoint;
                if (winnerPoints < newPoints && newPoints <= 21)
                {
                    winnerPoints = newPoints;
                    var winner = await _unitOfWork.Bots.GetById(points.BotId);
                    winnerName = winner.Name;
                }
            }


            game.WinnerName = winnerName.ToString();
            game.IsEnd = true;

            await _unitOfWork.Cards.DeleteRange(deck);
            await _unitOfWork.Games.Update(game);

        

        }

        public async Task<CreateStartGameResponseView> GetGameByGameIdAndUserId(Guid gameId, string userId)
        {
            var game = await _unitOfWork.Games.GetById(gameId);

            var result = new CreateStartGameResponseView();

            var userResult = new UserStartGameResponseView();
            var botResults = new List<BotStartGameResponseViewItem>();

            userResult.Name = await _unitOfWork.Users.GetNameById(userId);
            userResult.Points = await _unitOfWork.UserInGames.GetPointsByUserIdAndGameId(userId, gameId);

            var cardsUser = await _unitOfWork.StepsAccounts.GetAllByUserIdAndGameId(userId, gameId);

            userResult.Cards = cardsUser.Select(x => new StepUserStartGameResponseViewItem()
            {
                Rank = (RankTypeView)x.Rank,
                Suit = (SuitTypeView)x.Suit
            }).ToList();

            var botsInGames = await _unitOfWork.BotInGames.GetAllByGameId(game.Id);


            foreach(var bot in botsInGames)
            {
                var cardsBot = await _unitOfWork.StepsBots.GetByBotIdAndGameId(bot.BotId, game.Id);

                var botCardsResults = new List<StepBotStartGameResponseViewItem>();
                botCardsResults = cardsBot.Select(x => new StepBotStartGameResponseViewItem()
                {
                    Rank = (RankTypeView)x.Rank,
                    Suit = (SuitTypeView)x.Suit
                }).ToList();

                botResults.Add(new BotStartGameResponseViewItem
                {
                    Name = bot.Bot.Name,
                    Points = bot.CountPoint,
                    Cards = botCardsResults
                });
            }

            result.Bots = botResults;
            result.User = userResult;

            result.Winner = game.WinnerName;

            return result;

        }



        public async Task<CreateStartGameResponseView> GetNoEndGame(string userId)
        {
            var gameId = await GetNoEndGameId(userId);
            var game = await _unitOfWork.Games.GetById(gameId);

            var result = new CreateStartGameResponseView();

            var userResult = new UserStartGameResponseView();
            var botResults = new List<BotStartGameResponseViewItem>();

            userResult.Name = await _unitOfWork.Users.GetNameById(userId);
            userResult.Points = await _unitOfWork.UserInGames.GetPointsByUserIdAndGameId(userId, gameId);

            var cardsUser = await _unitOfWork.StepsAccounts.GetAllByUserIdAndGameId(userId, gameId);

            userResult.Cards = cardsUser.Select(x => new StepUserStartGameResponseViewItem()
            {
                Rank = (RankTypeView)x.Rank,
                Suit = (SuitTypeView)x.Suit
            }).ToList();

            var botsInGames = await _unitOfWork.BotInGames.GetAllByGameId(game.Id);


            foreach (var bot in botsInGames)
            {
                var cardsBot = await _unitOfWork.StepsBots.GetByBotIdAndGameId(bot.BotId, game.Id);

                var botCardsResults = new List<StepBotStartGameResponseViewItem>();
                botCardsResults = cardsBot.Select(x => new StepBotStartGameResponseViewItem()
                {
                    Rank = (RankTypeView)x.Rank,
                    Suit = (SuitTypeView)x.Suit
                }).ToList();

                botResults.Add(new BotStartGameResponseViewItem
                {
                    Name = bot.Bot.Name,
                    Points = bot.CountPoint,
                    Cards = botCardsResults
                });
            }

            result.Bots = botResults;
            result.User = userResult;

            result.Winner = game.WinnerName;

            return result;

        }

        public async Task<Guid> GetNoEndGameId(string userId)
        {
            var lastUserInGames = await _unitOfWork.UserInGames.GetNoEnd(userId);
            var lastGame = await _unitOfWork.Games.GetLastGame(userId);
            var response = lastGame.Id;
            return response;
        }

        public bool IsCard(BotInGame pointBot)
        {
            var result = pointBot.CountPoint <= 16;
            return result;
        }

    }
}
