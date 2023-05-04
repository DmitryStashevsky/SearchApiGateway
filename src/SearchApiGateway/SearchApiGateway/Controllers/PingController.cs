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
        /// 
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<StatusCodeResult> Get(CancellationToken token)
        {
            var isAvailable = await _searchServiceGateway.IsAvailableAsync(token);
            return isAvailable ? StatusCode(200) : StatusCode(500);
        }
    }
}