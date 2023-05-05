using SearchService.Search;

namespace SearchApiGateway.Services
{
	internal interface ICacheSearchService : ISearchService
	{
        void AddToCache(SearchRequest searchRequest, SearchResponse searchResponse);
    }
}

