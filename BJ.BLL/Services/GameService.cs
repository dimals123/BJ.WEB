using BJ.BLL.Configurations;
using BJ.BLL.Interfaces;
using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using ViewModels.Enums;
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

      

        private Game IsEndGame(List<Game> games, ref bool continueGame)
        {
            var game = new Game();
            foreach (var gameIsEnd in games)
            {
                if (gameIsEnd.IsEnd == false)
                {
                    game = gameIsEnd;
                    continueGame = true;
                    return gameIsEnd;
                }
            }
            return null;
            
        }

        public async Task<StartGameResponseView> Start(int countBots, string userId)
        {
       

            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {
                    
                bool continueGame = false;
                var userInGames = await _unitOfWork.UserInGames.GetAllByUserId(userId);
                var games = userInGames.Select(x => x.Game).ToList();
                
                var game = IsEndGame(games, ref continueGame);
                if(game == null)
                {
                    game = new Game
                    {
                        CountBots = countBots
                    };
                }

                

                var startGameResponseView = new StartGameResponseView();
                startGameResponseView.GameId = game.Id;

                if (!continueGame)
                {

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

                    //foreach (var bot in bots)
                    //{
                    //    botsInGame.Add(new BotInGame
                    //    {
                    //        BotId = bot.Id,
                    //        GameId = game.Id,
                    //        CountPoint = 0
                    //    });
                    //}

                    var cardsRemove = new List<Card>();

                    for (int i = 0; i < 2; i++)
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

                        userInGame.CountPoint += (int)currentCard.Rank;


                        cardsRemove.Add(currentCard);
                        deck.Remove(currentCard);

                        for (int j = 0; j < countBots; j++)
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

                            foreach (var botInGame in botsInGame)
                            {
                                if (bots[j].Id == botInGame.BotId)
                                {
                                    botInGame.CountPoint += (int)currentCard.Rank;
                                    break;
                                }
                            }


                            cardsRemove.Add(currentCard);
                            deck.Remove(currentCard);
                        }
                    }

                   

                    await _unitOfWork.Games.Create(game);
                    await _unitOfWork.StepsAccounts.CreateRange(stepUsers);
                    await _unitOfWork.StepsBots.CreateRange(stepBots);
                    await _unitOfWork.Cards.DeleteRange(cardsRemove);
                    await _unitOfWork.UserInGames.Create(userInGame);
                    await _unitOfWork.BotInGames.CreateRange(botsInGame);
                }
                transactionScope.Complete();
                
                return startGameResponseView;
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

                var deck = await _unitOfWork.Cards.GetAll();
                //var deck = await _unitOfWork.Cards.GetByGameId(game.Id);
                var bots = pointsBot.Select(x=>x.Bot).ToList();

                var currentCard = deck.FirstOrDefault();

                var stepUser = new StepUser
                {
                    GameId = gameId,
                    UserId = userId,
                    Rank = currentCard.Rank,
                    Suit = currentCard.Suit
                };
                await _unitOfWork.StepsAccounts.Create(stepUser);
  
                pointsUser.CountPoint += (int)currentCard.Rank;

                await _unitOfWork.UserInGames.Update(pointsUser);
             
                removeCards.Add(currentCard);
                deck.Remove(currentCard);

                var stepsBotAdd = new List<StepBot>();

                for (int i = 0; i < game.CountBots; i++)
                {
                    currentCard = deck.FirstOrDefault();

                    var pointBot = pointsBot.FirstOrDefault(x => x.BotId == bots[i].Id);

                    if (IsCard(pointBot))
                    {
                        var stepBot = new StepBot
                        {
                            BotId = bots[i].Id,
                            GameId = gameId,
                            Rank = currentCard.Rank,
                            Suit = currentCard.Suit
                        };
                        stepsBotAdd.Add(stepBot);

                        pointBot.CountPoint += (int)currentCard.Rank;

                        removeCards.Add(deck[0]);
                        deck.Remove(deck[0]);
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

                var deck = await _unitOfWork.Cards.GetAll();
                //var deck = await _unitOfWork.Cards.GetByGameId(game.Id);
                var bots = pointsBot.Select(x => x.Bot).ToList();

                var removeCards = new List<Card>();
                var stepsBotAdd = new List<StepBot>();

                var countStopBots = 0;
                while (countStopBots != game.CountBots)
                {
                    countStopBots = 0;
                    for (int i = 0; i < game.CountBots; i++)
                    {

                        if (IsCard(pointsBot[i]))
                        {
                            var currentCard = deck.FirstOrDefault();
                            var stepBot = new StepBot
                            {
                                BotId = bots.FirstOrDefault(x => x.Id == pointsBot[i].BotId).Id,
                                GameId = gameId,
                                Rank = currentCard.Rank,
                                Suit = currentCard.Suit
                            };
                            stepsBotAdd.Add(stepBot);

                            pointsBot[i].CountPoint += (int)currentCard.Rank;

                            removeCards.Add(currentCard);
                            deck.Remove(currentCard);
                        }
                        else
                        {
                            countStopBots++;
                        }
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
            var deck = await _unitOfWork.Cards.GetAll();
            //var deck = await _unitOfWork.Cards.GetByGameId(game.Id);


            string winnerName = string.Empty;
            int winnerPoints, newPoints;
            if (pointsUser.CountPoint <= 21)
            {
                winnerPoints = pointsUser.CountPoint;
                winnerName = await _unitOfWork.Users.GetNameById(userId);
            }
            else
            {
                winnerPoints = 0;
            }
            for (int i = 0; i < game.CountBots; i++)
            {
                newPoints = pointsBots[i].CountPoint;
                if (winnerPoints < newPoints && newPoints <= 21)
                {
                    winnerPoints = newPoints;
                    winnerName = await _unitOfWork.Bots.GetNameById(pointsBots[i].BotId);
                }
            }
            game.WinnerName = winnerName.ToString();
            game.IsEnd = true;

            await _unitOfWork.Cards.DeleteRange(deck);
            await _unitOfWork.Games.Update(game);

        

        }

        public async Task<CreateStartGameResponseView> CreateStartGameResultView(Guid gameId, string userId)
        {
            var game = await _unitOfWork.Games.GetById(gameId);

            var startGameResultView = new CreateStartGameResponseView();

            var userStartGameResultView = new UserStartGameResponseView();
            var botStartGameResultViewItem = new List<BotStartGameResponseViewItem>();

            userStartGameResultView.Name = await _unitOfWork.Users.GetNameById(userId);
            userStartGameResultView.Points = await _unitOfWork.UserInGames.GetPointsByUserIdAndGameId(userId, gameId);

            var cardsUser = await _unitOfWork.StepsAccounts.GetAllByUserIdAndGameId(userId, gameId);

            userStartGameResultView.Cards = cardsUser.Select(x => new StepUserStartGameResponseViewItem()
            {
                Rank = (RankTypeView)x.Rank,
                Suit = (SuitTypeView)x.Suit
            }).ToList();

            //foreach (var card in cardsUser)
            //{
            //    userStartGameResultView.Cards.Add(new StepUserStartGameResponseViewItem
            //    {
            //        Rank = (RankTypeView)card.Rank,
            //        Suit = (SuitTypeView)card.Suit
            //    });
            //}

            var botsInGames = await _unitOfWork.BotInGames.GetAllByGameId(game.Id);


            for (int i = 0; i < game.CountBots; i++)
            {
                var cardsBot = await _unitOfWork.StepsBots.GetByBotIdAndGameId(botsInGames[i].BotId, game.Id);

                var stepBotsStartGame = new List<StepBotStartGameResponseViewItem>();
                stepBotsStartGame = cardsBot.Select(x => new StepBotStartGameResponseViewItem()
                {
                    Rank = (RankTypeView)x.Rank,
                    Suit = (SuitTypeView)x.Suit
                }).ToList();

                //foreach (var card in cardsBot)
                //{
                //    var stepBotStartGameResultViewItem = new StepBotStartGameResponseViewItem
                //    {
                //        Rank = (RankTypeView)card.Rank,
                //        Suit = (SuitTypeView)card.Suit
                //    };
                //    stepBotsStartGame.Add(stepBotStartGameResultViewItem);
                //}



                botStartGameResultViewItem.Add(new BotStartGameResponseViewItem
                {
                    Name = botsInGames[i].Bot.Name,
                    Points = botsInGames[i].CountPoint,
                    Cards = stepBotsStartGame
                });




            }

            startGameResultView.Bots = botStartGameResultViewItem;
            startGameResultView.User = userStartGameResultView;

            startGameResultView.Winner = game.WinnerName;

            return startGameResultView;

        }



        public async Task<CreateStartGameResponseView> CreateStartGameResultView(string userId)
        {
            var gameId = await ReturnLastGame(userId);
            var game = await _unitOfWork.Games.GetById(gameId);

            var startGameResultView = new CreateStartGameResponseView();

            var userStartGameResultView = new UserStartGameResponseView();
            var botStartGameResultViewItem = new List<BotStartGameResponseViewItem>();

            userStartGameResultView.Name = await _unitOfWork.Users.GetNameById(userId);
            userStartGameResultView.Points = await _unitOfWork.UserInGames.GetPointsByUserIdAndGameId(userId, gameId);

            var cardsUser = await _unitOfWork.StepsAccounts.GetAllByUserIdAndGameId(userId, gameId);

            userStartGameResultView.Cards = cardsUser.Select(x => new StepUserStartGameResponseViewItem()
            {
                Rank = (RankTypeView)x.Rank,
                Suit = (SuitTypeView)x.Suit
            }).ToList();

            //foreach (var card in cardsUser)
            //{
            //    userStartGameResultView.Cards.Add(new StepUserStartGameResponseViewItem
            //    {
            //        Rank = (RankTypeView)card.Rank,
            //        Suit = (SuitTypeView)card.Suit
            //    });
            //}

            var botsInGames = await _unitOfWork.BotInGames.GetAllByGameId(game.Id);



            for (int i = 0; i < game.CountBots; i++)
            {
                var cardsBot = await _unitOfWork.StepsBots.GetByBotIdAndGameId(botsInGames[i].BotId, game.Id);

                var stepBotsStartGame = new List<StepBotStartGameResponseViewItem>();
                stepBotsStartGame = cardsBot.Select(x => new StepBotStartGameResponseViewItem()
                {
                    Rank = (RankTypeView)x.Rank,
                    Suit = (SuitTypeView)x.Suit
                }).ToList();

                //foreach (var card in cardsBot)
                //{
                //    var stepBotStartGameResultViewItem = new StepBotStartGameResponseViewItem
                //    {
                //        Rank = (RankTypeView)card.Rank,
                //        Suit = (SuitTypeView)card.Suit
                //    };
                //    stepBotsStartGame.Add(stepBotStartGameResultViewItem);
                //}



                botStartGameResultViewItem.Add(new BotStartGameResponseViewItem
                {
                    Name = botsInGames[i].Bot.Name,
                    Points = botsInGames[i].CountPoint,
                    Cards = stepBotsStartGame
                });




            }

            startGameResultView.Bots = botStartGameResultViewItem;
            startGameResultView.User = userStartGameResultView;

            startGameResultView.Winner = game.WinnerName;

            return startGameResultView;

        }

        public async Task<Guid> ReturnLastGame(string userId)
        {
            var lastUserInGames= await _unitOfWork.UserInGames.GetLastGame(userId);
            var lastGame = await _unitOfWork.Games.GetLastGame(userId);
            var response = lastGame.Id;
            return response;
        }

        public bool IsCard(BotInGame pointBot)
        {
            if (pointBot.CountPoint <= 16)
                return true;
            else return false;
        }

    }
}
