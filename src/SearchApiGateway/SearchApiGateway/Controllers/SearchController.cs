using Microsoft.AspNetCore.Mvc;

namespace SearchApiGateway.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;

        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public Task Post()
        {
            return Task.CompletedTask;
        }
    }
}