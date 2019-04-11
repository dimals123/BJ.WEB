using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.Dapper
{
    public class DapperUserInGameRepository : DapperBaseRepository<UserInGame>, IUserInGameRepository
    {
        public DapperUserInGameRepository(IDbConnection connection) : base(connection)
        {

        }

        public async Task<List<UserInGame>> GetAllByUserId(string userId)
        {

            string sql = "SELECT * FROM UserInGames as uig LEFT Join Games as g On g.Id = uig.GameId WHERE uig.UserId = @userId;";
            var result = await _connection.QueryAsync<UserInGame, Game, UserInGame>(sql,
             (uig, g) =>
             {
                 uig.Game = g;
                 return uig;
             },
            new { userId });
            return result.AsList();
        }

        public async Task<List<UserInGame>> GetAllFinishedByUserId(string userId)
        {
            var isFinished = true;
            string sql = "SELECT * FROM UserInGames as uig Left Join Games as g On g.Id = uig.GameId WHERE uig.UserId = @userId And g.IsFinished = @isFinished;";
            var result = await _connection.QueryAsync<UserInGame, Game, UserInGame>(sql, (uig, g) => 
            {
                uig.Game = g;
                return uig;
            },
            new { userId, isFinished });
            return result.AsList();

        }

        public async Task<UserInGame> GetByUserIdAndGameId(string userId, Guid gameId)
        {

            string sql = "SELECT * FROM UserInGames WHERE UserId = @userId And GameId = @gameId;";
            var result = await _connection.QueryFirstOrDefaultAsync<UserInGame>(sql, new { userId, gameId });
            return result;



        }

        public async Task<UserInGame> GetLastGame(string userId)
        {

            string sql = "SELECT Top (1) * FROM UserInGames as uig Left Join Games as g On g.Id = uig.GameId ORDER BY uig.CreationAt Desc;";
            var result = await _connection.QueryAsync<UserInGame, Game, UserInGame>(sql, (uig, g) =>
            {
                uig.Game = g;
                return uig;
            }, 
            new { userId });
            return result.FirstOrDefault();

        }

        public async Task<UserInGame> GetUnfinished(string userId)
        {
            var isFinished = false;
            string sql = "SELECT Top (1) * FROM UserInGames AS uig LEFT JOIN Games AS g On g.Id = uig.GameId WHERE uig.UserId = @userId And g.IsFinished = @isFinished;";
            var result = await _connection.QueryAsync<UserInGame, Game, UserInGame>(sql, (uig, g) =>
            {
                uig.Game = g;
                return uig;
            }, 
            new { userId, isFinished });
            return result.FirstOrDefault();

        }
    }
}
