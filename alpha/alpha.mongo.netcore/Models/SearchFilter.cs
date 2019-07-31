using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Alpha.Mongo.Netcore.Models
{
    public class SearchFilter
    {
        public string Filter { get; set; }
        public List<string> AndList { get; set; }
        public List<string> OrList { get; set; }
        public List<SearchFilter> Children { get; set; }
        public SearchFilter Initialize(string filter)
        {
            //search=*&$filter=(baseRate ge 60 and baseRate lt 300) and (baseRate ge 60 or baseRate lt 300) and accommodation eq 'Hotel' and city eq 'Nogales' or city eq 'test'
            //color eq blue and city eq 'Nogales' or city eq 'test'
            Filter = filter;
            string[] lines = Regex.Split(Filter, " or ");
            if(lines != null)
            {
                foreach(var line in lines)
                {
                    if (line.Contains("and"))
                    {
                        string[] andLines = Regex.Split(line, " and ");
                    }
                    else OrList.Add(line);
                }
            }

            List<string> insideParens = Filter.Split('(', ')').Where((item, index) => index % 2 != 0).ToList();
            foreach (var innerExpression in insideParens)
            {
                Children.Add(new SearchFilter().Initialize(innerExpression));
            };
            return this;
        }

    }
}
