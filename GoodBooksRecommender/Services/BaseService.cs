using GoodBooksRecommender.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoodBooksRecommender.Services
{
    public class BaseService
    {
        public MongoClient _client { get; set; }
        public static HttpClient _httpClient;
        public IMongoDatabase _db { get; set; }

        public BaseService()
        {
            var svc = new DataAccess();
            _client = svc._client;
            _httpClient = new HttpClient();
            _db = svc._db;
        }
    }
}
