using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;

namespace Lsw.Abp.BackgroundWorkers.Hangfire
{
    public abstract class HangfireBackgroundWorkerBase : BackgroundWorkerBase, IHangfireBackgroundWorker
    {
        public string CronExpression { get; set; }

        public abstract Task ExecuteAsync();
    }
}
