﻿using System.Net;
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
        /// <param name="token"></param>
        /// <returns><seealso cref="SearchResponse"/></returns>
        [HttpPost]
        [ProducesResponseType(typeof(SearchResponse), (int)HttpStatusCode.OK)]
        public async Task<SearchResponse> Post([FromBody] ApiSearchRequest searchRequest, CancellationToken token)
        {
            return await _searchServiceGateway.SearchAsync(searchRequest.ToDto(), token);
        }
    }
}