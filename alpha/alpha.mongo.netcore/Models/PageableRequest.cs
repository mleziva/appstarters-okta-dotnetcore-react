using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Mongo.Netcore.Models
{
    public class PageableRequest
    {
        private int top = 1;
        public int Top
        {
            get
            {
                return top;
            }
            set
            {
                if (value < 1) { top = 1; } else { top = value; }
            }
        }
        private int skip;
        public int Skip
        {
            get
            {
                return skip;
            }
            set
            {
                if (value < 0) { skip = 0; } else { skip = value; }
            }
        }
    }
}
