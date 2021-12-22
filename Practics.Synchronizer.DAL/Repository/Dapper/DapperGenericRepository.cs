using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Practics.Synchronizer.Core.Extensions;
using Practics.Synchronizer.Core.Interfaces;
using Practics.Synchronizer.DAL.Helpers;
using Practics.Synchronizer.DAL.Models.Base;

namespace Practics.Synchronizer.DAL.Repository.Dapper
{
    public class DapperGenericRepository<TEntity> : IRepository<TEntity, string> 
        where TEntity : RawEntity
    {
        private readonly string _tableName;
        private readonly string _connectionString;

        public DapperGenericRepository(IConfiguration configuration)
        {
            try
            {
                _tableName = RawEntityTableNames.GetTable(typeof(TEntity));
                _connectionString = configuration.GetConnectionString("Dapper");
            }
            catch (KeyNotFoundException e)
            {
                throw new Exception($"Для типа {typeof(TEntity).Name} не зарегистрирована таблица.");
            }
        }

        private SqlConnection SqlConnection()
        {
            return new SqlConnection(_connectionString);
        }

        private async Task<IDbConnection> CreateConnection()
        {
            var connection = SqlConnection();

            await connection.OpenAsync();

            return connection;
        }

        public async Task<TEntity> GetAsync(string id)
        {
            using var db = await CreateConnection();

            var binaryId = id.GuidToBytes();
            
            return (await db.QueryAsync<TEntity>($"SELECT * FROM {_tableName} WHERE [ID] = @id", new { id = binaryId })).FirstOrDefault();
        }

        public async Task<List<TEntity>> GetAsync(IEnumerable<string> ids)
        {
            using var db = await CreateConnection();
            
            var binaryIds = ids.Select(id => Encoding.UTF8.GetBytes(id)).ToList();

            return (await db.QueryAsync<TEntity>($"SELECT * FROM {_tableName} WHERE [ID] IN @id", new { id = binaryIds })).ToList();
        }

        public IQueryable<TEntity> GetAllAsQueryable()
        {
            using var db = CreateConnection().Result;

            return db.Query<TEntity>($"SELECT * FROM {_tableName}").AsQueryable();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            using var db = await CreateConnection();

            return (await db.QueryAsync<TEntity>($"SELECT * FROM {_tableName}")).ToList();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using var db = await CreateConnection();

            var binaryId = entity.Id.GuidToBytes();
            
            var properties = entity.GetType().GetProperties();

            var propertiesNames = properties.Select(x => x.Name).Where(x=> x != nameof(entity.Id));

            var propertiesValues = properties.Select(x => x.GetValue(entity))
                .Where(x=> (string) x != entity.Id);

            var queries = new List<TEntity>();
            
            foreach (var propertyName in propertiesNames)
            {
                foreach (var propertyValue in propertiesValues)
                {
                   queries.Add((await db.QueryAsync<TEntity>($"UPDATE {_tableName} SET {propertyName} = {propertyValue} WHERE [ID] = @id", 
                       new {binaryId})).FirstOrDefault());
                }
            }

            var updatedEntity = await GetAsync(entity.Id);

            return updatedEntity;
        }

        public async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            var updatedEntities = new List<TEntity>();
            
            foreach (var entity in entities)
            {
                updatedEntities.Add(await UpdateAsync(entity));
            }

            return updatedEntities;
        }

        public Task<TEntity> CreateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}