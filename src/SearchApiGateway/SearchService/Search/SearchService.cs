using SearchService.Filters;
using SearchService.Providers;

namespace SearchService.Search
{
	internal class SearchService : ISearchService
	{
        private readonly ISearchProviderFactory _searchProviderFactory;
        private readonly IFilterProvider _filterProvider;

        public SearchService(ISearchProviderFactory searchProviderFactory, IFilterProvider filterProvider)
        {
            _searchProviderFactory = searchProviderFactory;
            _filterProvider = filterProvider;
        }

        public async Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
        {
            var providers = await _searchProviderFactory.GetActiveProviders(cancellationToken);
            return providers.Length > 0;
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
        {
            var providers = await _searchProviderFactory.GetActiveProviders(cancellationToken);

            var searchTasks = providers.Select(x => x.SearchAsync(request, cancellationToken));
            await Task.WhenAll(searchTasks);

            var routes = searchTasks.SelectMany(x => x.Result.Routes).ToArray();

            return _filterProvider.Filter(routes, request.Filters);
        }
    }
}

