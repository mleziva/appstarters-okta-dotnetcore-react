using MongoDB.Driver;
using System;

namespace Alpha.Mongo.Netcore.Repository
{
    public class AlphaRepository<T> : IAlphaRepository<T>
    {
        private IMongoCollection<T> mongoCollection;
        public AlphaRepository(IMongoCollection<T> mongoCollection)
        {
            this.mongoCollection = mongoCollection;
        }
        public T Get(string id)
        {
            throw new NotImplementedException();
        }
        public T GetAll()
        {
            //take paging parameter eventually
            throw new NotImplementedException();
        }
        public void Insert(T insertDocument)
        {
            throw new NotImplementedException();
        }
        public void Update(T updateDocument)
        {
            throw new NotImplementedException();
        }
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
