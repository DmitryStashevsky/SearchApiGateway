namespace SearchService.Providers
{
	internal interface ISearchProviderFactory
    {
		Task<ISearchProvider[]> GetActiveProviders(CancellationToken cancellationToken);
		int ProvidersCount { get; }
    }
}

