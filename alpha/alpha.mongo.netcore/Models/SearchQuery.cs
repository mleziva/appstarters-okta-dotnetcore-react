using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Mongo.Netcore.Models
{
    public class SearchQuery : PageableRequest
    {
        public string SearchTerm { get; set; }
        public string Filter { get; set; }
        //public FilterDefinition<T> GetMongoFilters<T>()
        //{
        //    //GET /indexes/hotel/docs?search=lodging&$filter=City eq ‘Seattle’ and Parking and Type ne ‘motel’
        //    //"salary_frequency eq 'Annual' and salary_range_from gt 90000"
        //    //search=*&$filter=(baseRate ge 60 and baseRate lt 300) and accommodation eq 'Hotel' and city eq 'Nogales' or city eq 'test'
        //    //equals
        //    //ne
        //    //lt/le
        //    //gt/ge
        //    //And
        //    //Or
        //    //only handle 1 level of parens for now
        //    //check for parens
        //    //split into array on 'and/or'
        //    //create filters


        //    var filter1 = Builders<T>.Filter.Where(x => x.Year == 1994);
        //    var fitler2 = Builders<T>.Filter.And(filter, filter1);
        //    var filter3 = Builders<T>.Filter.Eq("Model", "Civic");
        //    var fitler4 = Builders<T>.Filter.And(fitler2, filter3);
        //    return filter1;
        //}
        //private FilterDefinition<T> GetSearchTermFilter<T>()
        //{
        //    return Builders<T>.Filter.Text(SearchTerm);
        //}
    }
}
