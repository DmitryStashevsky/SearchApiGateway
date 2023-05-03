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
                Departure = x.Origin,
                Arrival = x.Destination,
                DepartureDate = x.OriginDateTime,
                MinTimeLimit = x.Filters?.MinTimeLimit
            };
        };

        protected override Func<ProviderTwoSearchResponse, SearchResponse> MapSearchResponse => x =>
        {
            return new SearchResponse
            {
                Routes = x?.Routes.Select(y => new Route
                {
                    Origin = y.Departure.Point,
                    Destination = y.Arrival.Point,
                    OriginDateTime = y.Departure.Date,
                    DestinationDateTime = y.Departure.Date,
                    Price = y.Price,
                    TimeLimit = y.TimeLimit
                }).ToArray()
            };
        };
    }
}

