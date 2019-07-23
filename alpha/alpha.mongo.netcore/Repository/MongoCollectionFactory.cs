using MongoDB.Driver;

namespace Alpha.Mongo.Netcore.Repository
{
    public class MongoCollectionFactory
    {
        public string DefaultDbName;
        private static IMongoClient mongoClient;
        public MongoCollectionFactory(IMongoClient mongoClient, string defaultDbName)
        {
            DefaultDbName = defaultDbName;
            MongoCollectionFactory.mongoClient = mongoClient;
        }
        public IMongoCollection<T> GetCollection<T>()
        {
            //My.Namespace.Model.ModelName
            var collectionName = typeof(T).ToString();
            var modelName = collectionName.Substring(collectionName.LastIndexOf('.')+1).ToLower();
            return GetCollection<T>(modelName, DefaultDbName);
        }
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return GetCollection<T>(collectionName, DefaultDbName);
        }
        private IMongoCollection<T> GetCollection<T>(string collectionName, string databaseName)
        {
            var db = mongoClient.GetDatabase(databaseName);
            var collection = db.GetCollection<T>(collectionName);
            return collection;
        }
    }
}
