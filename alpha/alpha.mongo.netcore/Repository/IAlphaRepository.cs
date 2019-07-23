using Alpha.Mongo.Netcore.Models;
using System.Threading.Tasks;

namespace Alpha.Mongo.Netcore.Repository
{
    public interface IAlphaRepository<T> where T : BsonModel
    {
        Task Delete(string id);
        Task<T> Get(string id);
        Task<PageableResponse<T>> Get(PageableRequest pageableRequest);
        Task<string> Insert(T insertDocument);
        Task Update(string id, T updateDocument);
    }
}