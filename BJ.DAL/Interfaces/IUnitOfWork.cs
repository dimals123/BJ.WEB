using System;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {

        IBotRepository Bots { get; }
        ICardRepository Cards { get; }
        IGameRepository Games { get; }
        IUserInGameRepository UserInGames { get; }
        IBotInGameRepository BotInGames { get; }
        IStepUserRepository StepsAccounts { get; }
        IStepBotRepository StepsBots { get; }
        IUserRepository Users { get; }
        Task Save();
    }
}
