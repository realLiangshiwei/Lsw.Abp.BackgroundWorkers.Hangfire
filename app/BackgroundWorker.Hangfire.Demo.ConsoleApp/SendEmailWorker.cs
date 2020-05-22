using System.Threading.Tasks;
using Hangfire;
using Lsw.Abp.BackgroundWorkers.Hangfire;

namespace BackgroundWorker.Hangfire.Demo.ConsoleApp
{
    public class SendEmailWorker : HangfireBackgroundWorkerBase
    {
        private readonly HelloWorldService _helloWorldService;

        public SendEmailWorker(HelloWorldService helloWorldService)
        {
            _helloWorldService = helloWorldService;
            CronExpression = Cron.Minutely();
        }

        public override Task ExecuteAsync()
        {
            _helloWorldService.SayHello();
            return Task.CompletedTask;
        }
    }
}
