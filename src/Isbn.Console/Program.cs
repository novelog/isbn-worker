using Coravel;
using Isbn.Console.Workers;
using Isbn.Providers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, cfg) =>
    cfg.ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddProviders()
    .AddScheduler()
    .AddHealthChecks();

builder.Services.AddTransient<FakeTask>();

var app = builder.Build();
app.UseHealthChecks("/_health");

app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<FakeTask>()
        .EverySeconds(30)
        .PreventOverlapping(nameof(FakeTask))
        .RunOnceAtStart();
});

app.Run();
