using System.Net.Http.Json;
using SearchService.Search;

namespace SearchService.Providers
{
    public abstract class SearchProvider<Search, Response> : ISearchProvider
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _rootUrl;
        private readonly Uri _pingUrl;
        private readonly Uri _searchUrl;

        protected SearchProvider(
            HttpClient httpClient,
            string rootUrl,
            string pingUrl,
            string searchUrl)
        {
            _httpClient = httpClient;
            _rootUrl = new Uri(rootUrl);
            _pingUrl = new Uri(_rootUrl, pingUrl);
            _searchUrl = new Uri(_rootUrl, searchUrl);
        }

        public async Task<bool> IsAvailableAsync(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(_pingUrl, cancellationToken);
            return response.IsSuccessStatusCode;
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
        {
            var searchRequest = MapSearchRequest(request);
            var content = JsonContent.Create(searchRequest);
            var response = await _httpClient.PostAsync(_searchUrl, content, cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<Response>();
            return MapSearchResponse(result);
        }

        protected abstract Func<SearchRequest, Search> MapSearchRequest { get; }
        protected abstract Func<Response, SearchResponse> MapSearchResponse { get; }
    }
}

