using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Mongo.Netcore.Models
{
    public class PageableResponse<T>
    {
        public uint Top { get; set; } = 1;
        private uint skip;
        public uint Skip
        {
            get
            {
                return skip;
            }
            set
            {
                if (value < 0) { skip = 1; } else { skip = value; }
            }
        }
        public uint Count { get; set; }
        public IEnumerable<T> Items { get; set; }
        public uint TotalPages { get => Count / Top; }
        //1 based index of current page based on skip and top sizes
        public uint CurrentPage { get => Skip/Top + 1; }
    }
}
