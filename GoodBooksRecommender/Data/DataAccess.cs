using Dapper;
using GoodBooksRecommender.Helpers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GoodBooksRecommender.Data
{
    /// <summary>
    /// Handles all database access
    /// </summary>
    public class DataAccess
    {
        private string connString { get; set; }
        public MongoClient _client { get; set; }
        public IMongoDatabase _db { get; set; }

        public DataAccess(string connectionStringName = "GoodBooks", string Database = "GoodBooks")
        {            
            //Get connection string from appsettings
            var appSettingsJson = AppSettingsJson.GetAppSettings();
            connString = appSettingsJson["Data:ConnectionStrings:" + connectionStringName];            

            //colnnect to cluster and select db
            _client = new MongoClient(connString);
            _db = _client.GetDatabase(Database);
        }

    }
}
