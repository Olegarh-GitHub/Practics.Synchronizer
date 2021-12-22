using System;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Practics.Synchronizer.Core.Models.Events;

namespace Practics.Synchronizer.Consumers
{
    public class DeletedEventConsumer<TEntity, TDTO> : IConsumer<DeletedEvent<TDTO>>
    where TDTO : class
    {
        private readonly ILogger<DeletedEventConsumer<TEntity, TDTO>> _logger;

        public DeletedEventConsumer
        (
            ILogger<DeletedEventConsumer<TEntity, TDTO>> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<DeletedEvent<TDTO>> context)
        {
            var payload = context.Message.Id;
 
            _logger.LogInformation($"Была удалена сущность {typeof(TEntity)} с Id = {payload} в {DateTime.Now:dd.MM.yyyy hh:mm:ss}");
            
            return Task.CompletedTask;
        }
    }
}