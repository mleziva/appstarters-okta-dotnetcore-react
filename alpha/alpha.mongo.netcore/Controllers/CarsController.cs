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
        public async Task<PageableResponse<Car>> Get([FromQuery] PageableRequest pageableRequest)
        {
            return await repository.Get(pageableRequest);
        }

        // GET: api/Cars/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Car> Get(string id)
        {
            return await repository.Get(id);

        }

        // POST: api/Cars
        [HttpPost]
        public async Task<string> Post(Car car)
        {
            return await repository.Insert(car);
        }

        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public async Task Put(string id, Car car)
        {
            await repository.Update(id, car);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await repository.Delete(id);
        }
    }
}
