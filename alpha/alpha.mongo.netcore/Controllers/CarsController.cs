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
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private IAlphaRepository<Car> repository;
        public CarsController(IAlphaRepository<Car> repository)
        {
            this.repository = repository;
        }
        // GET: api/Cars
        [HttpGet]
        public PageableResponse<Car> Get(PageableRequest pageableRequest)
        {
            return repository.Get(pageableRequest);
        }

        // GET: api/Cars/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Cars
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
