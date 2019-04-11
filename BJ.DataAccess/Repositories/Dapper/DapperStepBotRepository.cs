using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.Dapper
{
    public class DapperStepBotRepository: DapperBaseRepository<BotStep>, IStepBotRepository
    {
        public DapperStepBotRepository(IDbConnection connection) : base(connection)
        {

        }

        public async Task<List<BotStep>> GetAllByBotIdAndGameId(Guid botId, Guid gameId)
        {

            string sql = "SELECT * FROM BotSteps Where BotId = @botId And GameId = @gameId;";
            var result = await _connection.QueryAsync<BotStep>(sql, new { botId, gameId });
            return result.AsList();

        }

        public async Task<List<BotStep>> GetAllByGameId(Guid gameId)
        {

            string sql = "SELECT * FROM BotSteps as bs Left Join Bots as b On b.Id = bs.BotId Where bs.GameId = @gameId;";
            var result = await _connection.QueryAsync<BotStep, Bot, BotStep>(sql, (bs, b) =>
            {
                bs.Bot = b;
                return bs;
            },
            new { gameId });
            return result.AsList();

        }
    }
}
