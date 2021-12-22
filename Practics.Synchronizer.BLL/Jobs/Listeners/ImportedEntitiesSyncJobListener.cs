using System.Threading;
using System.Threading.Tasks;
using Quartz;

namespace Practics.Synchronizer.BLL.Jobs.Listeners
{
    public class ImportedEntitiesSyncJobListener : IJobListener
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler;

        public ImportedEntitiesSyncJobListener(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }
        
        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.CompletedTask;
        }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.CompletedTask;
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException,
            CancellationToken cancellationToken = new ())
        {
            _scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            
            var jobName = context.JobDetail.Key.Name;
            
            await _scheduler.TriggerJob(new JobKey(jobName, "PublishJob"), cancellationToken);
        }
        
        public string Name { get; } = "SyncJobListener";
    }
}