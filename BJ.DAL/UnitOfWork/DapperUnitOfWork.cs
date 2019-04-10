using BJ.DataAccess.Repositories.Dapper;
using BJ.DataAccess.Repositories.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BJ.DataAccess.UnitOfWork
{
    public class DapperUnitOfWork: IUnitOfWork
    {
        private IDbConnection _connection;
        private DapperBotRepository dapperBotRepository;
        private DapperCardRepository dapperCardRepository;
        private DapperGameRepository dapperGameRepository;
        private DapperUserInGameRepository dapperUserInGameRepository;
        private DapperBotInGameRepository dapperBotInGameRepository;
        private DapperStepUserRepository dapperStepUserRepository;
        private DapperStepBotRepository dapperStepBotRepository;
        private DapperUserRepository dapperUserRepository;

        public DapperUnitOfWork(IDbConnection connection)
        {
            _connection = connection;
        }


        public IUserRepository Users
        {
            get
            {
                dapperUserRepository = dapperUserRepository ?? new DapperUserRepository(_connection);
                return dapperUserRepository;
            }
        }


        public IBotRepository Bots
        {
            get
            {
                dapperBotRepository = dapperBotRepository ?? new DapperBotRepository(_connection);
                return dapperBotRepository;
            }
        }

        public ICardRepository Cards
        {
            get
            {
                dapperCardRepository = dapperCardRepository ?? new DapperCardRepository(_connection);
                return dapperCardRepository;
            }
        }

        public IGameRepository Games
        {
            get
            {
                dapperGameRepository = dapperGameRepository ?? new DapperGameRepository(_connection);
                return dapperGameRepository;
            }
        }

        public IUserInGameRepository UserInGames
        {
            get
            {
                dapperUserInGameRepository = dapperUserInGameRepository ?? new DapperUserInGameRepository(_connection);
                return dapperUserInGameRepository;
            }
        }

        public IBotInGameRepository BotInGames
        {
            get
            {
                dapperBotInGameRepository = dapperBotInGameRepository ?? new DapperBotInGameRepository(_connection);
                return dapperBotInGameRepository;
            }
        }

        public IStepUserRepository UserSteps
        {
            get
            {
                dapperStepUserRepository = dapperStepUserRepository ?? new DapperStepUserRepository(_connection);
                return dapperStepUserRepository;
            }
        }

        public IStepBotRepository BotSteps
        {
            get
            {
                dapperStepBotRepository = dapperStepBotRepository ?? new DapperStepBotRepository(_connection);
                return dapperStepBotRepository;
            }
        }

        //private void resetRepositories()
        //{
        //    dapperBotRepository = null;
        //    dapperCardRepository = null;
        //    dapperGameRepository = null;
        //    dapperUserInGameRepository = null;
        //    dapperBotInGameRepository = null;
        //    dapperStepUserRepository = null;
        //    dapperStepBotRepository = null;
        //    dapperUserRepository = null;
        //}


        private bool disposed = false;



        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
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
