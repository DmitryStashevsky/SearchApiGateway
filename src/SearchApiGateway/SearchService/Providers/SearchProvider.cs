using System.Net.Http.Json;
using SearchService.Exceptions;
using SearchService.Generators;
using SearchService.Search;

namespace SearchService.Providers
{
    public abstract class SearchProvider<Search, Response> : ISearchProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IGuidGenerator _guidGenerator;
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
            HttpResponseMessage response = null;

            await ApiCall(async () =>
            {
                var response = await _httpClient.GetAsync(_pingUrl, cancellationToken);       
            }, _rootUrl.ToString());

            return response.IsSuccessStatusCode;
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest request, CancellationToken cancellationToken)
        {
            Response? result = default(Response);

            await ApiCall(async () =>
            {
                var searchRequest = MapSearchRequest(request);
                var content = JsonContent.Create(searchRequest);
                var response = await _httpClient.PostAsync(_searchUrl, content, cancellationToken);
                result = await response.Content.ReadFromJsonAsync<Response>();
            }, _rootUrl.ToString());
          
            return MapSearchResponse(result);
        }

        protected Guid GetGuid(params string[] args)
        {
            return _guidGenerator.Generate(args);
        }

        protected abstract Func<SearchRequest, Search> MapSearchRequest { get; }
        protected abstract Func<Response, SearchResponse> MapSearchResponse { get; }

        private static async Task ApiCall(Func<Task> apiCall, string apiName)
        {
            try
            {
                await apiCall();
            }
            catch (TimeoutException e)
            {
                throw new BusinessException($"API {apiName} doesn't available");
            }
        }
    }
}

