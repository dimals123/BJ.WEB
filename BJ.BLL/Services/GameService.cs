using BJ.BLL.Configurations;
using BJ.BLL.Interfaces;
using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace BJ.BLL.Services
{
    [Authorize]
    public class GameService:IGameService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GameService(IUnitOfWork _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }
      
        public async Task StartGame(string UserId, int countBots)
        {
            var game = new Game();
            await _unitOfWork.Games.Create(game);
            var bots = await DbInitialize.InitBots(_unitOfWork, countBots);
            var deck = await DbInitialize.InitCards(_unitOfWork);
            var handplayers = _unitOfWork.Cards.GetRange();

        }
    }
}
