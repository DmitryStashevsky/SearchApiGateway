using SearchService.Search;

namespace SearchApiGateway.Services
{
	internal class SearchServiceGateway : ISearchServiceGateway
    {
        private readonly ISearchService _searchService;
        private readonly ICacheSearchService _cacheSearchService;

        public SearchServiceGateway(ISearchService searchService, ICacheSearchService cacheSearchService)
        {
            _searchService = searchService;
            _cacheSearchService = cacheSearchService;
        }

        public async Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
        {
            return await _searchService.IsAvailableAsync(cancellationToken);
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
        {
            if (request.Filters != null && request.Filters.OnlyCached == true)
            {
                return await _cacheSearchService.SearchAsync(request, cancellationToken);
            }

            var result = await _searchService.SearchAsync(request, cancellationToken);

            _cacheSearchService.AddToCache(request, result);

            return result;
        }
    }
}