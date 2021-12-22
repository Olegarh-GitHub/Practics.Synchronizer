using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Practics.Synchronizer.Core.Interfaces;
using Practics.Synchronizer.Core.Models.Base;
using Practics.Synchronizer.DAL.Context;

namespace Practics.Synchronizer.BLL.Services
{
    public class ImportedEntityService<TEntity> where TEntity : ImportedEntity
    {
        private readonly IRepository<TEntity, int> _importedEntityRepository;
        private readonly IMapper _mapper;

        public ImportedEntityService
        (
            IMapper mapper,
            IRepository<TEntity, int> importedEntityRepository
        )
        {
            _mapper = mapper;
            _importedEntityRepository = importedEntityRepository;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity obj)
        {
            var existedObj = await _importedEntityRepository.GetAsync(obj.Id);

            if (existedObj == null)
            {
                return await _importedEntityRepository.CreateAsync(obj);
            }

            return null;
        }

        public virtual async Task UpdateAsync(TEntity obj)
        {
            var existedObj = await _importedEntityRepository.GetAsync(obj.Id);
            
            if (existedObj == null)
            {
                return;
            }

            _mapper.Map(obj, existedObj);

            await _importedEntityRepository.UpdateAsync(existedObj);
        }

        public async Task UpdateAsync(IEnumerable<TEntity> objects)
        {
            var objectsIds = objects.Select(x => x.Id).ToList();

            var existedObjects = await _importedEntityRepository
                .GetAllAsQueryable()
                .Where(x => objectsIds.Contains(x.Id))
                .ToListAsync();

            foreach (var existedObject in existedObjects)
            {
                var obj = objects.FirstOrDefault(x => x.Id == existedObject.Id);

                _mapper.Map(obj, existedObject);
                await _importedEntityRepository.UpdateAsync(existedObject);
            }
        }

        public virtual async Task DeleteAsync(int id)
        {
            var existedObj = await _importedEntityRepository.GetAsync(id);

            if (existedObj == null)
            {
                return;
            }

            await _importedEntityRepository.DeleteAsync(existedObj.Id);
        }

        public async Task DeleteAsync(IEnumerable<int> ids)
        {
            var existedObjects = await _importedEntityRepository
                .GetAllAsQueryable()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            foreach (var existedObj in existedObjects)
            {
                await _importedEntityRepository.DeleteAsync(existedObj.Id);
            }
        }

        public async Task<Dictionary<string, int>> ConvertExtKeyToIdAsync(IEnumerable<string> extKeys)
        {
            var importedObjects = await _importedEntityRepository
                .GetAllAsQueryable()
                .Where(x => extKeys.Contains(x.ExtKey))
                .ToDictionaryAsync(key=> key.ExtKey, value=> value.Id);

            return importedObjects;
        }
        
        public async Task<int> ConvertExtKeyToIdAsync(string extKey)
        {
            var ids = await ConvertExtKeyToIdAsync(new [] {extKey});

            return ids[extKey];
        }

        public async Task<Dictionary<int, string>> ConvertIdToExtKeyAsync(IEnumerable<int> ids)
        {
            var importedObjects = await _importedEntityRepository
                .GetAllAsQueryable()
                .Where(x => ids.Contains(x.Id))
                .ToDictionaryAsync(key=>key.Id, value=>value.ExtKey);

            return importedObjects;
        }

        public async Task<string> ConvertIdToExtKeyAsync(int id)
        {
            var keys = await ConvertIdToExtKeyAsync(new[] {id});

            return keys[id];
        }

        public async Task<TEntity> GetByExtKeyAsync(string extKey)
        {
            var obj = await _importedEntityRepository
                .GetAllAsQueryable()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ExtKey == extKey);

            return obj;
        }

        public async Task<List<TEntity>> GetByExtKeyAsync(IEnumerable<string> extKeys)
        {
            var objects = await _importedEntityRepository
                .GetAllAsQueryable()
                .AsNoTracking()
                .Where(x => extKeys.Contains(x.ExtKey))
                .ToListAsync();

            return objects;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var objects = await _importedEntityRepository
                .GetAllAsQueryable()
                .AsNoTracking()
                .ToListAsync();

            return objects;
        }

        public virtual async Task<List<TEntity>> GetCreatedAsync()
        {
            var createdEvents = await _importedEntityRepository
                .GetAllAsQueryable()
                .AsNoTracking()
                .Where(x => x.MarkedForCreate)
                .ToListAsync();

            return createdEvents;
        }

        public virtual async Task<List<TEntity>> GetUpdatedAsync()
        {
            var updatedEvents = await _importedEntityRepository
                .GetAllAsQueryable()
                .AsNoTracking()
                .Where(x => x.MarkedForUpdate)
                .ToListAsync();

            return updatedEvents;
        }

        public virtual async Task<List<TEntity>> GetDeletedAsync()
        {
            var deletedEvents = await _importedEntityRepository
                .GetAllAsQueryable()
                .AsNoTracking()
                .Where(x => x.MarkedForDelete)
                .ToListAsync();

            return deletedEvents;
        }
    }
}