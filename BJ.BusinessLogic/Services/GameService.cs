using BJ.BusinessLogic.Services.Interfaces;
using BJ.DataAccess.Entities;
using BJ.ViewModels.EnumsViews;
using BJ.ViewModels.GameViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using MoreLinq;
using BJ.BusinessLogic.Helpers.Interfaces;
using BJ.DataAccess.Entities.Enums;
using BJ.DataAccess.UnitOfWork;

namespace BJ.BusinessLogic.Services
{

    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IScoreHelper _scoreHelper;
        private readonly ICardsHelper _cardsHelper;


        public GameService(IUnitOfWork unitOfWork, IScoreHelper scoreHelper, ICardsHelper cardsHelper)
        {
            this._unitOfWork = unitOfWork;
            _scoreHelper = scoreHelper;
            _cardsHelper = cardsHelper;
        }


        public async Task<StartGameResponseView> Start(int countBots, string userId)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                var userInGame = await _unitOfWork.UserInGames.GetUnfinished(userId);

                var result = new StartGameResponseView();

                if (userInGame != null)
                {
                    result.GameId = userInGame.GameId;
                    return result;
                }

                var game = new Game
                {
                    CountBots = countBots,
                    WinnerName = String.Empty
                };



                await _unitOfWork.Games.Create(game);
                var bots = await _unitOfWork.Bots.GetCount(countBots);
                var deck = await CreateDeck(game);

                var userSteps = new List<UserStep>();
                var botSteps = new List<BotStep>();

                var newUserInGame = new UserInGame
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
                var cardsForRemove = new List<Card>();


                DealTwoCards(game, deck, cardsForRemove, userId, userSteps, newUserInGame, bots, botSteps, botInGames);

                await _unitOfWork.UserSteps.CreateRange(userSteps);
                await _unitOfWork.BotSteps.CreateRange(botSteps);
                await _unitOfWork.Cards.DeleteRange(cardsForRemove);
                await _unitOfWork.UserInGames.Create(newUserInGame);
                await _unitOfWork.BotInGames.CreateRange(botInGames);

                result.GameId = game.Id;

                transactionScope.Complete();

