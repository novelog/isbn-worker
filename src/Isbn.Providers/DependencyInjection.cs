using Isbn.Providers.Common;
using Isbn.Providers.Services.Mock;
using Microsoft.Extensions.DependencyInjection;

namespace Isbn.Providers;

public static class DependencyInjection
{
    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddKeyedSingleton<IIsbnService, FakeService>("fake");
        return services;
    }
}
