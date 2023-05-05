using SearchApiGateway.Cache;
using SearchService.Search;

namespace SearchApiGateway.Services
{
	internal class CacheSearchService : ICacheSearchService
    {
        private readonly IRouteCache _routeCache;

        public CacheSearchService(IRouteCache routeCache)
		{
            _routeCache = routeCache;

        }

        void ICacheSearchService.AddToCache(SearchRequest searchRequest, SearchResponse searchResponse)
        {
            _routeCache.AddToCache(searchRequest, searchResponse);
        }

        Task<bool> ISearchService.IsAvailableAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException("Can not be called for current implementation");
        }

        Task<SearchResponse> ISearchService.SearchAsync(SearchRequest searchRequest, CancellationToken cancellationToken)
        {
            return Task.FromResult(_routeCache.GetFromCache(searchRequest));
        }
    }
}

