using BJ.DAL.Interfaces;
using System;

namespace BJ.DAL.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private  BJContext db;
        private  BotRepository botRepository;
        private  CardRepository cardRepository;
        private  GameRepository gameRepository;
        private  PointAccountRepository pointAccountRepository;
        private  PointBotRepository pointBotRepository;
        private  StepAccountRepository stepAccountRepository;
        private  StepBotRepository stepBotRepository;

        public UnitOfWork(BJContext context)
        {
            db = context;
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

        public IPointAccountRepository PointsAccounts
        {
            get
            {
                if (pointAccountRepository == null)
                    pointAccountRepository = new PointAccountRepository(db);
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

        public IStepAccountRepository StepsAccounts
        {
            get
            {
                if (stepAccountRepository == null)
                    stepAccountRepository = new StepAccountRepository(db);
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



        public async void Save()
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
