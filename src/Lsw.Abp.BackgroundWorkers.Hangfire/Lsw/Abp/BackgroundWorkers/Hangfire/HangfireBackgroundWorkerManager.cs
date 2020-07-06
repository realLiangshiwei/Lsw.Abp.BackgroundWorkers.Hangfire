using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;

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
                RecurringJob.AddOrUpdate(() => hangfireBackgroundWorker.ExecuteAsync(),
                    hangfireBackgroundWorker.CronExpression);
            }
            else
            {
                int? period;

                if (worker is AsyncPeriodicBackgroundWorkerBase || worker is PeriodicBackgroundWorkerBase)
                {
                    var timer = (AbpTimer) worker.GetType()
                        .GetProperty("Timer", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(worker);
                    period = timer?.Period;
                }
                else
                {
                    return;
                }

                if (period == null)
                {
                    return;
                }

                var adapterType = typeof(HangfirePeriodicBackgroundWorkerAdapter<>).MakeGenericType(worker.GetType());
                var workerAdapter = Activator.CreateInstance(adapterType) as IHangfireBackgroundWorker;

                RecurringJob.AddOrUpdate(() => workerAdapter.ExecuteAsync(), GetCron(period.Value));
            }
        }

        protected virtual string GetCron(int period)
        {
            var time = TimeSpan.FromSeconds(period /= 1000);
            var cron = "";

            if (time.TotalSeconds <= 59)
            {
                cron = $"*/{period} * * * * *";
            }
            else if (time.TotalMinutes <= 59)
            {
                cron = Cron.Hourly(time.Minutes);
            }
            else if (time.TotalHours <= 59)
            {
                cron = Cron.Daily(time.Hours, time.Minutes);
            }
            else
            {
                Cron.Monthly(time.Days, time.Hours, time.Minutes);
            }

            return cron;
        }
    }
}
