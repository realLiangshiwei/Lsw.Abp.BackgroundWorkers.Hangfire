using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.DependencyInjection;

namespace Lsw.Abp.BackgroundWorkers.Hangfire
{
    [Dependency(ReplaceServices = true)]
    public class HangfireBackgroundWorkerManager : IBackgroundWorkerManager, ISingletonDependency
    {
        public Task StartAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.CompletedTask;
        }

        public void Add(IBackgroundWorker worker)
        {
            if (worker is IHangfireBackgroundWorker hangfireBackgroundWorker)
            {
                RecurringJob.AddOrUpdate(() => hangfireBackgroundWorker.ExecuteAsync(),hangfireBackgroundWorker.CronExpression);
            }
        }
    }
}
