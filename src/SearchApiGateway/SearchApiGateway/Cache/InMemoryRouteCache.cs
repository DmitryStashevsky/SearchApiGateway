using Microsoft.Extensions.Caching.Memory;
using SearchService.Search;
using Route = SearchService.Search.Route;

namespace SearchApiGateway.Cache
{
	internal class InMemoryRouteCache : IRouteCache
	{
        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        private MemoryCache _cacheWithFiltering = new MemoryCache(new MemoryCacheOptions());

        public void AddToCache(Route route)
        {
            _cache.Set(route.Id, route, DateTime.Now - route.TimeLimit);
        }

        public void AddToCache(SearchRequest searchRequest, SearchResponse searchResponse)
        {
            foreach(var route in searchResponse.Routes)
            {
                _cache.Set(route.Id, route, DateTime.Now - route.TimeLimit);
            }
            _cacheWithFiltering.Set(searchRequest, searchResponse, DateTime.Now - searchResponse.Routes.Min(x => x.TimeLimit));
        }

        public Route GetFromCache(Guid id)
        {
            Route? result;
            _cache.TryGetValue(id, out result);
            return result;
        }

        public SearchResponse GetFromCache(SearchRequest searchRequest)
        {
            SearchResponse? result;
            _cacheWithFiltering.TryGetValue(searchRequest, out result);
            return result;
        }
    }
}

