using System;
using Volo.Abp.DependencyInjection;

namespace BackgroundWorker.Hangfire.Demo.ConsoleApp
{
    public class HelloWorldService : ITransientDependency
    {
        public void SayHello()
        {
            Console.WriteLine("Hello World!");
        }
    }
}
