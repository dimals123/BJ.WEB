using BJ.BLL.Configurations;
using BJ.BLL.Helpers;
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
using MoreLinq;

namespace BJ.BLL.Services
{

    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;


        public GameService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }


        public async Task<StartGameResponseView> Start(int countBots, string userId)
        {


            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {

                var userInGames = await _unitOfWork.UserInGames.GetUnfinished(userId);

                var result = new StartGameResponseView();

                if (userInGames != null)
                {
                    result.GameId = userInGames.GameId;
                    return result;
                }

                var game = new Game
                {
                    CountBots = countBots
                };

                await _unitOfWork.Games.Create(game);

                var bots = await _unitOfWork.Bots.GetRangeByCount(countBots);
                var deck = await _unitOfWork.Cards.CreateDeck(game.Id);

                var stepUsers = new List<StepUser>();
                var stepBots = new List<StepBot>();

                var userInGame = new UserInGame
                {
                    GameId = game.Id,
                    UserId = userId,
                    CountPoint = 0
                };

                var botInGames = bots.Select(x => new BotInGame
                {
                    BotId = x.Id,
                    GameId = game.Id,
                    CountPoint = 0
                }).ToList();
                var cardsRemove = new List<Card>();


                DealTwoCards(game, deck, cardsRemove, userId, stepUsers, userInGame, bots, stepBots, botInGames);

                await _unitOfWork.StepsAccounts.CreateRange(stepUsers);
                await _unitOfWork.StepsBots.CreateRange(stepBots);
                await _unitOfWork.Cards.DeleteRange(cardsRemove);
                await _unitOfWork.UserInGames.Create(userInGame);
                await _unitOfWork.BotInGames.CreateRange(botInGames);

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

                var cardsForRemove = new List<Card>();

                var userInGame = await _unitOfWork.UserInGames.GetByUserIdAndGameId(userId, gameId);
                var botInGames = await _unitOfWork.BotInGames.GetAllByGameId(game.Id);

                //var deck = await _unitOfWork.Cards.GetAll();
                var deck = await _unitOfWork.Cards.GetAllByGameId(game.Id);
                var bots = botInGames.Select(x => x.Bot).ToList();

                var stepUser = new StepUser();
                var stepBots = new List<StepBot>();

                DealCard(game,deck, cardsForRemove, userId, ref stepUser, userInGame, bots, stepBots, botInGames);

                await _unitOfWork.StepsAccounts.Create(stepUser);
                await _unitOfWork.UserInGames.Update(userInGame);
                await _unitOfWork.StepsBots.CreateRange(stepBots);
                await _unitOfWork.BotInGames.UpdateRange(botInGames);

                await _unitOfWork.Cards.DeleteRange(cardsForRemove);
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

                var botInGames = await _unitOfWork.BotInGames.GetAllByGameId(game.Id);

                //var deck = await _unitOfWork.Cards.GetAll();
                var deck = await _unitOfWork.Cards.GetAllByGameId(game.Id);
                var bots = botInGames.Select(x => x.Bot).ToList();

                var removeCards = new List<Card>();
                var stepBots = new List<StepBot>();

                DealLast(game, deck, removeCards, bots, stepBots, botInGames);

                await EndGame(gameId, userId);
                await _unitOfWork.BotInGames.UpdateRange(botInGames);
                await _unitOfWork.StepsBots.CreateRange(stepBots);

                transactionScope.Complete();

            }

        }

        public async Task EndGame(Guid gameId, string userId)
        {
            var game = await _unitOfWork.Games.GetById(gameId);

            var botInGames = await _unitOfWork.BotInGames.GetAllByGameId(game.Id);
            var userInGame = await _unitOfWork.UserInGames.GetByUserIdAndGameId(userId, gameId);

            var bots = botInGames.Select(x => x.Bot).ToList();

            var deck = await _unitOfWork.Cards.GetAllByGameId(game.Id);

            var user = await _unitOfWork.Users.GetById(userId);

            game.WinnerName = GetWinner(user, userInGame, bots, botInGames);
            game.IsFinished = true;

            await _unitOfWork.Cards.DeleteRange(deck);
            await _unitOfWork.Games.Update(game);



        }

        public async Task<GetDetailsGameResponseView> GetDetails(Guid gameId, string userId)
        {
            var game = await _unitOfWork.Games.GetById(gameId);

            var result = new GetDetailsGameResponseView();

            var userResult = new UserGetDetailsGameResponseView();
            var botResults = new List<BotGetDetailsGameResponseViewItem>();

            var pointsUser = await _unitOfWork.UserInGames.GetByUserIdAndGameId(userId, gameId);

            var user = await _unitOfWork.Users.GetById(userId);

            userResult.Name = user.UserName;
            userResult.Points = pointsUser.CountPoint;

            var cardsUser = await _unitOfWork.StepsAccounts.GetAllByUserIdAndGameId(userId, gameId);

            userResult.Cards = cardsUser.Select(x => new StepUserGetDetailsGameResponseViewItem()
            {
                Rank = (RankTypeView)x.Rank,
                Suit = (SuitTypeView)x.Suit
            }).ToList();

            var botsInGames = await _unitOfWork.BotInGames.GetAllByGameId(game.Id);


            foreach (var bot in botsInGames)
            {
                var cardsBot = await _unitOfWork.StepsBots.GetAllByBotIdAndGameId(bot.BotId, game.Id);

                var botCardsResults = new List<StepBotGetDetailsGameResponseViewItem>();
                botCardsResults = cardsBot.Select(x => new StepBotGetDetailsGameResponseViewItem()
                {
                    Rank = (RankTypeView)x.Rank,
                    Suit = (SuitTypeView)x.Suit
                }).ToList();

                botResults.Add(new BotGetDetailsGameResponseViewItem
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



        public async Task<GetUnfinishedGameResponseView> GetUnfinished(string userId)
        {

            var userInGames = await _unitOfWork.UserInGames.GetUnfinished(userId);
            var game = userInGames.Game;

            var result = new GetUnfinishedGameResponseView();

            var userResult = new UserGetUnfinishedGameResponseView();
            var botResults = new List<BotGetUnfinishedGameResponseViewItem>();

            var pointsUser = await _unitOfWork.UserInGames.GetByUserIdAndGameId(userId, game.Id);

            var user = await _unitOfWork.Users.GetById(userId);

            userResult.Name = user.UserName;
            userResult.Points = pointsUser.CountPoint;

            var cardsUser = await _unitOfWork.StepsAccounts.GetAllByUserIdAndGameId(userId, game.Id);

            userResult.Cards = cardsUser.Select(x => new StepUserGetUnfinishedGameResponseViewItem()
            {
                Rank = (RankTypeView)x.Rank,
                Suit = (SuitTypeView)x.Suit
            }).ToList();

            var botsInGames = await _unitOfWork.BotInGames.GetAllByGameId(game.Id);


            foreach (var bot in botsInGames)
            {
                var cardsBot = await _unitOfWork.StepsBots.GetAllByBotIdAndGameId(bot.BotId, game.Id);

                var botCardsResults = new List<StepBotGetUnfinishedGameResponseViewItem>();
                botCardsResults = cardsBot.Select(x => new StepBotGetUnfinishedGameResponseViewItem()
                {
                    Rank = (RankTypeView)x.Rank,
                    Suit = (SuitTypeView)x.Suit
                }).ToList();

                botResults.Add(new BotGetUnfinishedGameResponseViewItem
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



        private bool IsNeedCard(BotInGame pointBot)
        {
            var result = pointBot.CountPoint <= 16;
            return result;
        }

        private void DealTwoCards(Game game, List<Card> deck, List<Card> cardsRemove, string userId, List<StepUser> stepUsers, UserInGame userInGame, List<Bot> bots, List<StepBot> stepBots, List<BotInGame> botInGames)
        {
            var startCards = 2;
            for (int i = 0; i < startCards; i++)
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

                userInGame.CountPoint += ScoreHelper.ValueCard(currentCard.Rank, userInGame.CountPoint);


                cardsRemove.Add(currentCard);
                deck.Remove(currentCard);

                foreach (var bot in bots)
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
                    
                    var botInGame =  botInGames.FirstOrDefault(x => x.BotId == bot.Id);
                    botInGame.CountPoint += ScoreHelper.ValueCard(currentCard.Rank, botInGame.CountPoint);
                    
                    cardsRemove.Add(currentCard);
                    deck.Remove(currentCard);
                }


            }
        }

        private void DealCard(Game game, List<Card> deck, List<Card> cardsRemove, string userId, ref StepUser stepUser, UserInGame userInGame, List<Bot> bots, List<StepBot> stepBots, List<BotInGame> botInGames)
        {
            var currentCard = deck.FirstOrDefault();

            stepUser = new StepUser
            {
                GameId = game.Id,
                UserId = userId,
                Rank = currentCard.Rank,
                Suit = currentCard.Suit
            };
        
            userInGame.CountPoint += ScoreHelper.ValueCard(currentCard.Rank, userInGame.CountPoint);

            cardsRemove.Add(currentCard);
            deck.Remove(currentCard);
            
            foreach (var bot in bots)
            {
                currentCard = deck.FirstOrDefault();

                var pointBot = botInGames.FirstOrDefault(x => x.BotId == bot.Id);

                if (IsNeedCard(pointBot))
                {
                    var stepBot = new StepBot
                    {
                        BotId = bot.Id,
                        GameId = game.Id,
                        Rank = currentCard.Rank,
                        Suit = currentCard.Suit
                    };
                    stepBots.Add(stepBot);

                    pointBot.CountPoint += ScoreHelper.ValueCard(currentCard.Rank, pointBot.CountPoint);

                    cardsRemove.Add(currentCard);
                    deck.Remove(currentCard);
                }
            }
        }

        private void DealLast(Game game, List<Card> deck, List<Card> cardsRemove, List<Bot> bots, List<StepBot> stepBots, List<BotInGame> botInGames)
        {
            foreach (var botInGame in botInGames)
            {
                while (IsNeedCard(botInGame))
                {
                    var currentCard = deck.FirstOrDefault();
                    var stepBot = new StepBot
                    {
                        BotId = bots.FirstOrDefault(x => x.Id == botInGame.BotId).Id,
                        GameId = game.Id,
                        Rank = currentCard.Rank,
                        Suit = currentCard.Suit
                    };
                    stepBots.Add(stepBot);

                    botInGame.CountPoint += ScoreHelper.ValueCard(currentCard.Rank, botInGame.CountPoint);

                    cardsRemove.Add(currentCard);
                    deck.Remove(currentCard);
                }
            }
        }

        private string GetWinner(User user, UserInGame userInGame, List<Bot> bots, List<BotInGame> botInGames)
        {


            var winner = botInGames
                .Where(x => x.CountPoint <= 21)
                .MaxBy(x => x.CountPoint)
                .FirstOrDefault();

            var bot = bots.FirstOrDefault(x => x.Id == winner.Id);

            var winnerName = (userInGame.CountPoint > winner.CountPoint) ? user.UserName : bot.Name;
            
            return winnerName;
        }
    }
}
