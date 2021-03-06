﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Mongo.Netcore.Models
{
    public class PageableResponse<T>
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
        public int Skip { get; set; }
        public long Count { get; set; }
        public IEnumerable<T> Items { get; set; }
        public long TotalPages { get => Count / Top; }
        //1 based index of current page based on skip and top sizes
        public long CurrentPage { get => Skip/Top + 1; }
    }
}
