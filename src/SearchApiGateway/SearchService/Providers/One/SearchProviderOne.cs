using SearchService.Search;

namespace SearchService.Providers.One
{
    internal class SearchProviderOne : SearchProvider<ProviderOneSearchRequest, ProviderOneSearchResponse>
	{
        public SearchProviderOne(HttpClient httpClient, SearchProviderOneSettings settings)
            : base(httpClient, settings.RootUrl, settings.PingUrl, settings.SearchUrl)
        { }

        protected override Func<SearchRequest, ProviderOneSearchRequest> MapSearchRequest => x =>
        {
            return new ProviderOneSearchRequest
            {
                From = x.Origin,
                To = x.Destination,
                DateFrom = x.OriginDateTime,
                DateTo = x.Filters?.DestinationDateTime,
                MaxPrice = x.Filters?.MaxPrice
            };
        };

        protected override Func<ProviderOneSearchResponse, SearchResponse> MapSearchResponse => x =>
        {
            return new SearchResponse
            {
                Routes = x.Routes?.Select(y => new Route
                {
                    Id = GetGuid(y.From, y.To, y.DateFrom.ToString(), y.DateTo.ToString(), y.Price.ToString(), y.TimeLimit.ToString(), nameof(SearchProviderOne)),
                    Origin = y.From,
                    Destination = y.To,
                    OriginDateTime = y.DateFrom,
                    DestinationDateTime = y.DateTo,
                    Price = y.Price,
                    TimeLimit = y.TimeLimit
                }).ToArray()
            };
        };
    }
}

