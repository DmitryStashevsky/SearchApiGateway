using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SearchService.Search;

namespace SearchApiGateway.Requests
{
    /// <summary>
    /// Search request for route
    /// </summary>
	public class ApiSearchRequest
	{
        /// <summary>
        /// Start point of route
        /// </summary>
        [Required]
        public string Origin { get; set; }

        /// <summary>
        /// End point of route, e.g. Sochi
        /// </summary>
        [Required]
        public string Destination { get; set; }

        /// <summary>
        /// Start date of route
        /// </summary>
        [Required]
        public DateTime OriginDateTime { get; set; }

        /// <summary>
        /// Filters
        /// </summary>
        public ApiSearchFilters? Filters { get; set; }

        [ApiExplorerSettings(IgnoreApi = true)]
        public SearchRequest ToDto() => new SearchRequest
        {
            Origin = Origin,
            Destination = Destination,
            OriginDateTime = OriginDateTime,
            Filters = Filters?.ToDto()
        };
    }
}

