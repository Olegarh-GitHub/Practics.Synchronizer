using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Practics.Synchronizer.BLL.Services;
using Practics.Synchronizer.Core.Models.Base;
using Practics.Synchronizer.DAL.Models.Base;
using Quartz;

namespace Practics.Synchronizer.BLL.Jobs
{
    [DisallowConcurrentExecution]
    public class ImportedEntitiesSyncJob<TRaw, TEntity> : IJob
    where TRaw : RawEntity
    where TEntity : ImportedEntity
    {
        private readonly ImportedEntityUpdateService<TRaw, TEntity> _entityUpdateService;
        private readonly ILogger<ImportedEntitiesSyncJob<TRaw, TEntity>> _logger;

        public ImportedEntitiesSyncJob
        (
            ImportedEntityUpdateService<TRaw, TEntity> entityUpdateService, 
            ILogger<ImportedEntitiesSyncJob<TRaw, TEntity>> logger)
        {
            _entityUpdateService = entityUpdateService;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var (created, updated, deleted) = await _entityUpdateService.Update();

            if (created > 0 || updated > 0 || deleted > 0)
                _logger.LogInformation(
                    $"{typeof(TEntity).Name}: {created} created, {updated} updated, {deleted} marked for deletion. ({DateTime.Now})");
        }
    }
}