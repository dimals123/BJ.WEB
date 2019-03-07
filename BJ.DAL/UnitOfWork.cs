using BJ.DAL.Interfaces;
using BJ.DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace BJ.DAL
{
    public class UnitOfWork:IUnitOfWork
    {
        private BJContext db;
        private BotRepository botRepository;
        private CardRepository cardRepository;
        private GameRepository gameRepository;
        private PointUserRepository pointAccountRepository;
        private PointBotRepository pointBotRepository;
        private StepUserRepository stepAccountRepository;
        private StepBotRepository stepBotRepository;
        private UserRepository userRepository;

        public UnitOfWork(BJContext context)
        {
            db = context;
        }

       

        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }


        public IBotRepository Bots
        {
            get
            {
                if (botRepository == null)
                    botRepository = new BotRepository(db);
                return botRepository;
            }
        }

        public ICardRepository Cards
        {
            get
            {
                if (cardRepository == null)
                    cardRepository = new CardRepository(db);
                return cardRepository;
            }
        }

        public IGameRepository Games
        {
            get
            {
                if (gameRepository == null)
                    gameRepository = new GameRepository(db);
                return gameRepository;
            }
        }

        public IPointUserRepository PointsAccounts
        {
            get
            {
                if (pointAccountRepository == null)
                    pointAccountRepository = new PointUserRepository(db);
                return pointAccountRepository;
            }
        }

        public IPointBotRepository PointsBots
        {
            get
            {
                if (pointBotRepository == null)
                    pointBotRepository = new PointBotRepository(db);
                return pointBotRepository;
            }
        }

        public IStepUserRepository StepsAccounts
        {
            get
            {
                if (stepAccountRepository == null)
                    stepAccountRepository = new StepUserRepository(db);
                return stepAccountRepository;
            }
        }

        public IStepBotRepository StepsBots
        {
            get
            {
                if (stepBotRepository == null)
                    stepBotRepository = new StepBotRepository(db);
                return stepBotRepository;
            }
        }



        public async Task Save()
        {

            await db.SaveChangesAsync();
        }

        private bool disposed = false;

       

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
