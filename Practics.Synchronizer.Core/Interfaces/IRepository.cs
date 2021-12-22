using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Practics.Synchronizer.Core.Interfaces
{
    public interface IRepository<TEntity, TId> where TEntity : IEntity<TId>
    {
        public Task<TEntity> GetAsync(TId id);
        public Task<List<TEntity>> GetAsync(IEnumerable<TId> ids);
        public IQueryable<TEntity> GetAllAsQueryable();
        public Task<List<TEntity>> GetAllAsync();

        public Task<TEntity> UpdateAsync(TEntity entity);
        public Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities);
        public Task<TEntity> CreateAsync(TEntity entity);
        public Task DeleteAsync(TId id);
    }
}