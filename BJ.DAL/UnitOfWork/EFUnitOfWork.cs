using BJ.DataAccess.Repositories.EF;
using BJ.DataAccess.Repositories.Interfaces;
using System;

namespace BJ.DataAccess.UnitOfWork
{
    public class EFUnitOfWork:IUnitOfWork
    {
        private BJContext db;
        private BotRepository botRepository;
        private CardRepository cardRepository;
        private GameRepository gameRepository;
        private UserInGameRepository userInGameRepository;
        private BotInGameRepository botInGameRepository;
        private StepUserRepository stepUserRepository;
        private StepBotRepository stepBotRepository;
        private UserRepository userRepository;


        public EFUnitOfWork(BJContext context)
        {
            db = context;
        }

       

        public IUserRepository Users
        {
            get
            {
                userRepository = userRepository ?? new UserRepository(db);
                return userRepository;
            }
        }


        public IBotRepository Bots
        {
            get
            {
                botRepository = botRepository ?? new BotRepository(db);
                return botRepository;
            }
        }

        public ICardRepository Cards
        {
            get
            {
                cardRepository = cardRepository ?? new CardRepository(db);
                return cardRepository;
            }
        }

        public IGameRepository Games
        {
            get
            {
                gameRepository = gameRepository ?? new GameRepository(db);
                return gameRepository;
            }
        }

        public IUserInGameRepository UserInGames
        {
            get
            {
                userInGameRepository = userInGameRepository ?? new UserInGameRepository(db);
                return userInGameRepository;
            }
        }

        public IBotInGameRepository BotInGames
        {
            get
            {
                botInGameRepository = botInGameRepository ?? new BotInGameRepository(db);
                return botInGameRepository;
            }
        }

        public IStepUserRepository UserSteps
        {
            get
            {
                stepUserRepository = stepUserRepository ?? new StepUserRepository(db);
                return stepUserRepository;
            }
        }

        public IStepBotRepository BotSteps
        {
            get
            {
                stepBotRepository = stepBotRepository ?? new StepBotRepository(db);
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
