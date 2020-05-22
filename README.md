# Lsw.Abp.BackgroundWorkers.Hangfire

Abp backgroundworkes system hangfire integrated

## Getting Started

### Install

Add `Lsw.Abp.BackgroundWorkers.Hangfire` Nuget Package to your project:

`dotnet add package Lsw.Abp.BackgroundWorkers.Hangfire`

Add `AbpBackgroundWorkerHangfireModule` to your module Dependency list:

```csharp
[DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpBackgroundWorkerHangfireModule)
    )]
public class AppModule : AbpModule
{
}
```

### Create a Background Worker

```csharp
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
```

Add worker to `IBackgroundWorkerManager`

```csharp
var backgroundWorkerManager = application.ServiceProvider.GetService<IBackgroundWorkerManager>();
var worker = application.ServiceProvider.GetService<SendEmailWorker>();
backgroundWorkerManager.Add(worker);
```