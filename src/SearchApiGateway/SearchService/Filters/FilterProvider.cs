using SearchService.Search;

namespace SearchService.Filters
{
	public class FilterProvider : IFilterProvider
	{
        public SearchResponse Filter(Route[] routes, SearchFilters filters)
        {
            var filteredRoutes = new List<Route>();
            decimal minPrice = 0;
            decimal maxPrice = 0;
            var minMinutesRoute = TimeSpan.FromMinutes(0);
            var maxMinutesRoute = TimeSpan.FromMinutes(0);
            foreach (var route in routes)
            {
                if (IsFilterConditionSucceed(route, filters))
                {
                    filteredRoutes.Add(route);

                    minPrice = minPrice > route.Price ? route.Price : minPrice;
                    maxPrice = maxPrice < route.Price ? route.Price : maxPrice;

                    var routeTime = route.DestinationDateTime - route.OriginDateTime;
                    minMinutesRoute = minMinutesRoute > routeTime ? routeTime : minMinutesRoute;
                    maxMinutesRoute = maxMinutesRoute < routeTime ? routeTime : maxMinutesRoute;
                }
            }

            return new SearchResponse
            {
                Routes = filteredRoutes.ToArray(),
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                MinMinutesRoute = minMinutesRoute.Minutes,
                MaxMinutesRoute = maxMinutesRoute.Minutes
            };
        }

        private bool IsFilterConditionSucceed(Route route, SearchFilters filters)
        {
            if (filters != null)
            {
                if (filters.MaxPrice != null && route.Price > filters.MaxPrice)
                {
                    return false;
                }
                if (filters.DestinationDateTime != null && route.DestinationDateTime > filters.DestinationDateTime)
                {
                    return false;
                }
                if (filters.MinTimeLimit != null && route.TimeLimit < filters.MinTimeLimit)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

