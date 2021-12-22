using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Practics.Synchronizer.BLL.Jobs;
using Practics.Synchronizer.BLL.Jobs.Listeners;
using Practics.Synchronizer.Core.Models;
using Practics.Synchronizer.Core.Models.DTO;
using Practics.Synchronizer.DAL.Models;
using Practics.Synchronizer.Settings;
using Quartz;
using Quartz.Impl.Matchers;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Practics.Synchronizer.Extensions
{
    public static class QuartzConfigurationExtensions
    {
        public static void AddQuartz(this IServiceCollection services, IConfiguration configuration)
        {
            var quartzConfig = new QuartzSynchronizePeriodsSettings();
            configuration.Bind("QuartzSyncPeriod", quartzConfig);
            services.AddQuartz(q =>
            {
                q.SchedulerId = "Scheduler-Core";

                q.UseMicrosoftDependencyInjectionScopedJobFactory(options =>
                {
                    options.AllowDefaultConstructor = true;
                });

                q.UseSimpleTypeLoader();
                q.UseInMemoryStore();
                q.UseDefaultThreadPool(tp =>
                {
                    tp.MaxConcurrency = 10;
                });

                #region AwardStatusJobs
                
                q.ScheduleJob<ImportedEntitiesSyncJob<AwardStatusRaw, AwardStatus>>(trigger => trigger
                    .WithIdentity($"{nameof(AwardStatus)}","SyncJob")
                    .WithSimpleSchedule
                    (
                        x=> x
                            .RepeatForever()
                            .WithInterval(TimeSpan.FromMinutes(quartzConfig.AwardStatus))
                    )
                    .StartNow()
                );
                
                q.ScheduleJob<ImportedEntitiesPublishJob<AwardStatus, AwardStatusDTO>>(trigger => trigger
                    .WithIdentity($"{nameof(AwardStatus)}", "PublishJob")
                    .WithSimpleSchedule
                    (
                        x=> x
                            .RepeatForever()
                            .WithInterval(TimeSpan.FromHours(quartzConfig.AwardStatus))
                    )
                    .StartNow()
                );
                
                #endregion
                
                #region DepartmentJobs
                
                q.ScheduleJob<ImportedEntitiesSyncJob<DepartmentRaw, Department>>(trigger => trigger
                    .WithIdentity($"{nameof(Department)}","SyncJob")
                    .WithSimpleSchedule
                    (
                        x=> x
                            .RepeatForever()
                            .WithInterval(TimeSpan.FromMinutes(quartzConfig.Department))
                    )
                    .StartNow()
                );
                
                q.ScheduleJob<ImportedEntitiesPublishJob<Department, DepartmentDTO>>(trigger => trigger
                    .WithIdentity($"{nameof(Department)}", "PublishJob")
                    .WithSimpleSchedule
                    (
                        x=> x
                            .RepeatForever()
                            .WithInterval(TimeSpan.FromHours(quartzConfig.Department))
                    )
                    .StartNow());
                
                #endregion
                
                #region PersonJobs
                
                q.ScheduleJob<ImportedEntitiesSyncJob<PersonRaw, Person>>(trigger => trigger
                    .WithIdentity($"{nameof(Person)}","SyncJob")
                    .WithSimpleSchedule
                    (
                        x=> x
                            .RepeatForever()
                            .WithInterval(TimeSpan.FromMinutes(quartzConfig.Person))
                    )
                    .StartNow()
                );
                
                q.ScheduleJob<ImportedEntitiesPublishJob<Person, PersonDTO>>(trigger => trigger
                    .WithIdentity($"{nameof(Person)}", "PublishJob")
                    .WithSimpleSchedule
                    (
                        x=> x
                            .RepeatForever()
                            .WithInterval(TimeSpan.FromHours(quartzConfig.Person))
                    )
                    .StartNow()
                );
                
                #endregion
                
                #region PersonContactsJobs
                
                q.ScheduleJob<ImportedEntitiesSyncJob<PersonContactsRaw, PersonContacts>>(trigger => trigger
                    .WithIdentity($"{nameof(PersonContacts)}","SyncJob")
                    .WithSimpleSchedule
                    (
                        x=> x
                            .RepeatForever()
                            .WithInterval(TimeSpan.FromMinutes(quartzConfig.PersonContacts))
                    )
                    .StartNow()
                );
                
                q.ScheduleJob<ImportedEntitiesPublishJob<PersonContacts, PersonContactsDTO>>(trigger => trigger
                    .WithIdentity($"{nameof(PersonContacts)}", "PublishJob")
                    .WithSimpleSchedule
                    (
                        x=> x
                            .RepeatForever()
                            .WithInterval(TimeSpan.FromHours(quartzConfig.PersonContacts))
                    )
                    .StartNow()
                );
                
                #endregion
                
                #region WorkerJobs
                
                q.ScheduleJob<ImportedEntitiesSyncJob<WorkerRaw, Worker>>(trigger => trigger
                    .WithIdentity($"{nameof(Worker)}","SyncJob")
                    .WithSimpleSchedule
                    (
                        x=> x
                            .RepeatForever()
                            .WithInterval(TimeSpan.FromMinutes(quartzConfig.Worker))
                    )
                    .StartNow()
                );
                
                q.ScheduleJob<ImportedEntitiesPublishJob<Worker, WorkerDTO>>(trigger => trigger
                    .WithIdentity($"{nameof(Worker)}", "PublishJob")
                    .WithSimpleSchedule
                    (
                        x=> x
                            .RepeatForever()
                            .WithInterval(TimeSpan.FromHours(quartzConfig.Worker))
                    )
                    .StartNow()
                );
                
                #endregion
                
                q.AddJobListener<ImportedEntitiesSyncJobListener>(GroupMatcher<JobKey>.GroupEquals("SyncJob"));
            });
            
            services.AddQuartzServer(options =>
            {
                options.WaitForJobsToComplete = true;
            });
        }
    }
}