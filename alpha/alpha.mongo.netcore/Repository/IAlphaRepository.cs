using Alpha.Mongo.Netcore.Models;

namespace Alpha.Mongo.Netcore.Repository
{
    public interface IAlphaRepository<T> where T : BsonModel
    {
        void Delete(string id);
        T Get(string id);
        PageableResponse<T> Get(PageableRequest pageableRequest);
        void Insert(T insertDocument);
        void Update(string id, T updateDocument);
    }
}