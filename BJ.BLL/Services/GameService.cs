using BJ.BLL.Configurations;
using BJ.BLL.Interfaces;
using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<StartGameResponseView> StartGame(StartGameView startGameView)
        {

            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {
                var game = new Game
                {
                    CountBots = startGameView.CountBots
                };
                

                var bots = await DbInitialize.InitBots(_unitOfWork, startGameView.CountBots);
                var deck = await DbInitialize.InitCards(_unitOfWork);

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
                _unitOfWork.Cards.DeleteRange(cardsRemove);
                await _unitOfWork.UserInGames.Create(userInGame);
               
                await _unitOfWork.BotInGames.CreateRange(botsInGame);
                await _unitOfWork.Save();
               
                var result = await CreateStartGameResultView(game.Id, startGameView.UserId);
                transactionScope.Complete();
              
                return result;        
            }


        }

        public async Task<StartGameResponseView> GetCards(GetCardsGameView getCardsGameView)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {

                var game = await _unitOfWork.Games.GetById(getCardsGameView.GameId);

                var removeCards = new List<Card>();

                var pointsUser = await _unitOfWork.UserInGames.Get(game.Id, getCardsGameView.UserId);
                var pointsBot = await _unitOfWork.BotInGames.GetAllBotsInGame(game.Id);

                var deck = await _unitOfWork.Cards.GetAll();
                var bots = await _unitOfWork.Bots.GetAllBots(pointsBot);

                var currentCard = deck.FirstOrDefault();

                var stepUser = new StepUser
                {
                    GameId = getCardsGameView.GameId,
                    UserId = getCardsGameView.UserId,
                    Rank = currentCard.Rank,
                    Suit = currentCard.Suit
                };
                await _unitOfWork.StepsAccounts.Create(stepUser);
                await _unitOfWork.Save();

                //throw new ApplicationException("Ошибка базы данных! Транзакция завершена неудачно.");
  
                pointsUser.CountPoint += (int)currentCard.Rank;

                _unitOfWork.UserInGames.Update(pointsUser);
                await _unitOfWork.Save();
                if (true)
             
                removeCards.Add(currentCard);
                deck.Remove(currentCard);

                var stepsBotAdd = new List<StepBot>();

                for (int i = 0; i < game.CountBots; i++)
                {
                    currentCard = deck.FirstOrDefault();

                    var pointBot = pointsBot.FirstOrDefault(x => x.BotId == bots[i].Id);

                    if (_unitOfWork.Bots.IsCard(pointBot))
                    {
                        var stepBot = new StepBot
                        {
                            BotId = bots[i].Id,
                            GameId = getCardsGameView.GameId,
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
                _unitOfWork.BotInGames.UpdateRange(pointsBot);

                _unitOfWork.Cards.DeleteRange(removeCards);
                await _unitOfWork.Save();
                //if (pointUser.CountPoint >= 21)
                //    await Stop(getCardsGameView);
                var result = await CreateStartGameResultView(getCardsGameView.GameId, getCardsGameView.UserId);
                transactionScope.Complete();
                
                return result;
            }

        }

        public async Task<StartGameResponseView> Stop(GetCardsGameView getCardsGameView)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {
                var game = await _unitOfWork.Games.GetById(getCardsGameView.GameId);

                var pointsBot = await _unitOfWork.BotInGames.GetAllBotsInGame(game.Id);

                var deck = await _unitOfWork.Cards.GetAll();
                var bots = await _unitOfWork.Bots.GetAllBots(pointsBot);

                var removeCards = new List<Card>();
                var stepsBotAdd = new List<StepBot>();

                var countStopBots = 0;
                while (countStopBots != game.CountBots)
                {
                    countStopBots = 0;
                    for (int i = 0; i < game.CountBots; i++)
                    {

                        if (_unitOfWork.Bots.IsCard(pointsBot[i]))
                        {
                            var currentCard = deck.FirstOrDefault();
                            var stepBot = new StepBot
                            {
                                BotId = bots.FirstOrDefault(x => x.Id == pointsBot[i].BotId).Id,
                                GameId = getCardsGameView.GameId,
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


                _unitOfWork.BotInGames.UpdateRange(pointsBot);
                await _unitOfWork.StepsBots.CreateRange(stepsBotAdd);
                await _unitOfWork.Save();
               
                var result = await EndGame(getCardsGameView);
                transactionScope.Complete();
                return result;

            }
           
        }

        private async Task<StartGameResponseView> EndGame(GetCardsGameView getCardsGameView)
        {

            var game = await _unitOfWork.Games.GetById(getCardsGameView.GameId);

            var pointsBots = await _unitOfWork.BotInGames.GetAllBotsInGame(game.Id);
            var pointUser = await _unitOfWork.UserInGames.Get(game.Id, getCardsGameView.UserId);

            var bots = await _unitOfWork.Bots.GetAllBots(pointsBots);
            var deck = await _unitOfWork.Cards.GetAll();

            var pointsUser = await _unitOfWork.UserInGames.GetAll();
            var pointsBot = await _unitOfWork.BotInGames.GetAll();



            string winnerName = string.Empty;
            int winnerPoints, newPoints;
            if (pointUser.CountPoint <= 21)
            {
                winnerPoints = pointUser.CountPoint;
                winnerName = (await _unitOfWork.Users.GetById(getCardsGameView.UserId)).UserName;
            }
            else
            {
                winnerPoints = 0;
            }
            for (int i = 0; i < game.CountBots; i++)
            {
                newPoints = pointsBot[i].CountPoint;
                if (winnerPoints < newPoints && newPoints <= 21)
                {
                    winnerPoints = newPoints;
                    winnerName = (await _unitOfWork.Bots.GetById(pointsBots[i].BotId)).Name;
                }
            }
            game.WinnerId = winnerName;

            _unitOfWork.Cards.DeleteRange(deck);
            _unitOfWork.Games.Update(game);
            await _unitOfWork.Save();

            var result = await CreateStartGameResultView(game.Id, getCardsGameView.UserId);

            return result;


        }

        private async Task<StartGameResponseView> CreateStartGameResultView(Guid gameId, string userId)
        {

            var game = await _unitOfWork.Games.GetById(gameId);

            var startGameResultView = new StartGameResponseView();

            var userStartGameResultView = new UserStartGameResponseView();
            var botStartGameResultViewItem = new List<BotStartGameResponseViewItem>();

            userStartGameResultView.Name = (await _unitOfWork.Users.GetById(userId)).UserName;
            userStartGameResultView.Points = (await _unitOfWork.UserInGames.Get(gameId, userId)).CountPoint;

            var cardsUser = await _unitOfWork.StepsAccounts.GetCardsByUserIdAndGameId(userId, gameId);

            //userStartGameResultView.Cards = cardsUser.Select(x => new StepUserStartGameResponseViewItem()
            //{
            //    Rank = (RankTypeView)x.Rank,
            //    Suit = (SuitTypeView)x.Suit
            //}).ToList();

            foreach (var card in cardsUser)
            {
                userStartGameResultView.Cards.Add(new StepUserStartGameResponseViewItem
                {
                    Rank = (RankTypeView)card.Rank,
                    Suit = (SuitTypeView)card.Suit
                });
            }

            var botsInGames = await _unitOfWork.BotInGames.GetAllBotsInGame(game.Id);


            for (int i = 0; i < game.CountBots; i++)
            {
                var cardsBot = await _unitOfWork.StepsBots.GetByBotIdAndGameId(botsInGames[i].BotId, game.Id);

                var stepBotsStartGame = new List<StepBotStartGameResponseViewItem>();
                //stepBotsStartGame = cardsBot.Select(x => new StepBotStartGameResponseViewItem()
                //{
                //    Rank = (RankTypeView)x.Rank,
                //    Suit = (SuitTypeView)x.Suit
                //}).ToList();

                foreach (var card in cardsBot)
                {
                    var stepBotStartGameResultViewItem = new StepBotStartGameResponseViewItem
                    {
                        Rank = (RankTypeView)card.Rank,
                        Suit = (SuitTypeView)card.Suit
                    };
                    stepBotsStartGame.Add(stepBotStartGameResultViewItem);
                }



                botStartGameResultViewItem.Add(new BotStartGameResponseViewItem
                {
                    Name = botsInGames[i].Bot.Name,
                    Points = botsInGames[i].CountPoint,
                    Cards = stepBotsStartGame
                });




            }

            startGameResultView.Bots = botStartGameResultViewItem;
            startGameResultView.User = userStartGameResultView;
            startGameResultView.GameId = game.Id;

            startGameResultView.Winner = game.WinnerId;

            return startGameResultView;

        }

    }
}
