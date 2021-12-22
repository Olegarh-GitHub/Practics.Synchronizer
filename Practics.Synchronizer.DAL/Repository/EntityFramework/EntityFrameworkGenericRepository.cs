using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Practics.Synchronizer.Core.Interfaces;
using Practics.Synchronizer.Core.Models.Base;
using Practics.Synchronizer.DAL.Context;

namespace Practics.Synchronizer.DAL.Repository.EntityFramework
{
    public class EntityFrameworkGenericRepository<TEntity> : IRepository<TEntity, int>
    where TEntity : ImportedEntity
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IMapper _mapper;

        public EntityFrameworkGenericRepository
        (
            ApplicationContext context,
            IMapper mapper
        )
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
            
            _mapper = mapper;
        }
        
        public async Task<TEntity> GetAsync(int id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public async Task<List<TEntity>> GetAsync(IEnumerable<int> ids)
        {
            var entities = await _dbSet.Where(x => ids.Contains(x.Id)).ToListAsync();

            return entities;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();

            return entities;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var oldEntity = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (oldEntity == null)
            {
                throw new ArgumentException("Сущность не найдена");
            }

            _mapper.Map(entity, oldEntity);

            _dbSet.Update(oldEntity);
            await _context.SaveChangesAsync();

            return oldEntity;
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

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var oldEntity = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (oldEntity != null)
            {
                throw new ArgumentException("Сущность с таким Id уже существует");
            }
            
            await _dbSet.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var oldEntity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

            if (oldEntity == null)
            {
                throw new ArgumentException("Сущность не найдена");
            }

            _dbSet.Remove(oldEntity);

            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}