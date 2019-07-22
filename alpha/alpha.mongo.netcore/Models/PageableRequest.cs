using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Mongo.Netcore.Models
{
    public class PageableRequest
    {
        public int Top { get; set; }
        public int Skip { get; set; }
    }
}
