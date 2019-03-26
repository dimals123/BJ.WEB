using BJ.BLL.Interfaces;
using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.BLL.Services
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

    }
}
