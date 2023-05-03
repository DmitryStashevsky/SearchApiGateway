using SearchService.Search;

namespace SearchService.Filters
{
	public interface IFilterProvider
	{
		SearchResponse Filter(Route[] routes, SearchFilters filters);
	}
}

