using SearchService.Providers.One;
using SearchService.Search;

namespace SearchService.Providers.Two
{
    internal class SearchProviderTwo : SearchProvider<ProviderTwoSearchRequest, ProviderTwoSearchResponse>
    {
        public SearchProviderTwo(HttpClient httpClient, SearchProviderTwoSettings settings)
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
                    Id = GetGuid(y.Departure.Point, y.Arrival.Point, y.Departure.Date.ToString(), y.Arrival.Date.ToString(),
                        y.Price.ToString(), y.TimeLimit.ToString(), nameof(SearchProviderTwo)),
                    Origin = y.Departure.Point,
                    Destination = y.Arrival.Point,
                    OriginDateTime = y.Departure.Date,
                    DestinationDateTime = y.Arrival.Date,
                    Price = y.Price,
                    TimeLimit = y.TimeLimit
                }).ToArray()
            };
        };
    }
}

