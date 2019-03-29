using BJ.BusinessLogic.Services.Interfaces;
using BJ.DataAccess.Repositories.Interfaces;
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
            var bots = await _unitOfWork.Bots.GetAll();
            var games = await _unitOfWork.Games.GetAll();
            var pointsUser = await _unitOfWork.UserInGames.GetAll();
            var pointsBot = await _unitOfWork.BotInGames.GetAll();
            var stepsUser = await _unitOfWork.StepsAccounts.GetAll();
            var stepsBots = await _unitOfWork.StepsBots.GetAll();
            await _unitOfWork.Cards.DeleteRange(deck);
            await _unitOfWork.Bots.DeleteRange(bots);
            await _unitOfWork.Games.DeleteRange(games);
            await _unitOfWork.UserInGames.DeleteRange(pointsUser);
            await _unitOfWork.BotInGames.DeleteRange(pointsBot);
            await _unitOfWork.StepsAccounts.DeleteRange(stepsUser);
            await _unitOfWork.StepsBots.DeleteRange(stepsBots);

        } 

        public async Task<GetAllGamesByUserIdView> GetUserGames(string userId)
        {
            var userInGames = await _unitOfWork
                .UserInGames
                .GetAllByUserId(userId);
            var games = userInGames
                .Select(x => x.Game)
                .ToList();

            var response = new GetAllGamesByUserIdView();

            response.Games = games.Select(x => new GameGetAllGamesByUserIdViewItem()
            {
                DateTime = x.CreationAt,
                CountBots = x.CountBots,
                Winner = x.WinnerName
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

            var stepUsers = await _unitOfWork.StepsAccounts.GetAllByUserIdAndGameId(userId, gameId);
            var stepBots = await _unitOfWork.StepsBots.GetAllByGameId(gameId);

            var response = new GetDetailsGameHistoryView()
            {
                DateTime = game.CreationAt,
                CountBots = game.CountBots,
                Winner = game.WinnerName,
                
                User = new UserGetDetailsGameHistoryView()
                {
                    Name = user.UserName,
                    Cards = stepUsers.Select(x => new StepUserGetDetailsGameHistoryViewItem()
                    {
                        Rank = (RankTypeView)x.Rank,
                        Suit = (SuitTypeView)x.Suit
                    }).ToList()
                },
                Bots = bots.Select(x => new BotGetDetailsGameHistoryViewItem()
                {
                    Name = x.Name,
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
