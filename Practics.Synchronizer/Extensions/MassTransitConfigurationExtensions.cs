using System.Windows.Input;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Practics.Synchronizer.Consumers;
using Practics.Synchronizer.Core.Models;
using Practics.Synchronizer.Core.Models.DTO;
using Practics.Synchronizer.Core.Models.Events;
using Practics.Synchronizer.Settings;

namespace Practics.Synchronizer.Extensions
{
    public static class MassTransitConfigurationExtensions
    {
        public static void AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.AddConsumer<CreatedEventConsumer<AwardStatus, AwardStatusDTO>>();
                x.AddConsumer<UpdatedEventConsumer<AwardStatus, AwardStatusDTO>>();
                x.AddConsumer<DeletedEventConsumer<AwardStatus, AwardStatusDTO>>();
                
                x.AddConsumer<CreatedEventConsumer<Department, DepartmentDTO>>();
                x.AddConsumer<UpdatedEventConsumer<Department, DepartmentDTO>>();
                x.AddConsumer<DeletedEventConsumer<Department, DepartmentDTO>>();
                
                x.AddConsumer<CreatedEventConsumer<Person, PersonDTO>>();
                x.AddConsumer<UpdatedEventConsumer<Person, PersonDTO>>();
                x.AddConsumer<DeletedEventConsumer<Person, PersonDTO>>();
                
                x.AddConsumer<CreatedEventConsumer<PersonContacts, PersonContactsDTO>>();
                x.AddConsumer<UpdatedEventConsumer<PersonContacts, PersonContactsDTO>>();
                x.AddConsumer<DeletedEventConsumer<PersonContacts, PersonContactsDTO>>();
                
                x.AddConsumer<CreatedEventConsumer<Worker, WorkerDTO>>();
                x.AddConsumer<UpdatedEventConsumer<Worker, WorkerDTO>>();
                x.AddConsumer<DeletedEventConsumer<Worker, WorkerDTO>>();

                var massTransitConfig = new MassTransitSettings();
                configuration.Bind("MassTransit", massTransitConfig);
                
                x.UsingRabbitMq((context, cfg) =>
                {
                    if (massTransitConfig.UseDelayedMessageScheduler)
                        cfg.UseDelayedMessageScheduler();
                    
                    cfg.Host(massTransitConfig.Host, massTransitConfig.Port, "/", h =>
                    {
                        h.Username(massTransitConfig.Username);
                        h.Password(massTransitConfig.Password);
                    });
                    
                    cfg.ReceiveEndpoint("Practics.Synchronizer:AwardStatusEvent-Listener", configurator =>
                    {
                        configurator.ConfigureConsumer<CreatedEventConsumer<AwardStatus, AwardStatusDTO>>(context);
                        configurator.ConfigureConsumer<UpdatedEventConsumer<AwardStatus, AwardStatusDTO>>(context);
                        configurator.ConfigureConsumer<DeletedEventConsumer<AwardStatus, AwardStatusDTO>>(context);
                    });
                    
                    cfg.ReceiveEndpoint("Practics.Synchronizer:DepartmentEvent-Listener", configurator =>
                    {
                        configurator.ConfigureConsumer<CreatedEventConsumer<Department, DepartmentDTO>>(context);
                        configurator.ConfigureConsumer<UpdatedEventConsumer<Department, DepartmentDTO>>(context);
                        configurator.ConfigureConsumer<DeletedEventConsumer<Department, DepartmentDTO>>(context);
                    });

                    cfg.ReceiveEndpoint("Practics.Synchronizer:PersonEvent-Listener", configurator =>
                    {
                        configurator.ConfigureConsumer<CreatedEventConsumer<Person, PersonDTO>>(context);
                        configurator.ConfigureConsumer<UpdatedEventConsumer<Person, PersonDTO>>(context);
                        configurator.ConfigureConsumer<DeletedEventConsumer<Person, PersonDTO>>(context);
                    });

                    cfg.ReceiveEndpoint("Practics.Synchronizer:PersonContactsEvent-Listener", configurator =>
                    {
                        configurator.ConfigureConsumer<CreatedEventConsumer<PersonContacts, PersonContactsDTO>>(context);
                        configurator.ConfigureConsumer<UpdatedEventConsumer<PersonContacts, PersonContactsDTO>>(context);
                        configurator.ConfigureConsumer<DeletedEventConsumer<PersonContacts, PersonContactsDTO>>(context);
                    });

                    cfg.ReceiveEndpoint("Practics.Synchronizer:WorkerEvent-Listener", configurator =>
                    {
                        configurator.ConfigureConsumer<CreatedEventConsumer<Worker, WorkerDTO>>(context);
                        configurator.ConfigureConsumer<UpdatedEventConsumer<Worker, WorkerDTO>>(context);
                        configurator.ConfigureConsumer<DeletedEventConsumer<Worker, WorkerDTO>>(context);
                    });

                    
                    cfg.UseJsonSerializer();
                });
            });

            services.AddMassTransitHostedService();
        }
    }
}