using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Mongo.Netcore.Services
{
    public interface IElasticSearch
    {
        Task<string> SearchAsync(string query);
    }
}
