using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories.Dapper
{
    public class DapperUserRepository: DapperBaseRepository<User>, IUserRepository
    {
        public DapperUserRepository(IDbConnection connection) : base(connection)
        {

        }

        public async Task<User> GetById(string userId)
        {

            string sql = "SELECT * FROM AspNetUsers WHERE Id = @userId;";
            var result = await _connection.QueryFirstOrDefaultAsync<User>(sql, new { userId });
            return result;

        }

        public new async Task<List<User>> GetAll()
        {
            string sql = "Select * From AspNetUsers;";
            var response = await _connection.QueryAsync<User>(sql);
            return response.AsList();
        }
    }
}
