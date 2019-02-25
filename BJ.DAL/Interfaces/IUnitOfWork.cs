using System;
using System.Collections.Generic;
using System.Text;

namespace BJ.DAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IBotRepository Bots { get; }
        ICardRepository Cards { get; }
        IGameRepository Games { get; }
        IPointAccountRepository PointsAccounts { get; }
        IPointBotRepository PointsBots { get; }
        IStepAccountRepository StepsAccounts { get; }
        IStepBotRepository StepsBots { get; }
        void Save();
    }
}
