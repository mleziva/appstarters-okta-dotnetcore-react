using Alpha.Mongo.Netcore.Controllers;
using Alpha.Mongo.Netcore.Models;
using Alpha.Mongo.Netcore.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Alpha.Mongo.Netcore.Test
{
    [TestClass]
    public class AlphaControllerTest
    {
        private static AlphaController<Car> controller;
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            var repository = new Mock<IAlphaRepository<Car>>();
            repository.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(new Car()));
            controller = new AlphaController<Car>(repository.Object);
        }
        [TestMethod]
        public async Task Get()
        {
            var response = await controller.Get("id");
        }
    }
}
