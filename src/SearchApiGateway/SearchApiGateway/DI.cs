using SearchApiGateway.Cache;
using SearchApiGateway.Services;

namespace SearchApiGateway
{
    public static class DI
    {
        public static void AddSearchServiceGatewayDI(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IRouteCache, InMemoryRouteCache>();
            serviceCollection.AddScoped<ISearchServiceGateway, SearchServiceGateway>();
            serviceCollection.AddScoped<ICacheSearchService, CacheSearchService>();
        }
    }
}

