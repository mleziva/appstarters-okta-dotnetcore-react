using System.Threading.Tasks;
using Alpha.Mongo.Netcore.Models;
using Alpha.Mongo.Netcore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Alpha.Mongo.Netcore.Controllers
{
    public class AlphaController<T> : ControllerBase where T : BsonModel
    {
        private IAlphaRepository<T> repository;
        public AlphaController(IAlphaRepository<T> repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<PageableResponse<T>> Get([FromQuery] PageableRequest pageableRequest)
        {
            return await repository.Get(pageableRequest);
        }

        [HttpGet("{id}")]
        public async Task<T> Get(string id)
        {
            return await repository.Get(id);

        }
        [HttpPost]
        public async Task<string> Post(T insertItem)
        {
            return await repository.Insert(insertItem);
        }
        [HttpPut("{id}")]
        public async Task Put(string id, T updateItem)
        {
            await repository.Update(id, updateItem);
        }
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await repository.Delete(id);
        }
    }
}
