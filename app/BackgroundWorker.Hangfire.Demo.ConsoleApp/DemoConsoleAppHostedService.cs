using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Volo.Abp;
using Volo.Abp.BackgroundWorkers;

namespace BackgroundWorker.Hangfire.Demo.ConsoleApp
{
    public class DemoConsoleAppHostedService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var application = AbpApplicationFactory.Create<DemoConsoleAppModule>(options =>
            {
                options.UseAutofac(); //Autofac integration
                options.Services.AddLogging(c => c.AddSerilog());
            }))
            {
                application.Initialize();

                var backgroundWorkerManager = application.ServiceProvider.GetService<IBackgroundWorkerManager>();
                var worker = application.ServiceProvider.GetService<SendEmailWorker>();
                backgroundWorkerManager.Add(worker);

                Console.ReadKey();
                application.Shutdown();
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
