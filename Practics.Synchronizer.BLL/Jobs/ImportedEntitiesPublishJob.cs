using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Practics.Synchronizer.BLL.Services;
using Practics.Synchronizer.Core.Models.Base;
using Practics.Synchronizer.Core.Models.Events;
using Quartz;

namespace Practics.Synchronizer.BLL.Jobs
{
    public class ImportedEntitiesPublishJob<TEntity, TDTO> : IJob
    where TEntity : ImportedEntity
    where TDTO : class
    {
        private readonly ImportedEntityService<TEntity> _importedEntityService;
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly ILogger<ImportedEntitiesPublishJob<TEntity, TDTO>> _logger;

        public ImportedEntitiesPublishJob
        (
            ImportedEntityService<TEntity> importedEntityService, 
            IBus bus, 
            IMapper mapper,
            ILogger<ImportedEntitiesPublishJob<TEntity, TDTO>> logger
        )
        {
            _importedEntityService = importedEntityService;
            _bus = bus;
            _mapper = mapper;
            _logger = logger;
        }

        
        public async Task Execute(IJobExecutionContext context)
        {
            var createdObjects = await _importedEntityService.GetCreatedAsync();
            var updatedObjects = await _importedEntityService.GetUpdatedAsync();
            var deletedObjects = await _importedEntityService.GetDeletedAsync();
            
            var created = 0;
            foreach (var entity in createdObjects)
            {
                var dto = _mapper.Map<TDTO>(entity);
                
                await _bus.Publish(new CreatedEvent<TDTO>(dto));
                
                entity.MarkedForCreate = false;
                created++;
            }

            await _importedEntityService.UpdateAsync(createdObjects);

            var updated = 0;
            foreach (var entity in updatedObjects)
            {
                var dto = _mapper.Map<TDTO>(entity);
                
                await _bus.Publish(new UpdatedEvent<TDTO>(dto));
                
                entity.MarkedForUpdate = false;
                updated++;
            }

            await _importedEntityService.UpdateAsync(updatedObjects);

            var deleted = 0;
            foreach (var entity in deletedObjects)
            {
                await _bus.Publish(new DeletedEvent<TDTO>(entity.Id));
                deleted++;
            }

            var deletedObjectsIds = deletedObjects.Select(x => x.Id);
            await _importedEntityService.DeleteAsync(deletedObjectsIds);
            
            if (created > 0 || updated > 0 || deleted > 0)
                _logger.LogInformation($"{typeof(TEntity).Name}: {created} created, {updated} updated, {deleted} deleted. ({DateTime.Now})");
        }
    }
}