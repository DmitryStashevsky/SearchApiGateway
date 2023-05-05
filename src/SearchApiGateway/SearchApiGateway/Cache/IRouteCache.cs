
using SearchService.Search;
using Route = SearchService.Search.Route;

namespace SearchApiGateway.Cache
{
	internal interface IRouteCache
	{
		void AddToCache(Route route);
		void AddToCache(SearchRequest searchRequest, SearchResponse searchResponse);
        Route GetFromCache(Guid id);
        SearchResponse GetFromCache(SearchRequest searchRequest);
    }
}