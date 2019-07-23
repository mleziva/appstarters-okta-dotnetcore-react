using Alpha.Mongo.Netcore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Mongo.Netcore.Models
{
    public class Boat : BsonModel
    {
        public string Make { get; set; }
        public int Year { get; set; }
        public float HorsePower { get; set; }
        public string Description { get; set; }
    }
}
