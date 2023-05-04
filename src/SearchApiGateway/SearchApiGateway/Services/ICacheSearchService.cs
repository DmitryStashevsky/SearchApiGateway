using SearchService.Search;

namespace SearchApiGateway.Services
{
	internal interface ICacheSearchService : ISearchService
	{
        void AddToCache(SearchService.Search.Route[] routes);
    }
}

