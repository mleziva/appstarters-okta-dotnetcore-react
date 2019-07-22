using Alpha.Mongo.Netcore.Models;
using MongoDB.Driver;
using System;

namespace Alpha.Mongo.Netcore.Repository
{
    public class AlphaRepository<T> : IAlphaRepository<T>
    {
        private IMongoCollection<T> collection;
        public AlphaRepository(IMongoCollection<T> mongoCollection)
        {
            this.collection = mongoCollection;
        }
        public T Get(string id)
        {
            throw new NotImplementedException();
        }
        public PageableResponse<T> Get(PageableRequest pageableRequest)
        {
            var result = collection.Find(FilterDefinition<T>.Empty)
                .Skip(pageableRequest.Skip)
                .Limit(pageableRequest.Top)
                .ToList();
            return new PageableResponse<T>()
            {
                Count = collection.CountDocuments(FilterDefinition<T>.Empty),
                Items = result,
                Skip = pageableRequest.Skip,
                Top = pageableRequest.Top
            };
        }
        public void Insert(T insertDocument)
        {
            collection.InsertOne(insertDocument);
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
