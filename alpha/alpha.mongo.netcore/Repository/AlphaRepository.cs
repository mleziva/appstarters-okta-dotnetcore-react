﻿using Alpha.Mongo.Netcore.Models;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Alpha.Mongo.Netcore.Repository
{
    public class AlphaRepository<T> : IAlphaRepository<T> where T : BsonModel
    {
        private IMongoCollection<T> collection;
        public AlphaRepository(IMongoCollection<T> mongoCollection)
        {
            this.collection = mongoCollection;
        }
        public async Task<T> Get(string id)
        {
            var stringFilter = createIdFilter(id);
            var entityStringFiltered = collection.Find(stringFilter);
            return await entityStringFiltered.SingleOrDefaultAsync();
        }
        public async Task<PageableResponse<T>> Get(PageableRequest pageableRequest)
        {
            var result = await collection.Find(FilterDefinition<T>.Empty)
                .Sort(CreateSortFromOrderBy(pageableRequest.OrderBy))
                .Skip(pageableRequest.Skip)
                .Limit(pageableRequest.Top)
                .ToListAsync();
            return new PageableResponse<T>()
            {
                Count = collection.CountDocuments(FilterDefinition<T>.Empty),
                Items = result,
                Skip = pageableRequest.Skip,
                Top = pageableRequest.Top
            };
        }
        public async Task<string> Insert(T insertDocument)
        {
            insertDocument.Id = null;
            await collection.InsertOneAsync(insertDocument);
            return insertDocument.Id;
        }
        public async Task Update(string id, T updateDocument)
        {
            updateDocument.Id = id;
            var stringFilter = createIdFilter(id);
            await collection.FindOneAndReplaceAsync(stringFilter, updateDocument);
        }
        public async Task Delete(string id)
        {
            var stringFilter = createIdFilter(id);
            await collection.FindOneAndDeleteAsync(stringFilter);
        }
        private string createIdFilter(string id)
        {
            return "{ _id: ObjectId('" + id + "') }";
        }
        private SortDefinition<T> CreateSortFromOrderBy(string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy)) return null;
            var terms = orderBy.Split(" ");
            var sort = terms[0];
            if (terms.Length == 2 && terms[1].Equals("desc", StringComparison.OrdinalIgnoreCase)){
                return Builders<T>.Sort.Descending(sort);
            }
            //maybe do something regarding capitalization eventually
            return Builders<T>.Sort.Ascending(sort);
        }
    }
}
