using Coravel;
using Grpc.Net.Client;
using Isbn.Console.Services;
using Isbn.Console.Transports;
using Isbn.Console.Workers;
using Isbn.Providers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, cfg) =>
    cfg.ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddProviders()
    .AddScheduler()
    .AddHealthChecks();

builder.Services.AddTransient<FakeTask>()
    .AddSingleton(_ => new GrpcTransport(new GrpcChannelOptions()))
    .AddSingleton<IBookService, BookService>();

var app = builder.Build();
app.UseHealthChecks("/_health");

app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule<FakeTask>()
        .EverySeconds(10)
        .PreventOverlapping(nameof(FakeTask))
        .RunOnceAtStart();
});

app.Run();
