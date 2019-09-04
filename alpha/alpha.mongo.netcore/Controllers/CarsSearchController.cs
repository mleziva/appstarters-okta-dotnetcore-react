using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alpha.Mongo.Netcore.Models;
using Alpha.Mongo.Netcore.Repository;
using Alpha.Mongo.Netcore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alpha.Mongo.Netcore.Controllers
{
    [Route("api/Cars")]
    [ApiController]
    public class CarsSearchController : ControllerBase
    {
        private IElasticSearch searchService;
        public CarsSearchController(IElasticSearch searchService)
        {
            this.searchService = searchService;
        }
        [Route("search")]
        [HttpGet]
        public async Task<string> Get(object searchQuery)
        {
            //make this simple
            return await searchService.SearchAsync(searchQuery.ToString());
        }
        [Route("search")]
        [HttpPost]
        public async Task<string> Post([FromBody]object searchQuery)
        {
            return await searchService.SearchAsync(searchQuery.ToString());
        }
    }
}