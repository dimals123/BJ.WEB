using BJ.DataAccess.Repositories.Interfaces;
using System;
using System.Data;

namespace BJ.DataAccess.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IBotRepository Bots { get; }
        ICardRepository Cards { get; }
        IGameRepository Games { get; }
        IUserInGameRepository UserInGames { get; }
        IBotInGameRepository BotInGames { get; }
        IStepUserRepository UserSteps { get; }
        IStepBotRepository BotSteps { get; }
        IUserRepository Users { get; }

    }
}
