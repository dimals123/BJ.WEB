using BJ.BusinessLogic.Services.Interfaces;
using BJ.DataAccess.UnitOfWork;
using BJ.ViewModels.EnumsViews;
using BJ.ViewModels.HistoryViews;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.BusinessLogic.Services
{
    public class HistoryService:IHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;


        public HistoryService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public async Task Clear()
        {
            var deck = await _unitOfWork.Cards.GetAll();
            var games = await _unitOfWork.Games.GetAll();
            var pointsUser = await _unitOfWork.UserInGames.GetAll();
            var pointsBot = await _unitOfWork.BotInGames.GetAll();
            var stepsUser = await _unitOfWork.UserSteps.GetAll();
            var stepsBots = await _unitOfWork.BotSteps.GetAll();
            await _unitOfWork.Cards.DeleteRange(deck);
            await _unitOfWork.Games.DeleteRange(games);
            await _unitOfWork.UserInGames.DeleteRange(pointsUser);
            await _unitOfWork.BotInGames.DeleteRange(pointsBot);
            await _unitOfWork.UserSteps.DeleteRange(stepsUser);
            await _unitOfWork.BotSteps.DeleteRange(stepsBots);

        } 

        public async Task<GetUserGamesHistoryView> GetUserGames(string userId)
        {
            var userInGames = await _unitOfWork
                .UserInGames
                .GetAllFinishedByUserId(userId);
            var games = userInGames
                .Select(x => x.Game)
                .Where(x=>x.IsFinished == true)
                .ToList();

            var user = await _unitOfWork.Users.GetById(userId);

            var response = new GetUserGamesHistoryView();



            response.Games = games.Select(x => new GameGetUserGamesHistoryViewItem()
            {
                GameId = x.Id,
                DateTime = x.CreationAt,
                CountBots = x.CountBots,
                Winner = x.WinnerName,
                IsWinner = x.WinnerName == user.UserName ? true : false
            }).ToList();

            return response;

        }

        public async Task<GetDetailsGameHistoryView> GetDetailsGame(string userId, Guid gameId)
        {
            var game = await _unitOfWork.Games.GetById(gameId);

            var userInGame = await _unitOfWork.UserInGames.GetByUserIdAndGameId(userId, gameId);
            var botInGames = await _unitOfWork.BotInGames.GetAllByGameId(gameId);

            var bots = botInGames
                .Select(x => x.Bot)
                .ToList();
            var user = await _unitOfWork.Users.GetById(userId);

            var stepUsers = await _unitOfWork.UserSteps.GetAllByUserIdAndGameId(userId, gameId);
            var stepBots = await _unitOfWork.BotSteps.GetAllByGameId(gameId);

            var response = new GetDetailsGameHistoryView()
            {
                DateTime = game.CreationAt,
                CountBots = game.CountBots,
                Winner = game.WinnerName,

                User = new UserGetDetailsGameHistoryView()
                {
                    Name = user.UserName,
                    Points = userInGame.CountPoint,
                    Cards = stepUsers.Select(x => new StepUserGetDetailsGameHistoryViewItem()
                    {
                        Rank = (RankTypeView)x.Rank,
                        Suit = (SuitTypeView)x.Suit
                    }).ToList()
                },
                Bots = bots.Select(x => new BotGetDetailsGameHistoryViewItem()
                {
                    Name = x.Name,
                    Points = botInGames.Where(c => c.BotId == x.Id).Select(c => c.CountPoint).FirstOrDefault(),
                    Cards = stepBots.Where(c => c.BotId == x.Id).Select(c => new StepBotGetDetailsGameHistoryViewItem()
                    {
                        Suit = (SuitTypeView)c.Suit,
                        Rank = (RankTypeView)c.Rank
                    }).ToList()
                }).ToList()
        };

            return response;
        }

    }
}
