namespace SearchService.Providers.Two
{
	internal class ProviderTwoSearchRequest
	{
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? MinTimeLimit { get; set; }
    }
}

