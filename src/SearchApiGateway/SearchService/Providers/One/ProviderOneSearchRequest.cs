namespace SearchService.Providers.One
{
	internal class ProviderOneSearchRequest
	{
        public string From { get; set; }
        public string To { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}

