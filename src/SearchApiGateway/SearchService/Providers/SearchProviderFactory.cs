namespace SearchService.Providers
{
	internal class SearchProviderFactory : ISearchProviderFactory
	{
        private readonly IEnumerable<ISearchProvider> _searchProviders;

        public int ProvidersCount => _searchProviders.Count();

        public SearchProviderFactory(IEnumerable<ISearchProvider> searchProviders)
		{
            _searchProviders = searchProviders;
        }

        public async Task<ISearchProvider[]> GetActiveProviders(CancellationToken cancellationToken)
        {
            var providersAvailability = _searchProviders.Select(x => new
            {
                Provider = x,
                IsAvailable = x.IsAvailableAsync(cancellationToken)
            });

            await Task.WhenAll(providersAvailability.Select(x => x.IsAvailable));

            return providersAvailability.Where(x => x.IsAvailable.Result).Select(x => x.Provider).ToArray();
        }
    }
}