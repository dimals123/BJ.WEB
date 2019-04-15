using BJ.DataAccess.Repositories.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace BJ.DataAccess.Repositories.Dapper
{
    public class DapperBaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected IDbConnection _connection { get; }

    


        public DapperBaseRepository(IDbConnection connection)
        {
            _connection = connection;
        }


        public async Task Create(T item)
        {
            var result = await _connection.InsertAsync(item);
            

        }

        public async Task CreateRange(List<T> items)
        {

            var result = await _connection.InsertAsync(items);
        }

        public async Task Delete(T item)
        {

            var result = await _connection.DeleteAsync(item);
        }

        public async Task DeleteRange(List<T> items)
        {

            var result = await _connection.DeleteAsync(items);

        }

        public async Task<List<T>> GetAll()
        {

            var response = await _connection.GetAllAsync<T>();
            return response.ToList();

        }

        public async Task<T> GetById(Guid id)
        {

            var response = await _connection.GetAsync<T>(id);
            return response;

        }

        public async Task<int> GetCount()
        {
            var result = _connection.GetAll<T>().Count();
            return result;

        }

        public async Task Update(T item)
        {

            await _connection.UpdateAsync(item);

        }

        public async Task UpdateRange(List<T> items)
        {

            await _connection.UpdateAsync(items);

        }

        public async Task<List<T>> GetCount(int count)
        {
            var tableName = typeof(T).Name + 's';
            string sql = "SELECT Top (@count) * FROM " + tableName + " ORDER BY newid();";
            var result = await _connection.QueryAsync<T>(sql, new { count });
            return result.AsList();

        }
    }
}
