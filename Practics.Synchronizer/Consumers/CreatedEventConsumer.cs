using System;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Practics.Synchronizer.Core.Models.Base;
using Practics.Synchronizer.Core.Models.Events;

namespace Practics.Synchronizer.Consumers
{
    public class CreatedEventConsumer<TEntity, TDTO> : IConsumer<CreatedEvent<TDTO>>
    where TDTO : class
    where TEntity : ImportedEntity
    {
        private readonly ILogger<CreatedEventConsumer<TEntity, TDTO>> _logger;
        private readonly IMapper _mapper;

        public CreatedEventConsumer
        (
            ILogger<CreatedEventConsumer<TEntity, TDTO>> logger,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public Task Consume(ConsumeContext<CreatedEvent<TDTO>> context)
        {
            var payload = context.Message.Payload;
            var entity = _mapper.Map<TEntity>(payload);
            
            _logger.LogInformation($"Была создана сущность {entity.GetType().Name} c Id = {entity.Id} в {DateTime.Now:dd.MM.yyyy hh:mm:ss}");
            
            return Task.CompletedTask;
        }
    }
}