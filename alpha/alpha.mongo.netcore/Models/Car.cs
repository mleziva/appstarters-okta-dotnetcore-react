using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace alpha.mongo.netcore.Models
{
    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
    }
}
