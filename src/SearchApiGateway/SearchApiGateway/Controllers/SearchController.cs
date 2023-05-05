using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SearchApiGateway.Requests;
using SearchApiGateway.Services;
using SearchService.Search;

namespace SearchApiGateway.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchServiceGateway _searchServiceGateway;

        public SearchController(ISearchServiceGateway searchServiceGateway)
        {
            _searchServiceGateway = searchServiceGateway;
        }

        /// <summary>
        /// Search route
        /// </summary>
        /// <param name="searchRequest"><seealso cref="ApiSearchRequest"/></param>
        /// <returns><seealso cref="SearchResponse"/></returns>
        [HttpPost]
        [ProducesResponseType(typeof(SearchResponse), (int)HttpStatusCode.OK)]
        public async Task<Results<BadRequest, Ok<SearchResponse>>> Post([FromBody] ApiSearchRequest searchRequest, CancellationToken token)
        {
            var result = await _searchServiceGateway.SearchAsync(searchRequest.ToDto(), token);
            return TypedResults.Ok(result);
        }
    }
}