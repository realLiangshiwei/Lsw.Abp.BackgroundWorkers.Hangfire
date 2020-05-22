using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;

namespace Lsw.Abp.BackgroundWorkers.Hangfire
{
    public interface IHangfireBackgroundWorker :　IBackgroundWorker
    {
        string CronExpression { get; set; }

        Task ExecuteAsync();
    }
}
