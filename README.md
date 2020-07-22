# Lsw.Abp.BackgroundWorkers.Hangfire

Abp backgroundworkes system hangfire integrated

 [![NuGet](https://img.shields.io/nuget/v/Lsw.Abp.BackgroundWorkers.Hangfire)](https://www.nuget.org/packages/Lsw.Abp.BackgroundWorkers.Hangfire/) [![NuGet](https://img.shields.io/nuget/dt/Lsw.Abp.BackgroundWorkers.Hangfire)](https://www.nuget.org/packages/Lsw.Abp.BackgroundWorkers.Hangfire/)

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