                return result;
            }

        }

        public async Task GetCards(Guid gameId, string userId)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                var game = await _unitOfWork.Games.GetById(gameId);

                var cardsForRemove = new List<Card>();

                var userInGame = await _unitOfWork.UserInGames.GetByUserIdAndGameId(userId, gameId);
                var botInGames = await _unitOfWork.BotInGames.GetAllByGameId(game.Id);

                var deck = await _unitOfWork.Cards.GetAllByGameId(game.Id);
                var bots = botInGames.Select(x => x.Bot).ToList();

                var stepUser = new UserStep();
                var stepBots = new List<BotStep>();

                DealCard(game,deck, cardsForRemove, userId, stepUser, userInGame, bots, stepBots, botInGames);

                await _unitOfWork.UserSteps.Create(stepUser);
                await _unitOfWork.UserInGames.Update(userInGame);
                await _unitOfWork.BotSteps.CreateRange(stepBots);
                await _unitOfWork.BotInGames.UpdateRange(botInGames);

                await _unitOfWork.Cards.DeleteRange(cardsForRemove);
                transactionScope.Complete();


            }

        }

        public async Task Stop(Guid gameId, string userId)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                var game = await _unitOfWork.Games.GetById(gameId);

                var botInGames = await _unitOfWork.BotInGames.GetAllByGameId(game.Id);

                var deck = await _unitOfWork.Cards.GetAllByGameId(game.Id);
                var bots = botInGames.Select(x => x.Bot).ToList();

                var cardsForRemove = new List<Card>();
                var stepBots = new List<BotStep>();

                DealLast(game, deck, cardsForRemove, bots, stepBots, botInGames);

                await EndGame(gameId, userId);
                await _unitOfWork.BotInGames.UpdateRange(botInGames);
                await _unitOfWork.BotSteps.CreateRange(stepBots);

                transactionScope.Complete();

            }

        }

        private async Task EndGame(Guid gameId, string userId)
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

            var cardsUser = await _unitOfWork.UserSteps.GetAllByUserIdAndGameId(userId, gameId);

            userResult.Cards = cardsUser.Select(x => new StepUserGetDetailsGameResponseViewItem()
            {
                Rank = (RankTypeView)x.Rank,
                Suit = (SuitTypeView)x.Suit
            }).ToList();

            var botsInGames = await _unitOfWork.BotInGames.GetAllByGameId(game.Id);
            var cardsAllBots = await _unitOfWork.BotSteps.GetAllByGameId(game.Id);

            botsInGames.ForEach(x =>
            {
                var cardsBot = cardsAllBots.Select(c => c).Where(c => c.BotId == x.BotId).ToList();

                var botCardsResults = new List<StepBotGetDetailsGameResponseViewItem>();
                botCardsResults = cardsBot.Select(c => new StepBotGetDetailsGameResponseViewItem()
                {
                    Rank = (RankTypeView)c.Rank,
                    Suit = (SuitTypeView)c.Suit

                }).ToList();

                botResults.Add(new BotGetDetailsGameResponseViewItem
                {
                    Name = x.Bot.Name,
                    Points = x.CountPoint,
                    Cards = botCardsResults
                });
            });

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

            var cardsUser = await _unitOfWork.UserSteps.GetAllByUserIdAndGameId(userId, game.Id);

            userResult.Cards = cardsUser.Select(x => new StepUserGetUnfinishedGameResponseViewItem()
            {
                Rank = (RankTypeView)x.Rank,
                Suit = (SuitTypeView)x.Suit
            }).ToList();

            var botsInGames = await _unitOfWork.BotInGames.GetAllByGameId(game.Id);
            var cardsAllBots = await _unitOfWork.BotSteps.GetAllByGameId(game.Id);

            botResults = botsInGames.Select(x => new BotGetUnfinishedGameResponseViewItem
            {
                Name = x.Bot.Name,
                Points = x.CountPoint,
                Cards = cardsAllBots.Where(c => c.BotId == x.BotId).Select(c => new StepBotGetUnfinishedGameResponseViewItem
                {
                    Rank = (RankTypeView)c.Rank,
                    Suit = (SuitTypeView)c.Suit
                }).ToList()
            }).ToList();


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

        private void DealTwoCards(Game game, List<Card> deck, List<Card> cardsForRemove, string userId, List<UserStep> stepUsers, UserInGame userInGame, List<Bot> bots, List<BotStep> stepBots, List<BotInGame> botInGames)
        {
            var startCards = 2;
            for (int i = 0; i < startCards; i++)
            {
                var currentCard = deck.FirstOrDefault();
                var stepUser = new UserStep
                {
                    GameId = game.Id,
                    UserId = userId,
                    Suit = currentCard.Suit,
                    Rank = currentCard.Rank
                };
                stepUsers.Add(stepUser);

                userInGame.CountPoint += _scoreHelper.ValueCard(currentCard.Rank, userInGame.CountPoint);


                cardsForRemove.Add(currentCard);
                deck.Remove(currentCard);


                bots.ForEach(x =>
                {
                    currentCard = deck.FirstOrDefault();
                    stepBots.Add(new BotStep
                    {
                        GameId = game.Id,
                        BotId = x.Id,
                        Suit = currentCard.Suit,
                        Rank = currentCard.Rank
                    });
                    var botInGame = botInGames.FirstOrDefault(b=>b.BotId == x.Id);
                    botInGame.CountPoint += _scoreHelper.ValueCard(currentCard.Rank, botInGame.CountPoint);

                    cardsForRemove.Add(currentCard);
                    deck.Remove(currentCard);
                });

               

            }
        }

        private void DealCard(Game game, List<Card> deck, List<Card> cardsForRemove, string userId, UserStep stepUser, UserInGame userInGame, List<Bot> bots, List<BotStep> stepBots, List<BotInGame> botInGames)
        {
            var currentCard = deck.FirstOrDefault();

            stepUser.GameId = game.Id;
            stepUser.UserId = userId;
            stepUser.Suit = currentCard.Suit;
            stepUser.Rank = currentCard.Rank;
        
            userInGame.CountPoint += _scoreHelper.ValueCard(currentCard.Rank, userInGame.CountPoint);

            cardsForRemove.Add(currentCard);
            deck.Remove(currentCard);


            bots.ForEach(x=> 
            {
                currentCard = deck.FirstOrDefault();

                var pointBot = botInGames.FirstOrDefault(p => p.BotId == x.Id);

                if (IsNeedCard(pointBot))
                {
                    var stepBot = new BotStep
                    {
                        BotId = x.Id,
                        GameId = game.Id,
                        Rank = currentCard.Rank,
                        Suit = currentCard.Suit
                    };
                    stepBots.Add(stepBot);

                    pointBot.CountPoint += _scoreHelper.ValueCard(currentCard.Rank, pointBot.CountPoint);

                    cardsForRemove.Add(currentCard);
                    deck.Remove(currentCard);
                }
            });
            
        }

        private void DealLast(Game game, List<Card> deck, List<Card> cardsForRemove, List<Bot> bots, List<BotStep> stepBots, List<BotInGame> botInGames)
        {
            foreach (var botInGame in botInGames)
            {
                while (IsNeedCard(botInGame))
                {
                    var currentCard = deck.FirstOrDefault();
                    var stepBot = new BotStep
                    {
                        BotId = bots.FirstOrDefault(x => x.Id == botInGame.BotId).Id,
                        GameId = game.Id,
                        Rank = currentCard.Rank,
                        Suit = currentCard.Suit
                    };
                    stepBots.Add(stepBot);

                    botInGame.CountPoint += _scoreHelper.ValueCard(currentCard.Rank, botInGame.CountPoint);

                    cardsForRemove.Add(currentCard);
                    deck.Remove(currentCard);
                }
            }
        }

        private string GetWinner(User user, UserInGame userInGame, List<Bot> bots, List<BotInGame> botInGames)
        {


            var winner = botInGames?
                .Where(x => x.CountPoint <= 21)?
                .MaxBy(x => x.CountPoint)?
                .FirstOrDefault();

            var bot = bots?.FirstOrDefault(x => x.Id == winner?.BotId);

            var winnerName = (userInGame.CountPoint > winner?.CountPoint && userInGame.CountPoint <= 21) ? user.UserName : bot?.Name;

            return winnerName;
        }


        private async Task<List<Card>> CreateDeck(Game game)
        {
            var cards = new List<Card>();
            var suitType = (SuitType[])Enum.GetValues(typeof(SuitType));
            var rankType = (RankType[])Enum.GetValues(typeof(RankType));

            suitType.ForEach(x =>
            {
                rankType.ForEach(r =>
                {
                    cards.Add(new Card
                    {
                        GameId = game.Id,
                        Suit = x,
                        Rank = r
                    });
                });
            });

            _cardsHelper.Swap(cards);
            await _unitOfWork.Cards.CreateRange(cards);
            return cards;

        }

        public async Task<bool> IsUnfinished(string userId)
        {
            var userInGames = await _unitOfWork.UserInGames.GetUnfinished(userId);
            var response = !(userInGames == null);
            return response;
        }

        public async Task<Guid> GetUnfinishedId(string userId)
        {
            var userInGames = await _unitOfWork.UserInGames.GetUnfinished(userId);
            var lastGameId = await GetLastGameId(userId);
            var response = userInGames == null ? lastGameId : userInGames.GameId;
            return response;
        }

        private async Task<Guid> GetLastGameId(string userId)
        {
            var userInGame = await _unitOfWork.UserInGames.GetLastGame(userId);
            var response = userInGame.Game.Id;
            return response;
        }
    }
}
