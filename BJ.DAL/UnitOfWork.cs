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
        private UserInGameRepository pointAccountRepository;
        private BotInGameRepository pointBotRepository;
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

        public IUserInGameRepository UserInGames
        {
            get
            {
                if (pointAccountRepository == null)
                    pointAccountRepository = new UserInGameRepository(db);
                return pointAccountRepository;
            }
        }

        public IBotInGameRepository BotInGames
        {
            get
            {
                if (pointBotRepository == null)
                    pointBotRepository = new BotInGameRepository(db);
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
