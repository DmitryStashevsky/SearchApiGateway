namespace SearchService.Providers.Two
{
	internal class ProviderTwoRoute
	{
        public ProviderTwoPoint Departure { get; set; }
        public ProviderTwoPoint Arrival { get; set; }
        public decimal Price { get; set; }
        public DateTime TimeLimit { get; set; }
    }
}

