using Alpha.Mongo.Netcore.Controllers;
using Alpha.Mongo.Netcore.Models;
using Alpha.Mongo.Netcore.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Moq;
using System.Threading.Tasks;

namespace Alpha.Mongo.Netcore.Test.Repository
{
    [TestClass]
    public class AlphaRepositoryTest
    {
        private static AlphaRepository<Car> repo;
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            var collection = new CollectionTestImp<Car>();
            repo = new AlphaRepository<Car>(collection);
        }
        [TestMethod]
        public async Task Get()
        {
            var response = await repo.Get("id");
        }
    }
}
