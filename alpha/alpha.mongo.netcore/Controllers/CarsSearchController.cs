using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alpha.Mongo.Netcore.Models;
using Alpha.Mongo.Netcore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alpha.Mongo.Netcore.Controllers
{
    [Route("api/Cars")]
    [ApiController]
    public class CarsSearchController : ControllerBase
    {
        private IAlphaRepository<Car> repository;
        public CarsSearchController(IAlphaRepository<Car> repository)
        {
            this.repository = repository;
        }
        [Route("search")]
        [HttpGet]
        public async Task<PageableResponse<Car>> Get([FromQuery] SearchQuery searchQuery)
        {
            //GET /indexes/hotel/docs?search=lodging&$filter=City eq ‘Seattle’ and Parking and Type ne ‘motel’
            return await repository.Get(pageableRequest);
        }
    }
}