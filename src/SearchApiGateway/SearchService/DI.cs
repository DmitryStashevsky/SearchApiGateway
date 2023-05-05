using Microsoft.Extensions.DependencyInjection;
using SearchService.Filters;
using SearchService.Generators;
using SearchService.Providers;
using SearchService.Providers.One;
using SearchService.Providers.Two;
using SearchService.Search;

namespace SearchService
{
	public static class DI
	{
		public static void AddSearchServiceDI(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<IFilterProvider, FilterProvider>();
			serviceCollection.AddSingleton<IGuidGenerator, KeyFieldGuidGenerator>();
            serviceCollection.AddScoped<ISearchProviderFactory, SearchProviderFactory>();
			serviceCollection.AddScoped<ISearchProvider, SearchProviderOne>();
            serviceCollection.AddScoped<ISearchProvider, SearchProviderTwo>();
            serviceCollection.AddScoped<ISearchService, Search.SearchService>();
        }
	}
}

