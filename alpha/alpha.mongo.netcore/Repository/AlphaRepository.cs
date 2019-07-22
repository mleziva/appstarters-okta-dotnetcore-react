using Alpha.Mongo.Netcore.Models;
using MongoDB.Driver;
using System;

namespace Alpha.Mongo.Netcore.Repository
{
    public class AlphaRepository<T> : IAlphaRepository<T> where T : BsonModel
    {
        private IMongoCollection<T> collection;
        public AlphaRepository(IMongoCollection<T> mongoCollection)
        {
            this.collection = mongoCollection;
        }
        public T Get(string id)
        {
            var stringFilter = createIdFilter(id);
            var entityStringFiltered = collection.Find(stringFilter);
            //var stockStringFiltered = await entityStringFiltered.SingleOrDefaultAsync();
            return entityStringFiltered.SingleOrDefault();
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
            insertDocument.Id = null;
            collection.InsertOne(insertDocument);
        }
        public void Update(string id, T updateDocument)
        {
            updateDocument.Id = id;
            var stringFilter = createIdFilter(id);
            var entityStringFiltered = collection.FindOneAndReplace(stringFilter, updateDocument);
        }
        public void Delete(string id)
        {
            var stringFilter = createIdFilter(id);
            var entityStringFiltered = collection.FindOneAndDelete(stringFilter);
        }
        private string createIdFilter(string id)
        {
            return "{ _id: ObjectId('" + id + "') }";
        }
    }
}
