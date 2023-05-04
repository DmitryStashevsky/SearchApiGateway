using Microsoft.AspNetCore.Mvc;
using SearchService.Search;

namespace SearchApiGateway.Requests
{
    /// <summary>
    /// Search filters for request
    /// </summary>
	public class ApiSearchFilters
    {
        /// <summary>
        /// End date of route
        /// </summary>
        public DateTime? DestinationDateTime { get; set; }

        /// <summary>
        /// Maximum price of route
        /// </summary>
        public decimal? MaxPrice { get; set; }

        /// <summary>
        /// Minimum value of timelimit for route
        /// </summary>
        public DateTime? MinTimeLimit { get; set; }

        /// <summary>
        /// Forcibly search in cached data
        /// </summary>
        public bool? OnlyCached { get; set; }

        [ApiExplorerSettings(IgnoreApi = true)]
        public SearchFilters ToDto() => new SearchFilters
        {
            DestinationDateTime = DestinationDateTime,
            MaxPrice = MaxPrice,
            MinTimeLimit = MinTimeLimit,
            OnlyCached = OnlyCached
        };
    }
}

