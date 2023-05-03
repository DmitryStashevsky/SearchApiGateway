using SearchService.Search;

namespace SearchService.Providers.Two
{
    internal class SearchProviderTwo : SearchProvider<ProviderTwoSearchRequest, ProviderTwoSearchResponse>
    {
        public SearchProviderTwo(HttpClient httpClient, ServiceProviderTwoSettings settings)
            : base(httpClient, settings.RootUrl, settings.PingUrl, settings.SearchUrl)
        { }

        protected override Func<SearchRequest, ProviderTwoSearchRequest> MapSearchRequest => x =>
        {
            return new ProviderTwoSearchRequest
            {
            };
        };

        protected override Func<ProviderTwoSearchResponse, SearchResponse> MapSearchResponse => x =>
        {
            return new SearchResponse
            {
                //todo add logic
            };
        };
    }
}

