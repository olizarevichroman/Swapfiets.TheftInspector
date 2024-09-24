using Microsoft.Extensions.DependencyInjection;

namespace Swapfiets.TheftInspector.BikeIndexAdapter.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBikeIndexAdapterServices(this IServiceCollection services)
    {
        services.AddHttpClient<IBikeIndexApiClient, BikeIndexApiClient>();

        return services.AddTransient<IBikeIndexApiClient, BikeIndexApiClient>();
    }
}
