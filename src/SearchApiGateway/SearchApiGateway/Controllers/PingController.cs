using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SearchApiGateway.Services;
using SearchService.Search;

namespace SearchApiGateway.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class PingController : ControllerBase
    {
        private readonly ISearchServiceGateway _searchServiceGateway;

        public PingController(ISearchServiceGateway searchServiceGateway)
        {
            _searchServiceGateway = searchServiceGateway;
        }

        /// <summary>
        /// Check availavility of search service
        /// </summary>
        /// <returns>Status codes 200 or 500</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<StatusCodeHttpResult> Get(CancellationToken token)
        {
            var isAvailable = await _searchServiceGateway.IsAvailableAsync(token);
            return isAvailable ? TypedResults.StatusCode(StatusCodes.Status200OK) : TypedResults.StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}