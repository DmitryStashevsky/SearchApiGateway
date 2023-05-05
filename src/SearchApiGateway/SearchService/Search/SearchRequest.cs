using System;

namespace SearchService.Search
{
	public class SearchRequest
	{
        // Mandatory
        // Start point of route, e.g. Moscow 
        public string Origin { get; set; }

        // Mandatory
        // End point of route, e.g. Sochi
        public string Destination { get; set; }

        // Mandatory
        // Start date of route
        public DateTime OriginDateTime { get; set; }

        // Optional
        public SearchFilters? Filters { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is SearchRequest request &&
                   Origin == request.Origin &&
                   Destination == request.Destination &&
                   OriginDateTime == request.OriginDateTime &&
                   Filters?.GetHashCode() == request.Filters?.GetHashCode();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Origin, Destination, OriginDateTime, Filters);
        }
    }
}

