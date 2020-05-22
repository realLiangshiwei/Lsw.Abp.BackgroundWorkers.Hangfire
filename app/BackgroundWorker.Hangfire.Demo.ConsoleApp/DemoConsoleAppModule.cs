using Hangfire;
using Hangfire.MemoryStorage;
using Lsw.Abp.BackgroundWorkers.Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace BackgroundWorker.Hangfire.Demo.ConsoleApp
{

    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpBackgroundWorkerHangfireModule)
    )]
    public class DemoConsoleAppModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<IGlobalConfiguration>(hangfireConfiguration =>
            {
                hangfireConfiguration.UseMemoryStorage();
            });
        }
    }
}
