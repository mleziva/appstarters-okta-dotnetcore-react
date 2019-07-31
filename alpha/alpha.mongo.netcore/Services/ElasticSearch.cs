using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Mongo.Netcore.Services
{
    public class ElasticSearch : IElasticSearch
    {
        public async Task<string> SearchAsync(string query)
        {
            var httpclient = new HttpClient();
            var message = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://localhost:9200/firstdb.search/_search"),
                Content = new StringContent(query, Encoding.UTF8, "application/json")
            };
            var response = await httpclient.SendAsync(message);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
