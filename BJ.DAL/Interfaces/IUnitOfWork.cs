using System;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {

        IBotRepository Bots { get; }
        ICardRepository Cards { get; }
        IGameRepository Games { get; }
        IPointUserRepository PointsAccounts { get; }
        IPointBotRepository PointsBots { get; }
        IStepUserRepository StepsAccounts { get; }
        IStepBotRepository StepsBots { get; }
        IUserRepository Users { get; }
        Task Save();
    }
}
