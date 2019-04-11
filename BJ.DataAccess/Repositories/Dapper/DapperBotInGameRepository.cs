using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.Dapper
{
    public class DapperBotInGameRepository : DapperBaseRepository<BotInGame>, IBotInGameRepository
    {

        public DapperBotInGameRepository(IDbConnection connection) : base(connection) { }

        public async Task<List<BotInGame>> GetAllByGameId(Guid gameId)
        {

            string sql = "SELECT * FROM BotInGames as big Left Join Bots as b On b.Id = big.BotId WHERE big.GameId = @gameId;";
            var result = await _connection.QueryAsync<BotInGame, Bot, BotInGame>(sql, (big, b) =>
            {
                big.Bot = b;
                return big;
            }, 
            new { gameId });
            return result.AsList();

        }

        public async Task<BotInGame> GetByGameIdAndBotId(Guid gameId, Guid botId)
        {

            string sql = "Select * from BotInGames Where GameId = @gameId AND BotId = @botId;";
            var result = await _connection.QueryFirstOrDefaultAsync<BotInGame>(sql, new { gameId, botId });
            return result;


        }
    }
}
