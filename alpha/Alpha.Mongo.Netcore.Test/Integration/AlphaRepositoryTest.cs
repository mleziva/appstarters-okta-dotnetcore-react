using Alpha.Mongo.Netcore.Controllers;
using Alpha.Mongo.Netcore.Models;
using Alpha.Mongo.Netcore.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Moq;
using System.Threading.Tasks;

namespace Alpha.Mongo.Netcore.Test.Integration
{
    [TestClass]
    public class SearchIntegration
    {
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
        }
        [TestMethod]
        public void Get()
        {
            var server = new MongoClient("mongodb://localhost:27017/admin");
            var db = server.GetDatabase("firstdb");
            var collection = db.GetCollection<Car>("car");
            var car1 = new Car()
            {
                Make = "Honda",
                Model = "Civic",
                Description = "rambunctions alpha banana rooster banana",
                Year = 1994
            };
            collection.InsertOne(car1);
            //collection.Indexes.CreateOne(new CreateIndexModel<Car>("{'Description':'text'}"));
            //collection.Indexes.DropOne("Description_text");
            collection.Indexes.CreateOne(new CreateIndexModel<Car>("{'$**':'text'}"));

            //var result = collection.Find("{$text: {$search: 'banana'}}, {score: {$meta: 'textScore'}}");
            //var list = result.ToList();

            var filter = Builders<Car>.Filter.Text("Honda");
            var filter1 = Builders<Car>.Filter.Where(x => x.Year == 1994);
            var fitler2 = Builders<Car>.Filter.And(filter, filter1);
            var projection = Builders<Car>.Projection.MetaTextScore("TextMatchScore");
            var sort = Builders<Car>.Sort.MetaTextScore("TextMatchScore");
            var sortedResult = collection
                .Find(fitler2)
                .Project<Car>(projection)
                .Sort(sort)
                .ToList();
        }
    }
}
