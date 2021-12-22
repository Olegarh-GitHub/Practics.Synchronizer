using System;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Practics.Synchronizer.Core.Models.Base;
using Practics.Synchronizer.Core.Models.Events;

namespace Practics.Synchronizer.Consumers
{
    public class UpdatedEventConsumer<TEntity, TDTO> : IConsumer<UpdatedEvent<TDTO>>
        where TDTO : class
        where TEntity : ImportedEntity
    {
        private readonly ILogger<UpdatedEventConsumer<TEntity, TDTO>> _logger;
        private readonly IMapper _mapper;

        public UpdatedEventConsumer
        (
            ILogger<UpdatedEventConsumer<TEntity, TDTO>> logger,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public Task Consume(ConsumeContext<UpdatedEvent<TDTO>> context)
        {
            var payload = context.Message.Payload;
            var entity = _mapper.Map<TEntity>(payload);
            
            _logger.LogInformation($"Была обновлена сущность {entity.GetType().Name} с Id = {entity.Id} в {DateTime.Now:dd.MM.yyyy hh:mm:ss}");
            
            return Task.CompletedTask;
        }
    }
}