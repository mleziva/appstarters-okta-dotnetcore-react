using Alpha.Mongo.Netcore.Controllers;
using Alpha.Mongo.Netcore.Models;
using Alpha.Mongo.Netcore.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using Moq;
using System;
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
        public void Search()
        {
            var server = new MongoClient("mongodb://localhost:27017/admin");
            var db = server.GetDatabase("firstdb");
            var collection = db.GetCollection<Car>("car");
            var car1 = new Car()
            {
                Make = "Honda",
                Model = "Civic",
                Description = "rambunctions alpha banana rooster banana",
                Year = 1999
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
            var filter3 = Builders<Car>.Filter.Eq("Model", "Civic");
            var fitler4 = Builders<Car>.Filter.And(fitler2, filter3);
            var projection = Builders<Car>.Projection.MetaTextScore("TextMatchScore");
            var sort = Builders<Car>.Sort.MetaTextScore("TextMatchScore");
            var sortedResult = collection
                .Find(fitler4)
                .Project<Car>(projection)
                .Sort(sort)
                .ToList();
            var sortedResult1 = collection
                .Find("{ 'Year': { '$gt' : 1995 , '$lt' : 2000 }, 'Model' : 'Civic' }")
                .Project<Car>(projection)
                .Sort(sort)
                .ToList();
            
        }
        [TestMethod]
        public void Facets()
        {
            var collection = new MongoClient("mongodb://localhost:27017/admin").GetDatabase("firstdb").GetCollection<Book>("book");
            var car1 = new Book()
            {
                Name = "b1",
                Tags = new System.Collections.Generic.Dictionary<string, string>()
                {
                    { "Edition","Blah" },
                    { "Published","2018" }
                }
            };
            collection.InsertOne(car1);

            var pipelinex = collection.Aggregate()
                .Match(b => b.Name == "b1")
                .Project("{Tags: { $objectToArray: \"$Tags\" }}")
                .Unwind("Tags")
                .SortByCount<BsonDocument>("$Tags");

            var outputx = pipelinex.ToList();
            var json = outputx.ToJson(new JsonWriterSettings { Indent = true });


            var project = PipelineStageDefinitionBuilder.Project<Book, BsonDocument>("{Tags: { $objectToArray: \"$Tags\" }}");
            var unwind = PipelineStageDefinitionBuilder.Unwind<BsonDocument, BsonDocument>("Tags");
            var sortByCount = PipelineStageDefinitionBuilder.SortByCount<BsonDocument, BsonDocument>("$Tags");

            var pipeline = PipelineDefinition<Book, AggregateSortByCountResult<BsonDocument>>.Create(new IPipelineStageDefinition[] { project, unwind, sortByCount });

            // string based alternative version
            //var pipeline = PipelineDefinition<Book, BsonDocument>.Create(
            //    "{ $project :{ Tags: { $objectToArray: \"$Tags\" } } }",
            //    "{ $unwind : \"$Tags\" }",
            //    "{ $sortByCount : \"$Tags\" }");

            var facetPipeline = AggregateFacet.Create("categorizedByTags", pipeline);

            var aggregation = collection.Aggregate().Match(b => b.Name == "b1").Facet(facetPipeline);

            var listx = aggregation.ToList();
            // var outputxy = listx.Facets.ToJson(new JsonWriterSettings { Indent = true });
            var output = aggregation.Single().Facets.ToJson(new JsonWriterSettings { Indent = true });

            Console.WriteLine(output);
        }
        [TestMethod]
        public void Facets2()
        {
            var collection = new MongoClient("mongodb://localhost:27017/admin").GetDatabase("firstdb").GetCollection<Car>("car");
            var result = collection
                  .Aggregate()
                  .Group(new BsonDocument
                  {
                   {
                       "_id", new BsonDocument
                       {
                           {"Make", "$Make"},
                       }
                   }
                  })
                  .Group(new BsonDocument
                  {
                   { "_id", "_id" },
                   {"count", new BsonDocument("$sum", 1)}
                  })
                  .First();
            var count = result["count"].AsInt32;
            var result1 = collection
                  .Aggregate()
                  .Group(new BsonDocument
                  {
                   {
                       "_id", new BsonDocument
                       {
                           {"Make", "$Make"},
                           {"Model", "$Model"},
                           {"Count", "$sum"}
                       }
                   }
                  }).ToList();
        }
    }
}
