using Alpha.Mongo.Netcore.Repository;


namespace Alpha.Mongo.Netcore.Models
{
    public class Car : BsonModel
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
    }
}
