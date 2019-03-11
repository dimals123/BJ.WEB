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
            _unitOfWork.Cards.DeleteRange(deck);
            _unitOfWork.Bots.DeleteRange(bots);
            _unitOfWork.Games.DeleteRange(games);
            _unitOfWork.UserInGames.DeleteRange(pointsUser);
            _unitOfWork.BotInGames.DeleteRange(pointsBot);
            _unitOfWork.StepsAccounts.DeleteRange(stepsUser);
            _unitOfWork.StepsBots.DeleteRange(stepsBots);
            await _unitOfWork.Save();

        }

        public async Task<List<Card>> GetDeck()
        {
            var cards = await _unitOfWork.Cards.GetAll();
            return cards;
        }

    }
}
