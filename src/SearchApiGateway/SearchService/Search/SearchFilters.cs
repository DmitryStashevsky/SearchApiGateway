namespace SearchService.Search
{
	public class SearchFilters
	{
        // Optional
        // End date of route
        public DateTime? DestinationDateTime { get; set; }

        // Optional
        // Maximum price of route
        public decimal? MaxPrice { get; set; }

        // Optional
        // Minimum value of timelimit for route
        public DateTime? MinTimeLimit { get; set; }

        // Optional
        // Forcibly search in cached data
        public bool? OnlyCached { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is SearchFilters filters &&
                   DestinationDateTime == filters.DestinationDateTime &&
                   MaxPrice == filters.MaxPrice &&
                   MinTimeLimit == filters.MinTimeLimit;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DestinationDateTime, MaxPrice, MinTimeLimit);
        }
    }
}

